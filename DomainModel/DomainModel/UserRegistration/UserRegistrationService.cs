using System;
using System.Web.Security;

namespace DomainModel.UserRegistration
{
    public class UserRegistrationService : IUserRegistration
    {


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
            throw new NotImplementedException();
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