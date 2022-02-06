using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Class
{
    public class UserRL : IUserRL
    {
        FundooDbContext dbContext;

        public UserRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public void RegisteredUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.id = new User().id;
                user.name = userPostModel.name;
                user.mobileNo = userPostModel.mobileNo;
                user.password = userPostModel.password;
                
                

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
