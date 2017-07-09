#region Using Directives

using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Loom.Web.Portal.IO;

#endregion

namespace Loom.Web.Portal
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <example>
    ///     The following web.config entries are required in the system.webserver section.
    ///     <code>
    /// &lt;handlers accessPolicy="Read, Execute, Script"&gt;<br />
    ///     &lt;add name="ScriptVirtualFile" path="*/scriptresource/*.js" verb="GET" type="System.Web.StaticFileHandler, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" /&gt;<br />
    ///     &lt;add name="StyleVirtualFile" path="*/styleresource/*.css" verb="GET" type="System.Web.StaticFileHandler, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" /&gt;<br />
    ///     &lt;add name="ImageVirtualFile" path="*/imageresource/*.*" verb="GET" type="System.Web.StaticFileHandler, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" /&gt;<br />
    ///     &lt;add name="ModuleVirtualFile" path="*/moduleresource/*.ascx" verb="GET" type="System.Web.StaticFileHandler, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" /&gt;<br />
    /// &lt;/handlers&gt;<br />
    /// </code>
    /// </example>
    public sealed class PortalRequestModule : IHttpModule
    {
        private static readonly DateTime LastModified;

        /// <summary>
        ///     Initializes the portal's <see cref="VirtualPathProvider" />s.
        /// </summary>
        /// <remarks>
        ///     The static constructor on the <see cref="PortalRequestModule" /> ensures that
        ///     the <see cref="VirtualPathProvider" />s are initialized only once and on the
        ///     first request to the portal application.
        /// </remarks>
        static PortalRequestModule()
        {
            // Virtual path providers are executed in the opposite order they are registered.
            // The order here reflects the most likely order that requests will be received,
            // so it should be the most performant order. For example, routes are requested 
            // and parsed, then modules are loaded. Next CSS files are loaded because they are 
            // typically at the top of the page. Images are then loaded in the body of the
            // page. Finally, in this framework the script resources are usually at the bottom 
            // of the page and are loaded last.
            HttpExtensions.RegisterVirtualPathProvider(new ScriptProvider());
            HttpExtensions.RegisterVirtualPathProvider(new ImageProvider());
            HttpExtensions.RegisterVirtualPathProvider(new StyleProvider());
            HttpExtensions.RegisterVirtualPathProvider(new ModuleProvider());
            HttpExtensions.RegisterVirtualPathProvider(new RoutePathProvider());

            LastModified = File.GetLastWriteTimeUtc(HttpContext.Current.Server.MapPath("~/bin/Loom.Web.Portal.dll"));
        }

        #region IHttpModule Members

        public void Init(HttpApplication app)
        {
            if (app.Context.Error != null)
                throw new HttpException(500, SR.ExceptionInHttpPipeline, app.Context.Error);

            PortalContext.Initialize(new ContextSettings(app, LastModified));
        }

        public void Dispose() { }

        #endregion
    }
}