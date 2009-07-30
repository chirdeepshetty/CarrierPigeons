namespace DomainModel
{
    public class Package
    {
        internal Package()
        {
        }

        public Package(string description, string weight, string dimensions)
        {
            this.Description = description;
            this.Weight = weight;
            this.Dimensions = dimensions;
        }

        public virtual string Description { get; set; }
        public virtual string Weight{ get; set; }
        public virtual string Dimensions { get; set; }

        public virtual bool Equals(Package other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Description, Description) && Equals(other.Weight, Weight) && Equals(other.Dimensions, Dimensions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Package)) return false;
            return Equals((Package) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Description != null ? Description.GetHashCode() : 0);
                result = (result*397) ^ (Weight != null ? Weight.GetHashCode() : 0);
                result = (result*397) ^ (Dimensions != null ? Dimensions.GetHashCode() : 0);
                return result;
            }
        }
    }
}