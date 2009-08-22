using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class UserTests : TestBase
    {
        [Test]
        public void TestUserConstructor()
        {
            Email email = new Email("user@carrierpigeons.com");
            UserName name = new UserName("first","last");
            User user = new User(email, name, "pwd123", null);
            Assert.AreEqual(user.Email.EmailAddress, email.EmailAddress);
            Assert.AreEqual(user.Name.FirstName, name.FirstName);
        }

        [Test]
        public void TestLoadUserByEmailAddress()
        {
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd", null);
            IUserRepository userRepository = UserRepository.Instance;
            userRepository.SaveUser(user);
            var loadedUser = userRepository.LoadUser("clinton@usa.gov");
            try
            {
                Assert.AreEqual(user.Email.EmailAddress, loadedUser.Email.EmailAddress);
            }finally
            {
                userRepository.Delete(user);
            }

        }

        [Test]
        public void TestUserGroupAssociation()
        {
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            UserGroup userGroup=new UserGroup{Id = 1,Name = "Pune",};
            User user = new User(email, name, "pwd", userGroup);
            IUserRepository userRepository = UserRepository.Instance;
            userRepository.SaveUser(user);
            var loadedUser = userRepository.LoadUser("clinton@usa.gov");
            try
            {
                Assert.AreEqual(userGroup,loadedUser.UserGroup);
            } 
            finally
            {
                userRepository.Delete(user);                   
            }
        }
    }
}
