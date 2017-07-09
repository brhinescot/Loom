#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Displays an image button that has a random client id on a web page.
    /// </summary>
    /// <remarks>
    ///     The control generates a new random id on each request. The
    ///     control may be used in the same way as a standard <see cref="ImageButton" />
    ///     without any modifications. The changing client id is transparent to
    ///     the developer.
    /// </remarks>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageButtonEx runat=server></{0}:ImageButtonEx>")]
    public class ImageButtonEx : ImageButton, ILocalizable, ISpamGuardian
    {
        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<ImageButtonEx> extender;

        #endregion

        public ImageButtonEx()
        {
            extender = new ControlExtender<ImageButtonEx>(this);
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