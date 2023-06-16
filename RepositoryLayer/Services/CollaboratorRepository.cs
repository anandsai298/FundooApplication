using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepository:ICollaboratorRepository
    {
        private readonly FundooContext fundooContext;
        public CollaboratorRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollaboratorEntity AddCollabratorEmail(string collabemail,int NoteID, int userID)
        {
            try
            {
                CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                collaboratorEntity.CollaborateEmail = collabemail;
                collaboratorEntity.UserID = userID;
                collaboratorEntity.NoteID = NoteID;
                fundooContext.Collaborators.Add(collaboratorEntity);
                fundooContext.SaveChanges();
                return collaboratorEntity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CollaboratorEntity> GetAllCollaborators(int userID)
        {
            try
            {
                var collemail = this.fundooContext.Collaborators.Where(a => a.UserID == userID).ToList();
                return collemail;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public CollaboratorEntity UpdateCollaboratorEmail(string oldemail,string newemail,int userID)
        {
            try
            {
                var usercollab = fundooContext.Collaborators.Where(a => a.UserID == userID);
                if(usercollab!=null)
                {
                    var emailcollab = usercollab.Where(a => a.CollaborateEmail == oldemail).FirstOrDefault();
                    if(emailcollab!=null)
                    {
                        emailcollab.CollaborateEmail = newemail;
                        fundooContext.SaveChanges();
                        return emailcollab;
                    }
                    else
                    {
                        return null;

                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCollaboratorEmail(int collaboratorID,int userID)
        {
            try
            {
                var collabuser = fundooContext.Collaborators.Where(a => a.UserID == userID);
                if (collabuser!=null)
                {
                    var dltemail = collabuser.Where(a => a.CollaborateID == collaboratorID).FirstOrDefault();
                    if(dltemail!=null)
                    {
                        fundooContext.Collaborators.Remove(dltemail);
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
    }
}
