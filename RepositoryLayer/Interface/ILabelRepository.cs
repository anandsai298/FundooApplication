using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        public LabelEntity AddLabel(string LableName, long userID, int NoteID);
        public List<LabelEntity> GetAllLabels(long userID);
        public LabelEntity UpdateLabelName(string oldname, string newname, long userID);
        public bool DeleteLable(int labelID, long userID);
    }
}
