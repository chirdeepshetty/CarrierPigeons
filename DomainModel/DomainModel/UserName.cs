using System;

namespace DomainModel
{
    public class UserName
    {
        public virtual string FirstName{ get; set;}
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        

        public UserName(string first, string middle, string last)
        {
            this.FirstName = first;
            this.MiddleName = middle;
            this.LastName = last;
        }
    }
}