using BusinessLogicLayer.Interface;
using com.sun.corba.se.impl.protocol.giopmsgheaders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooNotesApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        private readonly ILabelBusiness labelBus;
        public LableController(ILabelBusiness labelBus)
        {
            this.labelBus = labelBus;
        }
        [HttpPost("AddLabel")]
        public IActionResult AddLabel(string labelName,int NoteID)
        {
            try
            {
                long userID = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var labelCreate = labelBus.AddLabel(labelName, userID, NoteID);
                if(labelCreate!=null)
                {
                    return Ok(new ResponseModel<LabelEntity>{Status=true,Message="Add Label creation is successfull",Data=labelCreate});
                }
                else
                {
                    return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Add Label creation is UNsuccessfull", Data = labelCreate });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetAllLabels")]
        public IActionResult GetAllLabels()
        {
            try
            {
                long userID = Convert.ToInt64(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var getlabel = labelBus.GetAllLabels(userID);
                if (getlabel != null)
                {
                    return Ok(new { Status = true, Message = "Get all Labels is successfull", Data = getlabel });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "Get all Labels is UNsuccessfull", Data = getlabel });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("UpdateLabelName")]
        public IActionResult UpdateLabelName(string oldname, string newname)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var lblname = labelBus.UpdateLabelName(oldname, newname, userID);
                if(lblname!=null)
                {
                    return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Updation of labelname done successfully", Data = lblname });
                }
                else
                {
                    return Ok(new ResponseModel<LabelEntity> { Status = false, Message = "Updation of labelname was Unsuccessfully", Data = lblname });
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("DeleteLabel") ]
        public IActionResult DeleteLable(int labelID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
                var dltlbl = labelBus.DeleteLable(labelID, userID);
                if(dltlbl!=null)
                {
                    return Ok(new ResponseModel<bool> { Status = true, Message = "Deleteion of labe with labelID is successfull", Data = dltlbl });
                }
                else
                {
                    return Ok(new ResponseModel<bool> { Status = false, Message = "Deleteion of labe with labelID is UNsuccessfull", Data = dltlbl });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
