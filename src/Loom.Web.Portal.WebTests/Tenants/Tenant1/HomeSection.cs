#region Using Directives

using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal.WebTest.Tenants.Tenant1
{
    public class HomeSection : SectionBase
    {
        public override void OnRegister(SectionContext context)
        {
            context.RegisterHostName("tenant1.loomportal.com");
        }
    }
}