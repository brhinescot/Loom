#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Loom.Web.Portal.Configuration;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.Data;
using Loom.Web.Portal.Data.Portal;

#endregion

namespace Loom.Web.Portal.Routing
{
    internal static class TenantCache
    {
        private const string ConfigSectionName = "portalSettings";

        private static readonly Dictionary<string, string> TenantHostCache = new Dictionary<string, string>();
        private static readonly Dictionary<string, TenantData> TenantNameCache = RegisterAllSectionsEx();

        internal static string GetTenantName(string hostName)
        {
            return TenantHostCache.ContainsKey(hostName) ? TenantHostCache[hostName] : TenantNameCache["DefaultTenant"].Name;
        }

        internal static SectionData GetSectionOrDefault(string tenantName = null, string sectionName = DefaultSection.DefaultName)
        {
            sectionName = sectionName ?? DefaultSection.DefaultName;
            tenantName = tenantName ?? "DefaultTenant";

            if (!TenantNameCache.ContainsKey(tenantName))
                throw new HttpException(404, null);

            TenantData tenant = TenantNameCache[tenantName];
            return tenant.Sections.ContainsKey(sectionName) ? tenant.Sections[sectionName] : tenant.Sections[DefaultSection.DefaultName];
        }

        private static void InitializeDatabaseRoutes(Dictionary<string, TenantData> tenantDataCache)
        {
            List<Route> primaryRoutes;
            List<Route> secondaryRoutes;
            List<RouteModule> routeModules;
            Dictionary<int, Module> modules;

            PortalTrace.Write("Routes", "InitializeDatabaseRoutes", "Begin initializing primary routes");
            using (PortalDataSession session = new PortalDataSession("portal"))
            {
                PortalTrace.Write("Routes", "InitializeDatabaseRoutes", " - Database session opened.");

                primaryRoutes = session.Routes
                    .Select(Data.Portal.Route.Columns.Name.As("RouteName"),
                        Data.Portal.Route.Columns.RouteId.As("Id"),
                        Data.Portal.Route.Columns.Expression,
                        Data.Portal.Route.Columns.PageTitle,
                        Data.Portal.Route.Columns.Controller.As("ControllerName"),
                        Data.Portal.Route.Columns.Section.As("SectionName"),
                        Tenant.Columns.Name.As("TenantName"))
                    .Where((Data.Portal.Route.Columns.Deleted == false) & (Data.Portal.Route.Columns.Primary == true)).End()
                    .OrderBy(Data.Portal.Route.Columns.Ordinal).Convert().ToList<Route>();

                secondaryRoutes = session.Routes
                    .Select(Data.Portal.Route.Columns.Name.As("RouteName"),
                        Data.Portal.Route.Columns.RouteId.As("Id"),
                        Data.Portal.Route.Columns.Expression,
                        Data.Portal.Route.Columns.PageTitle,
                        Data.Portal.Route.Columns.Controller.As("ControllerName"),
                        Data.Portal.Route.Columns.Section.As("SectionName"),
                        Tenant.Columns.Name.As("TenantName"))
                    .Where((Data.Portal.Route.Columns.Deleted == false) & (Data.Portal.Route.Columns.Primary == false)).End()
                    .OrderBy(Data.Portal.Route.Columns.Ordinal).Convert().ToList<Route>();

                modules = session.Modules.ToDictionary<int>(Module.Columns.ModuleId);
                routeModules = session.RouteModules.OrderBy(RouteModule.Columns.ContainerName, RouteModule.Columns.Ordinal).ToList();

                PortalTrace.Write("Routes", "InitializeDatabaseRoutes", " - Database session closed.");
            }

            foreach (Route route in primaryRoutes)
            {
                if (Compare.IsNullOrEmpty(route.ControllerName))
                    route.ControllerName = "Portal";

                Route tmpRoute = route;
                List<RouteModule> currentRouteModules = routeModules.FindAll(rm => rm.RouteId == tmpRoute.Id);

                foreach (RouteModule currentRouteModule in currentRouteModules)
                {
                    Module moduleDefinition;
                    modules.TryGetValue(currentRouteModule.ModuleId, out moduleDefinition);
                    if (moduleDefinition != null)
                        route.Tiles.Add(new TileDefinition(currentRouteModule.ContainerName, moduleDefinition.Path, currentRouteModule.Settings, currentRouteModule.Data));
                }

                string tenantName = route.TenantName ?? "DefaultTenant";

                if (!tenantDataCache.ContainsKey(tenantName))
                    continue;

                TenantData tenantData = tenantDataCache[tenantName];

                if (!Compare.IsNullOrEmpty(route.SectionName) && tenantData.Sections.ContainsKey(route.SectionName))
                    tenantData.Sections[route.SectionName].AddPrimaryRoute(route);
                else
                    tenantData.DefaultSection.AddPrimaryRoute(route);
            }

            PortalTrace.Write("Routes", "InitializeDatabaseRoutes", "End initializing primary routes. {0} routes found.", primaryRoutes.Count);
            PortalTrace.Write("Routes", "InitializeDatabaseRoutes", "Begin initializing secondary routes");

            foreach (Route route in secondaryRoutes)
            {
                if (Compare.IsNullOrEmpty(route.ControllerName))
                    route.ControllerName = "Portal";

                string tenantName = route.TenantName ?? "DefaultTenant";

                if (!tenantDataCache.ContainsKey(tenantName))
                    continue;

                TenantData tenantData = tenantDataCache[tenantName];

                if (!Compare.IsNullOrEmpty(route.SectionName) && tenantDataCache.ContainsKey(route.SectionName))
                    tenantData.Sections[route.SectionName].AddPrimaryRoute(route);
                else
                    tenantData.DefaultSection.AddSecondaryRoute(route);
            }

            PortalTrace.Write("Routes", "InitializeDatabaseRoutes", "End initializing secondary routes. {0} routes found.", secondaryRoutes.Count);
        }

