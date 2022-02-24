using BusinessLayer.Interface;
using CommonLayer.UserAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("userAdress")]
    public class UserAddressController: ControllerBase
    {
        FundooDbContext fundooDBContext;
        IUserAddressBL UserAddressBL;
        public UserAddressController(IUserAddressBL UserAddressBL, FundooDbContext fundooDB)
        {
            this.UserAddressBL = UserAddressBL;
            this.fundooDBContext = fundooDB;
        }
        [Authorize]
        [HttpPost("addUserAddress")]
        public async Task<IActionResult> AddUserAddress(UserAddressPostModel userAddress)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                await this.UserAddressBL.AddUserAddress(userAddress, userId);

                return this.Ok(new { success = true, Message = $"User in Address is added successfull" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPost("updateUserAddress/{userId}/{AddressId}")]
        public async Task<IActionResult> UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId)
        {
            try
            {
                var UserId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                //int userId = Int32.Parse(UserId.Value);

                await this.UserAddressBL.UpdateUserAddress(userAddress, userId, AddressId);
                return this.Ok(new { success = true, Message = $"Address is updated successfull" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpGet("getUserAddress")]
        public async Task<IActionResult> GetUserAddresses()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                var userAddressList = new List<UserAddress>();
                userAddressList = await UserAddressBL.GetUserAddresses(UserId);

                return this.Ok(new { Success = true, message = $"Get UserAddress successfully ", data = userAddressList });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpDelete("deleteAddress/{AddressId}")]
        public IActionResult eleteUserAddress(int AddressId)
        {
            try
            {
                this.UserAddressBL.DeleteAddress(AddressId);
                return this.Ok(new { success = true, Message = $"Address is deleted successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

