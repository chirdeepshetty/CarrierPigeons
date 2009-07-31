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
            Assert.AreEqual("London",journey.Origin.Place);
            Assert.AreEqual("Mumbai", journey.Destination.Place);
            Assert.AreEqual("asd@dsf.com", journey.Traveller.Email.EmailAddress);
        }

        [Test]
        public void TestJourneyRepositorySave()
        {
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd");
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);
            int journeyid = journey.Id;
            Journey journeyEntity = journeyRepository.Load(journeyid);
            Assert.AreEqual("London", journey.Origin.Place);
            Assert.AreEqual("Mumbai", journey.Destination.Place);
            Assert.AreEqual("asd@dsf.com", journey.Traveller.Email.EmailAddress);
            
        }

        [Test]
        public void TestGetJourneyByUser ()
        {
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd");
            RepositoryFactory.GetUserRepository().SaveUser(traveller);
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);


            User user = new User(new Email("asd@dsf.com"), new UserName(null, null), null);
            List<Journey> journeyList = journeyRepository.FindJourneysByUser(user.Email.EmailAddress);
        }





    }
    }
