using System.Collections.Generic;
using System.Linq;
using System.Text;

using DomainModel;
using DomainModel.UserRegistration;
using NUnit.Framework;



namespace DomainModel.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void TestUserConstructor()
        {

            //Email email = new Email("user@carrierpigeons.com");
            //UserName name = new UserName();
            //User user = new User(email, name);

            Email email = new Email("user@carrierpigeons.com");
            UserName name = new UserName("first","last");
            User user = new User(email, name, "pwd123");

            Assert.AreEqual(user.Email.EmailAddress, email.EmailAddress);
            Assert.AreEqual(user.Name.FirstName, name.FirstName);

        }

        [Test]
        public void TestLoadUserByEmailAddress()
        {
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd");


            IUserRepository userRepository = RepositoryFactory.GetUserRepository();
            userRepository.SaveUser(user);
            var loadedUser = userRepository.LoadUser("clinton@usa.gov");
            Assert.AreEqual(user.Email.EmailAddress, loadedUser.Email.EmailAddress);
        }
    }
}
