#region Using Directives

using System.ComponentModel;
using System.Web.UI;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ParseChildren(true, "Text")]
    [ToolboxData("<{0}:TextBox runat=\"server\"></{0}:TextBox>")]
    public class TextBox : Input, ITextControl
    {
        public bool DisableEncoding { get; set; }

        public bool DisableAutoConmplete { get; set; }

        public AutoCompleteType AutoCompleteType { get; set; }

        protected override string InputType => "text";

        #region ITextControl Members

        [Localizable(true)]
        public virtual string Text { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (DisableAutoConmplete)
                writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");

            string supportsVCard = Context.Request.Browser["supportsVCard"];
            if (AutoCompleteType != AutoCompleteType.None && supportsVCard == "true")
                writer.AddAttribute(HtmlTextWriterAttribute.VCardName, EnumDescriptionAttribute.ToString(AutoCompleteType));
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
            return DisableEncoding ? Text : AntiXss.HtmlEncode(Text);
        }

        protected override void OnValuePosted(string value)
        {
            Text = value;
        }
    }
}