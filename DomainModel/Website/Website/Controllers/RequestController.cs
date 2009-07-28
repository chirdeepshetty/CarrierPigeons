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
                Package package = new Package(requestResponse.PackageDescription, requestResponse.PackageWeight,
                                              requestResponse.PackageDescription);
                Location origin = new Location(requestResponse.OriginPlace,
                                               new TravelDate(DateTime.Parse(requestResponse.OriginDate)));
                Location destination = new Location(requestResponse.DestinationPlace,
                                                    new TravelDate(DateTime.Parse(requestResponse.DestinationDate)));
                DomainModel.Request request = new Request(null, package, origin, destination);
                DomainModel.RequestRepository.Instance.Save(request);

                ViewData["Message"] = "Request Submitted Successfully!";
            }

            return View(requestResponse);

        }
    }
}