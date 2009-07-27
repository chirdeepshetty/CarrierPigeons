using System;

namespace DomainModel
{
    public class Email
    {   
        public Email(string email)
        {
            this.EmailAddress = email;
        }
        public virtual string EmailAddress { get; set; }
    
    }
}