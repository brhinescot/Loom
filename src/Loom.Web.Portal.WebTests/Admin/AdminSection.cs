#region Using Directives

#endregion

#region Using Directives

using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal.WebTest.Admin
{
    public class AdminSection : SectionBase
    {
        public override void OnRegister(SectionContext context)
        {
            //TODO: Enable host names for non-tenant paths? Maybe? Good idea?
//            context.RegisterHostName("admin.loomportal.com");
        }
    }
}