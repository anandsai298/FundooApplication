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
        public FundooNoteEntity AddNotes(NotesModel notesModel, int userID);
        public List<FundooNoteEntity> GetAllNotes(int userID);
        public List<FundooNoteEntity> Get_All_Notes();
        public bool Pin_UnPin_Note(int NoteID, int userID);
        public bool Archive(int NoteID, int userID);
        public bool Trash(int NoteID, int userID);
        public bool DeleteNotes(int NoteID, int userID);
        public FundooNoteEntity UpdateNotes(NotesModel updateModel, int NoteID, int userID);
        public FundooNoteEntity Colour(string colour, int NoteID, int userID);
        public string UploadImage(string path, int NoteID, int userID);
        public string UploadImageFormFile(IFormFile path, int NoteID, int userID);
        public FundooNoteEntity Remainder(DateTime remainder, int NoteID, int userID);
    }
}
