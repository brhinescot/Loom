#region Using Directives

using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.Routing
{
    public sealed class Route
    {
        private Regex evaluator;
        private string routeExpression;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Route" /> class.
        /// </summary>
        public Route()
        {
            Tiles = new Collection<TileDefinition>();
        }

        public Route(string routeName, string expression, string controllerName = null, string pageTitle = null, string sectionName = null)
        {
            RouteName = routeName;
            routeExpression = expression;
            ControllerName = controllerName;
            PageTitle = pageTitle;
            SectionName = sectionName;
            Tiles = new Collection<TileDefinition>();
            evaluator = RouteParser.GenerateRegEx(routeExpression);
        }

        public string ControllerName { get; set; }
        public string TenantName { get; set; }

        public string Expression
        {
            get => routeExpression;
            set
            {
                if (routeExpression == value)
                    return;

                routeExpression = value;
                evaluator = RouteParser.GenerateRegEx(routeExpression);
            }
        }

        public string PageTitle { get; set; }
        public string RouteName { get; set; }
        public string SectionName { get; set; }
        public int Id { get; set; }

        public Collection<TileDefinition> Tiles { get; }

        public bool IsMatch([NotNull] string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            return evaluator.IsMatch(path);
        }

        public RouteTokens GetTokens([NotNull] string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));

            RouteTokens tokens = new RouteTokens();

            Match match = evaluator.Match(path);
            if (!match.Success)
                return tokens;

            foreach (string groupName in evaluator.GetGroupNames())
            {
                Group matchedGroup = match.Groups[groupName];
                foreach (Capture capture in matchedGroup.Captures)
                    tokens.Add(groupName, capture.Value);
            }

            return tokens;
        }
    }
}