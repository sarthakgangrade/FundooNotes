using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using CommonLayer.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundooDbContext dbContext;
        
        public NoteRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public async Task CreateNotes(NotePostModel notePostModel, int userId)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(x => x.userId == userId);
                Note note = new Note();
                note.NotesId = new Note().NotesId;
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.color = notePostModel.color;
                note.IsArchive = false;
                note.IsTrash = false;
                note.IsPin = false;
                note.IsReminder = false;
                note.CreatedDate = DateTime.Now;
                dbContext.Note.Add(note);
                await dbContext.SaveChangesAsync();


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateNotes(int noteID, NotePostModel notesPost)
        {
            Note notes = dbContext.Note.Where(e => e.NotesId == noteID).FirstOrDefault();
            notes.Title = notesPost.Title;
            notes.Description = notesPost.Description;
            //notes.IsReminder=notesPost.IsReminder;
            //notes.color=notesPost.color;
            //notes.IsArchive=notesPost.IsArchive;
            //notes.IsPin=notesPost.IsPin;
            //notes.IsTrash=notesPost.IsTrash;
            notes.ModifiedDate = DateTime.Now;
            dbContext.Note.Update(notes);
            var result = dbContext.SaveChangesAsync();
            if (result != null)
                return true;
            else
                return false;

        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await dbContext.Note.ToListAsync();
        }

        public bool DeleteNote(int notesID)
        {
            Note notes = dbContext.Note.Where(e => e.NotesId == notesID).FirstOrDefault();
            if (notes != null)
            {
                dbContext.Note.Remove(notes);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Note>> changeColor(int noteID, string color)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == noteID);
                note.color = color;
                await dbContext.SaveChangesAsync();
                return await dbContext.Note.ToListAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ArchieveNote(int noteId)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(u => u.NotesId == noteId);
                note.IsArchive = true;
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task PinNote(int noteId)
        {
            try
            {
                var note = dbContext.Note.FirstOrDefault(x => x.NotesId == noteId);
                if (note != null)
                {
                    note.IsPin = true;

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task TrashNote(int noteId)
        {
            try
            {
                Note note = dbContext.Note.FirstOrDefault(e => e.NotesId == noteId);
                if (note != null)
                {
                    note.IsTrash = true;
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Note> GetNotesByNoteId(int noteId)
        {
            return dbContext.Note.Where(Y => Y.NotesId == noteId).ToList();
        }

    }

}
