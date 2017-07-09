#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    public abstract class BaseCompareValidatorEx : BaseCompareValidator
    {
        private const string EvaluationFunctionAttributeName = "evaluationfunction";
        private const string ClientScriptFunctionName = "validateBaseCompareValidator2";
        private Style defaultStyle;

        private Style errorStyle;

        ///<summary>
        ///</summary>
        [Browsable(false)]
        public Style DefaultStyle
        {
            get
            {
                if (defaultStyle == null)
                {
                    defaultStyle = new Style();
                    if (IsTrackingViewState)
                        ((IStateManager) defaultStyle).TrackViewState();
                }
                return defaultStyle;
            }
        }

        ///<summary>
        ///</summary>
        [Browsable(false)]
        public Style ErrorStyle
        {
            get
            {
                if (errorStyle == null)
                {
                    errorStyle = new Style();
                    if (IsTrackingViewState)
                        ((IStateManager) errorStyle).TrackViewState();
                }
                return errorStyle;
            }
        }

        /// <summary>
        ///     Gets or sets the background color of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Color"></see> that represents the background
        ///     color of the control. The default is <see cref="System.Drawing.Color.Empty"></see>,
        ///     which indicates that this property is not set.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public Color ErrorBackColor
        {
            get => ErrorStyle.BackColor;
            set => ErrorStyle.BackColor = value;
        }

        /// <summary>
        ///     Gets or sets the border color of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Color"></see> that represents the border color of the
        ///     control. The default is <see cref="System.Drawing.Color.Empty"></see>, which indicates
        ///     that this property is not set.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public Color ErrorBorderColor
        {
            get => ErrorStyle.BorderColor;
            set => ErrorStyle.BorderColor = value;
        }

        /// <summary>
        ///     Gets or sets the border style of the Web server control.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="System.Web.UI.WebControls.BorderStyle"></see> enumeration values.
        ///     The default is NotSet.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public BorderStyle ErrorBorderStyle
        {
            get => ErrorStyle.BorderStyle;
            set => ErrorStyle.BorderStyle = value;
        }

        /// <summary>
        ///     Gets or sets the border width of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Web.UI.WebControls.Unit"></see> that represents the border width
        ///     of a Web server control. The default value is <see cref="System.Web.UI.WebControls.Unit.Empty"></see>,
        ///     which indicates that this property is not set.
        /// </returns>
        /// <exception cref="System.ArgumentException">The specified border width is a negative value. </exception>
        [Category("ControlToValidateStyle")]
        public Unit ErrorBorderWidth
        {
            get => ErrorStyle.BorderWidth;
            set => ErrorStyle.BorderWidth = value;
        }

        /// <summary>
        ///     Gets or sets the cascading style sheet (CSS) class rendered by the Web server control on the client.
        /// </summary>
        /// <returns>
        ///     The CSS class rendered by the Web server control on the client. The default is
        ///     <see cref="System.String.Empty"></see>.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public string ErrorCssClass
        {
            get => ErrorStyle.CssClass;
            set => ErrorStyle.CssClass = value;
        }

        /// <summary>
        ///     Gets the font properties associated with the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Web.UI.WebControls.FontInfo"></see> that represents the font properties
        ///     of the Web server control.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public FontInfo ErrorFont => ErrorStyle.Font;

        /// <summary>
        ///     Gets or sets the foreground color (typically the color of the text) of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Color"></see> that represents the foreground color of the control.
        ///     The default is <see cref="System.Drawing.Color.Empty"></see>.
        /// </returns>
        [Category("ControlToValidateStyle")]
        public Color ErrorForeColor
        {
            get => ErrorStyle.ForeColor;
            set => ErrorStyle.ForeColor = value;
        }

        /// <summary>
        ///     Gets or sets the height of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Web.UI.WebControls.Unit"></see> that represents the height of the Web
        ///     server control. The default is <see cref="System.Web.UI.WebControls.Unit.Empty"></see>,
        ///     which indicates that this property is not set.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The
        ///     <see cref="System.Web.UI.WebControls.Unit.Value"></see> property of the
        ///     <see cref="System.Web.UI.WebControls.Unit"></see> is negative.
        /// </exception>
        [Category("ControlToValidateStyle")]
        public Unit ErrorHeight
        {
            get => ErrorStyle.Height;
            set => ErrorStyle.Height = value;
        }

        /// <summary>
        ///     Gets or sets the width of the Web server control.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Web.UI.WebControls.Unit"></see> that represents the width of the
        ///     Web server control. The default is <see cref="System.Web.UI.WebControls.Unit.Empty"></see>,
        ///     which indicates that this property is not set.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The
        ///     <see cref="System.Web.UI.WebControls.Unit.Value"></see> property of the
        ///     <see cref="System.Web.UI.WebControls.Unit"></see> is negative.
        /// </exception>
        [Category("ControlToValidateStyle")]
        public Unit ErrorWidth
        {
            get => ErrorStyle.Width;
            set => ErrorStyle.Width = value;
        }

        protected virtual void RegisterEvaluationFunction(string clientScriptFunctionName)
        {
            Page.ClientScript.RegisterExpandoAttribute(ClientID, EvaluationFunctionAttributeName, clientScriptFunctionName);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            WebControl control = GetControlToValidate();
            if (control == null)
                return;

            if (EnableClientScript)
            {
                Page.ClientScript.RegisterClientScriptResource(typeof(BaseCompareValidatorEx), WebResourcePath.BaseCompareValidator);
                RegisterEvaluationFunction(ClientScriptFunctionName);
                RegisterStyleExpandoAttributes(control);
            }

            if (!IsValid && control.ControlStyle != ErrorStyle)
            {
                DefaultStyle.CopyFrom(control.ControlStyle);
                control.ApplyStyle(ErrorStyle);
            }
            else
            {
                control.ControlStyle.CopyFrom(DefaultStyle);
            }
        }

        private void RegisterStyleExpandoAttributes(WebControl control)
        {
            if (ErrorStyle.BackColor != Color.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorBackColor", ColorTranslator.ToHtml(ErrorStyle.BackColor));
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "defaultBackColor", ColorTranslator.ToHtml(control.ControlStyle.BackColor));
            }
            if (ErrorStyle.BorderColor != Color.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorBorderColor", ColorTranslator.ToHtml(ErrorStyle.BorderColor));
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "defaultBorderColor", ColorTranslator.ToHtml(control.ControlStyle.BorderColor));
            }
            if (ErrorStyle.BorderStyle != BorderStyle.NotSet)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorBorderStyle", ErrorStyle.BorderStyle.ToString().ToLower());
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "defaultBorderStyle", control.ControlStyle.BorderStyle.ToString().ToLower());
            }
            if (ErrorStyle.BorderWidth != Unit.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorBorderWidth", ErrorStyle.BorderWidth.ToString().ToLower());
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "defaultBorderWidth", control.ControlStyle.BorderWidth.ToString().ToLower());
            }
            if (!Loom.Compare.IsNullOrEmpty(ErrorStyle.CssClass))
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorCssClass", ErrorStyle.CssClass.ToLower());
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "deafultCssClass", control.ControlStyle.CssClass.ToLower());
            }
            if (ErrorStyle.ForeColor != Color.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorForeColor", ColorTranslator.ToHtml(ErrorStyle.ForeColor));
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "deafultForeColor", ColorTranslator.ToHtml(control.ControlStyle.ForeColor));
            }
            if (ErrorStyle.Height != Unit.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorHeight", ErrorStyle.Height.ToString().ToLower());
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "deafultHeight", control.ControlStyle.Height.ToString().ToLower());
            }
            if (ErrorStyle.Width != Unit.Empty)
            {
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "errorWidth", ErrorStyle.Width.ToString().ToLower());
                Page.ClientScript.RegisterExpandoAttribute(ClientID, "deafultWidth", control.ControlStyle.Width.ToString().ToLower());
            }
        }

        protected override void LoadViewState(object savedState)
        {
            object[] objArray = (object[]) savedState;
            base.LoadViewState(objArray[0]);

            if (objArray[1] != null)
                ((IStateManager) ErrorStyle).LoadViewState(objArray[1]);
            if (objArray[2] != null)
                ((IStateManager) DefaultStyle).LoadViewState(objArray[2]);
        }

        protected override object SaveViewState()
        {
            object obj0 = base.SaveViewState();
            object obj1 = errorStyle != null ? ((IStateManager) errorStyle).SaveViewState() : null;
            object obj2 = defaultStyle != null ? ((IStateManager) defaultStyle).SaveViewState() : null;

            return new[] {obj0, obj1, obj2};
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (errorStyle != null)
                ((IStateManager) errorStyle).TrackViewState();
            if (defaultStyle != null)
                ((IStateManager) defaultStyle).TrackViewState();
        }

        private WebControl GetControlToValidate()
        {
            return FindControl(ControlToValidate) as WebControl;
        }
    }
}