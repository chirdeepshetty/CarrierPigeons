using System;
using System.Data;
using System.IO;
using NHibernate;


using System.Linq;


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


        public User LoadUser(string username)
        {
            var session = sessionFactory.OpenSession();
            
            IQuery query = session.CreateQuery("select U from User as U where U.Email.EmailAddress = '" + username + "'");
            var user = query.List<User>().FirstOrDefault();
            return user;
        }

    }
}