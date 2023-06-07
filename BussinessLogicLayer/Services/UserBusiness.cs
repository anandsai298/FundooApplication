using BusinessLogicLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserBusiness: IUserBusiness
    {
        private readonly IUserRepository IuserRep;
        public UserBusiness(IUserRepository iuserRep)
        {
            IuserRep = iuserRep;
        }
        public string Register(RegisterModel RegModel)
        {
            try
            {
                return IuserRep.Register(RegModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Login(LoginModel logModel)
        {
            try 
            {
                return IuserRep.Login(logModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
