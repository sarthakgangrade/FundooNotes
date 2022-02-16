using BusinessLayer.Interface;
using CommonLayer.Note;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL NoteRL;
        public NoteBL(INoteRL NoteRL)
        {
            this.NoteRL = NoteRL;
        }

        public async Task CreateNotes(NotePostModel notePostModel, int userId)
        {
            try
            {
                await NoteRL.CreateNotes(notePostModel, userId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateNotes(int noteID, NotePostModel notesModel)
        {
            try
            {
                if (NoteRL.UpdateNotes(noteID, notesModel))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Note>> GetAllNotes()
        {

            try
            {
                return await NoteRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool DeleteNote(int notesID)
        {
            try
            {
                if (NoteRL.DeleteNote(notesID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Note>> changeColor(int noteID, string color)
        {
            try
            {
                return await NoteRL.changeColor(noteID, color);
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
                await NoteRL.ArchieveNote(noteId);
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
                await NoteRL.PinNote(noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task TrashNote(int noteId)
        {
            try
            {
                await NoteRL.TrashNote(noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IEnumerable<Note> GetNotesByNoteId(int noteId)
        {
            try
            {
                return NoteRL.GetNotesByNoteId(noteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }




}
