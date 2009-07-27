namespace DomainModel
{
    public class Package
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual float Weight{ get; set; }
        public virtual Dimension Dimension { get; set; }
    }
}