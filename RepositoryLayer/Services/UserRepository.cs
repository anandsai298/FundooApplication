using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly FundooContext fundoo;
        private readonly IConfiguration Configuration;
        public UserRepository(FundooContext fundoo, IConfiguration Configuration)
        {
            this.fundoo= fundoo;
            this.Configuration = Configuration;
        }
        public UserEntity Register(RegisterModel RegModel)
        {
            try 
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = RegModel.FirstName;
                userEntity.LastName = RegModel.LastName;
                userEntity.Email = RegModel.Email;
                userEntity.Password = EncryptPassword(RegModel.Password);
                userEntity.CreateAt = DateTime.Now;
                userEntity.UpdateAt = DateTime.Now;
                fundoo.Users.Add(userEntity);
                fundoo.SaveChanges();
                return userEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EncryptPassword(string Password)
        {
            var EncryptPass=System.Text.Encoding.UTF8.GetBytes(Password);
            return System.Convert.ToBase64String(EncryptPass);
        }
        public string Login(LoginModel logModel)
        {
            string EnPassword=EncryptPassword(logModel.Password);
            var IfExists = this.fundoo.Users.Where(x => x.Email == logModel.Email && x.Password == EnPassword).FirstOrDefault();
            if (IfExists != null)
            {
                var token=GenerateToken(IfExists.UserID,IfExists.Email);
                return token;
            }
            return "Login UnsuccessFull Due to Invalid Email or Password";
        }
        public bool IfEmailExists(string Email)
        {
            var count = fundoo.Users.Where(x => x.Email.Equals(Email)).Count();
            return count > 0;
        }
        public String GenerateToken(int UserID,string Email)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserID",UserID.ToString())
            };
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], 
                Claims,expires: DateTime.Now.AddMinutes(15),signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var Check = fundoo.Users.Where(x => x.Email == email).FirstOrDefault();
                if(Check!=null)
                {
                    var token = GenerateToken(Check.UserID, Check.Email);
                    new MSMQ().SendMessage(token, Check.Email, Check.FirstName);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserTicket CreateTicketForPassword(string email,string token)
        {
            try
            {
                var Check = fundoo.Users.Where(x => x.Email == email).FirstOrDefault();
                if(Check!=null)
                {
                    UserTicket ticket = new UserTicket
                    {
                        FirstName = Check.FirstName,
                        LastName = Check.LastName,
                        Email = Check.Email,
                        Token = token,
                        IssueDateTime = DateTime.Now,
                    };
                    return ticket;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
