using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface IUserBusiness
    {
        public string Register(RegisterModel RegModel);
        public string Login(LoginModel logModel);
    }
}
