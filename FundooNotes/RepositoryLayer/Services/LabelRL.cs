using CommonLayer.Label;
using CommonLayer.Note;
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
    public class LabelRL : ILabelRL
    {
        FundooDbContext dbContext;

        public LabelRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateLabel(LabelModel labelModel, int NotesId, int userId)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(e => e.userId == userId);
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == NotesId);
                Label labels = new Label
                {
                    User = user,
                    Notes = note
                };
                //Label labels = new Label();
                labels.userId = userId;
                labels.NotesId = NotesId;
                labels.LabelId = new Label().LabelId;
                labels.LabelName = labelModel.LabelName;
                dbContext.Label.Add(labels);
                await dbContext.SaveChangesAsync();
                await dbContext.Label.Where(u => u.userId == userId)
                    .Include(u => u.Notes)
                    .Include(u=>u.User)

                   .ToListAsync();
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
                Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();
                label.LabelName = labelModel.LabelName;
                dbContext.Label.Update(label);
                var result = dbContext.SaveChangesAsync();
                if (result != null)
                {
                    return true;
                }
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
                Label label = dbContext.Label.Where(e => e.LabelId == LabelId).FirstOrDefault();
                if (label != null)
                {
                    dbContext.Label.Remove(label);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LabelResponse>> GetAllLabels(int userId)
        {
            Label labels = new Label();
            try
            {
                return await dbContext.Label.Where(l => l.userId == userId )
                    //.Include(u => u.Notes)
                    //.Include(u => u.User)
                    //.ToListAsync();

                    .Join(dbContext.Users
                  .Join(dbContext.Note,
                u => u.userId,
                n => n.userId,
                (u,n) => new NoteUserResponse
                {
                    userId= u.userId,
                    NotesId = n.NotesId,
                    email = u.email,
                    name = u.name,
                    CreatedDate= n.CreatedDate,
                    Title=n.Title,
                    Description=n.Description


                }),
                  l => l.Notes.NotesId,
                    un => un.NotesId,
                    (l, un) => new LabelResponse
                    {
                        userId = un.userId,
                        NotesId = l.Notes.NotesId,
                        Title = un.Title,
                        Description = un.Description,
                        CreatedDate = un.CreatedDate,
                        color = un.color,
                        name = un.name,
                        email = un.email,
                        LabelName = l.LabelName


                    }).ToListAsync();

                



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> GetLabelsByNoteID(int userId, int NotesId)
        {
            try
            {
                return await dbContext.Label.Where(e => e.NotesId == NotesId && e.userId == userId)
                    .Include(u => u.Notes)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
