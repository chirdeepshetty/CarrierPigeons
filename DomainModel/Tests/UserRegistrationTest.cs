using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.UserRegistration;
using Moq;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class UserRegistrationTest
    {
        private Mock<IUserRepository> _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IUserRepository>();
        }

        [Test]
        public void testUserRegistration()
        {

            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd", null);


            IUserRepository userRepository = RepositoryFactory.GetUserRepository();
            UserRegistration.UserRegistrationService service = new UserRegistrationService(userRepository);


            service.CreateUser(user);
            userRepository.Delete(user);
        }
        [Test]
        public void ShouldRegisterUserIfNotDuplicate()
        {
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd", null);
            mockRepository.Setup(ur => ur.LoadUser(user.EmailAddress)).Returns((User)null);
            mockRepository.Setup(ur => ur.SaveUser(user));
            new UserRegistrationService(mockRepository.Object).CreateUser(user);
            mockRepository.Verify(ur => ur.LoadUser(user.EmailAddress));
            mockRepository.Verify(ur => ur.SaveUser(user));


        }
        [Test]
        public void ShouldNotRegisterIfDuplicate()
        {
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd", null);
            mockRepository.Setup(ur => ur.LoadUser(user.EmailAddress)).Returns(user);
            Assert.Throws(typeof(DuplicateRegistrationException), delegate()
            {
                new UserRegistrationService(mockRepository.Object).
                    CreateUser(user);
            });

            mockRepository.Verify(ur => ur.LoadUser(user.EmailAddress));
        }

        [Test]
        public void ShouldTestInvalidUserLogin()
        {
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User nonExistentUser = new User(email, name, "pwd", null);

            _mockRepository.Setup(ur => ur.LoadUser(nonExistentUser.EmailAddress)).Returns((User)null);
            UserRegistration.UserRegistrationService service = new UserRegistrationService(_mockRepository.Object);

            Assert.False(service.AreCredentialsValid(email.EmailAddress, nonExistentUser.Password));
        }

        [Test]
        public void ShouldTestValidUserLogin()
        {
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd", null);


            _mockRepository.Setup(ur => ur.LoadUser(user.EmailAddress)).Returns(user);
            UserRegistration.UserRegistrationService service = new UserRegistrationService(_mockRepository.Object);
            
            Assert.True(service.AreCredentialsValid(email.EmailAddress, user.Password));
          
        }

       
    }
}
