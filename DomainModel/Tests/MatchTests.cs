using System;
using System.Collections.Generic;
using System.Threading;
using NHibernate;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class MatchTests : TestBase
    {
        [Test]
        public void MatchRepositoryLoadMatchesListByUserJourney()
        {
            
            var user = new User(new Email("abc@def.com"), null, "password", null);
            var package = new Package("Package", "Weight", "Dimensions");
            var origin = new Location("Origin", new TravelDate(DateTime.Today));
            var destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            var journey = new Journey(user, origin, destination);
            var request = new Request(user, null, origin, destination);
            var match = new Match(journey, request);
            UserRepository.Instance.SaveUser(user);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            IMatchRepository repository = MatchRepository.Instance;
            IList<Match> matchList = repository.LoadMatchesByUserJourney("abc@def.com");
            try
            {
                Assert.AreEqual(1, matchList.Count);
            }
            finally
            {
                repository.Delete(match);
                UserRepository.Instance.Delete(user);
            }
        }

        [Test]
        public void MatchRepositoryLoadMatchesListByUserRequest()
       { 
            var user = new User(new Email("abcdef1@tws.com"), null, "password", null);
            var package = new Package("Package", "Weight", "Dimensions");
            var origin = new Location("Origin", new TravelDate(DateTime.Today));
            var destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            var journey = new Journey(user, origin, destination);
            var request = new Request(user, package, origin, destination);
            UserRepository.Instance.SaveUser(user);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            var match = new Match(journey, request);
            IMatchRepository repository = MatchRepository.Instance;
            IList<Match> matchList = repository.LoadMatchesByUserRequest("abcdef1@tws.com");
            try
            {
                Assert.AreEqual(1, matchList.Count);
            }
            finally
            {
                repository.Delete(match);
            }
        }

        [Test]
        public void ShouldUpdateMatchStatus()
        {
            var user = new User(new Email("abcdef1@tws.com"), null, "password", null);
            var package = new Package("Package", "Weight", "Dimensions");
            var origin = new Location("Origin", new TravelDate(DateTime.Today));
            var destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            var journey = new Journey(user, origin, destination);
            var request = new Request(user, package, origin, destination);
            UserRepository.Instance.SaveUser(user);
            JourneyRepository.Instance.Save(journey);
            RequestRepository.Instance.Save(request);
            var match = new Match(journey, request);

            IMatchRepository matchRepository = MatchRepository.Instance;

            IList<Match> matchList = matchRepository.LoadMatchesByUserRequest("abcdef1@tws.com");

            foreach (Match myMatch in matchList)
            {
                myMatch.Status = MatchStatus.Accepted;
            }

            matchRepository.UpdateMatches(matchList);

            IList<Match> updatedMatchList = matchRepository.LoadMatchesByUserRequest("abcdef1@tws.com");

            try
            {
                Assert.AreEqual(1, matchList.Count);
                foreach (Match myMatch in updatedMatchList)
                {
                    Assert.AreEqual(MatchStatus.Accepted, myMatch.Status);
                }
            }
            finally
            {
                matchRepository.Delete(match);
                UserRepository.Instance.Delete(user);
            }
        }

        [Test]
        public void TestMatchConstructor()
        {
            var user = new User(new Email("abc@def.com"), null, "password", null);
            var package = new Package("Package", "Weight", "Dimensions");
            var origin = new Location("Origin", new TravelDate(DateTime.Today));
            var destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            var journey = new Journey(user, origin, destination);
            var request = new Request(user, null, origin, destination);
            var match = new Match(journey, request);
            Assert.AreEqual(origin.Place, match.Journey.Origin.Place);
            Assert.AreEqual(origin.Place, match.Request.Origin.Place);
            Assert.AreEqual(destination.Place, match.Journey.Destination.Place);
            Assert.AreEqual(destination.Place, match.Request.Destination.Place);
        }

        [Test]
        public void TestMatchRepositorySave()
        {
            var user = new User(new Email("abc@def.com"), null, "password", null);
            var origin = new Location("Origin", new TravelDate(DateTime.Today));
            var destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            var journey = new Journey(user, origin, destination);
            var request = new Request(user, null, origin, destination);
            var match = new Match(journey, request);
            UserRepository.Instance.SaveUser(user);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            IMatchRepository repository = MatchRepository.Instance;
            Match loadedMatch = repository.LoadMatchesByUserJourney(journey.Traveller.EmailAddress)[0];
            try
            {
                Assert.AreEqual(request.Destination.Place, loadedMatch.Request.Destination.Place);
            }
            finally
            {
                repository.Delete(match);
                JourneyRepository.Instance.Delete(journey);
                RequestRepository.Instance.Delete(request);
                UserRepository.Instance.Delete(user);
            }
        }
    }
}