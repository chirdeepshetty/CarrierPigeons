using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace DomainModel
{
    public class UserGroupRepository : RepositoryBase
    {

        private UserGroupRepository()
        {
        }
        static readonly UserGroupRepository _userGroupRepository = new UserGroupRepository();

        public static UserGroupRepository Instance
        {
            get
            {
                _userGroupRepository.SetSession();
                return _userGroupRepository;
            }
        }

        public IList LoadGroups()
        {
            IList list = Session.CreateCriteria(typeof(UserGroup)).List();
            return list;
        }

        public UserGroup LoadGroupById(int id)
        {
            UserGroup group = (UserGroup) Session.CreateCriteria(typeof (UserGroup)).Add(Expression.Eq("Id", id)).UniqueResult();
            return group;
        }
    }
}