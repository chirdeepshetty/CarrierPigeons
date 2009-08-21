using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class UserGroup
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual bool Equals(UserGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (UserGroup)) return false;
            return Equals((UserGroup) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
