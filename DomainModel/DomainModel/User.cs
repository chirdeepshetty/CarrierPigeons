﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class User
    {
        public virtual  int Id { get; set; }
        public virtual  string Email { get; set; }
        public virtual  string Name { get; set; }
    }
}
