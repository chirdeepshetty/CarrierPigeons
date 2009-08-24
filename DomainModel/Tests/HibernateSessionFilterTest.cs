using System;
using System.Threading;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class HibernateSessionFilterTest
    {
        [Test]
        public void shouldSetHibernateSessionInThreadLocal()
        {
            var mockSession = new Mock<ISession>();
            HibernateSessionFilter hibernateSessionFilter=new HibernateSessionFilter();
            hibernateSessionFilter.SetHibernateSessionInThreadLocal(mockSession.Object);

            LocalDataStoreSlot hibernateSessionDataSlot = Thread.GetNamedDataSlot(HibernateSessionFilter.HIBERNATE_SESSION_DATA_SLOT);
            ISession session = (ISession)Thread.GetData(hibernateSessionDataSlot);
            Assert.AreEqual(mockSession.Object,session);
        }
    }
}