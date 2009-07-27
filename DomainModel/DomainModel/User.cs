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
        public virtual  string Firstname { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Password { get; set; }

        

       
        public User(Email email, UserName name)
        {
            this.Email = email;
            this.Name = name;
        }

        public virtual  Email Email { get; set; }
        public virtual  UserName Name { get; set; }

    }
}
