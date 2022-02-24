using BusinessLayer.Interface;
using CommonLayer.UserAddress;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserAddressBL : IUserAddressBL
    {
        IUserAddressRL userAddressRL;

        public UserAddressBL(IUserAddressRL userAddressRL)
        {
            this.userAddressRL = userAddressRL;
        }
        public async Task AddUserAddress(UserAddressPostModel userAddress, int userId)
        {
            try
            {
                await userAddressRL.AddUserAddress(userAddress, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<UserAddress>> GetUserAddresses(int userId)
        {
            try
            {
                return await userAddressRL.GetUserAddresses(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId)
        {
            try
            {
                await userAddressRL.UpdateUserAddress(userAddress, userId, AddressId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteAddress(int AddressId)
        {
            try
            {
                await userAddressRL.DeleteAddress(AddressId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


    }
}
