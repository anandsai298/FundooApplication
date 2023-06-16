using BusinessLogicLayer.Interface;
using com.sun.corba.se.impl.protocol.giopmsgheaders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBusiness collabBus;
        public CollaboratorController(ICollaboratorBusiness collabBus)
        {
            this.collabBus = collabBus;
        }
        [HttpPost("AddCollaborator")]
        public IActionResult AddCollabratorEmail(string collabemail, int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var colluser = collabBus.AddCollabratorEmail(collabemail, NoteID, userID);
                if(colluser!=null)
                {
                    return Ok(new ResponseModel<CollaboratorEntity> { Status = true, Message = "AddCollabratoremail was succesfull", Data = colluser });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollaboratorEntity> { Status = false, Message = "AddCollabratoremail was UNsuccesfull", Data = colluser });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetAllCollaborators")]
        public IActionResult GetAllCollaborators()
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var collus = collabBus.GetAllCollaborators(userID);
                if (collus != null)
                {
                    return Ok(new { Status = true, Message = "GetAllCollaborators was succesfull", Data = collus });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "GetAllCollaborators was UNsuccesfull", Data = collus });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UpdateCollaboratorEmail")]
        public IActionResult UpdateCollaboratorEmail(string oldemail, string newemail)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var emailcollab = collabBus.UpdateCollaboratorEmail(oldemail, newemail, userID);
                if (emailcollab != null)
                {
                    return Ok(new { Status = true, Message = "Updation of collaborator email was sucess", Data = emailcollab });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "Updation of collaborator email was UNsucessfull", Data = emailcollab });
                }
            }
            catch(Exception e)
            {
                throw new Exception (e.Message);
            }
        }
        [HttpPost("DeleteCollaboratorEmail")]
        public IActionResult DeleteCollaboratorEmail(int collaboratorID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var idcollab = collabBus.DeleteCollaboratorEmail(collaboratorID, userID);
                if(idcollab!=null)
                {
                    return Ok(new { Status = true, Message = "Deletion of collaborator ID was sucess", Data = idcollab });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "Deletion of collaborator ID was UNsucess", Data = idcollab });
                }
            }
            catch(Exception e)
            {
                throw new Exception (e.Message);
            }
        }
    }
}
