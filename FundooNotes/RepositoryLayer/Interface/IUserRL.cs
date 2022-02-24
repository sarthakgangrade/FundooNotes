using CommonLayer.User;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void RegisteredUser(UserPostModel userPostModel);
        public string login(UserLogin userLogin);
        public bool ForgetPassword(string email);
        public void ResetPassword(string email, string password);
        public List<User> GetAllUsers();
        //public string EncryptPassword(string password);
        //private static string GenerateToken(string Email);
    }
}
