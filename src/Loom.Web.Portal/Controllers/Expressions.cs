#region Using Directives

using System.Text.RegularExpressions;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal static class Expressions
    {
        public static readonly Regex ControllerPathRegex = new Regex(@"(?<PATH>(?<TYPE>tenants|areas).*)?.controllers.\w*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly Regex TenantPathRegex = new Regex(@"(?<NAMESPACE>.*tenants\.(?<TENANT>\w*).*)\.(?<SECTION>\w*)section", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}