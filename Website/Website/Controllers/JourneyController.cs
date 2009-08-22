using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel;
using Website.Models;


namespace Website.Controllers
{
    public class JourneyController : Controller
    {
        private IList<Journey> GetJourneyList(User user)
        {
            return  JourneyRepository.Instance.FindJourneysByUser(user);
        }

       
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Create()
        {
            ViewData["EmailId"] = User.Identity.Name;
            ViewData["MyOtherJourneyDetails"] = GetJourneyList(UserRepository.Instance.LoadUser(User.Identity.Name));
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(JourneyModel journeyModel)
        {
            if (ModelState.IsValid)
            {  
                Location origin = new Location(journeyModel.OriginPlace, new TravelDate(DateTime.Parse(journeyModel.OriginDate)));
                Location destination = new Location(journeyModel.DestinationPlace, new TravelDate(DateTime.Parse(journeyModel.DestinationDate)));
               
                IUserRepository userRepository = UserRepository.Instance;
                DomainModel.User user = userRepository.LoadUser(User.Identity.Name);
                DomainModel.Journey journey = new Journey(user,  origin, destination);
                DomainModel.JourneyRepository.Instance.Save(journey);

                ViewData["MyOtherJourneyDetails"] = GetJourneyList(user);

                ViewData["Message"] = "Journey Created Successfully!";
               
            }

            return View(journeyModel);
        }

    }
}
