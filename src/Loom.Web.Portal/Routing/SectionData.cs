#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.Routing
{
    internal class SectionData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SectionData" /> class.
        /// </summary>
        public SectionData(string name, string sectionNamespace = null)
        {
            Argument.Assert.IsNotNull(name, nameof(name));

            Routes = new RouteCache(name);
            Controllers = new Dictionary<string, ControllerMetaWrapper>(StringComparer.OrdinalIgnoreCase);

            Name = name;
            Namespace = sectionNamespace;
        }

        public string Name { get; }
        public string Namespace { get; }
        public TenantData Tenant { get; private set; }

        private Dictionary<string, ControllerMetaWrapper> Controllers { get; }
        private RouteCache Routes { get; }

        public void AddPrimaryRoute(string routeName, string expression, string controllerName = null, string pageTitle = null)
        {
            Routes.AddPrimaryRoute(routeName, expression, controllerName, pageTitle);
        }

        public void AddPrimaryRoute(Route route)
        {
            Routes.AddPrimaryRoute(route);
        }

        public void AddSecondaryRoute(Route route)
        {
            Routes.AddSecondaryRoute(route);
        }

        public void AddSecondaryRoute(string routeName, string expression, string controllerName = null, string pageTitle = null)
        {
            Routes.AddSecondaryRoute(routeName, expression, controllerName, pageTitle);
        }

        internal ControllerMetaWrapper GetControllerMeta(string name = null)
        {
            if (name == null)
                name = "Home";

            ControllerMetaWrapper controllerWrapper;
            if (!Controllers.TryGetValue(name, out controllerWrapper))
                if (!Controllers.TryGetValue("Home", out controllerWrapper))
                    throw new ArgumentException("Cannot find a controller named '" + name + "'.");

            return controllerWrapper;
        }

        internal void AddControllerMeta(ControllerMetaWrapper controller, string name)
        {
            if (Controllers.ContainsKey(name))
            {
                Debugger.Break();
//                throw new ArgumentException("A controller named '" + name + "' has already been registered with the '" + name + "' section.");
                return;
            }

            Controllers.Add(name, controller);
        }

        public List<Route> GetMatchedRoutes(string path)
        {
            return Routes.GetMatchedRoutes(path);
        }
    }
}