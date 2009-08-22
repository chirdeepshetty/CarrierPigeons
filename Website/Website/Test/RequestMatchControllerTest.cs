using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DomainModel;
using Moq;
using NUnit.Framework;
using Website.Controllers;
using Match=DomainModel.Match;

namespace Website.Test
{
    [TestFixture]
    public class RequestMatchControllerTest
    {
        [Test]
        public void ShouldReturnListOfMatchedRequestsForUserJourney()
        {
            var matchRepositoryMock = new Mock<IMatchRepository> {};
            var userRepositoryMock = new Mock<IUserRepository> { };
            List<Match> matchedRequests = new List<Match>();
            Match match = new Match(new Journey(),new Request());
            match.Status = MatchStatus.Potential;
            matchedRequests.Add(match);
            matchRepositoryMock.Setup(ps => ps.LoadPotentialMatchesByUserJourney("test@test.com")).Returns(matchedRequests);

            Website.Controllers.RequestMatchController requestMatchController =
                            new Website.Controllers.RequestMatchController(matchRepositoryMock.Object, userRepositoryMock.Object, "test@test.com");
            ActionResult result = requestMatchController.MatchRequest();
            Assert.AreEqual(((IList<Match>) requestMatchController.ViewData["MatchList"]).Count, 1);
            matchRepositoryMock.Verify(ps => ps.LoadPotentialMatchesByUserJourney("test@test.com"));
        }

        [Test]
        public void ShouldAcceptSelectedMatchedRequestsAndChangeStatusOfTheRequests()
        {
            var matchRepositoryMock = new Mock<IMatchRepository> { };
            var userRepositoryMock = new Mock<IUserRepository> {};

            List<Match> matchedRequests = new List<Match>();
            Request request = new Request();
            User user = new User();
            user.Email = new Email("test@test.com");

            Match match = new Match(new Journey(), request);
            match.Status = MatchStatus.Potential;
            match.Id = 1;
            matchedRequests.Add(match);
            matchRepositoryMock.Setup(ps => ps.LoadPotentialMatchesByUserJourney("test@test.com")).Returns(matchedRequests);
            userRepositoryMock.Setup(ps => ps.LoadUser("test@test.com")).Returns(user);

            Website.Controllers.RequestMatchController requestMatchController =
                            new Website.Controllers.RequestMatchController(matchRepositoryMock.Object,userRepositoryMock.Object ,"test@test.com");
            requestMatchController.AcceptRequest(new string[]{"1"});
            Assert.AreEqual(match.Status,MatchStatus.Accepted);
            Assert.AreEqual(match.Request.AcceptingUser,user);
            matchRepositoryMock.Verify(ps => ps.UpdateMatches(matchedRequests));
          }
    }
}
