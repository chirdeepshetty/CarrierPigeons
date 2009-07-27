using System.Collections.Generic;
using System.Linq;
using System.Text;

using DomainModel;
using NUnit.Framework;



namespace DomainModel.Tests
{
    [TestFixture]
    public class UserTests
    {
        public void TestUserConstructor()
        {
            Email email = new DomainModel.Email("user@carrierpigeons.com");
            UserName name = new DomainModel.UserName("first", "middle", "last");
            User user = new DomainModel.User(email, name);
        }
    }
}
