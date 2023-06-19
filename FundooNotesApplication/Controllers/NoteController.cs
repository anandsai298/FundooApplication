using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Services;
using com.sun.corba.se.impl.protocol.giopmsgheaders;
using com.sun.tools.javac.util;
using javax.xml.crypto;
using jdk.nashorn.@internal.objects.annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Models;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convert = System.Convert;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesBusiness NoteBus;
        private readonly IDistributedCache distributedCache;
        public NoteController(INotesBusiness NoteBus, IDistributedCache distributedCache) 
        {
            this.NoteBus = NoteBus;
            this.distributedCache=distributedCache;
        }
        [HttpPost]
        [Route("AddNotes")]
        public IActionResult AddNotes(NotesModel notesModel)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
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
        [HttpGet("GetAllNotesOfUser")]
        public IActionResult GetAllNotes()
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
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
        [AllowAnonymous]
        [HttpGet("GetAllNotes")]
        public async Task<IActionResult> Get_All_Notes()
        {
            try
            {
                var cachekey = "NotesList";
                //serialization of string
                List<FundooNoteEntity> NoteList;
                byte[] NotesListRedis=await distributedCache.GetAsync(cachekey);
                if(NotesListRedis!=null)
                {
                    var Serialization=Encoding.UTF8.GetString(NotesListRedis);
                    NoteList=JsonConvert.DeserializeObject<List<FundooNoteEntity>>(Serialization);
                }
                else
                {
                    NoteList = NoteBus.Get_All_Notes();
                    var Serialization=JsonConvert.SerializeObject(NoteList);
                    var noteslist = Encoding.UTF8.GetBytes(Serialization);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(15)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(cachekey, noteslist, options);
                }
                return Ok(NoteList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("SearchNotes")]
        public IActionResult SearchNotes(string word)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var searchnotes = NoteBus.SearchNotes(word, userID);
                if(searchnotes!=null)
                {
                    return Ok(new { Status = true, Message = "Search note by title is done successfully", Data = searchnotes });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "Search note by using title  UNsuccessfully", Data = searchnotes });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("PinOrUnPin_Note")]
        public IActionResult Pin_UnPin_Note(int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var output = NoteBus.Pin_UnPin_Note(NoteID, userID);
                if (output != null)
                {
                    return Ok(new { status = true, Message = "Pin of note is done", Data = output });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Pin is unsuccessfull", Data = output });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("ArchiveNote")]
        public IActionResult Archive(int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var arch = NoteBus.Archive(NoteID, userID);
                if (arch != null)
                {
                    return Ok(new { status = true, Message = "Archive of note is Done", Data = arch });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Archive of note is Unsuccessfull", Data = arch });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("TrashNote")]
        public IActionResult Trash(int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var trsh = NoteBus.Trash(NoteID, userID);
                if (trsh != null)
                {
                    return Ok(new { status = true, Message = "trash note was successfull", Data = trsh });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "trash note was UNSuccessfull", Data = trsh });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("DeleteNotesPermanently")]
        public IActionResult DeleteNotes(int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var Dlt = NoteBus.DeleteNotes(NoteID, userID);
                if (Dlt != null)
                {
                    return Ok(new { status = true, Message = "Deletion of Note is Done", Data = Dlt });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Deletion was Unsuccesfull", Data = Dlt });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UpdateNotes")]
        public IActionResult UpdateNotes(NotesModel updateModel,int NoteID)
        {
            int userID = Convert.ToInt32((User.Claims.FirstOrDefault(a => a.Type == "UserID").Value));
            var upnot = NoteBus.UpdateNotes(updateModel, NoteID, userID);
            if(upnot != null)
            {
                return Ok(new { status = true,Message="Notes was updated succesfully",Data = upnot});
            }
            else
            {
                return BadRequest(new { status = false, Message = "Updation of notes was UNSuccessfull", Data = upnot });
            }
        }
        [HttpPost("Colour")]
        public IActionResult Colour(string colour, int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var clrnote = NoteBus.Colour(colour, NoteID, userID);
                if(clrnote != null)
                {
                    return Ok(new ResponseModel<FundooNoteEntity> { Status = true, Message = "Colour has been added", Data = clrnote });
                }
                else
                {
                    return BadRequest(new ResponseModel<FundooNoteEntity> { Status = false, Message = "Colour change was unsucessfull", Data = clrnote });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UploadImage")]
        public IActionResult UploadImage(string path,int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var noteimg = NoteBus.UploadImage(path, NoteID, userID);
                if (noteimg != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "Upload image was sucessfully", Data = noteimg });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Upload image was UNsucessfully", Data = noteimg });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UploadImageFormFile")]
        public IActionResult UploadImageFormFile(IFormFile path, int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var imgform = NoteBus.UploadImageFormFile(path, NoteID, userID);
                if(imgform != null)
                {
                    return Ok(new ResponseModel<string> { Status = true, Message = "Upload image form was sucessfully", Data = imgform });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Status = false, Message = "Upload image form was UNsucessfully", Data = imgform });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Remainder")]
        public IActionResult Remainder(DateTime remainder,int NoteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var rem = NoteBus.Remainder(remainder, NoteID, userID);
                if (rem != null)
                {
                    return Ok(new ResponseModel<FundooNoteEntity> { Status = true, Message = "Reamainder is set succesfully", Data = rem });
                }
                else
                {
                    return BadRequest(new ResponseModel<FundooNoteEntity> { Status = false, Message = "Reamainder is not set succesfully", Data = rem });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
