#region Using Directives

using System;
using Loom.Annotations;
using Loom.Web.IO;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class RedirectResult : ActionResult
    {
        public RedirectResult([NotNull] string url)
        {
            Argument.Assert.IsNotNullOrEmpty(url, nameof(url));

            Url = url;
        }

        public string Url { get; }

        protected override void OnExecute(ControllerContext context)
        {
            OnRedirect(Url, context);
        }

        protected virtual void OnRedirect(string url, ControllerContext context)
        {
            PortalTrace.Write("RedirectResult", "Execute", "Redirecting to '" + url + "'.");

            url = WebPath.FullyQualify(url);
            context.PortalContext.Response.IsRedirected = true;
            context.PortalContext.Response.RedirectLocation = new Uri(url);
            context.PortalContext.HttpContext.Response.Redirect(url, true);
        }
    }
}