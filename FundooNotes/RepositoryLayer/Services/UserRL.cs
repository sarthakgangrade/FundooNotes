﻿using CommonLayer.User;
using Crypteron.Internal.ThirdParty.Avro;
using Experimental.System.Messaging;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Message = Experimental.System.Messaging.Message;

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
                user.userId = new User().userId;
                user.name = userPostModel.name;
                user.email = userPostModel.email; ;
                user.password = userPostModel.password;

                user.password = StringCipher.Encrypt(userPostModel.password);

                dbContext.Users.Add(user);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string login(UserLogin userLogin)
        {
            try
            {
                User user = new User();
                var result = dbContext.Users.Where(x => x.email == userLogin.email && x.password == userLogin.password).FirstOrDefault();
                if (result != null)
                    return GenerateJWTToken(userLogin.email , user.userId);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string GenerateToken(string Email)
        {
            if (Email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        private static string GenerateJWTToken(string email, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email.ToString()),
                    new Claim("userId", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                var checkemail = dbContext.Users.FirstOrDefault(e => e.email == email);
                //var checkemail = dbContex.Users.FirstOrDefault(e => e.Email == email);
                if (checkemail != null)
                {
                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\FundooQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\FundooQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(email, checkemail.userId);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.sendMail(email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ResetPassword(string email, string password)
        {
            try
            {
                User user = new User();
                var result = dbContext.Users.FirstOrDefault(x => x.email == email);
                if (result != null)
                {
                    result.password = password;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                var result = dbContext.Users.ToList();
                
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.sendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        
        }
        
    }
}
