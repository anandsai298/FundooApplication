using BusinessLogicLayer.Interface;
using java.lang.reflect;
using Microsoft.AspNetCore.Http;
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
        public FundooNoteEntity AddNotes(NotesModel notesModel, int userID)
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
        public List<FundooNoteEntity> GetAllNotes(int userID)
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
        public List<FundooNoteEntity> Get_All_Notes()
        {
            try
            {
                return INotesRep.Get_All_Notes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Pin_UnPin_Note(int NoteID, int userID)
        {
            try
            {
                return INotesRep.Pin_UnPin_Note(NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Archive(int NoteID, int userID)
        {
            try
            {
                return INotesRep.Archive(NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Trash(int NoteID, int userID)

        {
            try
            {
                return INotesRep.Trash(NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNotes(int NoteID, int userID)
        {
            try
            {
                return INotesRep.DeleteNotes(NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public FundooNoteEntity UpdateNotes(NotesModel updateModel, int NoteID, int userID)
        {
            try
            {
                return INotesRep.UpdateNotes(updateModel, NoteID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public FundooNoteEntity Colour(string colour, int NoteID, int userID)
        {
            try
            {
                return INotesRep.Colour(colour, NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UploadImage(string path, int NoteID, int userID)
        {
            try
            {
                return INotesRep.UploadImage(path, NoteID, userID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string UploadImageFormFile(IFormFile path, int NoteID, int userID)
        {
            try
            {
                return INotesRep.UploadImageFormFile(path, NoteID, userID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FundooNoteEntity Remainder(DateTime remainder, int NoteID, int userID)
        {
            try
            {
                return INotesRep.Remainder(remainder, NoteID, userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
