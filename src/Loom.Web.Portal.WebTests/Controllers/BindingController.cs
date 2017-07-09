#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Controllers
{
    public class BindingController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}