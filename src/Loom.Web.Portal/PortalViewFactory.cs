#region Using Directives

using System.Web;
using System.Web.UI;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.UI;

#endregion

namespace Loom.Web.Portal
{
    internal sealed class PortalViewFactory : PageHandlerFactory
    {
        /// <summary>
        ///     Returns an instance of the <see cref="System.Web.IHttpHandler" /> interface to
        ///     process the requested resource.
        /// </summary>
        /// <returns>
        ///     A new <see cref="System.Web.IHttpHandler" /> that processes the request; otherwise, null.
        /// </returns>
        /// <param name="context">
        ///     An instance of the <see cref="System.Web.HttpContext" /> class
        ///     that provides references to intrinsic server objects (for example, Request, Response,
        ///     Session, and Server) used to service HTTP requests.
        /// </param>
        /// <param name="requestType">
        ///     The
        ///     HTTP data transfer method (GET or POST) that the client uses.
        /// </param>
        /// <param name="virtualPath">
        ///     The
        ///     virtual path to the requested resource.
        /// </param>
        /// <param name="path">
        ///     The
        ///     <see cref="System.Web.HttpRequest.PhysicalApplicationPath" /> property to the requested
        ///     resource.
        /// </param>
        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            PortalTrace.Write("PortalViewFactory", "GetHandler", "Begin base.GetHandler");

            return GetPortalHandler(context, requestType, virtualPath, path);
        }

        private IHttpHandler GetPortalHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            IHttpHandler handler = base.GetHandler(context, requestType, virtualPath, path);

            PortalTrace.Write("PortalViewFactory", "GetHandler", "End base.GetHandler");

            PortalView view = handler as PortalView;

            PortalTrace.WriteIf(view == null, "PortalViewFactory", "GetHandler", "'{0}' is not a portal view. Skipping initialization.", virtualPath);
            if (view != null)
            {
                PortalTrace.Write("PortalViewFactory", "GetHandler", "Initializing portal view for '{0}'", virtualPath);
                InitializeView(view);
            }

            return handler;
        }

        private static void InitializeView(PortalView view)
        {
            IPortalContext context = PortalContext.Current;
            IViewResult result = context.Request.Result as IViewResult;
            if (result == null)
                return;

            view.ViewData = context.Request.Result.ViewData;
            view.InitializeContext(PortalContext.Current);
        }
    }
}