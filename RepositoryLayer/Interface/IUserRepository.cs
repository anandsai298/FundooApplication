using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        public string Register(RegisterModel RegModel);
        public string Login(LoginModel logModel);

    }
}
