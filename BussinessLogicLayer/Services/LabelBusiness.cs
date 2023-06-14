using BusinessLogicLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class LabelBusiness:ILabelBusiness
    {
        private readonly ILabelRepository labelRepository;
        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public LabelEntity AddLabel(string LableName, long userID, int NoteID)
        {
            try
            {
                return labelRepository.AddLabel(LableName, userID, NoteID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelEntity> GetAllLabels(long userID)
        {
            try
            {
                return labelRepository.GetAllLabels(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LabelEntity UpdateLabelName(string oldname, string newname, long userID)
        {
            try
            {
                return labelRepository.UpdateLabelName(oldname, newname, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLable(int labelID, long userID)
        {
            try
            {
                return labelRepository.DeleteLable(labelID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
