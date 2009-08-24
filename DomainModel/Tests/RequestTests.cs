using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;


namespace DomainModel.Tests
{
    [TestFixture]
    public class RequestTests : TestBase
    {
        [Test]
        public void TestRequestCreation()
        {
            TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd", null);
            Request request = new Request(user, package, fromLocation, toLocation);
        }

        [Test]
        public void TestRequestRepository()
        {
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd", null);
            UserRepository.Instance.SaveUser(user);

            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            

            Request request = new Request(user, package, fromLocation, toLocation);

            IRequestRepository repository = RequestRepository.Instance;
            repository.Save(request);
            try
            {
                Assert.NotNull(request.Id);
            }
            finally
            {
                repository.Delete(request);
                UserRepository.Instance.Delete(user);
            }
        }

        [Test]
        public void TestRequestSearch()
        {
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd", null);
            UserRepository.Instance.SaveUser(user);

            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            
            Request request = new Request(user, package, fromLocation, toLocation);

            IRequestRepository repository = RequestRepository.Instance;
            repository.Save(request);
            DomainModel.TravelDate travelDate2 = new TravelDate(DateTime.Now);
            List<Request> requestlist = repository.Search(fromLocation, toLocation, travelDate2);           
            try
            {
                Assert.GreaterOrEqual(requestlist.Count, 1);
            }
            finally
            {
                repository.Delete(request);
                UserRepository.Instance.Delete(user);
            }
        }

        [Test]
        public void TestFindRequestHavingSameLocationButFromASameUser()
        {
            Journey journey = null;

            UserGroup group = UserGroupRepository.Instance.LoadGroupById(1);
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", group);
            UserRepository.Instance.SaveUser(traveller);

            Location origin = new Location("Bangalore", new TravelDate(DateTime.Now.AddDays(1)));
            Request request = new Request(traveller, new Package(null, null, null), origin,
                                          new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(10))));
            RequestRepository.Instance.Save(request);

            Location destination = new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(20)));
            journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);

            IEnumerable<Request> requests = RequestRepository.Instance.Find(journey);
            try{
                Console.WriteLine("####" + requests.Count());
            Assert.True(requests.Count() == 0);
            }
            finally
            {
                RequestRepository.Instance.Delete(request);
                JourneyRepository.Instance.Delete(journey);
                UserRepository.Instance.SaveUser(traveller);
            }
    }



        [Test]
        public void TestFindRequestHavingSameLocationButFromADifferentUserButSameGroup()
        {
            Journey journey = null;
            UserGroup group = UserGroupRepository.Instance.LoadGroupById(1);
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", group);
            UserRepository.Instance.SaveUser(traveller);

            Location origin = new Location("Bangalore", new TravelDate(DateTime.Now.AddDays(1)));
            Request request = new Request(traveller, new Package(null, null, null), origin, new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(10))));
            RequestRepository.Instance.Save(request);

            Location destination = new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(20)));

            User traveller1 = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", group);
            UserRepository.Instance.SaveUser(traveller1);

            journey = new Journey(traveller1, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);

            IEnumerable<Request> requests = RequestRepository.Instance.Find(journey);

            try
            {
                Assert.True(requests.Count() == 1);
            }
            finally
            {
                RequestRepository.Instance.Delete(request);
                JourneyRepository.Instance.Delete(journey);
                UserRepository.Instance.Delete(traveller1);
                UserRepository.Instance.SaveUser(traveller);
            }
        }


        [Test]
        public void TestFindRequestHavingMatchingLocationButFromADifferentGroup()
        {
            Journey journey = null;

            UserGroup group = UserGroupRepository.Instance.LoadGroupById(1);
            User requestingUser = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", group);
            RepositoryFactory.GetUserRepository().SaveUser(requestingUser);

            Location origin = new Location("Bangalore", new TravelDate(DateTime.Now.AddDays(1)));
            Request request = new Request(requestingUser, new Package(null, null, null), origin,
                                          new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(10))));
            RequestRepository.Instance.Save(request);

            UserGroup group2 = UserGroupRepository.Instance.LoadGroupById(2);
            User traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", group2);
            RepositoryFactory.GetUserRepository().SaveUser(traveller);

            Location destination = new Location("Hyderabad", new TravelDate(DateTime.Now.AddDays(20)));
            journey = new Journey(traveller, origin, destination);
            IJourneyRepository journeyRepository = JourneyRepository.Instance;
            journeyRepository.Save(journey);

            IEnumerable<Request> requests = RequestRepository.Instance.Find(journey);
            try
            {
                Console.WriteLine("####" + requests.Count());
                Assert.True(requests.Count() == 0);
            }
            finally
            {
                RequestRepository.Instance.Delete(request);
                JourneyRepository.Instance.Delete(journey);
                RepositoryFactory.GetUserRepository().SaveUser(requestingUser);
                RepositoryFactory.GetUserRepository().SaveUser(traveller);
            }
        }

        [Test]
        public void GetRequestsByUser()
        {
            IRequestRepository requestRepository = RequestRepository.Instance;
            IUserRepository userRepository = UserRepository.Instance;
            Email email1 = new Email("lokeshwer1@gmail.com");
            Email email2 = new Email("lokeshwer2@gmail.com");
            User user1 = new User(new Email("user1@carrierpigeons.com"), new UserName("User1", "Last"), "pwd", null);
            User user2 = new User(new Email("user2@carrierpigeons.com"), new UserName("User2", "Last"), "pwd", null);
            userRepository.SaveUser(user1);
            userRepository.SaveUser(user2);
            Request request1 = new Request(user1, new Package("test pkg", "1kg", "1x1x1kg"),
                                           new Location("London", new TravelDate(DateTime.Now)),
                                           new Location("Paris", new TravelDate(DateTime.Now)));
            Request request2 = new Request(user2, new Package("test pkg", "1kg", "1x1x1kg"),
                                           new Location("London", new TravelDate(DateTime.Now)),
                                           new Location("Paris", new TravelDate(DateTime.Now)));
            requestRepository.Save(request1);
            requestRepository.Save(request2);
            IEnumerable<Request> requests = requestRepository.SearchByUser(user1.EmailAddress);
            try
            {
                Assert.True(requests.Contains(request1));
                Assert.False(requests.Contains(request2));
            }
            finally
            {
                requestRepository.Delete(request1);
                requestRepository.Delete(request2);
            }
        }

       
    }
}