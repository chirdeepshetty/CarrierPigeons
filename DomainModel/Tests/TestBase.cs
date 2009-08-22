using System;
using System.Threading;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace DomainModel.Tests
{
    public class TestBase
    {
        protected static ISessionFactory SessionFactory;
        private ISession _session;
        private ITransaction _transaction;
        private static int _init=0;

        [SetUp]
        public void TestSetup()
        {
            Thread.AllocateNamedDataSlot("hibernateSession");
            LocalDataStoreSlot hibernateSessionDataStoreSlot = Thread.GetNamedDataSlot("hibernateSession");
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
            LocalDataStoreSlot hibernateSessionDataSlot = Thread.GetNamedDataSlot("hibernateSession");
            ISession session = (ISession)Thread.GetData(hibernateSessionDataSlot);
            _transaction.Rollback();
            Thread.FreeNamedDataSlot("hibernateSession");
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