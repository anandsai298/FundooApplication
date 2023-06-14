using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooContext fundooContext;
        public LabelRepository(FundooContext fundooContext) 
        {
            this.fundooContext= fundooContext;
        }
        public LabelEntity AddLabel(string LableName,long userID,int NoteID)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();

                var existinglabelname =fundooContext.Labels.Where(a=>a.LabelName==LableName &&  a.UserID==userID).FirstOrDefault();
                if (existinglabelname!=null)
                {
                    existinglabelname.NoteID = NoteID;
                    existinglabelname.LabelName=LableName;
                    //fundooContext.Labels.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return existinglabelname;
                }
                else
                {
                    labelEntity.LabelName = LableName;
                    labelEntity.UserID = (int)userID;
                    labelEntity.NoteID = NoteID;
                    fundooContext.Labels.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelEntity>GetAllLabels(long userID)
        {
            try
            {
                var labellist = this.fundooContext.Labels.Where(a => a.UserID == userID).ToList();
                return labellist;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LabelEntity UpdateLabelName(string oldname,string newname,long userID)
        {
            try
            {
                var lblnm = fundooContext.Labels.Where(a => a.UserID == userID);
                if(lblnm!=null)
                {
                    var namelbl = lblnm.Where(a => a.LabelName == oldname).FirstOrDefault();
                    if(namelbl != null)
                    {
                        namelbl.LabelName = newname;
                        fundooContext.SaveChanges();
                        return namelbl;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLable(int labelID, long userID)
        {
            try
            {
                var userlbl = fundooContext.Labels.Where(a => a.UserID == userID);
                if (userlbl != null)
                {
                    var dltlbl = userlbl.Where(a => a.LabelID == labelID).FirstOrDefault();
                    if (dltlbl != null)
                    {
                        fundooContext.Labels.Remove(dltlbl);
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
