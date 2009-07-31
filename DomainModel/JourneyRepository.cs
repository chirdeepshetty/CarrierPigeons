using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DomainModel
{
    public class JourneyRepository : IJourneyRepository
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;
        
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
        }

        public Journey Load(int journeyId)
        {
            var session = sessionFactory.OpenSession();
            Journey journey = new Journey();
            session.Load(journey, journeyId);
            return journey;
            
        }

        #endregion
    }

    public interface IJourneyRepository
    {
        void Save(Journey journey);
        Journey Load(int journeyId);
    }
}