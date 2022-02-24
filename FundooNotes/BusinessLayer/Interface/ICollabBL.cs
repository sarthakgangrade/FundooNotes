using CommonLayer.Collab;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        Task<List<Collab>> AddCollab(int userId, int NotesId, CollabModel postModel);
        Task<List<Collab>> GetAllCollabs(int userId, int NotesId);
        Task RemoveCollab(int CollabId, int userId);

    }
}
