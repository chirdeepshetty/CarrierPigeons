using System;

namespace DomainModel
{
    public class Journey
    {
        public Journey(User traveller, Location origin, Location destination)
        {
            this.Traveller = traveller;
            this.Destination = destination;
            this.Origin = origin;
        }

        public virtual Location Origin { get; set; }
        public virtual Location Destination { get; set; }
        public virtual User Traveller { get; set; }
    }
}