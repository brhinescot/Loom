#region Using Directives

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true, "Text")]
    [ToolboxData("<{0}:Hyperlink runat=\"server\"></{0}:Hyperlink>")]
    public class Hyperlink : PortalControl, ITextControl
    {
        public char AccessKey { get; set; }
        public string Name { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string NavigateUrl { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.A;

        #region ITextControl Members

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        public virtual string Text { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Href, Compare.IsNullOrEmpty(NavigateUrl)
                ? "javascript:void(0);"
                : ResolveUrl(NavigateUrl));

            if (AccessKey != '\0')
                writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey.ToString());
            if (!Compare.IsNullOrEmpty(Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            writer.Write(Text);
        }

        protected override void OnSetViewData(object data)
        {
            if (data == null)
                return;

            string s = data as string;
            if (!Compare.IsNullOrEmpty(s))
            {
                Text = s;
                return;
            }

            Text = data.ToString();
        }
    }
}