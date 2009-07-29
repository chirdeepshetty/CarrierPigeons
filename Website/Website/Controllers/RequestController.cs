using System;
using System.Web.Mvc;
using DomainModel;
using Website.Models;

namespace Website.Controllers
{
    public class RequestController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Create()
        {
           
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult Create(CreateRequestResponse requestResponse)
        {
            if (ModelState.IsValid)
            {
                Package package = new Package(requestResponse.PackageDescription, requestResponse.PackageWeight, requestResponse.PackageDescription);
                Location origin = new Location(requestResponse.OriginPlace, new TravelDate(DateTime.Parse(requestResponse.OriginDate)));
                Location destination = new Location(requestResponse.DestinationPlace, new TravelDate(DateTime.Parse(requestResponse.DestinationDate)));
                //if (User.Identity.IsAuthenticated)
                {
                    IUserRepository userRepository = RepositoryFactory.GetUserRepository();
                    //DomainModel.User user = userRepository.LoadUser(User.Identity.Name);
                    DomainModel.User user = new User(new Email("user@users.com"), new UserName("FName", "LName"), "pwd");
                    DomainModel.Request request = new Request(user, package, origin, destination);
                    DomainModel.RequestRepository.Instance.Save(request);

                    ViewData["Message"] = "Request Submitted Successfully!";
                }
                //else
                //{
                //    ViewData["Message"] = "You need to be registered to view this page!";
                //}
            }

            return View(requestResponse);

        }
    }
}