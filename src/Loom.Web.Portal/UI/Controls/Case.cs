#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(false)]
    [ToolboxData("<{0}:Case runat=\"server\"></{0}:Case>")]
    public class Case : PortalControl
    {
        public string Value { get; set; }

        protected override void RenderBeginTag(HtmlTextWriter writer) { }

        protected override void RenderEndTag(HtmlTextWriter writer) { }
    }
}