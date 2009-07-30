using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class MatchTests
    {
        [Test]
        public void TestMatchConstructor()
        {
            User user = new User(new Email("abc@def.com"), null, "password");
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(user, origin, destination);
            Request request = new Request(user, null, origin, destination);
            Match match = new Match(journey, request);
            Assert.AreEqual(origin.Place, match.Journey.Origin.Place);
            Assert.AreEqual(origin.Place, match.Request.Origin.Place);
            Assert.AreEqual(destination.Place, match.Journey.Destination.Place);
            Assert.AreEqual(destination.Place, match.Request.Destination.Place);

        }
    }
}
