namespace DomainModel
{
    public class Package
    {
        public Package(string description, string weight, string dimensions)
        {
            this.Description = description;
            this.Weight = weight;
            this.Dimensions = dimensions;
        }

        public virtual string Description { get; set; }
        public virtual string Weight{ get; set; }
        public virtual string Dimensions { get; set; }
    }
}