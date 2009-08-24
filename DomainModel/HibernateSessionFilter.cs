using System;
using System.Threading;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;

namespace DomainModel
{
    public class HibernateSessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = OpenHibernateSession();
            SetHibernateSessionInThreadLocal(session);
        }

        private void SetHibernateSessionInThreadLocal(ISession session)
        {
            Thread.AllocateNamedDataSlot("hibernateSession");
            LocalDataStoreSlot hibernateSessionDataStoreSlot = Thread.GetNamedDataSlot("hibernateSession");
            Thread.SetData(hibernateSessionDataStoreSlot,session);
        }

        private ISession OpenHibernateSession()
        {
            Configuration configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LocalDataStoreSlot hibernateSessionDataSlot = Thread.GetNamedDataSlot("hibernateSession");
            ISession session = (ISession) Thread.GetData(hibernateSessionDataSlot);
            session.Close();
            Thread.FreeNamedDataSlot("hibernateSession");
        }
    }
}
