using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Class
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void RegisteredUser(UserPostModel userPostModel)
        {

            try
            {
                userRL.RegisteredUser(userPostModel);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
