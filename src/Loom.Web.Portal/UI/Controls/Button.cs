#region Using Directives

using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}:Button runat=\"server\"></{0}:Button>")]
    [ParseChildren(false)]
    public class Button : Input, ITextControl
    {
        protected override string InputType => "button";

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Button;

        #region ITextControl Members

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        public virtual string Text { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (Controls.Count == 0)
                writer.Write(Text);
            else
                base.RenderChildren(writer);
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