using CommonLayer.Collab;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollabRL: ICollabRL
    {
        FundooDbContext dbContext;

        public CollabRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Collab>> AddCollab(int userId, int NotesId, CollabModel postModel)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(e => e.userId == userId);
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);


                Collab collab = new Collab();
                collab.userId = userId;
                collab.NotesId = NotesId;
                collab.CollabId = new Collab().CollabId;
                collab.CollabEmail = postModel.CollabEmail;
                collab.User = user;
                collab.Notes = note;
                dbContext.Collab.Add(collab);
                await dbContext.SaveChangesAsync();
                return await dbContext.Collab.Where(u => u.userId == userId)
                    .Include(u => u.User)
                    .Include(u => u.Notes)
                     .ToListAsync();

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
                Collab collab = new Collab();
                
                return await dbContext.Collab.Where(u => u.userId == userId && u.NotesId == NotesId)
                    .Include(u => u.Notes)
                    .Include(u => u.User)

                    .ToListAsync();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveCollab(int CollabId, int Userid)
        {
            try
            {
                Collab collabarator = await dbContext.Collab.Where(u => u.CollabId == CollabId).FirstOrDefaultAsync();
                if (collabarator != null)
                {
                    // Collabarator collabarator = new Collabarator();
                    this.dbContext.Collab.Remove(collabarator);
                    await this.dbContext.SaveChangesAsync();
                    // await dbContext.collabarators.ToListAsync();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
