using System;

namespace DomainModel
{
    public class UserName
    {
        public virtual string FirstName{ get; set;}
        public virtual string LastName { get; set; }

        public UserName()
        {
          
        }


        public UserName(string first, string last)
        {
            this.FirstName = first;
            this.LastName = last;
        }
    }
}