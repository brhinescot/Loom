#region Using Directives

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loom.Web.UI.Design.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [Designer(typeof(DesignerControlDesigner))]
    [ToolboxData("<{0}:DesignerControl runat=server></{0}:DesignerControl>")]
    [ParseChildren(false)]
    public class DesignerControl : CompositeControl
    {
        private const string LeftKey = "LeftKey";
        private const string TopKey = "TopKey";
        private const string ShowHeaderKey = "ShowHeaderKey";
        private const string HeaderTextKey = "HeaderTextKey";

        [Browsable(true)]
        [Description("The text to display in the header.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public string HeaderText
        {
            get
            {
                object obj = ViewState[HeaderTextKey];
                return obj == null ? string.Empty : obj.ToString();
            }
            set => ViewState[HeaderTextKey] = value;
        }

        [Browsable(true)]
        [Description("Determines if a header is rendered for the designer control.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("true")]
        public bool ShowHeader
        {
            get
            {
                object obj = ViewState[ShowHeaderKey];
                return obj == null || (bool) obj;
            }
            set => ViewState[ShowHeaderKey] = value;
        }

        [Browsable(true)]
        [Description("The absolute top position of the control.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Unit Top
        {
            get
            {
                object obj = ViewState[TopKey];
                return obj == null ? Unit.Empty : (Unit) obj;
            }
            set => ViewState[TopKey] = value;
        }

        [Browsable(true)]
        [Description("The absolute left position of the control.")]
        [Category("Appearance")]
        [Bindable(false)]
        [DefaultValue("")]
        public Unit Left
        {
            get
            {
                object obj = ViewState[LeftKey];
                return obj == null ? Unit.Empty : (Unit) obj;
            }
            set => ViewState[LeftKey] = value;
        }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Left, Left.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.Top, Top.ToString());
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!ShowHeader)
            {
                base.RenderContents(writer);
                return;
            }

            writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "SteelBlue");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "White");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "20px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(HeaderText);
            writer.RenderEndTag();
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "5px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.RenderContents(writer);
            writer.RenderEndTag();
        }
    }
}