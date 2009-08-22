using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.UserRegistration;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class JourneyTests
    {

        [Test]
        public void TestJourneyCreation()
        {
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", null);
            
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
            Assert.AreEqual("London", journey.Origin.Place);
            Assert.AreEqual("Mumbai", journey.Destination.Place);
            Assert.AreEqual("asd@dsf.com", journey.Traveller.Email.EmailAddress);
        }

 
        public void TestJourneyRepositorySave()
        {
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", null);
            RepositoryFactory.GetUserRepository().SaveUser(traveller);
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);
            int journeyid = journey.Id;
            Journey journeyEntity = journeyRepository.Load(journeyid);
            try
            {
                Assert.AreEqual("London", journey.Origin.Place);
                Assert.AreEqual("Mumbai", journey.Destination.Place);
                Assert.AreEqual("asd@dsf.com", journey.Traveller.Email.EmailAddress);
            }finally
            {
                journeyRepository.Delete(journey);
                RepositoryFactory.GetUserRepository().Delete(traveller);
            }

        }

               [Test]

        public void TestGetJourneyByUser()
        {
            User traveller = new User(new Email("abcd@dsf.com"), new UserName("first", "last"), "pwd", null);

            RepositoryFactory.GetUserRepository().SaveUser(traveller);

            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);
            
            IList<Journey> journeyList = journeyRepository.FindJourneysByUser(traveller);
        try{
            Assert.GreaterOrEqual(journeyList.Count,1);
        }finally
            {
                journeyRepository.Delete(journey);
                RepositoryFactory.GetUserRepository().Delete(traveller);
            }
        
        }


        [Test]
        public void TestGetJourneyByRequest()
        {
            Journey journey = null;
            
                Guid locationId = Guid.NewGuid();
                UserGroup userGroup = UserGroupRepository.Instance.LoadGroupById(1);
                User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", userGroup);
                RepositoryFactory.GetUserRepository().SaveUser(traveller);

                Location origin = new Location("Bangalore", new TravelDate(DateTime.Now.AddDays(1)));
                Location destination = new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(10)));
                journey = new Journey(traveller, origin, destination);
                IJourneyRepository journeyRepository = JourneyRepository.Instance;
                journeyRepository.Save(journey);

        
                User another = new User(new Email("r@abc.com"), new UserName("first", "last"), "pwd", userGroup);
                RepositoryFactory.GetUserRepository().SaveUser(another);

                Request request = new Request(another, new Package(null, null, null), origin, new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(20))));
                RequestRepository.Instance.Save(request);
                IEnumerable<Journey> journeyList = journeyRepository.Find(request);
                Assert.True(journeyList.Count() > 0 );
                try
                {
                Assert.IsTrue(journeyList.Where(a => a.Origin.Place == request.Origin.Place && a.Destination.Place.Equals(request.Destination.Place)).Count() == 1);
            }
            finally
            {
                JourneyRepository.Instance.Delete(journey);
                RepositoryFactory.GetUserRepository().Delete(traveller);
            }

        }

        [Test]
        public void TestShouldFailIfSameUsersJourneysAreMatching()
        {
            Journey journey = null;

            Guid locationId = Guid.NewGuid();
            UserGroup userGroup = UserGroupRepository.Instance.LoadGroupById(1);
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", userGroup);
            RepositoryFactory.GetUserRepository().SaveUser(traveller);

            Location origin = new Location("Bangalore", new TravelDate(DateTime.Now.AddDays(1)));
            Location destination = new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(10)));
            journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);

            Request request = new Request(traveller, new Package(null, null, null), origin, new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(20))));
            RequestRepository.Instance.Save(request);
            IEnumerable<Journey> journeyList = journeyRepository.Find(request);
            
            try
            {
                Assert.True(journeyList.Count() == 0);
            }
            finally
            {
                JourneyRepository.Instance.Delete(journey);
                RepositoryFactory.GetUserRepository().Delete(traveller);
            }

        }

        [Test]
        public void TestCreateJourneyForAnExistingUser()
        {
            IUserRepository userRepository = RepositoryFactory.GetUserRepository();
            UserRegistration.UserRegistrationService service = new UserRegistrationService(userRepository);
            User traveller = new User(new Email("test@test.com"), new UserName("Test", "User"), "12345", null);

            service.CreateUser(traveller);
            
            Location origin = new Location("London", new TravelDate(DateTime.Now));
            Location destination = new Location("Mumbai", new TravelDate(DateTime.Now));
            Journey journey = new Journey(traveller, origin, destination);

            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);

            IList<Journey> journeyList = journeyRepository.FindJourneysByUser(traveller);

            try
            {
                Assert.AreEqual(1,journeyList.Count);
            }
            finally
            {
                journeyRepository.Delete(journey);
                userRepository.Delete(traveller);
            }
        }
    }
}
