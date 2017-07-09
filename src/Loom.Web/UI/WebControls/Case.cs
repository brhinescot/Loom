#region Using Directives

using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ParseChildren(false)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:Case runat=\"server\"></{0}:Case>")]
    public class Case : CompositeControl
    {
        public bool Default { get; set; }
        public string Value { get; set; }
    }
}