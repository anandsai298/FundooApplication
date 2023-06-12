using BusinessLogicLayer.Interface;
using com.sun.org.apache.bcel.@internal.generic;
using Microsoft.Data.SqlClient.Server;
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
        public UserEntity Register(RegisterModel RegModel)
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
        public bool IfEmailExists(string Email)
        {
            try
            {
                return IuserRep.IfEmailExists(Email);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return IuserRep.ForgetPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ResetPassword(ResetPassword resetPassword, string Email)
        {
            try
            {
                return IuserRep.ResetPassword(resetPassword, Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserTicket CreateTicketForPassword(string email, string token)
        {
            try
            {
                return IuserRep.CreateTicketForPassword(email, token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       /* public ReviewEntity ReviewRegister(ReviewRegisterModel revModel)
        {
            try
            {
                return IuserRep.ReviewRegister(revModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/
    }
}
