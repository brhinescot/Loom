#region Using Directives

using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LiteralEx runat=server></{0}:LiteralEx>")]
    public class LiteralEx : Literal, ILocalizable, ISpamGuardian
    {
        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<LiteralEx> extender;

        #endregion

        public LiteralEx()
        {
            extender = new ControlExtender<LiteralEx>(this);
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("...")]
        [Localizable(true)]
        public string TruncateText
        {
            get
            {
                string s = (string) ViewState["TruncateText"];
                return s ?? "...";
            }

            set => ViewState["TruncateText"] = value;
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(-1)]
        [Localizable(true)]
        public int TruncateLength
        {
            get
            {
                object obj = ViewState["TruncateLength"];
                return obj == null ? -1 : (int) obj;
            }

            set => ViewState["TruncateLength"] = value;
        }

        #region ILocalizable Members

        [Bindable(true)]
        [Category("Misc")]
        [DefaultValue("")]
        public string ResourceKey
        {
            get
            {
                object o = ViewState["ResourceKey"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["ResourceKey"] = value;
        }

        #endregion

        #region ISpamGuardian Members

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool AntiSpam
        {
            get
            {
                object o = ViewState["AntiSpam"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["AntiSpam"] = value;
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            int tailLength = TruncateText.Length;
            string text = TruncateLength > tailLength - 1 && Text.Length > TruncateLength ? Text.Substring(0, TruncateLength - (tailLength + 1)) + TruncateText : Text;

            if (text.Length == 0)
                return;

            if (Mode != LiteralMode.Encode)
                writer.Write(text);
            else
                HttpUtility.HtmlEncode(text, writer);
        }
    }
}