using NHibernate;
using System.Linq;


namespace DomainModel
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private UserRepository()
        {
        }

        static UserRepository _userRepository = new UserRepository();
         
        public static IUserRepository Instance
        {
            get
            {
                _userRepository.SetSession();
                return _userRepository;
            }
        }

        public void SaveUser(User user)
        {
            Session.Save(user);
        }

        public void Delete(User user)
        {
            Session.Delete(user);
        }


        public User LoadUser(string username)
        {
            IQuery query = Session.CreateQuery("select U from User as U where U.Email.EmailAddress = '" + username + "'");
            var user = query.List<User>().FirstOrDefault();
            return user;
        }
    }
}