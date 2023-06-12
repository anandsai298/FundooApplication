using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Services;
using com.sun.corba.se.impl.protocol.giopmsgheaders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesBusiness NoteBus;
        public NoteController(INotesBusiness NoteBus) 
        {
            this.NoteBus = NoteBus;
        }
        [HttpPost]
        [Route("AddNotes")]
        public IActionResult AddNotes(NotesModel notesModel)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var NoteCreate = NoteBus.AddNotes(notesModel, userID);
                if (NoteCreate != null)
                {
                    return Ok(new ResponseModel<FundooNoteEntity> { Status = true, Message = "Notes added successfully", Data = NoteCreate });
                }
                else
                {
                    return BadRequest(new ResponseModel<FundooNoteEntity> { Status = false, Message = "Notes added UNsuccessfully", Data = NoteCreate });

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("GetAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var getNotes = NoteBus.GetAllNotes(userID);
                if (getNotes != null)
                {
                    return Ok(new { status = true, Message = "Notes will displayed", Data = getNotes });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Notes will not displayed", Data = getNotes });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
