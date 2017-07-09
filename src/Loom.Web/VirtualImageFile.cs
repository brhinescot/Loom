#region Using Directives

using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using Loom.Drawing;
using Loom.Web.IO;

#endregion

namespace Loom.Web
{
    internal class VirtualImageFile : VirtualFile
    {
        public VirtualImageFile(string virtualPath) : base(virtualPath)
        {
            Argument.Assert.IsNotNullOrEmpty(virtualPath, nameof(virtualPath));
        }

        public override Stream Open()
        {
            DateTime dt = DateTime.Now.AddDays(14);
            HttpResponse response = HttpContext.Current.Response;

            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(dt);
            response.Cache.SetMaxAge(TimeSpan.FromDays(14));

            string storageKey = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Replace("~/Virtual_Image/", string.Empty);

            if (Compare.IsNullOrEmpty(storageKey))
                throw new HttpException(404, string.Format("The image virtual path '{0}' is invalid. The required format is '/Virtual_Image/[Cache Key]'.", VirtualPath));

            object image = HttpContext.Current.Cache[storageKey];
            if (image == null)
                throw new HttpException(404, string.Format("The image virtual path '{0}' is invalid or the image is not cached. The required format is '/Virtual_Image/[Cache Key]'.", VirtualPath));

            byte[] imageBytes = image as byte[];

            if (imageBytes == null)
                throw new HttpException(404, string.Format("The image virtual path '{0}' is invalid or not an image. The required format is '/Virtual_Image/[Cache Key]'.", VirtualPath));

            MemoryStream stream = new MemoryStream();
            stream.Write(imageBytes, 0, imageBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static void CacheThumbnail(string imagePath, Size imageSize, int imageQuality, string cacheKey, int cacheDuration)
        {
            Cache cache = HttpContext.Current.Cache;

            if (!File.Exists(imagePath))
            {
                imagePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["imageNotAvailablePlaceholder"]);
                if (!File.Exists(imagePath))
                    return;
            }

            using (Image image = Image.FromFile(imagePath))
            using (Bitmap thumbnail = Thumbnail.FromImage(image, imageSize.Width, imageSize.Height))
            {
                cache.Add(WebPath.GetPathWithoutQuerystring(cacheKey), JpgFormat.ToByteArray(thumbnail, imageQuality), null,
                    Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheDuration),
                    CacheItemPriority.High, null);
            }
        }
    }
}