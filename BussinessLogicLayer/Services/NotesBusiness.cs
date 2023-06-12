using BusinessLogicLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class NotesBusiness : INotesBusiness
    {
        private readonly INotesRepository INotesRep;
        public NotesBusiness(INotesRepository iNotesRep)
        {
            INotesRep=iNotesRep;
        }
        public FundooNoteEntity AddNotes(NotesModel notesModel, long userID)
        {
            try
            {
                return INotesRep.AddNotes(notesModel, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<FundooNoteEntity> GetAllNotes(long userID)
        {
            try
            {
                return INotesRep.GetAllNotes(userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
