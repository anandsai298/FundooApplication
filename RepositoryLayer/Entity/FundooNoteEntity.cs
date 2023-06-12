using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class FundooNoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string colour { get; set; }
        public string Image { get; set; }
        public DateTime Remainder { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPinned { get; set; }
        public bool IsTrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
