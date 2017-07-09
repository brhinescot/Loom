#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;

#endregion

namespace Loom.Web.IO
{
    /// <summary>
    ///     A class for managing the upload and download of files over the web.
    /// </summary>
    [Guid("4AA66CA6-7704-4EE8-AAB1-729C3B9FDB59")]
    public static class WebFile
    {
        private static readonly StringMapping Mapping = new ExplictStringMapping
        {
            {".htm", "text/html"},
            {".html", "text/html"},
            {".txt", "text/plain"},
            {".xml", "text/xml"},
            {".css", "text/css"},
            {".rtf", "application/richtext"},
            {".exe", "application/octet-stream"},
            {".pdf", "application/pdf"},
            {".zip", "application/zip"},
            {".tgz", "application/x-compressed"},
            {".mp3", "audio/x-mpeg"},
            {".wma", "audio/x-ms-wma"},
            {".wav", "audio/x-wav"},
            {".mid", "audio/mid"},
            {".rmi", "audio/mid"},
            {".fla", "audio/flac"},
            {".flac", "audio/flac"},
            {".wmv", "video/x-ms-wmv"},
            {".asf", "video/x-ms-asf"},
            {".asx", "video/x-ms-asx"},
            {".mpeg", "video/mpeg"},
            {".mpg", "video/mpeg"},
            {".mp2", "video/mpeg"},
            {".mpa", "video/mpeg"},
            {".mpe", "video/mpeg"},
            {".mpv2", "video/mpeg"},
            {".avi", "video/avi"},
            {".mov", "video/quicktime"},
            {".qt", "video/quicktime"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".png", "image/png"},
            {".gif", "image/gif"},
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".bmp", "image/bmp"},
            {".wmf", "image/x-wmf"},
            {".ico", "image/vnd.microsoft.icon"},
            {".doc", "application/msword"},
            {".dot", "application/msword"},
            {".xls", "application/vnd.ms-excel"},
            {".xlm", "application/vnd.ms-excel"},
            {".xlc", "application/vnd.ms-excel"},
            {".xla", "application/vnd.ms-excel"},
            {".xlt", "application/vnd.ms-excel"},
            {".xlw", "application/vnd.ms-excel"},
            {".pot", "application/ms-powerpoint"},
            {".pps", "application/ms-powerpoint"},
            {".ppt", "application/ms-powerpoint"},
            {".mdb", "application/ms-access"},
            {".pub", "application/x-mspublisher"},
            {".mpp", "application/vnd.ms-project"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"}
        };

        /// <summary>
        ///     Save an uploaded file to the server
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="path"></param>
        /// <returns>A string containing the unique file name generated on the server.</returns>
        public static string Upload(HttpPostedFile postedFile, string path)
        {
            return PrivateUpload(postedFile, path);
        }

        /// <summary>
        ///     Streams the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        public static void Stream(string virtualPath, string friendlyName = null)
        {
            PrivateStream(virtualPath, friendlyName);
        }

        /// <summary>
        ///     Streams the specified file to the client browser.
        /// </summary>
        /// <param name="virtualPath">The path to the download file.</param>
        /// <param name="friendlyName">An optional name for the downloaded file.</param>
        /// <param name="compressionRation">The ratio to compress the files.</param>
        public static void Download(string virtualPath, string friendlyName = null, int compressionRation = -1)
        {
            PrivateDownload(virtualPath, friendlyName, compressionRation > -1, compressionRation);
        }

        /// <summary>
        ///     Streams the specified file to the client browser.
        /// </summary>
        /// <param name="virtualPath">The path to the download file.</param>
        /// <param name="friendlyName">An optional name for the downloaded file.</param>
        /// <param name="compressionRation">The ratio to compress the files.</param>
        /// <param name="additionalFileVirtualPaths">
        ///     An array of paths to additional files to
        ///     add to the zip file.
        /// </param>
        public static void Download(string virtualPath, string friendlyName, int compressionRation, params string[] additionalFileVirtualPaths)
        {
            PrivateDownload(virtualPath, friendlyName, true, compressionRation, additionalFileVirtualPaths);
        }

        /// <summary>
        ///     Returns the string representation of a file's mime type
        ///     based on its extension.
        /// </summary>
        /// <param name="extension">
        ///     The extension to get the mime
        ///     type for.
        /// </param>
        /// <returns>A string representing the mime type.</returns>
        public static string GetMimeType(string extension)
        {
            return Mapping[extension];
        }

