using System;
using System.Data;
using System.IO;
using NHibernate;

namespace DomainModel
{
    public class UserRepository : Repository,IUserRepository
    {
        
        public void SaveUser(User user)
        {
            var session = sessionFactory.OpenSession();
            IDbConnection connection = session.Connection;

            session.Save(user);

            session.Close();       
        }
    }
}