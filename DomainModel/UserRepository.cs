using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;

using System.Linq;

namespace DomainModel
{
    public class UserRepository : Repository,IUserRepository
    {
        private static ISessionFactory sessionFactory;
        static UserRepository ()
        {
            sessionFactory = InitalizeSessionFactory(new FileInfo("UserRegistration/User.hbm.xml"));
        }

        public User LoadUser(string username)
        {
            var session = sessionFactory.OpenSession();
            IQuery query = session.CreateQuery("from User");
            User user = query.List<User>().First();

            
            return null;
        }

        public void SaveUser(User user)
        {
            CreateSession(sessionFactory).Save(user);            
        }
    }

}