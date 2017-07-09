#region Using Directives

using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Caching;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class ModuleProvider : ResourceFileProvider
    {
        private const string Key = "__moduleDefault";
        private const string Prefix = "/moduleresource";

        [NotNull]
        protected override string DefaultResourceKey => Key;

        [NotNull]
        protected override string ResourcePathPrefix => Prefix;

//        [NotNull]
//        public override string GetVirtualPath(string virtualPath)
//        {
//            int length = virtualPath.StartsWith("~") ? 1 : HttpContext.Current.Request.ApplicationPath.Length;
//            if (length == 1)
//                return virtualPath;
//
//            return virtualPath.Substring(length);
//        }

        [NotNull]
        public override string GetExtension(string virtualPath)
        {
            return Path.GetExtension(virtualPath);
        }

        [CanBeNull]
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (!IsResourceFile(virtualPath))
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            PortalTrace.Write("StyleProvider", "GetCacheDependency", "Creating new CacheDependency for path '{0}'.", virtualPath);
            return new CacheDependency(HttpContext.Current.Server.MapPath("~/bin/Loom.Web.Portal.dll"));
        }
    }
}