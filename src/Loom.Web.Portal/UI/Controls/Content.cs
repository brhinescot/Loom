#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(false)]
    public class Content : PortalControl
    {
        public string BoxId { get; set; }

        protected override void RenderBeginTag(HtmlTextWriter writer) { }

        protected override void RenderEndTag(HtmlTextWriter writer) { }
    }
}