using CommonLayer.Label;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task CreateLabel(LabelModel labelModel,int NotesId, int userId);
        public bool UpdateLabel(int LabelId, LabelModel labelModel);
        Task<List<LabelResponse>> GetAllLabels(int userId);
        public Task<List<Label>> GetLabelsByNoteID(int userId, int NotesId);
        public bool DeleteLabel(int LabelId);



    }
}
