namespace Loom.Web.Portal.Controllers
{
    public class PortalController : MvcController
    {
        public ActionResult Index()
        {
            ActionResult result = View();
            return result;
        }
    }
}