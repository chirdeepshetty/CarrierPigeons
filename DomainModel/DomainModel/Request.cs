using System;

namespace DomainModel
{
    public class Request
    {
        public virtual  int Id { get; set; }
        public virtual  string Origin { get; set; }
        public virtual  string Destination { get; set; }
        public virtual  DateTime Date { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual Package Package { get; set; }
    }
}