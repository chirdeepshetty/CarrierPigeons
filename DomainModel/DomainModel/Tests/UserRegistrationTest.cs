using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.UserRegistration;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class UserRegistrationTest : InMemoryTestFixtureBase
    {

    
        [Test]
        public void testUserRegistration ()
        {
            
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");
            User user = new User(email, name, "pwd");


            UserRepository userRepository = (UserRepository)new RepositoryFactory ().GetUserRepository();
            userRepository.SaveUser(user);
            UserRegistration.UserRegistrationService  service = new UserRegistrationService();
            service.Repository = userRepository;

            service.CreateUser(user);
            
            


        }
    }
}
