using NHibernate.Type;

namespace DomainModel
{
    public class Match
    {
        public Match()
        {
        }

        public Match(Journey journey, Request request)
        {
            this.Journey = journey;
            this.Request = request;
        }

        public virtual int Id { get; set; }

        public virtual Journey Journey
        {
            get; set;
        }

        public virtual Request Request
        {
            get;
            set;
        }

        public virtual MatchStatus Status
        {
            get; 
            set;
        }

    }

    public enum MatchStatus
    {
        Created,
        Requested,
        Accepted,
        Rejected,
        Cancelled
    }
}