using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel;

namespace Website.Controllers
{
    public class RequestMatchController : AbstractBaseController
    {
        private readonly IMatchRepository _matchRepository;

        public RequestMatchController()
         : this(null, null)
        {}

        public RequestMatchController(IMatchRepository matchRepository, string loggedInUser)
        {
            Console.WriteLine("RequestMatchController Constructor Called");
            _matchRepository = matchRepository ?? DomainModel.MatchRepository.Instance;
            _loggedInUser = loggedInUser;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MatchRequest()
        {
            var matches = _matchRepository.LoadMatchesByUserJourney(GetLoggedInUser());
            ViewData["MatchList"] = matches;
            return View();
        }
    }
}
