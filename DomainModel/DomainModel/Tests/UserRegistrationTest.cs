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
        public void testCreateUserRepository ()
      {

          RepositoryFactory factory = new RepositoryFactory();
          UserRepository userRepository = (UserRepository)factory.GetUserRepository();
          //userRepository.save(user);
          
          
 
      }
        
        [Test]
        public void testUserRegistration ()
        {
            
            UserName name = new UserName("Bill", "Clinton");
            Email email = new Email("clinton@usa.gov");


            User user = new User(email, name, "pwd");

            UserRegistration.UserRegistrationService  service = new UserRegistrationService();

            service.CreateUser(user);

        }
    }
}
