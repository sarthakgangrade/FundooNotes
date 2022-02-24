using CommonLayer.Note;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Task CreateNotes(NotePostModel notePostModel, int userId);
        public bool UpdateNotes(int noteID, NotePostModel notesPost);
        Task<List<Note>> GetAllNotes();
        public bool DeleteNote(int notesID);
        Task<List<Note>> changeColor(int noteID, string color);
        Task ArchieveNote(int noteId);
        Task PinNote(int noteId);
        Task TrashNote(int noteId);
        public IEnumerable<Note> GetNotesByNoteId(int noteId);
    }
}
