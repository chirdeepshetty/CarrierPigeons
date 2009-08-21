using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DomainModel;

namespace Website.Controllers
{
    public class AbstractBaseController : Controller
    {
        protected String _loggedInUser;

        protected string GetLoggedInUser()
        {
            return _loggedInUser ?? User.Identity.Name;
        }
    }
}
