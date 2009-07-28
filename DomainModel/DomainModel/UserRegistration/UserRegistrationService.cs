using System;
using System.Collections.Generic;
using System.Web.Security;
using DomainModel.Tests;
using NHibernate;

namespace DomainModel.UserRegistration
{
    public class UserRegistrationService : IUserRegistration
    {
        public UserRepository Repository { get; set;}

        public UserRegistrationService()
        {
          
        }


        public int MinPasswordLength
        {
            get
            {
                return 8;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return true;
        }

        public MembershipCreateStatus CreateUser(User user)
        {
            
            Repository.SaveUser(user);

            return MembershipCreateStatus.Success;

        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }


        //public bool ChangePassword(string userName, string oldPassword, string newPassword)
        //{
        //    //User currentUser = _provider.GetUser(userName, true /* userIsOnline */);
        //    //return currentUser.ChangePassword(oldPassword, newPassword);
        //}

    }
}