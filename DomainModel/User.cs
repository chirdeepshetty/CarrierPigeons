using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Tests;
using Iesi.Collections.Generic;

namespace DomainModel
{
    public class User
    {
        public virtual  int Id { get; set; }
        public virtual string Password { get; set; }
        public virtual UserGroup UserGroup { get; set; }

        public User ()
        {
            
        }

        public User(Email email, UserName name, string password, UserGroup userGroup)
        {
            this.Email = email;
            this.Name = name;
            this.Password = password;
            UserGroup = userGroup;
        }

        public virtual  Email Email { get; set; }
        public virtual  UserName Name { get; set; }
        public virtual string EmailAddress { get { return Email.EmailAddress; } }
        public virtual ISet<Journey> UserJournies { get; set; }
        public virtual ISet<Request> UserRequests { get; set; }

        public virtual bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Email.Equals(Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (User)) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }

        
    }
}
