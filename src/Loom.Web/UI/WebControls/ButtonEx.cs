#region Using Directives

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ButtonEx runat=server></{0}:ButtonEx>")]
    public class ButtonEx : Button, ILocalizable, ISpamGuardian
    {
        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<ButtonEx> extender;

        public ButtonEx()
        {
            extender = new ControlExtender<ButtonEx>(this);
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
    }
}