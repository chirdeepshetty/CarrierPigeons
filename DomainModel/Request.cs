using System;

namespace DomainModel
{
    public class Request
    {
        public Request()
        {
        }

        public Request(User user, Package package, Location origin, Location destination)
        {
            this.Destination = destination;
            this.RequestedUser = user;
            this.Origin = origin;
            this.Package = package;
            
        }

        public virtual string Id { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual Package Package { get; set; }
        public virtual Location Origin { get; set; }
        public virtual Location Destination { get; set; }

        public virtual bool Equals(Request other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.RequestedUser.Equals(RequestedUser) && other.Package.Equals(Package) &&
                   other.Origin.Equals(Origin) && other.Destination.Equals(Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Request)) return false;
            return Equals((Request) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (RequestedUser != null ? RequestedUser.GetHashCode() : 0);
                result = (result*397) ^ (Package != null ? Package.GetHashCode() : 0);
                result = (result*397) ^ (Origin != null ? Origin.GetHashCode() : 0);
                result = (result*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
                return result;
            }
        }
    }
}