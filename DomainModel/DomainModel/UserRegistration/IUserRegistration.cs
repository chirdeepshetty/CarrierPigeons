using System.Web.Security;
using DomainModel;

namespace DomainModel.UserRegistration
{
    public interface IUserRegistration
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        //bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}