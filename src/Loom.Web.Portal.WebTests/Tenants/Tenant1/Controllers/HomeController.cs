﻿#region Using Directives

using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.WebTest.Tenants.Tenant1.Controllers
{
    public class HomeController : MvcController
    {
        public ActionResult Index()
        {
            ViewData.Name = "Brian 1";
            return View();
        }

        public ActionResult MasterTest()
        {
            return View();
        }
    }
}