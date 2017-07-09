#region Using Directives

using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class ImageFile : VirtualFile
    {
        public ImageFile(string virtualPath) : base(virtualPath) { }

        public override Stream Open()
        {
            string path = HttpContext.Current.Server.MapPath("~/" + Path.GetFileName(VirtualPath));
            SetCache(path);
            return File.OpenRead(path);
        }

        private static void SetCache(string path)
        {
            HttpCachePolicy cache = HttpContext.Current.Response.Cache;
            TimeSpan expires = TimeSpan.FromDays(14);
            cache.SetExpires(DateTime.Now.Add(expires));
            cache.SetMaxAge(expires);
            cache.SetLastModified(File.GetLastWriteTime(path));
        }
    }
}