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

        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Place.Equals(Place) && other.Date.Equals(Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Location)) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Place != null ? Place.GetHashCode() : 0)*397) ^ (Date != null ? Date.GetHashCode() : 0);
            }
        }
    }
}