#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     The HttpModule that installs the response filter
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  	<httpModules>
    /// 			<add name="SafeMailLinkModule" type="Loom.Web.SafeMailLinkModule, Loom.Web" />
    /// 		</httpModules>
    ///  ]]>
    ///  </code>
    /// </example>
    public class SafeMailLinkModule : IHttpModule
    {
        private const string InstalledKey = "Loom.Web.SafeMailLinkModule";

        #region IHttpModule Members

        /// <summary>
        ///     Inits the specified context.
        /// </summary>
        /// <param name="context">Context.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void Init(HttpApplication context)
        {
            context.ReleaseRequestState += HandleReleaseRequestState;
            context.PreSendRequestHeaders += HandleReleaseRequestState;
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose() { }

        #endregion

        private static void HandleReleaseRequestState(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication) sender).Context;

            // if the filter has not been installed yet, create a new instance 
            // of it, and install it
            if (context.Items.Contains(InstalledKey))
                return;

            HttpResponse response = context.Response;

            if (response.ContentType.Equals("text/html"))
                response.Filter = new SafeMailLinkStream(response.Filter);

            context.Items.Add(InstalledKey, new object());
        }
    }
}