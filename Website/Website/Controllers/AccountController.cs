using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using DomainModel;
using DomainModel.Tests;
using  DomainModel.UserRegistration;

namespace Website.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.

        public AccountController()
            : this(null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IUserRegistration service)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationService();
            UserRegistrationService = service ?? new UserRegistrationService(UserRepository.Instance);
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IUserRegistration UserRegistrationService
        {
            get;
            private set;
        }

        public ActionResult LogOn()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string email, string password, bool rememberMe, string returnUrl)
        {

            if (!ValidateLogOn(email, password))
            {
                return View();
            }

            FormsAuth.SignIn(email, rememberMe);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {

            FormsAuth.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HibernateSessionFilter]
        public ActionResult Register()
        {

            ViewData["PasswordLength"] = UserRegistrationService.MinPasswordLength;


            ViewData["UserGroups"] = new SelectList(UserGroupRepository.Instance.LoadGroups(), "Id", "Name");
            return View();
        }

        [HibernateSessionFilter]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string firstname, string surname, string email, string password, string confirmPassword, Int32 userGroups)
        {

            //ViewData["PasswordLength"] = UserRegistrationService.MinPasswordLength;
            ViewData["PasswordLength"] = 8;
            String fullname = firstname + " " + surname;

            if (ValidateRegistration(fullname, email, password, confirmPassword))
            {
                // Attempt to register the user
                try
                {
                    UserGroup userGroup = UserGroupRepository.Instance.LoadGroupById(userGroups);   
                    UserRegistrationService.CreateUser(new User(new Email(email), new UserName(fullname, ""), password, userGroup));
                    //FormsAuth.SignIn(email, false /* createPersistentCookie */);
                    return RedirectToAction("LogOn", "Account");
                }
                catch (DuplicateRegistrationException)
                {
                    ModelState.AddModelError("_FORM", "Email address already registered with us.");
                }
            }
            return View();
        }

        [Authorize]
        [HibernateSessionFilter]
        public ActionResult ChangePassword()
        {

            ViewData["PasswordLength"] = UserRegistrationService.MinPasswordLength;

            return View();
        }

        [Authorize]
        [HibernateSessionFilter]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {

            ViewData["PasswordLength"] = UserRegistrationService.MinPasswordLength;

            if (!ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            //try
            //{
            //    if (UserRegistrationService.ChangePassword(User.Identity.Name, currentPassword, newPassword))
            //    {
            //        return RedirectToAction("ChangePasswordSuccess");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
            //        return View();
            //    }
            //}
            //catch
            //{
            //    ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
            //    return View();
            //}
            return View();
        }

        [HibernateSessionFilter]
        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods

        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < UserRegistrationService.MinPasswordLength)
            {
                ModelState.AddModelError("newPassword",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a new password of {0} or more characters.",
                         UserRegistrationService.MinPasswordLength));
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }
            if (!UserRegistrationService.AreCredentialsValid(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < UserRegistrationService.MinPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         UserRegistrationService.MinPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }
            return ModelState.IsValid;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Email address is already registered.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
