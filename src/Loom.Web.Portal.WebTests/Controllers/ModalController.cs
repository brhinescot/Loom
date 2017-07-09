#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Controllers
{
    public class ModalController : MvcController
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