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
    /// <summary>
    ///     Displays a text box control that has a random client id for user input.
    /// </summary>
    /// <remarks>
    ///     The control generates a new random id on each request. The
    ///     control may be used in the same way as a standard <see cref="TextBox" />
    ///     without any modifications. The changing client id is transparent to
    ///     the developer.
    /// </remarks>
    [ValidationProperty("Text")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TextBoxEx runat=server></{0}:TextBoxEx>")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class TextBoxEx : TextBox, ILocalizable, ISpamGuardian
    {
        private const string ClickScript = @"
        function Colossus_DoClick(buttonName,e){
            var key;
            if(window.event) key = window.event.keyCode;
            else key = e.which;
            if (key == 13){
                var btn = document.all ? document.all[buttonName] : document.getElementById(buttonName);
                if (btn != null){ 
                    e.returnValue=false;
                    e.cancel = true;   
                    btn.click();
                    return false;                 
                }
            }
            return true;
       }";

        /// <summary>
        ///     This object is required for custom localization and anti spam functionality.
        /// </summary>
        private readonly ControlExtender<TextBoxEx> extender;

        public TextBoxEx()
        {
            extender = new ControlExtender<TextBoxEx>(this);
        }

        [Browsable(true)]
        [Category("Behavior")]
        public string EnterClickButtonId
        {
            get
            {
                object o = ViewState["EnterClickButtonId"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["EnterClickButtonId"] = value;
        }

        [Browsable(true)]
        [Category("Behavior")]
        public string SomeProperty
        {
            get
            {
                object o = ViewState["SomeProperty"];
                return o == null ? string.Empty : (string) o;
            }
            set => ViewState["SomeProperty"] = value;
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

            if (!DesignMode && !Compare.IsNullOrEmpty(EnterClickButtonId))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(TextBoxEx), ClickScript, ClickScript, true);
                Control control = Page.FindChild(EnterClickButtonId);
                if (control == null)
                    throw new InvalidOperationException("Cannot render enter click button script. A control named '" + EnterClickButtonId + "' was not found.");

                Attributes.Add("onkeypress", "javascript:return Colossus_DoClick('" + control.ClientID + "', event);");
            }
        }
    }
}