using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace DomainModel.Tests
{
    [TestFixture]
    public class RequestTests
    {
        [Test]
        public void TestRequestCreation ()
        {
            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd");
            Request request = new Request(user, package, fromLocation, toLocation);

        }

    }

  
}