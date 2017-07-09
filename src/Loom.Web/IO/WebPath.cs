#region Using Directives

using System;
using System.IO;
using System.Web;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.IO
{
    /// <summary>
    ///     Summary description for WebPath.
    /// </summary>
    public static class WebPath
    {
        public static string GetPathWithoutQuerystring(string virtualPath)
        {
            Argument.Assert.IsNotNullOrEmpty(virtualPath, nameof(virtualPath));

            int length = virtualPath.IndexOf("?", StringComparison.Ordinal);
            return length > 0 ? virtualPath.Substring(0, length) : virtualPath;
        }

        /// <summary>
        ///     Gets the directory.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetDirectory(string virtualPath)
        {
            return PrivateGetDirectory(virtualPath);
        }

        /// <summary>
        ///     Gets the extension.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetExtension(string virtualPath)
        {
            return PrivateGetExtension(virtualPath);
        }

        /// <summary>
        ///     Gets the name of the file.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetFileName(string virtualPath)
        {
            return PrivateGetFileName(virtualPath);
        }

        /// <summary>
        ///     Gets the file name without extension.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string virtualPath)
        {
            return PrivateGetFileNameWithoutExtension(virtualPath);
        }

        /// <summary>
        ///     Changes the extension.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <param name="extension">Extension.</param>
        /// <returns></returns>
        public static string ChangeExtension(string virtualPath, string extension)
        {
            return PrivateChangeExtension(virtualPath, extension);
        }

        /// <summary>
        ///     Gets the full path.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetFullPath(string virtualPath)
        {
            Argument.Assert.IsNotNull(virtualPath, "virtualPath");
            return PrivateGetFullPath(virtualPath);
        }

        /// <summary>
        ///     Determines whether the specified virtualPath has an extension.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns>
        ///     <c>true</c> if the specified virtualPath has an extension; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasExtension(string virtualPath)
        {
            return virtualPath != null && PrivateHasExtension(virtualPath);
        }

        /// <summary>
        ///     Gets the name of the leaf.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetLeafName(string virtualPath)
        {
            Argument.Assert.IsNotNull(virtualPath, "virtualPath");
            return PrivateGetLeafName(virtualPath);
        }

        /// <summary>
        ///     Gets the name of the branch.
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        /// <returns></returns>
        public static string GetBranchName(string virtualPath)
        {
            Argument.Assert.IsNotNull(virtualPath, "virtualPath");
            return PrivateGetBranchName(virtualPath);
        }

        /// <summary>
        ///     Combines the specified virtual path1 and virtualPath2.
        /// </summary>
        /// <param name="virtualPath1">Virtual path1.</param>
        /// <param name="virtualPath2">Virtual path2.</param>
        /// <returns></returns>
        public static string Combine(string virtualPath1, string virtualPath2)
        {
            Argument.Assert.IsNotNull(virtualPath1, "virtualPath1");
            Argument.Assert.IsNotNull(virtualPath2, "virtualPath2");
            return PrivateCombine(virtualPath1, virtualPath2);
        }

        /// <summary>
        ///     Retrieves and html encodes post paramaters from a request.
        /// </summary>
        /// <param name="request">A HttpRequest object to take the paramater from.</param>
        /// <param name="paramaterName">The name of the paramater you want to retrieve.</param>
        /// <returns>An html encoded string that represents the paramater or null if the paramater does not exist.</returns>
        public static string GetSafeParamater(HttpRequest request, string paramaterName)
        {
            Argument.Assert.IsNotNull(request, "request");

            string value = request.Form[paramaterName];
            return value != null ? AntiXss.HtmlEncode(value) : string.Empty;
        }

        /// <summary>
        ///     Translates an ASP.NET path like /myapp/subdir/page.aspx
        ///     into an application relative path: subdir/page.aspx. The
        ///     path returned is based of the application base and
        ///     starts either with a subdirectory or page name (ie. no ~)
        ///     The path is turned into all lower case.
        /// </summary>
        /// <param name="logicalPath">A logical, server root relative path (ie. /myapp/subdir/page.aspx)</param>
        /// <returns>Application relative path (ie. subdir/page.aspx)</returns>
        public static string GetAppRelativePath(string logicalPath)
        {
            logicalPath = logicalPath.ToLower();
            string appPath = HttpContext.Current.Request.ApplicationPath.ToLower();
            if (appPath != "/")
                appPath += "/";
            else
                // Root web relative path is empty - strip off leading slash
                return logicalPath.TrimStart('/');

            return logicalPath.Replace(appPath, "");
        }

        public static string GetVirtualPathFromPhysicalPath(string physicalPath)
        {
            Argument.Assert.IsNotNull(physicalPath, nameof(physicalPath));

            HttpRequest request = HttpContext.Current.Request;

            string physicalApplicationPath = request.PhysicalApplicationPath;
            if (physicalApplicationPath != null)
                return "~\\" + physicalPath.Substring(physicalApplicationPath.Length);
            return null;
        }

        public static string GetApplicationPathFromPhysicalPath(string physicalPath)
        {
            Argument.Assert.IsNotNull(physicalPath, nameof(physicalPath));

            return physicalPath.Substring(HttpContext.Current.Request.PhysicalApplicationPath.Length).Replace("\\", "/");
        }

        public static string FullyQualify(string url)
        {
            Argument.Assert.IsNotNull(url, nameof(url));

            string appPath = VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath);
            url = url.Replace("~/", appPath);

            return VirtualPathUtility.IsAbsolute(url) ? HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + url : url;
        }

        public static string AppendTrailingSlash(string path)
        {
            if (path == null)
                return string.Empty;

            if (path.Length == 0)
                return path;

            if (!Path.HasExtension(path) && !path.EndsWith("/"))
                path = path + '/';

            return path;
        }

        public static bool IsAppRelative(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, "path");

            if (path.Length >= 1)
                return path[0] == '~';

            return false;
        }

        public static bool IsDirectoryRelative(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, "path");

            if (IsAppRelative(path))
                return false;

            if (path.IndexOf(":", StringComparison.Ordinal) != -1)
                return false;

            return !IsAbsolute(path);
        }

        public static bool IsAbsolute(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, "path");

            if (path[0] != '/')
                return path[0] == '\\';

            return true;
        }

        internal static bool IsValidVirtualPathWithoutProtocol(string path)
        {
            Argument.Assert.IsNotNull(path, "path");

            return path.IndexOf(":", StringComparison.Ordinal) == -1;
        }

        private static string PrivateGetDirectory(string path)
        {
            string directory = AppendTrailingSlash(path);
            directory = ReplacePathSeperator(directory);

            if (Compare.IsNullOrEmpty(directory))
                throw new ArgumentException(SR.ExceptionEmptyPathNoDirectory);

            if (!directory.StartsWith("/"))
                throw new ArgumentException(SR.ExceptionPathMustBeRooted);

            directory = directory.Substring(0, directory.LastIndexOf('/'));
            return directory.Length == 0 ? "/" : directory;
        }

        private static string PrivateGetExtension(string virtualPath)
        {
            return Path.GetExtension(virtualPath);
        }

        private static string PrivateGetFileName(string virtualPath)
        {
            return Path.GetFileName(virtualPath);
        }

        private static string PrivateGetFileNameWithoutExtension(string virtualPath)
        {
            return Path.GetFileNameWithoutExtension(virtualPath);
        }

        private static string PrivateChangeExtension(string virtualPath, string extension)
        {
            string returnString = Path.ChangeExtension(virtualPath, extension);
            return ReplacePathSeperator(returnString);
        }

        private static string PrivateGetFullPath(string virtualPath)
        {
            string returnString = Path.GetFullPath(virtualPath);
            return ReplacePathSeperator(returnString).Substring(2);
        }

        private static bool PrivateHasExtension(string virtualPath)
        {
            return Path.HasExtension(virtualPath);
        }

        private static string PrivateGetLeafName(string path)
        {
            string returnString = path;
            if (returnString.IndexOf("?", StringComparison.Ordinal) > 0)
                returnString = returnString.Substring(0, returnString.IndexOf("?", StringComparison.Ordinal));

            if (!Path.HasExtension(returnString))
            {
                returnString = GetDirectory(returnString);
                if (returnString.LastIndexOf("/", StringComparison.Ordinal) == 0)
                    returnString = string.Empty;
            }
            else
            {
                returnString = GetFileName(returnString);
            }

            if (returnString.LastIndexOf("/", StringComparison.Ordinal) > 0)
                returnString = returnString.Substring(returnString.LastIndexOf("/", StringComparison.Ordinal) + 1);
            return returnString.TrimStart('/');
        }

        private static string PrivateGetBranchName(string path)
        {
            string returnString = path;

            if (!IsValidVirtualPathWithoutProtocol(returnString))
                returnString = returnString.Substring(returnString.IndexOf("/", returnString.IndexOf(":", StringComparison.Ordinal) + 3, StringComparison.Ordinal));

            if (!IsAbsolute(returnString))
                returnString = "/" + returnString;

            returnString = returnString.TrimEnd('/');
            if (returnString.LastIndexOf("/", StringComparison.Ordinal) > 0)
                returnString = returnString.Substring(0, returnString.LastIndexOf("/", StringComparison.Ordinal));
            return returnString;
        }

        private static string PrivateCombine(string virtualPath1, string virtualPath2)
        {
            string path1 = virtualPath1;
            string path2 = virtualPath2;

            if (!path1.EndsWith("/"))
                path1 = path1 + "/";
            path2 = path2.TrimStart('/');
            return ReplacePathSeperator(path1 + path2);
        }

        private static string ReplacePathSeperator(string virtualPath)
        {
            return virtualPath != null ? virtualPath.Replace(@"\", @"/") : string.Empty;
        }
    }
}