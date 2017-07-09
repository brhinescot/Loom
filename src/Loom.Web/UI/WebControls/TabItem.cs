#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ParseChildren(false)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:TabItem runat=\"server\"></{0}:TabItem>")]
    public class TabItem : CompositeControl
    {
        [Browsable(true)]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                object obj = ViewState["Text"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["Text"] = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string Title
        {
            get
            {
                object obj = ViewState["Title"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["Title"] = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Bindable(true)]
        [DefaultValue("")]
        public string NavigateUrl
        {
            get
            {
                object obj = ViewState["NavigateUrl"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState["NavigateUrl"] = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Fieldset;

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "innerForm");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}