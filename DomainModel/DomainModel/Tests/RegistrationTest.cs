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
        private ISession session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            InitalizeSessionFactory(new FileInfo("src/User.hbm.xml"));
        }

        [SetUp]
        public void SetUp()
        {
            session = this.CreateSession();
        }

        [TearDown]
        public void TearDown()
        {
            session.Dispose();
        }


        [Test]
        public void TestCreateUser()
        {
            UserName name =new UserName("first","last");
            var user = new User(new Email("test@test.com"), name);
            session.Save(user);

        }
    }
}
