using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaborateID { get; set; }
        public string CollaborateEmail { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [JsonIgnore]
        public virtual UserEntity User{get;set;}
        [ForeignKey("Note")]
        public int NoteID { get; set; }
        [JsonIgnore]
        public virtual FundooNoteEntity Note { get; set; }

    }
}
