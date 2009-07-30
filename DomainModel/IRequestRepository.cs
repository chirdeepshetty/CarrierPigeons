using System.Collections.Generic;

namespace DomainModel
{
    public interface IRequestRepository
    {
        void Save(Request request);
        List<Request> Search(Location location, Location toLocation, TravelDate date);
        IEnumerable<Request> SearchByUser(string address);
        void Delete(Request request);
    }
}