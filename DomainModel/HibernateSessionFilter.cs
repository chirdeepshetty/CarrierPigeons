using System;
using System.Threading;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;

namespace DomainModel
{
    public class HibernateSessionFilter : ActionFilterAttribute
    {
        public const string HIBERNATE_SESSION_DATA_SLOT = "hibernateSession";
        private static readonly ISessionFactory _sessionFactory;
        private static readonly Configuration _configuration;

        static HibernateSessionFilter()
        {
            _configuration = new Configuration();
            _configuration.AddAssembly(typeof(HibernateSessionFilter).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = OpenHibernateSession();
            SetHibernateSessionInThreadLocal(session);
        }

        internal void SetHibernateSessionInThreadLocal(ISession session)
        {
            if(Thread.GetNamedDataSlot(HIBERNATE_SESSION_DATA_SLOT)==null)
                Thread.AllocateNamedDataSlot(HIBERNATE_SESSION_DATA_SLOT);

            LocalDataStoreSlot hibernateSessionDataStoreSlot = Thread.GetNamedDataSlot(HIBERNATE_SESSION_DATA_SLOT);
            Thread.SetData(hibernateSessionDataStoreSlot,session);
        }

        private ISession OpenHibernateSession()
        {
            
            return _sessionFactory.OpenSession();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LocalDataStoreSlot hibernateSessionDataSlot = Thread.GetNamedDataSlot(HIBERNATE_SESSION_DATA_SLOT);
            ISession session = (ISession) Thread.GetData(hibernateSessionDataSlot);
            session.Close();
            Thread.FreeNamedDataSlot(HIBERNATE_SESSION_DATA_SLOT);
        }
    }
}
