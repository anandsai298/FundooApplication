using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity Register(RegisterModel RegModel);
        public string Login(LoginModel logModel);
        public bool IfEmailExists(string Email);
        public string ForgetPassword(string email);
        public UserTicket CreateTicketForPassword(string email, string token);
        public string ResetPassword(ResetPassword resetPassword, string Email);
        //public ReviewEntity ReviewRegister(ReviewRegisterModel revModel);
    }
}
