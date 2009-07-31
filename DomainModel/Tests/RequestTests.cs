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
        public void TestRequestCreation ()
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
            User user = new User(new Email("user@carrierpigeons.com"), new UserName("First", "Last"),"pwd");

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
            IRequestRepository repository = RequestRepository.Instance;
            List<Request> requestlist = repository.Search(fromLocation,toLocation,travelDate);
            Assert.GreaterOrEqual(requestlist.Count,1);


        }


    }
}