using System;

namespace DomainModel
{
    public class Location
    {
        internal  Location()
        {
        }

        public Location(string place, TravelDate travelDate)
        {
            this.Place = place;
            this.Date = travelDate;
        }

        public virtual string Place { get; set; }
        public virtual TravelDate Date { get; set; }
    }
}