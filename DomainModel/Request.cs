using System;

namespace DomainModel
{
    public class Request
    {
        internal Request()
        {
        }

        public Request(User user, Package package, Location origin, Location destination)
        {
            this.Destination = destination;
            this.RequestedUser = user;
            this.Origin = origin;
            
        }

        public virtual string Id { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual Package Package { get; set; }
        public virtual Location Origin { get; set; }
        public virtual Location Destination { get; set; }
    }
}