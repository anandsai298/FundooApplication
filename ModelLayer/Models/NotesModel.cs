using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public class NotesModel
    {
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
    }
}
