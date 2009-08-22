using System;
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

        public virtual void Accept(User user)
        {
            Status = MatchStatus.Accepted;
            Request.AcceptingUser = user;
        }

        public virtual bool IsAccepted()
        {
            return Status==MatchStatus.Accepted;
        }

    }

    public enum MatchStatus
    {
        Potential,
        Accepted
    }
}