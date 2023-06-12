using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness iUserBus;
        private readonly FundooContext fundoo;
        public UserController(IUserBusiness iUserBus, FundooContext fundoo)
        {
            this.iUserBus = iUserBus;
            this.fundoo=fundoo;
        }
        
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel regMod)
        {
            try
            {
                var IfEmailExists = iUserBus.IfEmailExists(regMod.Email);
                if (IfEmailExists)
                {
                    Logger.LogMessage("Email already Exists");
                    return Ok(new { Status = false, Message = "Email already Exists" });
                }
                else
                {
                    var Ask = iUserBus.Register(regMod);
                    if (Ask != null)
                    {
                        return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Register SuccessFull", Data = Ask });
                    }
                    else
                    {
                        return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Register UNSuccessFull", Data = Ask });
                    }
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
                    return Ok(new ResponseModel<string> { Status = true, Message = "Login SuccessFull",Data= Asklog });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Login UNSuccessFull", Data = Asklog });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPatch("ResetPassword")]
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(a => a.Type=="Email").Value;
                var forget = iUserBus.ResetPassword(resetPassword, email);
                if(forget != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "reset password successfull", Data = forget });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "reset password UNsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
