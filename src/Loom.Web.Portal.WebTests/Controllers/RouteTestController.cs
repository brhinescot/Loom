#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Controllers
{
    public class RouteTestController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUser(string userId)
        {
            return Json(new {userId});
        }

        public ActionResult AddUser(string role, TestUser user)
        {
            return Json(new {name = user.Name, email = user.Email, role});
        }

        public ActionResult AddMunster(string userId, string munsterName)
        {
            return Json(new {id = userId, name = munsterName});
        }

        public ActionResult Database(int id)
        {
            return Json(new {action = "database", id});
        }
    }

    public class TestUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}