using System;
using System.Data;
using System.IO;
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

        #endregion
    }
}