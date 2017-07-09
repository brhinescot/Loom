#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Configuration;
using Loom.Web.Portal.Configuration;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.Http;
using Loom.Web.Portal.IO;
using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal
{
    /// <summary>
    ///     Encapsulates all portal specific information about an individual portal request.
    /// </summary>
    public sealed class PortalContext : ContextBase<PortalContext>, IPortalContext, IDisposable
    {
        private const string ControllerActionToken = "action";
        private const string ControllerNameToken = "controller";
        private const string ImageDefaultKey = "__imageDefault";
        private const string ImagesNamespace = "Loom.Web.Portal.Resources.Images";
        private const string ModuleDefaultKey = "__moduleDefault";
        private const string ModulesNamespace = "Loom.Web.Portal.Resources.Modules";
        private const string ResourceAssemblyName = "Loom.Web.Portal";
        private const string RootPath = "/";
        private const string ScriptDefaultKey = "__scriptDefault";
        private const string ScriptNamespace = "Loom.Web.Portal.Resources.Script";
        private const string StyleDefaultKey = "__styleDefault";
        private const string StyleNamespace = "Loom.Web.Portal.Resources.Style";

        private readonly PortalSettingsSection config = GetConfig();
        private readonly VirtualResources virtualResources = new VirtualResources();

        private PortalContext(ContextSettings settings)
        {
            HttpApplication app = settings.HttpApplication;

            app.BeginRequest += (sender, args) => BeginRequest(app.Context);
            app.AuthorizeRequest += (sender, args) => AuthorizeRequest();
//            app.EndRequest += (sender, args) => Dispose();
        }

        internal SectionData Section { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            IPortalContext portalContext = PopObject();
            if (portalContext != this)
                throw new InvalidOperationException("The object removed from the context stack is not the current object.");
        }

        #endregion

        #region IPortalContext Members

        public ScriptSetting Modernizer { get; private set; }

        /// <summary>
        ///     Gets an <see cref="IPortalRequest" />
        ///     containing information about the portal request and route that matched the
        ///     current <see cref="Path" />.
        /// </summary>
        public IPortalRequest Request { get; private set; }

        /// <summary>
        ///     Gets an <see cref="IPortalResponse" />
        ///     object that can be used to modify the response sent to the client.
        /// </summary>
        public IPortalResponse Response { get; private set; }

        public ScriptSetting JQuery { get; private set; }

        public bool SetupMode { get; private set; }

        public IHttpContext HttpContext { get; private set; }

        public DateTime LastModified { get; private set; }

        PortalSettingsSection IPortalContext.Config
        {
            [DebuggerStepThrough] get { return config; }
        }

        public VirtualResourceData GetVirtualResourceData(string name)
        {
            if (virtualResources == null || virtualResources.Count == 0)
                return null;

            return virtualResources.GetData(name);
        }

        #endregion

        public static void Initialize(ContextSettings settings)
        {
            PortalContext context = new PortalContext(settings);

            context.SetVirtualResourceData(ImageDefaultKey, new VirtualResourceData(ImagesNamespace, ResourceAssemblyName));
            context.SetVirtualResourceData(StyleDefaultKey, new VirtualResourceData(StyleNamespace, ResourceAssemblyName));
            context.SetVirtualResourceData(ScriptDefaultKey, new VirtualResourceData(ScriptNamespace, ResourceAssemblyName));
            context.SetVirtualResourceData(ModuleDefaultKey, new VirtualResourceData(ModulesNamespace, ResourceAssemblyName));
            context.SetVirtualResourceData("Admin", new VirtualResourceData(ModulesNamespace + ".Admin", ResourceAssemblyName));
        }

        private void BeginRequest(HttpContext app)
        {
            PushObject(this);

            HttpContext = new WrappedHttpContext(app);

            string currentExecutionFilePath = HttpContext.Request.CurrentExecutionFilePath;
            bool isPortalRoute = !Path.HasExtension(currentExecutionFilePath);
            string virtualPath;
            string portalPath;

            int appPathLength = HttpContext.Request.ApplicationPath.Length;
            if (isPortalRoute)
            {
                if (currentExecutionFilePath.Length > 1 && currentExecutionFilePath.EndsWith("/"))
                    HttpContext.Response.RedirectPermanent(currentExecutionFilePath.TrimEnd('/'));

                virtualPath = VirtualPathUtility.AppendTrailingSlash(currentExecutionFilePath) + RoutePathProvider.VirtualPageName;
                portalPath = appPathLength > 1 ? currentExecutionFilePath.Substring(appPathLength) : currentExecutionFilePath;

                if (Compare.IsNullOrEmpty(portalPath))
                    portalPath = RootPath;
            }
            else
            {
                virtualPath = currentExecutionFilePath;
                portalPath = appPathLength > 1 ? virtualPath.Substring(appPathLength) : virtualPath;
            }

            CreateContext(HttpContext, portalPath, virtualPath, isPortalRoute);

            if (!Request.IsPortalRoute || Request.Result == null)
                return;

            PortalTrace.Write("PortalRequestModule", "OnBeginRequest", "Rewriting portal path '{0}' to '{1}'.", Request.Path, Request.VirtualPath);

            HttpContext.RewritePath(Request.VirtualPath);
        }

        private void AuthorizeRequest()
        {
            //TODO: Handle null Request.Result

            PortalTrace.Warn("PortalContext", "BeginRequest", "Request.Result is null.");
            if (Request.Result == null)
//#if (!DEBUG)                
                return; //throw new HttpException(404, null);
//#else

//#endif
            if (Request.IsPortalRoute && !(Request.Result is IViewResult))
                Request.Result.Execute(new ControllerContext(this));
        }

        private static void AddConfigOptions(PortalContext context, PortalRequest request)
        {
            context.SetupMode = context.config.Setup;
            context.JQuery = new ScriptSetting(context.config.JQuery.Version);
            context.Modernizer = new ScriptSetting(context.config.Modernizer.Version);

            request.AllowPhysicalPages = context.config.Routes.AllowPhysicalPages;
        }

        private static void AddRouteContext(PortalRequest request, PortalResponse response, SectionData section)
        {
            PortalTrace.Write("PortalRequestModule", "InitializeRouteContext", "Begin InitializeRouteContext.");

            if (section == null)
                throw new InvalidOperationException("No section found.");

            List<Route> routes = section.GetMatchedRoutes(request.Path);

            if (routes.Count > 0)
                AddRouteData(request, response, routes[0]);

            PortalTrace.Write("PortalRequestModule", "InitializeRouteContext", "End InitializeRouteContext.");
        }

        private static void AddRouteData(PortalRequest request, PortalResponse response, Route route)
        {
            if (route == null)
                return;

            request.MergeTokens(route.GetTokens(request.Path));
            NameValueCollection queryString = request.QueryString;
            if (queryString.Count > 0)
            {
                RouteTokens queryTokens = new RouteTokens();
                foreach (string s in queryString)
                    queryTokens.Add(s, queryString[s]);
                request.MergeTokens(queryTokens);
            }

            request.ControllerName = route.ControllerName ?? request.GetTokenValue(ControllerNameToken);

            foreach (TileDefinition tile in route.Tiles)
                response.Tiles.Add(tile);

            if (!Compare.IsNullOrEmpty(route.PageTitle))
                request.Title = FormattableObject.ToString(request.Tokens.ToDictonary(), route.PageTitle, null);
        }

        private static void AddControllerResult(PortalContext context, PortalRequest request, SectionData section)
        {
            ControllerMetaWrapper controller = section.GetControllerMeta(request.ControllerName);
            string actionName = controller.Name == "Home" ? request.GetTokenValue(ControllerNameToken) : request.GetTokenValue(ControllerActionToken);

            if (actionName == null || actionName == controller.Name)
                actionName = "Index";

            request.ControllerName = controller.Name;
            request.ActionName = actionName;
            request.Result = controller.Execute(context);
        }

        private static PortalSettingsSection GetConfig()
        {
            return (PortalSettingsSection) WebConfigurationManager.GetSection("portalSettings") ?? new PortalSettingsSection();
        }

        private void CreateContext(IHttpContext httpContext, string portalPath, string virtualPath, bool isPortalRoute)
        {
            foreach (VirtualResourcesElement resource in config.VirtualResources)
                SetVirtualResourceData(resource.Name, new VirtualResourceData(resource.Namespace, resource.Assembly));

            PortalRequest request = CreateRequest(httpContext, portalPath, virtualPath, isPortalRoute);
            PortalResponse response = new PortalResponse(this);

            Request = request;
            Response = response;

            AddConfigOptions(this, request);

            if (isPortalRoute)
            {
                int firstSlashIndex = portalPath.IndexOf('/', 1);
                int length = firstSlashIndex >= 0 ? firstSlashIndex : portalPath.Length;
                request.TenantName = TenantCache.GetTenantName(httpContext.Request.Url.Host);
                if (!Compare.IsNullOrEmpty(request.TenantName))
                    request.VirtualPath = "/Tenants/" + request.TenantName + request.VirtualPath;

                Section = TenantCache.GetSectionOrDefault(request.TenantName, portalPath.Length == 1 ? null : portalPath.Substring(1, length - 1));
                AddRouteContext(request, response, Section);
                AddControllerResult(this, request, Section);
            }

            PortalTrace.Write("PortalRequestModule", "CreateContext", "PortalContext created.");
            PortalTrace.Write("PortalRequestModule", "CreateContext", "Setup={0}, AllowPhysicalPages={1}, jQuery Version={2}", config.Setup, config.Routes.AllowPhysicalPages, config.JQuery.Version);
        }

        private PortalRequest CreateRequest(IHttpContext httpContext, string portalPath, string virtualPath, bool isPortalRoute)
        {
            PortalRequest request = new PortalRequest
            {
                Path = portalPath,
                VirtualPath = virtualPath,
                IsPortalRoute = isPortalRoute,
                AllowPhysicalPages = config.Routes.AllowPhysicalPages,
                IsXmlHttpRequest = Headers.GetRequestedWith(httpContext) == HeaderValue.XmlHttpRequest
            };
            return request;
        }

        internal void SetVirtualResourceData(string name, VirtualResourceData data)
        {
            virtualResources.SetData(name, data);
        }

        #region Nested type: Headers

        private struct Headers
        {
            #region Constants

            private const string RequestedWith = "X-Requested-With";

            #endregion

            internal static string GetRequestedWith(IHttpContext httpContext)
            {
                return httpContext.Request.Headers[RequestedWith];
            }
        }

        #endregion

        #region Nested type: HeaderValue

        private struct HeaderValue
        {
            #region Constants

            internal const string XmlHttpRequest = "XMLHttpRequest";

            #endregion
        }

        #endregion
    }
}