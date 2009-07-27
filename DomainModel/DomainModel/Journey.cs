using System;

namespace DomainModel
{
    public class Journey
    {
        public virtual int Id { get; set; }
        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        public virtual DateTime DepartureDate { get; set; }
        public virtual DateTime ArrivalDate { get; set; }
        public virtual User Traveller { get; set; }
    }
}