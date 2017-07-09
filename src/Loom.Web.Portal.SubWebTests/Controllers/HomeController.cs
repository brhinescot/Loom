#region License information
// ************************************************************************
// HomeController.cs
// Part of the Loom.Web.Portal.SubWebTests application.
// 
// Copyright © 2004, 2011 Colossus Interactive, LLC
// All Rights Reserved
//    
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// mailto:licensing@colossusinteractive.com
// ************************************************************************
#endregion

using Loom.Web.Portal.Controllers;

namespace Loom.Web.Portal.SubWebTests.Controllers
{
    public class HomeController : MvcController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}