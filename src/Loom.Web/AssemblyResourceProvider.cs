#region Using Directives

using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;

#endregion

namespace Loom.Web
{
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            return IsAppResourcePath(virtualPath) || Previous.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return IsAppResourcePath(virtualPath)
                ? new AssemblyResourceVirtualFile(virtualPath)
                : Previous.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsAppResourcePath(virtualPath)
                ? null
                : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private static bool IsAppResourcePath(string virtualPath)
        {
            return virtualPath.StartsWith("/Virtual_Resource/", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}