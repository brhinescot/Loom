namespace Loom.Web.Portal.Controllers
{
    public class AjaxRedirectResult : RedirectResult
    {
        public AjaxRedirectResult(string url) : base(url) { }

        protected override void OnRedirect(string url, ControllerContext context)
        {
            PortalTrace.Write("AjaxRedirectResult", "OnRedirect", "Redirecting to '" + url + "'.");
            context.PortalContext.Response.AjaxRedirect(url, true);
        }
    }
}