#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ParseChildren(true, "ChildNodes")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class TreeNode
    {
        private TreeNodeCollection childNodes;

        [DefaultValue(null)]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [MergableProperty(false)]
        public TreeNodeCollection ChildNodes => childNodes ?? (childNodes = new TreeNodeCollection());

        public bool Closed { get; set; }
        public bool Collapsable { get; private set; }
        public string CssClass { get; set; }
        public bool Expandable { get; private set; }
        public string Text { get; set; }
        public string Value { get; set; }

        internal void Render(HtmlTextWriter writer)
        {
            bool hasChildNodes = childNodes != null && childNodes.Count > 0;
            Expandable = Closed && hasChildNodes;
            Collapsable = !Closed && hasChildNodes;

            // <li>
            if (Closed && hasChildNodes) writer.AddAttribute(HtmlTextWriterAttribute.Class, "closed");
            if (Collapsable) writer.AddAttribute(HtmlTextWriterAttribute.Class, "collapsable");
            if (Expandable) writer.AddAttribute(HtmlTextWriterAttribute.Class, "expandable");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);

            // <span>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Value);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(Text);
            writer.RenderEndTag();
            // </span>

            if (ChildNodes.Count > 0)
            {
                // <ul>
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                foreach (TreeNode node in ChildNodes)
                    node.Render(writer);
                writer.RenderEndTag();
                // </ul>
            }

            writer.RenderEndTag();
            // </li>

            writer.WriteLine();
        }
    }
}