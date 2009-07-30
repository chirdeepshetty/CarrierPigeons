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
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(JourneyModel journeyModel)
        {
            if (ModelState.IsValid)
            {  
                Location origin = new Location(journeyModel.OriginPlace, new TravelDate(DateTime.Parse(journeyModel.OriginDate)));
                Location destination = new Location(journeyModel.DestinationPlace, new TravelDate(DateTime.Parse(journeyModel.DestinationDate)));
               
                IUserRepository userRepository = RepositoryFactory.GetUserRepository();
                DomainModel.User user = userRepository.LoadUser(User.Identity.Name);
                DomainModel.Journey journey = new Journey(user,  origin, destination);
                DomainModel.JourneyRepository.Instance.Save(journey);

                ViewData["Message"] = "Journey Created Successfully!";
               
            }

            return View(journeyModel);
        }

    }
}
