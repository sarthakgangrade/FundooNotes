using CommonLayer.UserAddress;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserAddressRL: IUserAddressRL
    {
        
        FundooDbContext dbContext;

        public UserAddressRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddUserAddress(UserAddressPostModel userAddress, int userId)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(e => e.userId == userId);
                UserAddress address = new UserAddress();
                address.AddressId = new UserAddress().AddressId;
                address.userId = userId;
                address.Address = userAddress.Address;
                address.City = userAddress.City;
                address.State = userAddress.State;
                dbContext.Address.Add(address);
                await dbContext.SaveChangesAsync();
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
                UserAddress useraddress = dbContext.Address.Where(e => e.userId == userId).FirstOrDefault();
                useraddress.City = userAddress.City;
                useraddress.State = userAddress.State;

                dbContext.Address.Update(useraddress);
                await dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<UserAddress>> GetUserAddresses(int userId)
        {
            return await dbContext.Address.Where(u => u.userId == userId)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task DeleteAddress(int AddressId)
        {
            try
            {
                UserAddress userAddress = dbContext.Address.Where(e => e.AddressId == AddressId).FirstOrDefault();
                dbContext.Address.Remove(userAddress);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}

