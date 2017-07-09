#region Using Directives

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    public class VirtualImageProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            return IsVirtualFilePath(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, virtualPath) || Previous.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            string path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;

            return !IsVirtualFilePath(path, virtualPath) ? Previous.GetFile(virtualPath) : new VirtualImageFile(path);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsVirtualFilePath(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, virtualPath)
                ? null
                : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private static bool IsVirtualFilePath(string appRelativeCurrentExecutionFilePath, string virtualPath)
        {
            return appRelativeCurrentExecutionFilePath.StartsWith("~/Virtual_Image", StringComparison.InvariantCultureIgnoreCase) ||
                   virtualPath.StartsWith("~/Virtual_Image", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}