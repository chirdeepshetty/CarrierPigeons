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


        [Test]
        public void testUserRegistration()
        {

            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd");


            IUserRepository userRepository = RepositoryFactory.GetUserRepository();
            UserRegistration.UserRegistrationService service = new UserRegistrationService(userRepository);


            service.CreateUser(user);




        }
        [Test]
        public void ShouldRegisterUserIfNotDuplicate()
        {
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd");
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
            User user = new User(email, name, "pwd");
            mockRepository.Setup(ur => ur.LoadUser(user.EmailAddress)).Returns(user);
            Assert.Throws(typeof(DuplicateRegistrationException), delegate()
            {
                new UserRegistrationService(mockRepository.Object).
                    CreateUser(user);
            });

            mockRepository.Verify(ur => ur.LoadUser(user.EmailAddress));


        }
    }
}
