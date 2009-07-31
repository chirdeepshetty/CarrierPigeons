using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Website.Controllers
{
    public class JourneyMatchController : Controller
    {
        //
        // GET: /JourneyMatch/

        public ActionResult Index()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MatchJourney()
        {
            var matches = DomainModel.MatchRepository.Instance.LoadMatchesByUserJourney(User.Identity.Name);
           
            ViewData["MatchList"] = matches;
            return View();
        }

    }
}
