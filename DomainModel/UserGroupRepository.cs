using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Cfg;

namespace DomainModel
{
    public class UserGroupRepository
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;

        private UserGroupRepository()
        {
            if(sessionFactory!=null)
                return;
            configuration = new Configuration();
            configuration.AddAssembly(this.GetType().Assembly);
            sessionFactory = configuration.BuildSessionFactory();
        }
        static readonly UserGroupRepository _userGroupRepository = new UserGroupRepository();

        public static UserGroupRepository Instance
        {
            get
            {
                return _userGroupRepository;
            }
        }

        public IList LoadGroups()
        {
            ISession session = sessionFactory.OpenSession();
            IList list = session.CreateCriteria(typeof(UserGroup)).List();
            return list;
        }
    }
}