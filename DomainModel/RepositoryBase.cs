using System.Threading;
using NHibernate;

namespace DomainModel
{
    public class RepositoryBase
    {
        protected ISession Session;

        public void SetSession()
        {
            Session = (ISession) Thread.GetData(Thread.GetNamedDataSlot("hibernateSession"));
        }
    }
}