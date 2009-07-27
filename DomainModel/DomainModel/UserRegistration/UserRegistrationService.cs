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

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status = MembershipCreateStatus.Success;// = user.Create(userName, password, email);
            //_provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        //public bool ChangePassword(string userName, string oldPassword, string newPassword)
        //{
        //    //User currentUser = _provider.GetUser(userName, true /* userIsOnline */);
        //    //return currentUser.ChangePassword(oldPassword, newPassword);
        //}
    }
}