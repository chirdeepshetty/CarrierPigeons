using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Tests;

namespace DomainModel
{
    public class User
    {
        public User(Email email, UserName name)
        {
            throw new NotImplementedException();
        }

        public virtual  int Id { get; set; }
        public virtual  string Email { get; set; }
        public virtual  string Name { get; set; }
    }
}
