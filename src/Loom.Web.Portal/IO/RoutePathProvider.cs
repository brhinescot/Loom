#region Using Directives

using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class RoutePathProvider : VirtualPathProvider
    {
        internal const string VirtualPageName = "__virtual.aspx";

        public override bool FileExists(string virtualPath)
        {
            bool isPortalPath = IsRouteFile(virtualPath);
            PortalTrace.WriteIf(!isPortalPath, "RoutePathProvider", "FileExists", "'{0}' is not a virtual portal file. Delegating to previous VirtualPathProvider.", virtualPath);

            return isPortalPath || Previous.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            IPortalRequest portalRequest = PortalContext.Current.Request;

            IViewResult result = portalRequest.Result as IViewResult;
            if (result != null && IsRouteFile(virtualPath))
                return new RouteFile(virtualPath, portalRequest.Result);

            PortalTrace.Write("RoutePathProvider", "GetFile", "'{0}' is not a virtual portal file. Delegating to previous VirtualPathProvider.", virtualPath);

            if (Path.GetExtension(virtualPath) != ".aspx")
                return Previous.GetFile(virtualPath);

            return portalRequest.AllowPhysicalPages
                ? Previous.GetFile(virtualPath)
                : new RouteFile(virtualPath, new FileNotFoundResult());
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            IViewResult result = PortalContext.Current.Request.Result as IViewResult;
            if (!IsRouteFile(virtualPath) || result == null)
                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            PortalTrace.Write("RoutePathProvider", "GetCacheDependency", "Creating new CacheDependency for path '{0}'.", virtualPath);
            return new CacheDependency(HttpContext.Current.Server.MapPath(result.DependencyPath));
        }

        private static bool IsRouteFile(string virtualPath)
        {
            return Path.GetFileName(virtualPath) == VirtualPageName;
        }
    }
}