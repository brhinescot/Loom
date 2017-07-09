#region Using Directives

using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CheckBoxValidator runat=server></{0}:CheckBoxValidator>")]
    [ValidationProperty("Checked")]
    public class CheckBoxValidator : BaseValidator
    {
        private const string ClientScriptFunctionName = "validateCheckbox";
        private const string EvaluationFunctionAttributeName = "evaluationfunction";

        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [IDReferenceProperty]
        [Description("The control to validate.")]
        [TypeConverter(typeof(CheckBoxControlConverter))]
        public new string ControlToValidate
        {
            get => base.ControlToValidate;
            set => base.ControlToValidate = value;
        }

        protected override bool EvaluateIsValid()
        {
            CheckBox checkBox = FindControl(ControlToValidate) as CheckBox;
            return checkBox != null ? checkBox.Checked : false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (EnableClientScript)
            {
                Page.ClientScript.RegisterClientScriptResource(typeof(CheckBoxValidator), WebResourcePath.CheckBoxValidator);
                Page.ClientScript.RegisterExpandoAttribute(ClientID, EvaluationFunctionAttributeName, ClientScriptFunctionName);
            }

            base.OnPreRender(e);
        }

        protected override bool ControlPropertiesValid()
        {
            string controlToValidate = ControlToValidate;
            if (controlToValidate.Length == 0)
                throw new HttpException("The 'ControlToValidate' property can not be blank.");

            CheckBox checkBox = FindControl(ControlToValidate) as CheckBox;
            return checkBox != null;
        }
    }
}