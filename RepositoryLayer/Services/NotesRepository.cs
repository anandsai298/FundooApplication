using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using javax.security.auth.login;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        public NotesRepository(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public FundooNoteEntity AddNotes(NotesModel notesModel,int userID)
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
                fundooNoteEntity.UserID = userID;
                fundooContext.FundooNotes.Add(fundooNoteEntity);
                fundooContext.SaveChanges();
                return fundooNoteEntity;
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
                var notesList = this.fundooContext.FundooNotes.Where(a => a.UserID == userID).ToList();
                return notesList;
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
                var notesList = this.fundooContext.FundooNotes.ToList();
                return notesList;
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
                FundooNoteEntity note = this.fundooContext.FundooNotes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note.IsPinned == false)
                {
                    note.IsPinned = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsPinned = false;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                FundooNoteEntity note= this.fundooContext.FundooNotes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if(note.IsArchive == false)
                {
                    note.IsPinned = false;
                    note.IsArchive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsArchive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Trash(int NoteID, int userID)
        {
            try
            {
                FundooNoteEntity note = this.fundooContext.FundooNotes.Where(a => a.NoteID == NoteID).FirstOrDefault();
                if(note.IsTrash == false)
                {
                    note.IsPinned=false;
                    note.IsTrash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    note.IsTrash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNotes(int NoteID, int userID)
        {
            try
            {
                var useroutput = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if(useroutput!=null)
                {
                    var findnotes = useroutput.Where(x => x.NoteID == NoteID).FirstOrDefault();
                    if (findnotes.IsTrash == true)
                    {
                        if (findnotes != null)
                        {
                            fundooContext.FundooNotes.Remove(findnotes);
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
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
        public FundooNoteEntity UpdateNotes(NotesModel updateModel,int NoteID, int userID)
        {
            try
            {
                var userop = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if(userop!=null)
                {
                    var findnotes = userop.Where(x => x.NoteID == NoteID).FirstOrDefault();
                    if(findnotes!=null)
                    {
                        findnotes.Title = updateModel.Title;
                        findnotes.Description = updateModel.Description;
                        findnotes.colour = updateModel.colour;
                        findnotes.Image = updateModel.Image;
                        findnotes.Remainder = updateModel.Remainder;
                        findnotes.UpdatedAt = updateModel.UpdatedAt;
                        fundooContext.SaveChanges();
                        return findnotes;
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public FundooNoteEntity Colour(string colour,int NoteID, int userID)
        {
            try
            {
                var clruser = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if (clruser!=null)
                {
                    var clrNote = clruser.Where(x => x.NoteID == NoteID).FirstOrDefault();
                    if(clrNote.colour!=null)
                    {
                        clrNote.colour = colour;
                        fundooContext.SaveChanges();
                        return clrNote;
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
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
        public string UploadImage(string path,int NoteID, int userID)
        {
            try
            {
                var userfilter = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if(userfilter!=null)
                {
                    var notesfind = userfilter.FirstOrDefault(a => a.NoteID == NoteID);
                    if(notesfind!=null)
                    {
                        Account account = new Account("ds1rfhdlx", "362425625833256", "aq3rWwY2cefnsSiNPOH_Uolne7g");
                        Cloudinary cloud = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path),
                            PublicId = notesfind.Title
                        };
                        ImageUploadResult uploadResult = cloud.Upload(uploadParams);
                        notesfind.UpdatedAt = DateTime.Now;
                        notesfind.Image = uploadResult.Url.ToString();
                        fundooContext.SaveChanges();
                        return "Image uploaded succesfully";
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message); 
            }
        }
        public string UploadImageFormFile(IFormFile path, int NoteID, int userID)
        {
            try
            {
                var userfilter = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if (userfilter != null)
                {
                    var notesfind = userfilter.FirstOrDefault(a => a.NoteID == NoteID);
                    if (notesfind != null)
                    {
                        Account account = new Account("ds1rfhdlx", "362425625833256", "aq3rWwY2cefnsSiNPOH_Uolne7g");
                        Cloudinary cloud = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path.FileName,path.OpenReadStream()),
                            PublicId = notesfind.Title
                        };
                        ImageUploadResult uploadResult = cloud.Upload(uploadParams);
                        notesfind.UpdatedAt = DateTime.Now;
                        notesfind.Image = uploadResult.Url.ToString();
                        fundooContext.SaveChanges();
                        return "Image uploaded succesfully";
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public FundooNoteEntity Remainder(DateTime remainder,int NoteID, int userID)
        {
            try
            {
                var userfilter = fundooContext.FundooNotes.Where(a => a.UserID == userID);
                if (userfilter != null)
                {
                    var notesfind = userfilter.Where(x => x.NoteID == NoteID).FirstOrDefault();
                    if (notesfind != null)
                    {
                        notesfind.Remainder = remainder;
                        fundooContext.SaveChanges();
                        return notesfind;
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
            catch(Exception ex)

            {
                throw new Exception(ex.Message);
            }        }
    }
}
