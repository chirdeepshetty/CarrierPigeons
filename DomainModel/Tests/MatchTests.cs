using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class MatchTests
    {
        [Test]
        public void TestMatchConstructor()
        {
            User user = new User(new Email("abc@def.com"), null, "password");
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(user, origin, destination);
            Request request = new Request(user, null, origin, destination);
            Match match = new Match(journey, request);
            Assert.AreEqual(origin.Place, match.Journey.Origin.Place);
            Assert.AreEqual(origin.Place, match.Request.Origin.Place);
            Assert.AreEqual(destination.Place, match.Journey.Destination.Place);
            Assert.AreEqual(destination.Place, match.Request.Destination.Place);

        }

        [Test]
        public void TestMatchRepositorySave()
        {
            User user = new User(new Email("abc@def.com"), null, "password");
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(user, origin, destination);
            Request request = new Request(user, null, origin, destination);
            Match match = new Match(journey, request);
            RepositoryFactory.GetUserRepository().SaveUser(user);
            RequestRepository.Instance.Save(request);
            IMatchRepository repository = MatchRepository.Instance;
            repository.Save(match);
            var loadedMatch = repository.Load(match.Id);
            try
            {
                Assert.AreEqual(match.Id, loadedMatch.Id);
                Assert.AreEqual(request.Destination.Place, loadedMatch.Request.Destination.Place);
        
            }
            finally
            {
                repository.Delete(match);
            }
        }

        [Test]
        public void MatchRepositoryLoadMatchesListByUserRequest()
        {

            User user = new User(new Email("abcdef1@tws.com"), null, "password");
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(user, origin, destination);
            Request request = new Request(user, package, origin, destination);
            RepositoryFactory.GetUserRepository().SaveUser(user);
            RequestRepository.Instance.Save(request);
            Match match = new Match(journey, request);
            IMatchRepository repository = MatchRepository.Instance;
            repository.Save(match);
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
        public void MatchRepositoryLoadMatchesListByUserJourney()
        {

            
                User user = new User(new Email("eml@twks.com"), null, "password");
                Package package = new Package("Package", "Weight", "Dimensions");
                Location origin = new Location("Origin", new TravelDate(DateTime.Today));
                Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
                Journey journey = new Journey(user, origin, destination);
                Request request = new Request(user, package, origin, destination);
                Match match = new Match(journey, request);
                IMatchRepository repository = MatchRepository.Instance;

                repository.Save(match);
                IList<Match> matchList = repository.LoadMatchesByUserJourney("eml@twks.com");
                try
                {
                Assert.AreEqual(1, matchList.Count);
            }
            finally 
            {

                repository.Delete(match); 
            }
            
                      
            
        }


    }
}
