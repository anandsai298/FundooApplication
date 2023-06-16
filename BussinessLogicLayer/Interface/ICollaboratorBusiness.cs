using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface ICollaboratorBusiness
    {
        public CollaboratorEntity AddCollabratorEmail(string collabemail, int NoteID, int userID);
        public List<CollaboratorEntity> GetAllCollaborators(int userID);
        public CollaboratorEntity UpdateCollaboratorEmail(string oldemail, string newemail, int userID);
        public bool DeleteCollaboratorEmail(int collaboratorID, int userID);
    }
}
