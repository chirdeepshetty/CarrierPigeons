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
<<<<<<< HEAD:DomainModel/DomainModel/Tests/RequestTests.cs
            User user = new User(new Email("user@carrierpigeins.com"), new UserName("First", "Last"));
=======
            User user = new User(new Email("user@carrierpigeins.com"), new UserName("First","Last"));
>>>>>>> 7565b92096f3ced939ba3f1e48d3f00d825595f3:DomainModel/DomainModel/Tests/RequestTests.cs
            Request request = new Request(user, package, fromLocation, toLocation);

        }

    }

  
}