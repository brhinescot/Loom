#region Using Directives

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class StyleProvider : ResourceFileProvider
    {
        #region Constants

#if DEBUG
        private const string Extension = ".css";
#endif
#if !DEBUG
        private const string Extension = ".min.css";
#endif

        private const string Key = "__styleDefault";
        private const string Prefix = "/styleresource";

        #endregion

        protected override string DefaultResourceKey => Key;

        protected override string ResourcePathPrefix => Prefix;

        public override string GetExtension(string virtualPath)
        {
            return Extension;
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (!IsResourceFile(virtualPath))
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            PortalTrace.Write("StyleProvider", "GetCacheDependency", "Creating new CacheDependency for path '{0}'.", virtualPath);
            return new CacheDependency(HttpContext.Current.Server.MapPath("~/bin/Loom.Web.Portal.dll"));
        }
    }
}