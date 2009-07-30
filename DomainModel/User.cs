using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Tests;

namespace DomainModel
{
    public class User
    {
        public virtual  int Id { get; set; }
        public virtual string Password { get; set; }

        public User ()
        {
            
        }

        public User(Email email, UserName name, string password)
        {
            this.Email = email;
            this.Name = name;
            this.Password = password;
        }

        public virtual  Email Email { get; set; }
        public virtual  UserName Name { get; set; }
        public virtual string EmailAddress { get { return Email.EmailAddress; } }

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
