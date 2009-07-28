using System.Web.Mvc;
using DomainModel;

namespace Website.Controllers
{
    public class RequestController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Create()
        {
            return View("Create");
        }
    }
}