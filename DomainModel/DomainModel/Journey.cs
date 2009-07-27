using System;

namespace DomainModel
{
    public class Journey
    {
        public Journey(User traveller, Location location, Location destination)
        {
            this.Traveller = traveller;
            this.Destination = destination;
            this.Origin = Origin;
        }

        public virtual Location Origin { get; set; }
        public virtual Location Destination { get; set; }
        public virtual User Traveller { get; set; }
    }
}