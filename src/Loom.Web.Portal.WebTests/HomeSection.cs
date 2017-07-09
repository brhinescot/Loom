#region Using Directives

using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal.WebTest
{
    public class HomeSection : SectionBase
    {
        public override void OnRegister(SectionContext context)
        {
            context.AddPrimaryRoute("UserAndRole", "/users/{action,[AddUser]}/{role}/", "RouteTest");
            context.AddPrimaryRoute("OneToken", "/users/{action,[GetUser]}/{userId}/", "RouteTest");
            context.AddPrimaryRoute("AddMunster", "/users/{action,[AddMunster]}/{userId}/{munsterName}", "RouteTest");
        }
    }
}