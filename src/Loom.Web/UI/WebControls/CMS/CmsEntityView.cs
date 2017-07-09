#region Using Directives

using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls.CMS
{
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:CmsEntityView runat=server></{0}:CmsEntityView>")]
    public class CmsEntityView : EntityView
    {
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Fieldset;

        [Bindable(true)]
        [DefaultValue("")]
        public string Title
        {
            get
            {
                object o = ViewState["Title"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["Title"] = value;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.Write(Title);
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "innerForm");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}