#region Using Directives

using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using Loom.Annotations;
using Loom.Web.IO;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class ImageProvider : ResourceFileProvider
    {
        private const string Key = "__imageDefault";
        private const string Prefix = "/imageresource";

        [NotNull]
        protected override string DefaultResourceKey => Key;

        [NotNull]
        protected override string ResourcePathPrefix => Prefix;

        [CanBeNull]
        public override string GetContentType(string extension)
        {
            return WebFile.GetMimeType(extension);
        }

        public override CacheDependency GetCacheDependency([NotNull] string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            Argument.Assert.IsNotNullOrEmpty(virtualPath, nameof(virtualPath));

            if (!IsResourceFile(virtualPath))
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            PortalTrace.Write("ImageProvider", "GetCacheDependency", "Creating new CacheDependency for path '{0}'.", virtualPath);
            return new CacheDependency(HttpContext.Current.Server.MapPath("~/bin/Loom.Web.Portal.dll"));
        }
    }
}