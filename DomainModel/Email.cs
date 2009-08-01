using System;

namespace DomainModel
{
    public class Email
    {


        public Email()
        {
            
        }
        
        public Email(string email)
        {
            this.EmailAddress = email;
        }
        public virtual string EmailAddress { get; set; }

        public bool Equals(Email other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.EmailAddress, EmailAddress);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Email)) return false;
            return Equals((Email) obj);
        }

        public override int GetHashCode()
        {
            return (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
        }
    }
}