using BusinessLayer.Interface;
using CommonLayer.Collab;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabBL:ICollabBL
    {
        ICollabRL CollabRL;
        public CollabBL(ICollabRL CollabRL)
        {
            this.CollabRL = CollabRL;
        }

        public async Task<List<Collab>> AddCollab(int userId, int NotesId, CollabModel postModel)
        {
            try
            {
                return await CollabRL.AddCollab(userId, NotesId, postModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Collab>> GetAllCollabs(int userId, int NotesId)
        {
            try
            {
                return await CollabRL.GetAllCollabs(userId, NotesId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveCollab(int CollabId, int userId)
        {
            try
            {
                await CollabRL.RemoveCollab(CollabId, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
