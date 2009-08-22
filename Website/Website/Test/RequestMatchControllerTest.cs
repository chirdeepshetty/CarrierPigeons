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
            List<Match> matchedRequests = new List<Match>();
            Match match = new Match(new Journey(),new Request());
            match.Status = MatchStatus.Created;
            matchedRequests.Add(match);
            matchRepositoryMock.Setup(ps => ps.LoadMatchesByUserJourney("test@test.com")).Returns(matchedRequests);

            Website.Controllers.RequestMatchController requestMatchController = 
                            new Website.Controllers.RequestMatchController(matchRepositoryMock.Object,"test@test.com");
            ActionResult result = requestMatchController.MatchRequest();
            Assert.AreEqual(((IList<Match>) requestMatchController.ViewData["MatchList"]).Count, 1);
            matchRepositoryMock.Verify(ps => ps.LoadMatchesByUserJourney("test@test.com"));
        }

        [Test]
        public void ShouldAcceptSelectedMatchedRequestsAndChangeStatusOfTheRequests()
        {
            var matchRepositoryMock = new Mock<IMatchRepository> { };
            List<Match> matchedRequests = new List<Match>();
            Match match = new Match(new Journey(), new Request());
            match.Status = MatchStatus.Created;
            matchedRequests.Add(match);
            matchRepositoryMock.Setup(ps => ps.LoadMatchesByUserJourney("test@test.com")).Returns(matchedRequests);

            Website.Controllers.RequestMatchController requestMatchController =
                            new Website.Controllers.RequestMatchController(matchRepositoryMock.Object, "test@test.com");
            //TBC
        }
    }
}
