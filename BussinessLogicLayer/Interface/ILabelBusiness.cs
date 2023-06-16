using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity AddLabel(string LableName, int userID, int NoteID);
        public List<LabelEntity> GetAllLabels(int userID);
        public LabelEntity UpdateLabelName(string oldname, string newname, int userID);
        public bool DeleteLable(int labelID, int userID);
    }
}
