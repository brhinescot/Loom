#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Controllers
{
    public class HomeController : MvcController
    {
        public ActionResult Index()
        {
            ViewData.Name = "Brian";
            return View();
        }

        public ActionResult MasterTest()
        {
            return View();
        }
    }
}