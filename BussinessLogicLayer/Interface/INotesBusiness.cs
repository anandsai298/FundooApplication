using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface INotesBusiness
    {
        public FundooNoteEntity AddNotes(NotesModel notesModel, long userID);
        public List<FundooNoteEntity> GetAllNotes(long userID);
    }
}