        private static Dictionary<string, TenantData> RegisterAllSectionsEx()
        {
            Dictionary<string, TenantData> tenantDataCache = new Dictionary<string, TenantData>(StringComparer.OrdinalIgnoreCase);

            BuildManagerWrapper buildManager = new BuildManagerWrapper();
            IEnumerable<Type> sectionTypes = TypeCache.GetFilteredTypesFromAssemblies("cportal_sections", t => typeof(SectionBase).IsAssignableFrom(t) && t.GetConstructor(new Type[0]) != null, buildManager);
            IEnumerable<Type> controllerTypes = TypeCache.GetFilteredTypesFromAssemblies("cportal_controllers", t => typeof(MvcController).IsAssignableFrom(t) && t.GetConstructor(new Type[0]) != null, buildManager);

            foreach (Type type in sectionTypes)
            {
                SectionBase sectionBase = (SectionBase) Activator.CreateInstance(type);
                SectionContext context = new SectionContext();

                if (Compare.IsNullOrEmpty(sectionBase.Name))
                    sectionBase.Name = type.Name.EndsWith("section", StringComparison.OrdinalIgnoreCase) ? type.Name.Substring(0, type.Name.Length - 7) : type.Name;

                sectionBase.OnRegister(context);

                if (Compare.IsNullOrEmpty(sectionBase.Name))
                    throw new PortalFatalException("The 'Name' property of the SectionContext has not been properly initialized or has an empty value.");

                SectionData section = new SectionData(sectionBase.Name, type.Namespace);
                section.AddPrimaryRoute(sectionBase.DefaultRoute);

                foreach (Route route in context.Routes)
                    section.AddPrimaryRoute(route);

                Match match = Expressions.TenantPathRegex.Match(type.FullName);
                string tenantName = match.Success ? match.Groups["TENANT"].Value : "DefaultTenant";

                TenantData tenantData;
                if (tenantDataCache.ContainsKey(tenantName))
                {
                    tenantData = tenantDataCache[tenantName];

                    if (tenantData.Sections.ContainsKey(section.Name))
                        throw new PortalFatalException(string.Format("A section named {0} has already been added.", section.Name));

                    tenantData.Sections.Add(section.Name, section);
                }
                else
                {
                    tenantData = new TenantData();
                    tenantData.Sections.Add(section.Name, section);
                    tenantDataCache.Add(tenantName, tenantData);
                }

                if (match.Groups["NAMESPACE"].Length > 0)
                    tenantData.Namespace = match.Groups["NAMESPACE"].Value;
                if (match.Groups["TENANT"].Length > 0)
                    tenantData.Name = match.Groups["TENANT"].Value;

                foreach (string host in context.Hosts)
                {
                    if (TenantHostCache.ContainsKey(host))
                        throw new PortalFatalException(string.Format("The host {0} has already been added.", host));

                    TenantHostCache.Add(host, tenantData.Name);
                }
            }

            foreach (TenantData tenantData in tenantDataCache.Values)
                if (!tenantData.Sections.ContainsKey(DefaultSection.DefaultName))
                {
                    tenantData.DefaultSection = new DefaultSection();
                    tenantData.DefaultSection.AddPrimaryRoute(new Route("Default", "/{controller,0,1}/{action,0,1}/{arguments,*}/"));

                    tenantData.Sections.Add(DefaultSection.DefaultName, tenantData.DefaultSection);
                }
                else
                {
                    tenantData.DefaultSection = tenantData.Sections[DefaultSection.DefaultName];
                }

            // Register controllers with matching tenant.
            foreach (Type controllerType in controllerTypes)
            {
                string controllerNamespace = controllerType.Namespace;
                if (controllerNamespace == null)
                    throw new PortalFatalException("Controllers must have a namespace.");

                TenantData foundTenant = controllerNamespace.Contains("Tenants")
                    ? tenantDataCache.Values.FirstOrDefault(td => td.Namespace != null && td.Sections.Values.Any(sd => controllerNamespace.StartsWith(sd.Namespace)))
                    : tenantDataCache["DefaultTenant"];

                if (foundTenant == null)
                    throw new PortalFatalException("Cannot find a tenant.");

                RegisterControllerEx(controllerType, foundTenant, null);
            }

            PortalSettingsSection config = (PortalSettingsSection) ConfigurationManager.GetSection(ConfigSectionName);
            if (config != null && config.Routes.AllowDatabaseRoutes)
                InitializeDatabaseRoutes(tenantDataCache);

            if (config != null && config.Setup)
                RegisterControllerEx(typeof(SetupController), new TenantData(), null);

            return tenantDataCache;
        }

