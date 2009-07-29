using System;
using System.IO;
using NHibernate;

namespace DomainModel
{
    public class UserRepository : Repository,IUserRepository
    {
        private static ISessionFactory sessionFactory;
        static UserRepository ()
        {
            sessionFactory = InitalizeSessionFactory(new FileInfo("UserRegistration/User.hbm.xml"));
        }


        public void SaveUser(User user)
        {
            CreateSession(sessionFactory).Save(user);            
        }
    }

}