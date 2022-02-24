using CommonLayer.Note;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        Task CreateNotes(NotePostModel notePostModel, int Userid);
        public bool UpdateNotes(int noteID, NotePostModel notesModel);
        Task<List<Note>> GetAllNotes();
        public bool DeleteNote(int notesID);
        Task<List<Note>> changeColor(int noteID, string color);
        Task ArchieveNote(int noteId);
        Task PinNote(int noteId);
        Task TrashNote(int noteId);
        public IEnumerable<Note> GetNotesByNoteId(int noteId);
    }
}
