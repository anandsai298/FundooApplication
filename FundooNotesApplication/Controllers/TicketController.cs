using Automatonymous.Binders;
using BusinessLogicLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.security.krb5.@internal;
using System;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public IUserBusiness userBusiness;
        private readonly IBus bus;
        public TicketController(IUserBusiness userBusiness, IBus bus)
        {
            this.userBusiness = userBusiness;
            this.bus = bus;
        }
        [HttpPost("ForgetPassword")]
        public async Task< IActionResult> CreateTicketForPassword(string email)
        {
            try
            {
                if (email != null)
                {
                    var token = userBusiness.ForgetPassword(email);
                    if (token != null)
                    {
                        var output = userBusiness.CreateTicketForPassword(email, token);
                        Uri uri = new Uri("rabbitmq://localhost/fundooQueue");
                        var endPoint = await bus.GetSendEndpoint(uri);
                        await endPoint.Send(output);
                        return Ok(new { success = true, message = "Email ForgetPassword Sent Successfully",data=output});
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Email was not Registered" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
