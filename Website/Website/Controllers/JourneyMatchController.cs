using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel;

namespace Website.Controllers
{
    //[HibernateSessionFilter]
    public class JourneyMatchController : Controller
    {
        //
        // GET: /JourneyMatch/

        [HibernateSessionFilter]
        public ActionResult Index()
        {
            return View();
        }
        [HibernateSessionFilter]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MatchJourney()
        {
            var matches = DomainModel.MatchRepository.Instance.LoadMatchesByUserRequest(User.Identity.Name);
           
            ViewData["MatchList"] = matches;
            return View();
        }

    }
}
