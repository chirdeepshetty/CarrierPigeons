using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace DomainModel
{
    public class JourneyRepository : IJourneyRepository
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;
        public event JourneyCreatedEventHandler JourneyCreated;

        private JourneyRepository()
        {
            if(sessionFactory!=null)
                return;
            configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            sessionFactory = configuration.BuildSessionFactory();
        }
        static JourneyRepository _journeyRepository = new JourneyRepository();

        public static IJourneyRepository Instance
        {
            get
            {
                return _journeyRepository;
            }
        }

        #region IJourneyRepository Members

        public void Save(Journey journey)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;
            
            session.Save(journey);
            
            session.Close();
            if(this.JourneyCreated != null)
                JourneyCreated(new JourneyCreatedEventArgs{Journey = journey});
        }

        public Journey Load(int journeyId)
        {
            var session = sessionFactory.OpenSession();
            Journey journey = new Journey();
            session.Load(journey, journeyId);
            return journey;
            
        }

        public void Delete(Journey journey)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;

            session.Delete(journey);
            session.Flush();
            session.Close();
        }

        public IList<Journey> FindJourneysByUser(User user)
        {
            var session = sessionFactory.OpenSession();
            const string querystring = "from Journey as J  where J.Traveller = :traveller";
            IQuery query = session.CreateQuery(querystring);
            query.SetEntity("traveller", user);
            var journeyList = (List<Journey>)query.List<Journey>();
            return journeyList;

        }

        public IEnumerable<Journey> Find(Request request)
        {
            var session = sessionFactory.OpenSession();
            const string findByRequest = "from Journey as J where J.Origin.Place = :origin and J.Destination.Place = :destination and J.Destination.Date.DateTime <= :arrivalDate";
            IQuery query = session.CreateQuery(findByRequest);
            query.SetString("origin", request.Origin.Place);
            query.SetString("destination", request.Destination.Place);
            query.SetDateTime("arrivalDate", request.Destination.Date.DateTime);
            return query.List<Journey>();
        }

        #endregion
    }

    public interface IJourneyRepository
    {
        void Save(Journey journey);
        Journey Load(int journeyId);
        event JourneyCreatedEventHandler JourneyCreated;
        IList<Journey> FindJourneysByUser(User user);
        IEnumerable<Journey> Find(Request request);
        void Delete(Journey journey);
    }

    public delegate void JourneyCreatedEventHandler(JourneyCreatedEventArgs e);

    public class JourneyCreatedEventArgs : EventArgs
    {
        public Journey Journey { get; set; }
    }
}