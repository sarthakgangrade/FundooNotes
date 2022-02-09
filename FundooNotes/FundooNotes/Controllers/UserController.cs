using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
                return this.Ok(new { success = true, message = $"Registration Successful{userPostModel.email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost("login")]
        public ActionResult LogInUser(UserLogin userLogin)
        {
            try
            {
                var result=this.userBL.login(userLogin);
                if (result != null)
                    return this.Ok(new { success = true, message = $"LogIn Successful {userLogin.email}, Token = {result}" });
                else
                    return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("ForgotPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                this.userBL.ForgetPassword(email);
                return this.Ok(new { success = true, message = $"forgot password sucessfull" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [AllowAnonymous]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(string email, string password)
        {
            try
            {

                var identity = User.Identity as ClaimsIdentity;
                    if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    this.userBL.ResetPassword(email, password);
                    return Ok(new { success = true, message = "Password Changed Sucessfully", email = $"{UserEmailObject}" });
                }
                return this.BadRequest(new { success = false, message = $"Password changed unSuccessfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Table data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
