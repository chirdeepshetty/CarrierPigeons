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
    public class MatchRepository : IMatchRepository
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;

        private MatchRepository()
        {
            if(sessionFactory!=null)
                return;
            configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            sessionFactory = configuration.BuildSessionFactory();
        }
        static MatchRepository _matchRepository = new MatchRepository();

        public static IMatchRepository Instance
        {
            get
            {
                return _matchRepository;
            }
        }

        #region IMatchRepository Members

        public void Save(Match match)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;

            session.Save(match);
            
            session.Close();
        }

        public Match Load(int matchId)
        {
            var session = sessionFactory.OpenSession();
            Match match = new Match();
            session.Load(match, matchId);
            return match;
            
        }

        #endregion
    }

    public interface IMatchRepository
    {
        void Save(Match match);
        Match Load(int matchId);
    }
}