using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DomainModel
{
    public class RequestRepository : RepositoryBase, IRequestRepository
    {
        public event RequestCreatedEventHandler RequestCreated;
        
       

        private RequestRepository()
        {
        }
        static RequestRepository _requestRepository = new RequestRepository();
        
        public static IRequestRepository Instance
        {
            get
            {
                _requestRepository.SetSession();
                return _requestRepository;
            }
        }

        

        public void Save(Request request)
        {
            Session.Save(request);
            if(this.RequestCreated != null)
                RequestCreated(new RequestCreatedEventArgs{Request = request});
        }

        public List<Request> Search(Location location, Location toLocation, TravelDate date)
        {
            string querystring =
                "from Request as R where R.Destination.Place = :destination and R.Origin.Place= :origin and R.Destination.Date.DateTime <= :date";
            IQuery query = Session.CreateQuery(querystring);
            query.SetString("destination", toLocation.Place);
            query.SetString("origin", location.Place);
            query.SetDateTime("date", date.DateTime);
            var requestList = (List<Request>) query.List<Request>();
            return requestList;
        }

        public IEnumerable<Request> SearchByUser(string address)
        {
            string findByUser = 
                "from Request as R where R.RequestedUser.Email.EmailAddress = :email";
            IQuery query = Session.CreateQuery(findByUser);
            query.SetString("email", address);
            return query.List<Request>();
        }

        public IEnumerable<Request> Find(Journey journey)
        {
            string findByJourney =
                @"select R from Request as R, Journey J 
                where J.Traveller = :journeyUser 
                and J.Traveller <> R.RequestedUser                 
                and J.Traveller.UserGroup = R.RequestedUser.UserGroup               
                and R.Origin.Place = :origin                 
                and R.Destination.Place = :destination
                and R.Destination.Date.DateTime <= :arrivalDate";
            IQuery query = Session.CreateQuery(findByJourney);
            query.SetParameter("journeyUser", journey.Traveller);
            query.SetString("origin", journey.Origin.Place);
            query.SetString("destination", journey.Destination.Place);
            query.SetDateTime("arrivalDate", journey.Destination.Date.DateTime);
            return query.List<Request>();
        }

        public void Delete(Request request)
        {
            Session.Delete(request);
        }
    }

    public delegate void RequestCreatedEventHandler(RequestCreatedEventArgs e);

    public class RequestCreatedEventArgs : EventArgs
    {
        public Request Request { get; set; }
    }
}