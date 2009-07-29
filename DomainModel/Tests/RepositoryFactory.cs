using System;

namespace DomainModel.Tests
{
    public class RepositoryFactory
    {
        public static IUserRepository GetUserRepository()
        {
            return new UserRepository();
        }
    }
}