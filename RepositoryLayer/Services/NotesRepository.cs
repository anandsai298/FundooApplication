using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRepository:INotesRepository
    {
        private readonly FundooContext fundooContext;
        public NotesRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public FundooNoteEntity AddNotes(NotesModel notesModel,long userID)
        {
            try
            {
                FundooNoteEntity fundooNoteEntity = new FundooNoteEntity();
                fundooNoteEntity.Title= notesModel.Title;
                fundooNoteEntity.Description= notesModel.Description;
                fundooNoteEntity.colour = notesModel.colour;
                fundooNoteEntity.Image=notesModel.Image;
                fundooNoteEntity.Remainder= notesModel.Remainder;
                fundooNoteEntity.IsArchive= notesModel.IsArchive;
                fundooNoteEntity.IsPinned= notesModel.IsPinned;
                fundooNoteEntity.IsTrash=notesModel.IsTrash;
                fundooNoteEntity.CreatedAt= notesModel.CreatedAt;
                fundooNoteEntity.UpdatedAt= notesModel.UpdatedAt;
                fundooNoteEntity.UserID = (int)userID;
                fundooContext.FundooNotes.Add(fundooNoteEntity);
                fundooContext.SaveChanges();
                return fundooNoteEntity;
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
                var notesList = this.fundooContext.FundooNotes.Where(a => a.UserID == userID).ToList();
                return notesList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
