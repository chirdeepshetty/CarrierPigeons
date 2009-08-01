using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class JourneyRequestMatcher
    {
        public JourneyRequestMatcher(IRequestRepository requestRepository, IJourneyRepository journeyRepository)
        {
            this._requestRepository = requestRepository;
            this._journeyRepository = journeyRepository;

            _requestRepository.RequestCreated += new RequestCreatedEventHandler(_requestRepository_RequestCreated);
            _journeyRepository.JourneyCreated += new JourneyCreatedEventHandler(_journeyRepository_JourneyCreated);
        }

        public IEventPublisher EventPublisher { get; set; }

        void _journeyRepository_JourneyCreated(JourneyCreatedEventArgs e)
        {
            PublishEvent(e);
            Journey j = e.Journey;

            IEnumerable<Request> requests = RequestRepository.Instance.Find(j).AsEnumerable();
            foreach (Request request in requests)
            {
                Match match = new Match(j, request);
                MatchRepository.Instance.Save(match);
            }
        }

        private void PublishEvent(JourneyCreatedEventArgs e)
        {
            if (this.EventPublisher != null)
                this.EventPublisher.AddEvent(e);
        }

        void _requestRepository_RequestCreated(RequestCreatedEventArgs e)
        {
            Request r = e.Request;
            IEnumerable<Journey> journeys = JourneyRepository.Instance.Find(r).AsEnumerable();
            foreach (Journey journey in journeys)
            {
                Match match = new Match(journey, r);
                MatchRepository.Instance.Save(match);
            }
        }

        IRequestRepository _requestRepository { get; set; }
        IJourneyRepository _journeyRepository { get; set; }
    }

    public interface IEventPublisher
    {
        void AddEvent(EventArgs e);
        IEnumerable<string> Events { get; }
    }
}


