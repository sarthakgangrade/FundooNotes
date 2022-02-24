using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Collab;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    
    
        [ApiController]
        [Route("Collab")]

        public class CollabController : ControllerBase
        {
            ICollabBL CollabBL;
            public CollabController(ICollabBL CollabBL)
            {
                this.CollabBL = CollabBL;
            }
            [Authorize]
            [HttpPost("addCollabartor/{NotesId}")]
            public async Task<IActionResult> AddCollab(int noteId, CollabModel postModel)
            {
                try
                {
                    var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                    int UserId = Int32.Parse(userId.Value);
                    await CollabBL.AddCollab(UserId, noteId, postModel);

                    return this.Ok(new { success = true, message = "Collabartion added successfully", response = noteId, postModel });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            [Authorize]
            [HttpGet("getAllCollabs/{NotesId}")]
            public async Task<IActionResult> GetAllCollabs(int NotesId)
            {
                try
                {
                    int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                    var CollabrationList = new List<Collab>();
                    CollabrationList = await CollabBL.GetAllCollabs(userId, NotesId);

                    return this.Ok(new { Success = true, message = $"GetAll Collab of Userid={userId} ", data = CollabrationList });

                }
                catch (Exception)
                {
                    throw;
                }
            }

            [Authorize]
            [HttpDelete("deleteCollabs/{CollabId}")]
            public async Task<IActionResult> RemoveCollabs(int CollabId)
            {
                try
                {
                    var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Userid", StringComparison.InvariantCultureIgnoreCase));
                    int UserId = Int32.Parse(userId.Value);

                    await CollabBL.RemoveCollab(CollabId, UserId);
                    return this.Ok(new { success = true, message = "Collabartion deleted successfully", response = CollabId });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
    }

