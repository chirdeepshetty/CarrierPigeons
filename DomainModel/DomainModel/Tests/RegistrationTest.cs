using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DomainModel.UserRegistration;
using NHibernate;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class RegistrationTest : InMemoryTestFixtureBase
    {

        [Test]
        public void TestCreateUser()
        {
            UserName name =new UserName("first","last");
            var user = new User(new Email("test@test.com"), name, "Pwd");
            session.Save(user);
            IQuery query = session.CreateQuery("from User");
            IList<User> users = query.List<User>();
            Assert.AreEqual(1, users.Count);

        }
    }
}
