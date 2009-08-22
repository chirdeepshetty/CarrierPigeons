using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DomainModel
{
    public class RequestRepository : IRequestRepository
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;
        public event RequestCreatedEventHandler RequestCreated;
        
       

        private RequestRepository()
        {
            if(sessionFactory!=null)
                return;
            configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            sessionFactory = configuration.BuildSessionFactory();
        }
        static RequestRepository _requestRepository = new RequestRepository();
        
        public static IRequestRepository Instance
        {
            get
            {
                return _requestRepository;
            }
        }

        

        public void Save(Request request)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;
            
            session.Save(request);
            
            session.Close();

            if(this.RequestCreated != null)
                RequestCreated(new RequestCreatedEventArgs{Request = request});
        }

        public List<Request> Search(Location location, Location toLocation, TravelDate date)
        {
            var session = sessionFactory.OpenSession();
            string querystring =
                "from Request as R where R.Destination.Place = :destination and R.Origin.Place= :origin and R.Destination.Date.DateTime <= :date";
            IQuery query = session.CreateQuery(querystring);
            query.SetString("destination", toLocation.Place);
            query.SetString("origin", location.Place);
            query.SetDateTime("date", date.DateTime);
            var requestList = (List<Request>) query.List<Request>();
            return requestList;
        }

        public IEnumerable<Request> SearchByUser(string address)
        {
            var session = sessionFactory.OpenSession();
            string findByUser = 
                "from Request as R where R.RequestedUser.Email.EmailAddress = :email";
            IQuery query = session.CreateQuery(findByUser);
            query.SetString("email", address);
            return query.List<Request>();
        }

        public IEnumerable<Request> Find(Journey journey)
        {

            //elect J from Journey J, Request R 
            //                                where J.Traveller <> R.RequestedUser
            //                                and R.RequestedUser = :requestedUser 
            //                                and J.Origin.Place = :origin 
            //                                and J.Destination.Place = :destination 
            //                                and J.Destination.Date.DateTime <= :arrivalDate


            var session = sessionFactory.OpenSession();
            string findByJourney =
                @"select R from Request as R, Journey J 
                where J.Traveller <> R.RequestedUser                 
                and J.Traveller.UserGroup = R.RequestedUser.UserGroup
                and J.Traveller = :journeyUser
                and R.Origin.Place = :origin                 
                and R.Destination.Place = :destination
                and R.Destination.Date.DateTime <= :arrivalDate";
            IQuery query = session.CreateQuery(findByJourney);
            query.SetParameter("journeyUser", journey.Traveller);
            query.SetString("origin", journey.Origin.Place);
            query.SetString("destination", journey.Destination.Place);
            query.SetDateTime("arrivalDate", journey.Destination.Date.DateTime);
            return query.List<Request>();
        }

        public void Delete(Request request)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;

            session.Delete(request);
            session.Flush();
            session.Close();
        }
    }

    public delegate void RequestCreatedEventHandler(RequestCreatedEventArgs e);

    public class RequestCreatedEventArgs : EventArgs
    {
        public Request Request { get; set; }
    }
}