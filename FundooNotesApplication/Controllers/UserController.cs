using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness iUserBus;
        public UserController(IUserBusiness iUserBus)
        {
            this.iUserBus = iUserBus;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel regMod)
        {
            try
            {
                var Ask = iUserBus.Register(regMod);
                if (Ask != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Register SuccessFull"});
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Register UNSuccessFull" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        [HttpPost]
        [Route ("Login")]
        public IActionResult Login(LoginModel logModel)
        {
            try
            {
                var Asklog = iUserBus.Login(logModel);
                if (Asklog != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Login SuccessFull" });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Login UNSuccessFull" });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
