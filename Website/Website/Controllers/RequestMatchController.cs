using System;
using System.Collections;
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
        private readonly IUserRepository _userRepository;

        public RequestMatchController()
         : this(null, null, null)
        {}

        public RequestMatchController(IMatchRepository matchRepository, IUserRepository userRepository,string loggedInUser)
        {
            _matchRepository = matchRepository ?? DomainModel.MatchRepository.Instance;
            _userRepository = userRepository ?? UserRepository.Instance;
            _loggedInUser = loggedInUser;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MatchRequest()
        {
            var matches = _matchRepository.LoadPotentialMatchesByUserJourney(GetLoggedInUser());
            ViewData["MatchList"] = matches;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AcceptRequest(string[] acceptRequest)
        {
            var matches = _matchRepository.LoadPotentialMatchesByUserJourney(GetLoggedInUser());
            User acceptingUser = _userRepository.LoadUser(GetLoggedInUser());
            IList < Match > acceptedMatches= new List<Match>();
            List<string> acceptedRequestList = acceptRequest.ToList();

            if(acceptedRequestList.Count()==0)
            {
                return RedirectToAction("MatchRequest", "RequestMatch");
            }

            foreach (Match match in matches)
            {
                if (acceptedRequestList.Contains(match.Id.ToString()))
                {
                    match.Accept(acceptingUser);
                    acceptedMatches.Add(match);
                }
            }

            if(acceptedMatches.Count>0)
            {
                _matchRepository.UpdateMatches(acceptedMatches);
            }
            return RedirectToAction("MatchRequest", "RequestMatch");
        }
    }
}
