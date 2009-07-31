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

        #region IRequestRepository Members

        public void Save(Request request)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;
            
            session.Save(request);
            
            session.Close();
        }

        public List<Request> Search(Location location, Location toLocation, TravelDate date)
        {
            
            var session = sessionFactory.OpenSession();
            string querystring = "from Request as R where R.Destination.Place = :destination and R.Origin.Place= :origin and R.Destination.Date.DateTime <= :date";
            IQuery query = session.CreateQuery(querystring);
            query.SetString("destination", toLocation.Place);
            query.SetString("origin", location.Place);
            query.SetDateTime("date", date.DateTime);
            var requestList = (List<Request>) query.List<Request>();
            return requestList;
        }

        #endregion
    }
}