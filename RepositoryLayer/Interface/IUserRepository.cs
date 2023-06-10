using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        public UserEntity Register(RegisterModel RegModel);
        public string Login(LoginModel logModel);
        public bool IfEmailExists(string Email);
        public string ForgetPassword(string email);
        public UserTicket CreateTicketForPassword(string email, string token);

    }
}
