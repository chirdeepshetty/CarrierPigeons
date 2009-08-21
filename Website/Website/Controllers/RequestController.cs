using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using DomainModel;
using Website.Models;

namespace Website.Controllers
{
    public class RequestController : Controller
    {
        private IEnumerable<Request> GetAllRequests(String emailId)
        {
            return RequestRepository.Instance.SearchByUser(emailId);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Create()
        {
            ViewData["AllRequests"] = GetAllRequests(User.Identity.Name);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(CreateRequestResponse requestResponse)
        {
            if (ModelState.IsValid)
            {
                Package package = new Package(requestResponse.PackageDescription, requestResponse.PackageWeight, requestResponse.PackageDescription);
                Location origin = new Location(requestResponse.OriginPlace, null);
                Location destination = new Location(requestResponse.DestinationPlace, new TravelDate(DateTime.Parse(requestResponse.DestinationDate)));
                if (User.Identity.IsAuthenticated)
                {
                    IUserRepository userRepository = RepositoryFactory.GetUserRepository();
                    DomainModel.User user = userRepository.LoadUser(User.Identity.Name);
                    DomainModel.Request request = new Request(user, package, origin, destination);
                    DomainModel.RequestRepository.Instance.Save(request);

                    ViewData["Message"] = "Request Submitted Successfully!";
                    ViewData["AllRequests"] = GetAllRequests(User.Identity.Name);
                }
                else
                {
                    ViewData["Message"] = "You need to be registered to view this page!";
                }
            }

            return View(requestResponse);

        }

      

       

    }
}