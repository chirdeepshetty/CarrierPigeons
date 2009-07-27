using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.UserRegistration;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class RegistrationTest
    {
        [Test]
        public void TestCreateUser()
        {
            Registration reg = new Registration();
            reg.CreateUser("test", "test", "test@123.com");
        }
    }
}
