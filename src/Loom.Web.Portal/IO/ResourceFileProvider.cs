#region Using Directives

using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web.Portal.IO
{
    internal abstract class ResourceFileProvider : VirtualPathProvider
    {
        protected abstract string DefaultResourceKey { get; }
        protected abstract string ResourcePathPrefix { get; }

        public virtual string GetVirtualPath(string virtualPath)
        {
            int length = virtualPath.StartsWith("~") ? 2 : HttpContext.Current.Request.ApplicationPath.Length;
            string result = length == 1 ? virtualPath : virtualPath.Substring(length);

            PortalTrace.Write("ResourceFileProvider", "GetVirtualPath(string)", "Using virtual path {0}", virtualPath);
            return result;
        }

        public virtual string GetExtension(string virtualPath)
        {
            return Path.GetExtension(PortalContext.Current.Request.Path);
        }

        public virtual string GetContentType(string virtualPath)
        {
            return null;
        }

        public override bool FileExists(string virtualPath)
        {
            return IsResourceFile(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (!IsResourceFile(virtualPath))
                return base.GetFile(virtualPath);

            string localPath = GetVirtualPath(virtualPath).Substring(ResourcePathPrefix.Length + 1);
            string extension = GetExtension(virtualPath);
            string fileName = Path.GetFileNameWithoutExtension(localPath) + extension;
            string resourceKey = Path.GetDirectoryName(localPath);
            if (Compare.IsNullOrEmpty(resourceKey))
                resourceKey = DefaultResourceKey;

            VirtualResourceData data = PortalContext.Current.GetVirtualResourceData(resourceKey);
            if (data == null)
                throw new HttpException(404, "The virtual resource '" + virtualPath + "' cannot be found.");

            string resourcePath = data.Namespace + "." + fileName;
            return new ResourceFile(virtualPath, resourcePath, data.Assembly, GetContentType(extension));
        }

        protected virtual bool IsResourceFile(string virtualPath)
        {
            return GetVirtualPath(virtualPath).StartsWith(ResourcePathPrefix, StringComparison.OrdinalIgnoreCase);
        }
    }
}