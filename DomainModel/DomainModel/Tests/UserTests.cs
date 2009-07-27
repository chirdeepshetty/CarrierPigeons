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
            Email email = new Email("user@carrierpigeons.com");
            UserName name = new UserName("first", "middle", "last");
            User user = new User(email, name);
        }
    }
}
