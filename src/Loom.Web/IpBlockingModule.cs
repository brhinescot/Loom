#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Summary description for IPBlockingModule.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the
    ///     web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///      <system.web>
    ///          <httpModules>
    /// 	            <add name="IpBlockingModule" type="Loom.Web.IpBlockingModule, Loom.Web" />
    /// 	        </httpModules>
    ///      ...
    ///  ]]>
    ///  </code>
    /// </example>
    public class IpBlockingModule : IHttpModule
    {
        private FileListCache fileListCache;

        #region IHttpModule Members

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the module
        ///     that implements <see cref="IHttpModule"></see>.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        ///     Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="HttpApplication" /> that
        ///     provides access to the methods, properties, and events common to
        ///     all application objects within an ASP.NET application
        /// </param>
        public void Init(HttpApplication context)
        {
            string path = VirtualPathUtility.ToAbsolute("~/SiteConfig/blockedips.config");
            fileListCache = new FileListCache(path);
            context.BeginRequest += HandleBeginRequest;
        }

        #endregion

        private void HandleBeginRequest(object sender, EventArgs args)
        {
            HttpApplication context = sender as HttpApplication;
            if (context == null)
                return;

            string ipAddress = context.Context.Request.UserHostAddress;
            if (Compare.IsNullOrEmpty(ipAddress))
                return;

            if (fileListCache == null || !fileListCache.Contains(ipAddress))
                return;

            context.Context.Response.StatusCode = 403;
            context.Context.Response.SubStatusCode = 6;
            context.Context.Response.StatusDescription = "Temporary ban due to potential scripting attack.";
            context.Context.Response.SuppressContent = true;
            context.Context.Response.Complete();
        }
    }
}