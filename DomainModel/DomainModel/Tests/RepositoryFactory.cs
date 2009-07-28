using System;

namespace DomainModel.Tests
{
    public class RepositoryFactory
    {
        public Repository GetUserRepository()
        {
            return new UserRepository();
        }
    }
}