﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class User
    {
        public virtual  int Id { get; set; }
        public virtual  string Email { get; set; }
        public virtual  string Firstname { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Password { get; set; }

        public bool ChangePassword(string password, string s)
        {
            //ToDo
            return true;
        }
    }
}
