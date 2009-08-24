using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture()]
    public class JourneyRequestMatcherTests : TestBase
    {
        [Test]
        public void TestJourneyRequestMatcherConstructor()
        {
            var requestRepository = new Mock<IRequestRepository>();
            var journeyRepository = new Mock<IJourneyRepository>();
            JourneyRequestMatcher matcher = new JourneyRequestMatcher(requestRepository.Object, journeyRepository.Object);
        }

        [Test]
        public void TestIfEventsAreSubscribedTo()
        {
            var requestRepository = new Mock<IRequestRepository>();
            var journeyRepository = new Mock<IJourneyRepository>();
            Request request = new Request();
            Journey journey = new Journey();
            requestRepository.Setup(a => a.Save(request)).Raises(a => a.RequestCreated += null, RequestCreatedEventArgs.Empty);
            journeyRepository.Setup(a => a.Save(journey)).Raises(a => a.JourneyCreated += null, new JourneyCreatedEventArgs());
            JourneyRequestMatcher matcher = new JourneyRequestMatcher(requestRepository.Object, journeyRepository.Object);
            matcher.EventPublisher = new EventPublisher();
            //TODO: Fix later
        }

        [Test]
        public void TestMatchRepositorySave()
        {
            Journey journey = null;
            Request request = null;
            User traveller = null;
           
                Guid locationId = Guid.NewGuid();
                traveller = new User(new Email("asd@dsf.com"), new UserName("first", "last"), "pwd", null);
                UserRepository.Instance.SaveUser(traveller);
                Location origin = new Location(locationId.ToString(), new TravelDate(DateTime.Now));
                Location destination = new Location("TestGetJourneyByRequest Test Destination", new TravelDate(DateTime.Now));
                journey = new Journey(traveller, origin, destination);
                IJourneyRepository journeyRepository = JourneyRepository.Instance;
                journeyRepository.Save(journey);

                request = new Request(traveller, new Package(null, null, null), origin, destination);
                RequestRepository.Instance.Save(request);
                Match match = new Match(journey, request);
                MatchRepository.Instance.Save(match);
                try
                {
                Assert.NotNull(match.Id);
            }
            finally
            {
                MatchRepository.Instance.Delete(match);
            }
        }
    }

    public class EventPublisher : IEventPublisher
    {
        List<string> _EventsReceived = new List<string>();
        
        public void AddEvent(EventArgs e)
        {
            var eventType = e.GetType().ToString();
            _EventsReceived.Add(eventType);
        }

        public IEnumerable<string> Events
        {
            get
            {
                return this._EventsReceived.AsEnumerable();
            }
        }
    }
}
