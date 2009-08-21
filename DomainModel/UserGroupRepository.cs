using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

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
            session.Close();
            return list;
        }

        public UserGroup LoadGroupById(int id)
        {
            ISession session = sessionFactory.OpenSession();
            UserGroup group = (UserGroup) session.CreateCriteria(typeof (UserGroup)).Add(Expression.Eq("Id", id)).UniqueResult();
            session.Close();
            return group;
        }
    }
}