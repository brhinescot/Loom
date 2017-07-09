#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     HttpModule to prevent caching
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  	<httpModules>
    /// 			<add name="NoCacheModule" type="Loom.Web.NoCacheModule, Loom.Web" />
    /// 		</httpModules>
    ///  ]]>
    ///  </code>
    /// </example>
    public class NoCacheModule : IHttpModule
    {
        private readonly EventHandler onEndRequest;

        /// <summary>
        ///     Creates a new <see cref="NoCacheModule" /> instance.
        /// </summary>
        public NoCacheModule()
        {
            onEndRequest = HandleEndRequest;
        }

        #region IHttpModule Members

        /// <summary>
        ///     Peaks into the Initialization of the Http request.
        /// </summary>
        /// <param name="context">The current executing context.</param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += onEndRequest;
        }

        /// <summary>
        ///     Implementation of the dispose method.
        /// </summary>
        public void Dispose() { }

        #endregion

        private static void HandleEndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication) sender;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-100);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "private");
            context.Response.CacheControl = "no-cache";
        }
    }
}