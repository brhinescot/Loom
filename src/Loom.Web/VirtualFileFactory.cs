#region Using Directives

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    public class VirtualFileFactory : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            return IsVirtualFilePath(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, virtualPath) || Previous.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            string path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;

            if (!IsVirtualFilePath(path, virtualPath))
                return Previous.GetFile(virtualPath);

            if (path.StartsWith("~/Virtual_Resource/", StringComparison.InvariantCultureIgnoreCase))
                return new AssemblyResourceVirtualFile(virtualPath);
            if (path.StartsWith("~/Virtual_Image/", StringComparison.InvariantCultureIgnoreCase))
                return new VirtualImageFile(path);
            if (path.StartsWith("~/Virtual_Script/", StringComparison.InvariantCultureIgnoreCase))
                return new VirtualScriptFile(path);

            return Previous.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsVirtualFilePath(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, virtualPath)
                ? null
                : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private static bool IsVirtualFilePath(string appRelativeCurrentExecutionFilePath, string virtualPath)
        {
            return appRelativeCurrentExecutionFilePath.StartsWith("~/Virtual_", StringComparison.InvariantCultureIgnoreCase) ||
                   virtualPath.StartsWith("~/Virtual_", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}