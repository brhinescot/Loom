#region Using Directives

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DropDownlistEx runat=\"server\"></{0}:DropDownlistEx>")]
    public class DropDownListEx : DropDownList, ILocalizable, ISpamGuardian
    {
        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<DropDownListEx> extender;

        public DropDownListEx()
        {
            extender = new ControlExtender<DropDownListEx>(this);
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
                return o != null && (bool) o;
            }

            set => ViewState["AntiSpam"] = value;
        }

        #endregion
    }
}