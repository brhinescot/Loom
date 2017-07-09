#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [DefaultProperty("Items")]
    [ParseChildren(true, "Items")]
    [Designer("System.Web.UI.Design.WebControls.ListControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:ListBox runat=\"server\"></{0}:ListBox>")]
    public class ListBox : DropDownList
    {
        public bool Multiple { get; set; }
        public int Rows { get; set; }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Size, Rows > 1 ? Rows.ToString() : "4");
            if (Multiple)
                writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
        }
    }
}