        private static void RegisterControllerEx(Type controllerType, TenantData tenantData, string name)
        {
            Match match = Expressions.ControllerPathRegex.Match(controllerType.FullName);
            if (!match.Success)
                return;

            SectionData section = tenantData.Sections.Values.FirstOrDefault(sec => controllerType.Namespace == sec.Namespace + ".Controllers");
            if (section == null)
                return;

            string controllerName = name ?? controllerType.Name;
            if (controllerName.EndsWith("controller", StringComparison.OrdinalIgnoreCase))
                controllerName = controllerName.Substring(0, controllerName.Length - 10);

            ControllerMetaWrapper wrapper = new ControllerMetaWrapper(controllerType, controllerName);

            string pathPrefix = HttpContext.Current.Request.ApplicationPath + (tenantData.Name != null ? "Tenants/" + tenantData.Name + "/" : null) + (section.Name == null || section.Name == DefaultSection.DefaultName ? null : section.Name); // + (match.Groups["PATH"].Value.Replace(".", "/")));
            bool needsSlash = !pathPrefix.EndsWith("/");

            wrapper.BaseViewVirtualPaths.Add(pathPrefix + (needsSlash ? "/" : null) + "Views/" + controllerName + "/");
            wrapper.BaseViewVirtualPaths.Add(pathPrefix + (needsSlash ? "/" : null) + "Views/Shared/");

            section.AddControllerMeta(wrapper, controllerName);
        }
    }
}