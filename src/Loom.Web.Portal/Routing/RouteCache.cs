#region Using Directives

using System;
using System.Collections.Generic;
using System.Text;
using Loom.Annotations;
using Loom.Collections;

#endregion

namespace Loom.Web.Portal.Routing
{
    public sealed class RouteCache
    {
        private readonly Route defaultRoute;
        private readonly OrderedDictionary<string, Route> primaryRoutes = new OrderedDictionary<string, Route>();
        private OrderedDictionary<string, Route> secondaryRoutes;

        internal RouteCache([NotNull] string sectionName)
        {
            Argument.Assert.IsNotNullOrEmpty(sectionName, nameof(sectionName));

            defaultRoute = new Route("Default", JoinRouteTokens("/", string.Equals(sectionName, "HOME", StringComparison.OrdinalIgnoreCase) ? null : sectionName, "{controller,0,1}", "{action,0,1}", "{arguments,*}"));
        }

        public void AddPrimaryRoute(string routeName, string expression, string controllerName = null, string pageTitle = null)
        {
            Argument.Assert.IsNotNull(routeName, nameof(routeName));
            Argument.Assert.IsNotNull(expression, nameof(expression));

            AddPrimaryRoute(new Route(routeName, expression, controllerName, pageTitle));
        }

        public void AddPrimaryRoute(Route route)
        {
            Argument.Assert.IsNotNull(route, nameof(route));

            PortalTrace.Write("Routes", "AddPrimaryRoute", "Adding route {0}.", route.Expression);

            primaryRoutes.Insert(0, route.RouteName, route);
        }

        public void AddSecondaryRoute(Route route)
        {
            if (secondaryRoutes == null)
                secondaryRoutes = new OrderedDictionary<string, Route>();

            PortalTrace.Write("Routes", "AddSecondaryRoute", "Adding route {0}.", route.Expression);
            secondaryRoutes.Add(route.RouteName, route);
        }

        public void AddSecondaryRoute(string routeName, string expression, string controllerName = null, string pageTitle = null)
        {
            if (secondaryRoutes == null)
                secondaryRoutes = new OrderedDictionary<string, Route>();

            PortalTrace.Write("Routes", "AddSecondaryRoute", "Adding route {0}.", expression);
            secondaryRoutes.Add(routeName, new Route(routeName, expression, controllerName, pageTitle));
        }

        [NotNull]
        internal List<Route> GetMatchedRoutes(string path)
        {
            PortalTrace.Write("Routes", "GetMatchedRoutes", "Begin GetMatchedRoutes.");

            List<Route> matchedRoutes = new List<Route>();
            foreach (KeyValuePair<string, Route> route in primaryRoutes)
            {
                if (!route.Value.IsMatch(path))
                    continue;

                PortalTrace.Write("Routes", "GetMatchedRoutes", "Found matching primary route '{0}'", route.Value.RouteName);
                matchedRoutes.Add(route.Value);
                break;
            }

            if (matchedRoutes.Count == 0)
            {
                PortalTrace.Warn("Routes", "GetMatchedRoutes", "No matching primary routes found.");
                if (!defaultRoute.IsMatch(path))
                    return matchedRoutes;

                PortalTrace.Warn("Routes", "GetMatchedRoutes", "Using default '/controller/action/parameters' route.");
                matchedRoutes.Add(defaultRoute);
            }

            if (secondaryRoutes != null && secondaryRoutes.Count > 0)
                foreach (KeyValuePair<string, Route> pair in secondaryRoutes)
                {
                    Route route = pair.Value;
                    if (!route.IsMatch(path))
                        continue;

                    PortalTrace.Write("Routes", "GetMatchedRoutes", "Found matching secondary route {0}", route.RouteName);
                    matchedRoutes.Add(route);
                }

            PortalTrace.Write("Routes", "GetMatchedRoutes", "End GetMatchedRoutes. {1} routes found", path, matchedRoutes.Count);

            return matchedRoutes;
        }

        private static string JoinRouteTokens(string seperator, params string[] tokens)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string token in tokens)
            {
                if (Compare.IsNullOrEmpty(token))
                    continue;

                builder.Append(string.Concat(seperator, token));
            }

            return builder.ToString();
        }
    }
}