        /// <summary>
        ///     Creates a unique file name from a given file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>A string representing the unique file name created.</returns>
        public static string MakeUniqueFileName(string path)
        {
            return PrivateMakeUniqueFileName(path);
        }

        /// <summary>
        ///     Maps the virtual paths in a string array to their physical paths.
        /// </summary>
        /// <param name="virtualPathArray">A string array containing the virtual paths to map.</param>
        /// <returns>A string array containing the physical paths to the files.</returns>
        public static string[] MapVirtualPathArray(string[] virtualPathArray)
        {
            return PrivateMapVirtualPaths(virtualPathArray);
        }

        private static string PrivateUpload(HttpPostedFile postedFile, string path)
        {
            Argument.Assert.IsNotNull(postedFile, nameof(postedFile));
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            if (postedFile.ContentLength == 0)
                throw new PostedFileSizeException(SR.ExceptionPostedFileSizeZeroLength(postedFile.FileName));

            string fileName;
            string directory;
            if (!Path.HasExtension(path))
            {
                fileName = MakeUniqueFileName(postedFile.FileName);
                directory = path;
            }
            else
            {
                fileName = Path.GetFileName(path);
                directory = Path.GetDirectoryName(path);
            }

            postedFile.SaveAs(Path.Combine(directory, fileName));

            return fileName;
        }

        private static void PrivateStream(string virtualPath, string friendlyName)
        {
            Argument.Assert.IsNotNull(virtualPath, nameof(virtualPath));

            HttpContext context = HttpContext.Current;
            FileInfo info = new FileInfo(context.Server.MapPath(virtualPath));
            if (!info.Exists)
                throw new FileNotFoundException("Unable to find file " + info.FullName, info.FullName);

            if (null == friendlyName)
                friendlyName = Path.GetFileName(virtualPath);

            string ext = Path.GetExtension(virtualPath);
            string type = GetMimeType(ext);

            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.AppendHeader("Content-Type", type);
            context.Response.AppendHeader("Content-Disposition", "inline; filename=" + context.Server.UrlEncode(friendlyName));
            context.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            context.Response.AppendHeader("Content-Length", info.Length.ToString(NumberFormatInfo.InvariantInfo));
            context.Response.TransmitFile(virtualPath);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        private static void PrivateDownload(string virtualPath, string friendlyName, bool zipFile, int compressionRation, params string[] additionalFiles)
        {
            Argument.Assert.IsNotNull(virtualPath, nameof(virtualPath));

            HttpContext context = HttpContext.Current;
            FileInfo info = new FileInfo(context.Server.MapPath(virtualPath));

            if (!info.Exists)
                throw new FileNotFoundException("Unable to find file " + info.FullName, info.FullName);

            if (null == friendlyName)
                friendlyName = Path.GetFileName(virtualPath);

            // Clone the file array to prevent tampering after method call
            //
            string[] safeAdditionalFiles = (string[]) additionalFiles.Clone();

            string type;
            if (zipFile)
            {
                type = GetMimeType(".zip");
                friendlyName = Path.ChangeExtension(Path.GetFileName(friendlyName), "zip");
            }
            else
            {
                type = GetMimeType(Path.GetExtension(virtualPath));
            }

            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.AppendHeader("Content-Type", type);
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlEncode(friendlyName));
            context.Response.AppendHeader("Content-Transfer-Encoding", "binary");

            context.Response.AppendHeader("Content-Length", info.Length.ToString(NumberFormatInfo.InvariantInfo));
            context.Response.TransmitFile(virtualPath);

            context.Response.Complete();
        }

        private static string PrivateMakeUniqueFileName(string path)
        {
            FileInfo info = new FileInfo(path);
            string returnString = info.Name.Replace(" ", "_").Replace("!", string.Empty);

            returnString = DateTime.Now.ToString("yyyyMMddhhmmssfffffff-", DateTimeFormatInfo.InvariantInfo) +
                           returnString;

            return returnString;
        }

        private static string[] PrivateMapVirtualPaths(string[] virtualPaths)
        {
            string[] mappedPaths = new string[virtualPaths.Length];

            for (int i = 0; i < mappedPaths.Length; i++)
                mappedPaths[i] = HttpContext.Current.Server.MapPath(virtualPaths[i]);

            return mappedPaths;
        }
    }
}