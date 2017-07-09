#region Using Directives

using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class ScriptProvider : VirtualPathProvider
    {
        #region Constants

#if DEBUG
        private const string Extension = ".js";
#endif
#if !DEBUG
        private const string Extension = ".min.js";
#endif
        private const string ScriptResourcePrefix = "Loom.Web.Portal.Resources.Script.";
        private const string ResourcePathPrefix = "/scriptresource";

        private const string JQueryResourcePrefix = "Loom.Web.Portal.Resources.Script.jquery-";
        private const string JQueryFileName = "jquery.js";
        private const string ModernizrResourcePrefix = "Loom.Web.Portal.Resources.Script.modernizr-";
        private const string ModernizrFileName = "modernizr.js";

        #endregion

        public override bool FileExists(string virtualPath)
        {
            return IsScriptResource() || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (!IsScriptResource())
                return base.GetFile(virtualPath);

            string fileName = Path.GetFileName(virtualPath);
            switch (fileName)
            {
                case JQueryFileName:
                {
                    ScriptSetting jQuery = PortalContext.Current.JQuery;
                    return new ResourceFile(virtualPath, JQueryResourcePrefix + jQuery.Version + Extension, "Loom.Web.Portal");
                }
                case ModernizrFileName:
                {
                    ScriptSetting modernizer = PortalContext.Current.Modernizer;
                    return new ResourceFile(virtualPath, ModernizrResourcePrefix + modernizer.Version + Extension, "Loom.Web.Portal");
                }
            }

            return new ResourceFile(virtualPath, ScriptResourcePrefix + Path.GetFileNameWithoutExtension(fileName) + Extension, "Loom.Web.Portal");
        }

        private static bool IsScriptResource()
        {
            return PortalContext.Current.Request.Path.StartsWith(ResourcePathPrefix, StringComparison.OrdinalIgnoreCase);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (!IsScriptResource())
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

            PortalTrace.Write("ScriptProvider", "GetCacheDependency", "Creating new CacheDependency for path '{0}'.", virtualPath);
            return new CacheDependency(HttpContext.Current.Server.MapPath("~/bin/Loom.Web.Portal.dll"));
        }
    }
}