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

      
        public List<Journey> FindJourneysByUser(string emailid)
        {

            User user = RepositoryFactory.GetUserRepository().LoadUser(emailid);
            var session = sessionFactory.OpenSession();
            string querystring = "from Journey as J  where J.Traveller = :user_id";
            IQuery query = session.CreateQuery(querystring);
            query.SetEntity("user_id", user);
            //query.SetInt32("user_id", user.Id);
            var journeyList = (List<Journey>)query.List<Journey>();
            return journeyList;

        }

        #endregion
    }

    public interface IJourneyRepository
    {
        void Save(Journey journey);
        Journey Load(int journeyId);
        event JourneyCreatedEventHandler JourneyCreated;
        List<Journey> FindJourneysByUser(string emailid);
    }

    public delegate void JourneyCreatedEventHandler(JourneyCreatedEventArgs e);

    public class JourneyCreatedEventArgs : EventArgs
    {
        public Journey Journey { get; set; }
    }
}