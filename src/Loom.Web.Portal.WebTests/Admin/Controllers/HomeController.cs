#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Admin.Controllers
{
    public class HomeController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}