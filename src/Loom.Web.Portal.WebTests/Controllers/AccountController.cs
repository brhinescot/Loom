#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Controllers
{
    public class AccountController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Preferences()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Signout()
        {
            // TODO: Handle redirecting to index page/
            return AjaxRedirect("#!routetest");
        }
    }
}