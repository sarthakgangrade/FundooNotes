using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;

namespace FundooNotes.Controllers
{

    [ApiController]
    [Route("Controller")]

    public class UserController : ControllerBase
    {
        FundooDbContext fundooDBContext;
        IUserBL userBL;
        public UserController(IUserBL userBL, FundooDbContext fundooDB)
        {
            this.userBL = userBL;
            this.fundooDBContext = fundooDB;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

        [HttpPost ("register")]
        public ActionResult RegisteredUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisteredUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful{userPostModel.mobileNo}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }




    }
}
