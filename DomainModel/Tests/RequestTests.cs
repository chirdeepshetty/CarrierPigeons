using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;


namespace DomainModel.Tests
{
    [TestFixture]
    public class RequestTests
    {
        [Test]
        public void DropAndRecreateSchema()
        {
            var configuration = new Configuration();
            var mapping = configuration.BuildMapping();
            var sessionFactory = configuration.BuildSessionFactory();
            ISession openSession = sessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;

            StringWriter writer = new StringWriter();
            new SchemaExport(configuration).Execute(false, true, false, true, connection, null);
            openSession.Close();
        }

        [Test]
        public void TestRequestCreation()
        {
            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd");
            Request request = new Request(user, package, fromLocation, toLocation);
        }

        [Test]
        public void TestRequestRepository()
        {
            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"), "pwd");

            Request request = new Request(user, package, fromLocation, toLocation);

            IRequestRepository repository = RequestRepository.Instance;
            repository.Save(request);
        }

        [Test]
        public void TestRequestSearch()
        {
            DomainModel.TravelDate travelDate = new TravelDate(DateTime.Now);
            Location fromLocation = new Location("place", travelDate);
            Location toLocation = new Location("place", travelDate);
            Package package = new Package("My Package", "1 Kg", "1x2x3 kg");
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"),"pwd");
            Request request = new Request(user, package, fromLocation, toLocation);

            IRequestRepository repository = RequestRepository.Instance;
            repository.Save(request);
            DomainModel.TravelDate travelDate2 = new TravelDate(DateTime.Now);
            List<Request> requestlist = repository.Search(fromLocation, toLocation, travelDate2);
            Assert.GreaterOrEqual(requestlist.Count, 1);
        }

        [Test]
        public void GetRequestsByUser()
        {
            IRequestRepository requestRepository = RequestRepository.Instance;
            IUserRepository userRepository = RepositoryFactory.GetUserRepository();
            Email email1 = new Email("lokeshwer1@gmail.com");
            Email email2 = new Email("lokeshwer2@gmail.com");
            User user1 = new User(new Email("user1@carrierpigeons.com"), new UserName("User1", "Last"), "pwd");
            User user2 = new User(new Email("user2@carrierpigeons.com"), new UserName("User2", "Last"), "pwd");
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
                userRepository.Delete(user1);
                userRepository.Delete(user2);
            }
        }

       
    }
}