using CommonLayer.UserAddress;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserAddressRL
    {
        public Task AddUserAddress(UserAddressPostModel userAddress, int userId);
        public Task<List<UserAddress>> GetUserAddresses(int userId);
        public Task UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId);
        public Task DeleteAddress(int AddressId);
    }
}
