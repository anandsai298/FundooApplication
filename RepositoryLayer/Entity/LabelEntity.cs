using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelID { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
        [ForeignKey("Note")]
        public int NoteID { get; set; }
        [JsonIgnore]
        public virtual FundooNoteEntity Note { get; set; }

    }
}
