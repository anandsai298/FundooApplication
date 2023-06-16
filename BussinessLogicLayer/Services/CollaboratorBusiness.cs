using BusinessLogicLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepository collabrep;
        public CollaboratorBusiness(ICollaboratorRepository collabrep)
        {
            this.collabrep = collabrep;
        }
        public CollaboratorEntity AddCollabratorEmail(string collabemail, int NoteID, int userID)
        {
            try
            {
                return collabrep.AddCollabratorEmail(collabemail, NoteID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CollaboratorEntity> GetAllCollaborators(int userID)
        {
            try
            {
                return collabrep.GetAllCollaborators(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CollaboratorEntity UpdateCollaboratorEmail(string oldemail, string newemail, int userID)
        {
            try
            {
                return collabrep.UpdateCollaboratorEmail(oldemail, newemail, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    public bool DeleteCollaboratorEmail(int collaboratorID, int userID)
        {
            try
            {
                return collabrep.DeleteCollaboratorEmail(collaboratorID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
