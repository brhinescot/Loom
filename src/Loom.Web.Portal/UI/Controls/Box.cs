#region Using Directives

using System.Diagnostics;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [DebuggerDisplay("Box: ID={ID}")]
    [ToolboxData("<{0}:box runat=\"server\"></{0}:box>")]
    public class Box : PortalControl
    {
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
    }
}