#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Tenants.Tenant1.Products.Controllers
{
    public class HomeController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}