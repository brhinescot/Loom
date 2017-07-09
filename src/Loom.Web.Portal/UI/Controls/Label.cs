#region Using Directives

using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true, "Text")]
    [ToolboxData("<{0}:Label runat=\"server\"></{0}:Label>")]
    public class Label : PortalControl, ITextControl
    {
        public char AccessKey { get; set; }
        public string Name { get; set; }
        public string AssociatedControlId { get; set; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Label;

        #region ITextControl Members

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        public virtual string Text { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (!Compare.IsNullOrEmpty(AssociatedControlId))
                writer.AddAttribute(HtmlTextWriterAttribute.For, AssociatedControlId);

            if (AccessKey != '\0')
                writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey.ToString(CultureInfo.CurrentCulture));
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