#region Using Directives

using System.ComponentModel;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true, "Text")]
    [ToolboxData("<{0}:CheckBox runat=\"server\"></{0}:CheckBox>")]
    public class CheckBox : Input, ITextControl
    {
        public bool Checked { get; set; }

        protected override string InputType => "checkbox";

        public string Value { get; set; }

        #region ITextControl Members

        [Localizable(true)]
        [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
        public virtual string Text { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Checked)
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            if (!Compare.IsNullOrEmpty(Text))
            {
                if (!Compare.IsNullOrEmpty(ID))
                    writer.AddAttribute(HtmlTextWriterAttribute.For, ID);
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(Text);
                writer.RenderEndTag();
            }

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

        protected override string GetValue()
        {
            return Value;
        }

        protected override void OnValuePosted(string value)
        {
            base.OnValuePosted(value);

            Checked = value == "on";
        }
    }
}