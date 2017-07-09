#region Using Directives

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:RadioButtonEx runat=server></{0}:RadioButtonEx>")]
    public class RadioButtonEx : RadioButton, ILocalizable, ISpamGuardian
    {
        #region Designer generated code

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<RadioButtonEx> extender;

        #endregion

        public RadioButtonEx()
        {
            extender = new ControlExtender<RadioButtonEx>(this);
        }

        /// <summary>
        ///     Gets or sets a value indicating if radio button grouping should be enforced across items in
        ///     repeater controls.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ForceGroup
        {
            get
            {
                object o = ViewState["ForceGroup"];
                return o != null ? (bool) o : false;
            }

            set => ViewState["ForceGroup"] = value;
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (ForceGroup)
                Page.ClientScript.RegisterClientScriptResource(typeof(RadioButtonEx), WebResourcePath.SingleRadioCheck);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (ForceGroup)
                RenderForcedGroup(writer);
        }

        private void RenderForcedGroup(HtmlTextWriter writer)
        {
            string parentClientId = null;
            for (Control parent = Parent; parent != null; parent = parent.Parent)
            {
                if (!(parent is INamingContainer) || parent is IDataItemContainer)
                    continue;

                parentClientId = parent.ClientID;
                break;
            }

            if (parentClientId == null)
                return;

            writer.AddAttribute("onClick", string.Format(@"SetSingleRadioChecked('{0}[\\w\\$]*{1}',this)", parentClientId, GroupName));
        }
    }
}