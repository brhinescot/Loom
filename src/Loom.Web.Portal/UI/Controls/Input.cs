#region Using Directives

using System;
using System.Globalization;
using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public abstract class Input : PortalControl, IFormInput
    {
        protected abstract string InputType { get; }

        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Input;

        public string Tooltip { get; set; }

        #region IFormInput Members

        public string Name { get; set; }
        public char AccessKey { get; set; }
        public int TabIndex { get; set; }
        public bool Disabled { get; set; }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            if (Page.Request.Form.HasKeys())
                LoadPostData();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, InputType);

            if (!Compare.IsNullOrEmpty(Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
            else if (!Compare.IsNullOrEmpty(ID))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, ID);

            string value = GetValue();
            if (!Compare.IsNullOrEmpty(value))
                writer.AddAttribute(HtmlTextWriterAttribute.Value, value);

            if (AccessKey != '\0')
                writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey.ToString());
            if (TabIndex > 0)
                writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString(CultureInfo.InvariantCulture));
            if (Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            if (!Compare.IsNullOrEmpty(Tooltip))
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Tooltip);
        }

        private void LoadPostData()
        {
            if (Compare.IsNullOrEmpty(Name))
                Name = ID;
            if (Compare.IsNullOrEmpty(Name))
                return;

            OnValuePosted(Context.Request.Form[Name]);
        }

        protected virtual void OnValuePosted(string value) { }

        protected virtual string GetValue()
        {
            return null;
        }
    }
}