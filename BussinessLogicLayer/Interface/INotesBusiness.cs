using Microsoft.AspNetCore.Http;
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
        public List<FundooNoteEntity> Get_All_Notes();
        public bool Pin_UnPin_Note(int NoteID, long userID);
        public bool Archive(int NoteID, long userID);
        public bool Trash(int NoteID, long userID);
        public bool DeleteNotes(int NoteID, long userID);
        public FundooNoteEntity UpdateNotes(NotesModel updateModel, int NoteID, long userID);
        public FundooNoteEntity Colour(string colour, int NoteID, long userID);
        public string UploadImage(string path, int NoteID, long userID);
        public string UploadImageFormFile(IFormFile path, int NoteID, long userID);
        public FundooNoteEntity Remainder(DateTime remainder, int NoteID, long userID);
    }
}
