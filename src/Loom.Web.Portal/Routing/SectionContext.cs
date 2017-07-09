#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Web.Portal.Routing
{
    public class SectionContext
    {
        private readonly List<string> hosts = new List<string>();

        private readonly List<Route> routes = new List<Route>();

        internal IEnumerable<Route> Routes => routes;

        internal IEnumerable<string> Hosts => hosts;

        internal bool IsDefaultTenant => hosts.Count == 0;

        public void AddPrimaryRoute(string name, string expression, string controller = null, string title = null)
        {
            Argument.Assert.IsNotNull(name, nameof(name));
            Argument.Assert.IsNotNull(expression, nameof(expression));

            AddPrimaryRoute(new Route(name, expression, controller, title));
        }

        public void AddPrimaryRoute(Route route)
        {
            Argument.Assert.IsNotNull(route, nameof(route));
            routes.Insert(0, route);
        }

        public void RegisterHostName(string hostName)
        {
            Argument.Assert.IsNotNull(hostName, nameof(hostName));

            if (!hosts.Contains(hostName))
                hosts.Add(hostName);
        }
    }
}