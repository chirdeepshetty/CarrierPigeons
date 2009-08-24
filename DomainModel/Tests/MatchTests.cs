using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DomainModel.Tests
{
    [TestFixture]
    public class MatchTests :TestBase
    {
        [Test]
        public void TestMatchConstructor()
        {
            User user = new User(new Email("abc@def.com"), null, "password", null);
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
            UserGroup group = new UserGroup { Id = 1, Name = "Pune" };
            User traveller = new User(new Email("abc1@def.com"), null, "password", group);
            User requestor = new User(new Email("abc2@def.com"), null, "password", group);
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(traveller, origin, destination);
            Request request = new Request(requestor, null, origin, destination);
            Match match = new Match(journey, request);
            UserRepository.Instance.SaveUser(traveller);
            UserRepository.Instance.SaveUser(requestor);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            IMatchRepository repository = MatchRepository.Instance;
            var loadedMatch = MatchRepository.Instance.LoadMatchesByUserRequest(request.RequestedUser.EmailAddress)[0];
            try
            {
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
            UserGroup group=new UserGroup{Id = 1,Name = "Pune"};
            User traveller = new User(new Email("abcdef1@tws.com"), null, "password", group);
            User requestor = new User(new Email("abcdef2@tws.com"), null, "password", group);
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(traveller, origin, destination);
            Request request = new Request(requestor, package, origin, destination);
            UserRepository.Instance.SaveUser(traveller);
            UserRepository.Instance.SaveUser(requestor);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            Match match = new Match(journey, request);
            IMatchRepository repository = MatchRepository.Instance;
            IList<Match> matchList = repository.LoadMatchesByUserRequest("abcdef2@tws.com");
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
                UserGroup group = new UserGroup { Id = 1, Name = "Pune" };
            
                User traveller = new User(new Email("eml@twks.com"), null, "password", group);
                User requestor = new User(new Email("em2@twks.com"), null, "password", group);
                Package package = new Package("Package", "Weight", "Dimensions");
                Location origin = new Location("Origin", new TravelDate(DateTime.Today));
                Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
                Journey journey = new Journey(traveller, origin, destination);
                Request request = new Request(requestor, package, origin, destination);
                UserRepository.Instance.SaveUser(traveller);                                   
                UserRepository.Instance.SaveUser(requestor);
                RequestRepository.Instance.Save(request);                       
                JourneyRepository.Instance.Save(journey);
                            
                Match match = new Match(journey, request);
                IMatchRepository repository = MatchRepository.Instance;
                IList<Match> matchList = repository.LoadPotentialMatchesByUserJourney("eml@twks.com");
                try
                {
                Assert.AreEqual(1, matchList.Count);
            }
            finally 
            {

                repository.Delete(match); 
                UserRepository.Instance.Delete(traveller);
            }
            
                      
            
        }

        [Test]
        public void ShouldUpdateMatchStatusAndAcceptingUserInRequest()
        {
            UserGroup group = new UserGroup { Id = 1, Name = "Pune" };
            User user1 = new User(new Email("abcdef1@tws.com"), null, "password", group);
            User user2 = new User(new Email("abcdef2@tws.com"), null, "password", group);
            Package package = new Package("Package", "Weight", "Dimensions");
            Location origin = new Location("Origin", new TravelDate(DateTime.Today));
            Location destination = new Location("Destination", new TravelDate(DateTime.Today.AddDays(1)));
            Journey journey = new Journey(user1, origin, destination);
            Request request = new Request(user2, package, origin, destination);
            UserRepository.Instance.SaveUser(user1);
            UserRepository.Instance.SaveUser(user2);
            RequestRepository.Instance.Save(request);
            JourneyRepository.Instance.Save(journey);
            Match match = new Match(journey, request);
            IMatchRepository matchRepository = MatchRepository.Instance;
            IList<Match> matchList = matchRepository.LoadMatchesByUserRequest("abcdef2@tws.com");
            foreach (Match myMatch in matchList)
            {
                myMatch.Status = MatchStatus.Accepted;
                myMatch.Request.AcceptingUser = user1;
            }

            matchRepository.UpdateMatches(matchList);
            
            IList<Match> updatedMatchList = matchRepository.LoadMatchesByUserRequest("abcdef2@tws.com");

            try
            {
                Assert.AreEqual(1, matchList.Count);
                foreach (Match myMatch in updatedMatchList)
                {
                    Assert.AreEqual(MatchStatus.Accepted,myMatch.Status);
                    Assert.AreEqual(myMatch.Request.AcceptingUser,user1);
                }
            }
            finally
            {

                matchRepository.Delete(match);
                UserRepository.Instance.Delete(user1);
                UserRepository.Instance.Delete(user2);
            }
        }
    }
}
