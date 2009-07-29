using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class JourneyTests
    {

        [Test]
        public void TestJourneyCreation ()
        {
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd");
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
        }
    }
}
