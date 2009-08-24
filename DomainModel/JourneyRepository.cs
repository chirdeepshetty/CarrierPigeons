using System;
using System.Collections.Generic;
using NHibernate;

namespace DomainModel
{
    public class JourneyRepository : RepositoryBase, IJourneyRepository
    {
        public event JourneyCreatedEventHandler JourneyCreated;

        private JourneyRepository()
        {
        }

        static JourneyRepository _journeyRepository = new JourneyRepository();

        public static IJourneyRepository Instance
        {
            get
            {
                _journeyRepository.SetSession();
                return _journeyRepository;
            }
        }

        #region IJourneyRepository Members

        public void Save(Journey journey)
        {
            
            Session.Save(journey);
            if(JourneyCreated != null)
                JourneyCreated(new JourneyCreatedEventArgs{Journey = journey});
        }

        public Journey Load(int journeyId)
        {
            Journey journey = new Journey();
            Session.Load(journey, journeyId);
            return journey;
        }

        public void Delete(Journey journey)
        {
            Session.Delete(journey);
            Session.Flush();
        }

        public IList<Journey> FindJourneysByUser(User user)
        {
            const string querystring = "from Journey as J  where J.Traveller = :traveller";
            IQuery query = Session.CreateQuery(querystring);
            query.SetEntity("traveller", user);
            var journeyList = (List<Journey>)query.List<Journey>();
            return journeyList;

        }

        public IEnumerable<Journey> Find(Request request)
        {
            const string findByRequest = @"select J from Journey J, Request R 
                                            where J.Traveller <> R.RequestedUser                                            
                                            and R.RequestedUser = :requestedUser
                                            and J.Traveller.UserGroup = R.RequestedUser.UserGroup 
                                            and J.Origin.Place = :origin 
                                            and J.Destination.Place = :destination 
                                            and J.Destination.Date.DateTime <= :arrivalDate ";

            IQuery query = Session.CreateQuery(findByRequest);
            query.SetString("origin", request.Origin.Place);
            query.SetString("destination", request.Destination.Place);
            query.SetDateTime("arrivalDate", request.Destination.Date.DateTime);
            query.SetParameter("requestedUser",request.RequestedUser);
            return query.List<Journey>();
        }

        #endregion
    }

    public interface IJourneyRepository
    {
        void Save(Journey journey);
        Journey Load(int journeyId);
        event JourneyCreatedEventHandler JourneyCreated;
        IList<Journey> FindJourneysByUser(User user);
        IEnumerable<Journey> Find(Request request);
        void Delete(Journey journey);
    }

    public delegate void JourneyCreatedEventHandler(JourneyCreatedEventArgs e);

    public class JourneyCreatedEventArgs : EventArgs
    {
        public Journey Journey { get; set; }
    }
}