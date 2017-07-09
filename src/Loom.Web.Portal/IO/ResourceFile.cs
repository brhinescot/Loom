#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class ResourceFile : VirtualFile
    {
        private readonly string assemblyName;
        private readonly string contentType;

        private readonly string resourcePath;

        public ResourceFile(string virtualPath, string resourcePath, string assemblyName, string contentType = null) : base(virtualPath)
        {
            this.resourcePath = resourcePath;
            this.assemblyName = assemblyName;
            this.contentType = contentType;
        }

        public override Stream Open()
        {
            if (!Compare.IsNullOrEmpty(contentType))
            {
                if (IsClientCached(PortalContext.Current.LastModified))
                    return GetCachedResponse();

                ConfigureClientResponse();
            }

            Assembly assembly = Assembly.Load(assemblyName);
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream != null)
                return stream;

            throw new HttpException(404, "The requested resource file was not found.");
        }

        private void ConfigureClientResponse()
        {
            SetCache();
            HttpContext.Current.Response.AddHeader("ContentType", contentType);
        }

        private static Stream GetCachedResponse()
        {
            SetCache(true);
            HttpContext.Current.Response.StatusCode = 304;
            HttpContext.Current.Response.SuppressContent = true;
            return new MemoryStream(new byte[0]);
        }

        private static void SetCache(bool suppressLastModified = false)
        {
            HttpContext.Current.Response.AddHeader("Age", ((int) (PortalContext.Current.LastModified - DateTime.Now).TotalSeconds).ToString(CultureInfo.InvariantCulture));
            HttpCachePolicy cache = HttpContext.Current.Response.Cache;
            TimeSpan expires = TimeSpan.FromDays(14);

            cache.SetExpires(DateTime.UtcNow.Add(expires));
            cache.SetMaxAge(expires);
            DateTime dt = PortalContext.Current.LastModified;
            string eTagDate = "\"" + dt.ToString("s", DateTimeFormatInfo.InvariantInfo) + "\"";
            HttpContext.Current.Response.AddHeader("ETag", eTagDate);

            if (!suppressLastModified)
                cache.SetLastModified(PortalContext.Current.LastModified);
        }

        private static bool IsClientCached(DateTime lastModified)
        {
            lastModified = new DateTime(lastModified.Year, lastModified.Month, lastModified.Day, lastModified.Hour, lastModified.Minute, lastModified.Second);

            string incomingEtag = HttpContext.Current.Request.Headers["If-None-Match"];
            string eTagDate = "\"" + lastModified.ToString("s", DateTimeFormatInfo.InvariantInfo) + "\"";
            if (string.CompareOrdinal(incomingEtag, eTagDate) == 0)
                return true;

            string modifiedHeader = HttpContext.Current.Request.Headers["If-Modified-Since"];
            if (modifiedHeader == null)
                return false;

            DateTime isModifiedSince;
            if (!DateTime.TryParse(modifiedHeader, out isModifiedSince))
                return false;

            DateTime universalTime = isModifiedSince.ToUniversalTime();
            return universalTime >= lastModified;
        }
    }
}