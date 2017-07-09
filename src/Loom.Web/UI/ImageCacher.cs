#region Using Directives

using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using Loom.Web.IO;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI
{
    public static class ImageCacher
    {
        private const string ImageGenerationServiceBaseUrl = "~/Virtual_Image/";

        public static void StoreImage(ImageEx imageEx)
        {
            string key = imageEx.StorageKey;

            if (Compare.IsNullOrEmpty(imageEx.StorageKey))
                imageEx.StorageKey = key = "__thumbnail_" + imageEx.MaximumSize.Width + "," + imageEx.MaximumSize.Height + "," + imageEx.Quality + "/" + imageEx.ImageUrl.Replace("~/", string.Empty);

            if (imageEx.Page.Cache[key] != null)
            {
                if (imageEx.OriginalImageUrl == null)
                    imageEx.OriginalImageUrl = imageEx.ImageUrl;
                imageEx.ImageUrl = ImageGenerationServiceBaseUrl + key;
                Debug.WriteLine("ImageCacher.ImageEx:\t\tcached url location =\t'" + imageEx.ImageUrl + "' key = '" + key + "'.");
                return;
            }

            string path = HttpContext.Current.Server.MapPath(WebPath.GetPathWithoutQuerystring(imageEx.OriginalImageUrl ?? imageEx.ImageUrl));
            Size size = imageEx.MaximumSize;

            VirtualImageFile.CacheThumbnail(path, size, imageEx.Quality, key, imageEx.CacheDuration);

            imageEx.OriginalImageUrl = imageEx.ImageUrl;
            imageEx.ImageUrl = ImageGenerationServiceBaseUrl + key;
            Debug.WriteLine("ImageCacher.ImageEx:\t\tnew url location =\t'" + imageEx.ImageUrl + "' key = '" + key + "'.");
        }

        public static void StoreImage(HyperLinkEx hyperLinkEx)
        {
            string key = hyperLinkEx.StorageKey;

            if (Compare.IsNullOrEmpty(key))
                hyperLinkEx.StorageKey = key = "__thumbnail_" + hyperLinkEx.MaximumThumbnailSize.Width + "," + hyperLinkEx.MaximumThumbnailSize.Height + "," + hyperLinkEx.ThumbnailQuality + "/" + hyperLinkEx.ImageUrl.Replace("~/", string.Empty);

            if (hyperLinkEx.Page.Cache[key] != null)
            {
                if (hyperLinkEx.OriginalImageUrl == null)
                    hyperLinkEx.OriginalImageUrl = hyperLinkEx.ImageUrl;
                hyperLinkEx.ImageUrl = ImageGenerationServiceBaseUrl + key;
                Debug.WriteLine("ImageCacher.HyperLinkEx:\tcached url location =\t'" + hyperLinkEx.ImageUrl + "' key = '" + key + "'.");
                return;
            }

            string path = HttpContext.Current.Server.MapPath(WebPath.GetPathWithoutQuerystring(hyperLinkEx.OriginalImageUrl ?? hyperLinkEx.ImageUrl));
            Size size = hyperLinkEx.MaximumThumbnailSize;

            VirtualImageFile.CacheThumbnail(path, size, hyperLinkEx.ThumbnailQuality, key, hyperLinkEx.ThumbnailCacheDuration);

            hyperLinkEx.OriginalImageUrl = hyperLinkEx.ImageUrl;
            hyperLinkEx.ImageUrl = ImageGenerationServiceBaseUrl + key;
            Debug.WriteLine("ImageCacher.HyperLinkEx:\tnew url location =\t'" + hyperLinkEx.ImageUrl + "' key = '" + key + "'.");
        }

        public static string StoreImage(Control container, string imageUrl, int width, int height, int quality)
        {
            string key = "__thumbnail_" + width + "," + height + "," + quality + "/" + imageUrl.Replace("~/", string.Empty);

            if (container.Page.Cache[key] != null)
                return ImageGenerationServiceBaseUrl + key;

            string path = HttpContext.Current.Server.MapPath(WebPath.GetPathWithoutQuerystring(imageUrl));
            Size size = new Size(width, height);

            VirtualImageFile.CacheThumbnail(path, size, quality, key, 5);

            return ImageGenerationServiceBaseUrl + key;
        }

        public static string StoreImage(Control container, string imageUrl, Size maximumSize, int quality)
        {
            return StoreImage(container, imageUrl, maximumSize.Width, maximumSize.Height, quality);
        }

        public static void ClearCache()
        {
            Cache cache = HttpContext.Current.Cache;
            foreach (DictionaryEntry item in cache)
            {
                string key = item.Key as string;
                if (key == null)
                    continue;

                if (key.StartsWith("__thumbnail_"))
                    cache.Remove(key);
            }
        }
    }
}