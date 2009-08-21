using System.Web.Security;
using DomainModel;

namespace DomainModel.UserRegistration
{
    public interface IUserRegistration
    {
        int MinPasswordLength { get; }

        bool ValidateCredentials(string userName, string password);
        MembershipCreateStatus CreateUser(User user);
        //bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}