using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Note;
using Crypteron.Internal.ThirdParty.Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("Note")]

    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        FundooDbContext fundooDBContext;
        INoteBL NoteBL;
        public NoteController(INoteBL NoteBL, FundooDbContext fundooDB, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.NoteBL = NoteBL;
            this.fundooDBContext = fundooDB;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("addnotes")]
        public async Task<ActionResult> CreateNotes(NotePostModel notePostModel)
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userid = Int32.Parse(Userid.Value);
                await this.NoteBL.CreateNotes(notePostModel, userid);

                return this.Ok(new { success = true, message = $"Note Created Sucessfully" });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("updatenote/{noteID}")]
        public IActionResult UpdateNotes(int noteID, NotePostModel notesModel)
        {
            try
            {
                if (NoteBL.UpdateNotes(noteID, notesModel))
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully", response = notesModel, noteID });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("getAllNoteusingRedis")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var cacheKey = "NoteList";
                string serializedNoteList;
                var noteList = new List<Note>();
                var redisnoteList = await distributedCache.GetAsync(cacheKey);
                if (redisnoteList != null)
                {
                    serializedNoteList = Encoding.UTF8.GetString(redisnoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializedNoteList);
                }
                else
                {
                    noteList = await NoteBL.GetAllNotes();
                    serializedNoteList = JsonConvert.SerializeObject(noteList);
                    redisnoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                }
                return this.Ok(noteList);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("deleteNotes/{noteID}")]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                if (NoteBL.DeleteNote(noteID))
                {
                    return this.Ok(new { Success = true, message = "Notes deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("changeColor/{noteID}/{color}")]
        public async Task<IActionResult> changeColor(int noteID, string color)
        {
            try
            {
                List<Note> note = await NoteBL.changeColor(noteID, color);
                if (note != null)
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully", data = note });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("archieveNote/{noteID}")]
        public async Task<IActionResult> IsArchieve(int noteID)
        {
            try
            {
                await NoteBL.ArchieveNote(noteID);


                return this.Ok(new { Success = true, message = $"NoteArchieve successfull for {noteID}" });




            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("pinnotes/{noteId}")]
        public async Task<IActionResult> PinNote(int noteId)
        {
            try
            {
                var result = NoteBL.PinNote(noteId);
                await NoteBL.PinNote(noteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("trash/{noteId}")]
        public async Task<IActionResult> TrashNotes(int noteId)
        {
            try
            {
                var result = NoteBL.TrashNote(noteId);
                await NoteBL.TrashNote(noteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("getbynoteId/{noteId}")]
        public IEnumerable<Note> GetNotesByUserId(int noteId)
        {
            //int NoteId = (User.Claims.FirstOrDefault(x => x.Type == "NoteId").Value);
            try
            {
                return NoteBL.GetNotesByNoteId(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }

}
