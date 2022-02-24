using CommonLayer.UserAddress;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserAddressBL
    {
        Task AddUserAddress(UserAddressPostModel userAddress, int userId);
        Task<List<UserAddress>> GetUserAddresses(int userId);
        Task UpdateUserAddress(UserAddressPostModel userAddress, int userId, int AddressId);
        Task DeleteAddress(int AddressId);
    }

}
