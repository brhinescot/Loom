#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

#endregion

namespace Loom.Web.UI
{
    /// <summary>
    ///     Caches dynamic images on the web server.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpHandler in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  <httpHandlers>
    /// 		<add verb="GET" path="cachedimageservice.axd" type="Loom.Web.UI.CachedImageService, Loom.Web" />
    /// 	</httpHandlers>
    ///  ]]>
    ///  </code>
    /// </example>
    public sealed class CachedImageService : IHttpHandler
    {
        private const string ContentType = "image/jpeg";

        #region IHttpHandler Members

        /// <summary>
        ///     Gets a value indicating whether this instance is reusable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is reusable; otherwise, <c>false</c>.
        /// </value>
        public bool IsReusable => true;

        /// <summary>
        ///     Processes the request.
        /// </summary>
        /// <param name="context">Context.</param>
        public void ProcessRequest(HttpContext context)
        {
            // Retrieve the DATA query string parameter
            string storageKey = context.Request["data"];

            if (storageKey == null)
            {
                WriteError("The 'data' querystring value is empty.");
                return;
            }

            // Grab data from the cache 
            object imageObject = context.Cache[storageKey];
            if (imageObject == null)
            {
                WriteError("No image found for data key '" + storageKey + "'.");
                return;
            }

            byte[] imageBytes = imageObject as byte[];
            if (imageBytes != null)
            {
                WriteImageBytes(imageBytes, context);
            }
            else
            {
                Image image = imageObject as Image;
                if (image != null)
                    WriteImage(image, context);
            }
        }

        #endregion

        private static void WriteImageBytes(byte[] img, HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = ContentType;
            context.Response.OutputStream.Write(img, 0, img.Length);
            context.Response.Complete();
        }

        private static void WriteImage(Image img, HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = ContentType;
            img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            context.Response.Complete();
        }

        private static void WriteError(string message)
        {
            throw new HttpException(404, "Error retrieving dynamic image. " + message);
        }
    }
}