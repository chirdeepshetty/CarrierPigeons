using System;

namespace DomainModel
{
    public class RepositoryFactory
    {
        public static IUserRepository GetUserRepository()
        {
            return new UserRepository();
        }
    }
}