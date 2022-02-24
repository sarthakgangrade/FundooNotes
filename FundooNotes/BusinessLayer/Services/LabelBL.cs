using BusinessLayer.Interface;
using CommonLayer.Label;
using RepositoryLayer.Class;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL:ILabelBL
    {
        ILabelRL LabelRL;
        public LabelBL(ILabelRL LabelRL)
        {
            this.LabelRL = LabelRL;
        }
        public async Task CreateLabel(LabelModel labelModel, int NotesId, int userId)
        {
            try
            {
                 await LabelRL.CreateLabel(labelModel, NotesId, userId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateLabel(int LabelId, LabelModel labelModel)
        {
            try
            {
                if (LabelRL.UpdateLabel(LabelId, labelModel))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteLabel(int LabelId)
        {
            try
            {
                if (LabelRL.DeleteLabel(LabelId))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LabelResponse>> GetAllLabels(int userId)
        {
            try
            {
                return await LabelRL.GetAllLabels(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetLabelsByNoteID(int userId, int NoteId)
        {
            try
            {
                return await LabelRL.GetLabelsByNoteID(userId, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
