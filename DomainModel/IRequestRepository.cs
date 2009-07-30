using System.Collections.Generic;

namespace DomainModel
{
    public interface IRequestRepository
    {
        void Save(Request request);
        List<Request> Search(Location location, Location toLocation, TravelDate date);
        event RequestCreatedEventHandler RequestCreated;
    }
}