using System;
using System.Threading;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using HSF = DomainModel.HibernateSessionFilter;
namespace DomainModel.Tests
{
    public class TestBase
    {
        protected static ISessionFactory SessionFactory;
        private ISession _session;
        private ITransaction _transaction;
        private static int _init;

        [SetUp]
        public void TestSetup()
        {
            if (Thread.GetNamedDataSlot(HSF.HIBERNATE_SESSION_DATA_SLOT) == null)
                Thread.AllocateNamedDataSlot(HSF.HIBERNATE_SESSION_DATA_SLOT);

            LocalDataStoreSlot hibernateSessionDataStoreSlot = Thread.GetNamedDataSlot(HSF.HIBERNATE_SESSION_DATA_SLOT);
            Thread.SetData(hibernateSessionDataStoreSlot, GetSession());
            _transaction=_session.BeginTransaction();
            if(_init==0)
            {
                new JourneyRequestMatcher(RequestRepository.Instance, JourneyRepository.Instance);
                _init ++;
            }
        }

        [TearDown]
        public void TearDown()
        {
            LocalDataStoreSlot hibernateSessionDataSlot = Thread.GetNamedDataSlot(HSF.HIBERNATE_SESSION_DATA_SLOT);
            ISession session = (ISession)Thread.GetData(hibernateSessionDataSlot);
            _transaction.Rollback();
            Thread.FreeNamedDataSlot(HSF.HIBERNATE_SESSION_DATA_SLOT);
        }


        protected ISession GetSession()
        {
            Configuration configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            SessionFactory = configuration.BuildSessionFactory();
            _session = SessionFactory.OpenSession();
            return _session;
        }
    }
}