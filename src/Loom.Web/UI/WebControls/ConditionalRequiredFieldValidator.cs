#region Using Directives

using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    ///<summary>
    ///</summary>
    [ToolboxData("<{0}:ConditionalRequiredFieldValidator runat=\"server\" Operator=\"NotEqual\" ValueToCompare=\"\" ErrorMessage=\"ConditionalRequiredFieldValidator\"></{0}:ConditionalRequiredFieldValidator>")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ConditionalRequiredFieldValidator : BaseCompareValidatorEx
    {
        private const string ClientScriptFunctionName = "validateConditionalCompare";
        private const string ControlToCompareAttributeName = "controltocompare";
        private const string TriggerValueAttributName = "triggervalue";
        private const string OperatorAttributeName = "operator";
        private static readonly Regex StandardDateFormat = new Regex(@"^\s*(\d+)([-/]|\. ?)(\d+)\2(\d+)\s*$", RegexOptions.Compiled);

        ///<summary>
        ///</summary>
        [Themeable(false)]
        [Description("The control to compare")]
        [TypeConverter(typeof(ValidatedControlConverter))]
        [Category("Behavior")]
        [DefaultValue("")]
        public string ControlToCompare
        {
            get
            {
                object controlName = ViewState["ControlToCompare"];
                if (controlName != null)
                    return (string) controlName;

                return string.Empty;
            }
            set => ViewState["ControlToCompare"] = value;
        }

        ///<summary>
        ///</summary>
        [Themeable(false)]
        [Description("The trigger value.")]
        [Category("Behavior")]
        [DefaultValue("")]
        public string ValueToCompare
        {
            get
            {
                object triggerValue = ViewState["ValueToCompare"];
                if (triggerValue != null)
                    return (string) triggerValue;

                return string.Empty;
            }
            set => ViewState["ValueToCompare"] = value;
        }

        /// <summary>
        ///     The operator to apply to the condition.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [Themeable(false)]
        [Category("Behavior")]
        [Description("Gets or sets the operator to apply to the condition.")]
        [DefaultValue(ValidationCompareOperator.Equal)]
        public ValidationCompareOperator Operator
        {
            get
            {
                object op = ViewState["Operator"];
                if (op != null)
                    return (ValidationCompareOperator) op;

                return ValidationCompareOperator.Equal;
            }
            set
            {
                if (value < ValidationCompareOperator.Equal || value > ValidationCompareOperator.DataTypeCheck)
                    throw new ArgumentOutOfRangeException("value");

                ViewState["Operator"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the initial value of the associated input control.
        /// </summary>
        /// <returns>
        ///     A string that specifies the initial value of the associated input control. The default is
        ///     <see cref="String.Empty" />.
        /// </returns>
        [Themeable(false)]
        [Description("RequiredFieldValidator_InitialValue")]
        [Category("Behavior")]
        [DefaultValue("")]
        public string InitialValue
        {
            get
            {
                object obj = ViewState["InitialValue"];
                if (obj != null)
                    return (string) obj;

                return string.Empty;
            }
            set => ViewState["InitialValue"] = value;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        protected override bool EvaluateIsValid()
        {
            string controlToCompareValue = GetControlValidationValue(ControlToCompare);
            if (controlToCompareValue == null || controlToCompareValue.Trim().Length == 0)
                return true;

            bool renderUplevelDate = Type == ValidationDataType.Date && !DetermineRenderUplevel();
            if (renderUplevelDate && !IsInStandardDateFormat(controlToCompareValue))
                controlToCompareValue = ConvertToShortDateString(controlToCompareValue);

            bool cultureInvariantRightText = false;
            string requiredFieldValue;
            if (ControlToValidate.Length > 0)
            {
                requiredFieldValue = GetControlValidationValue(ControlToValidate);
                if (renderUplevelDate && !IsInStandardDateFormat(requiredFieldValue))
                    requiredFieldValue = ConvertToShortDateString(requiredFieldValue);
            }
            else
            {
                requiredFieldValue = ValueToCompare;
                cultureInvariantRightText = CultureInvariantValues;
            }

            bool checkForRequired = Compare(controlToCompareValue, false, ValueToCompare, cultureInvariantRightText, Operator, Type);
            if (!checkForRequired)
                return true;

            return requiredFieldValue == null || !requiredFieldValue.Trim().Equals(InitialValue.Trim());
        }

        /// <summary>
        ///     Raises the PreRender event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> containing the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!EnableClientScript)
                return;

            Page.ClientScript.RegisterClientScriptResource(typeof(ConditionalRequiredFieldValidator), WebResourcePath.ConditionalRequiredFieldValidation);
            Page.ClientScript.RegisterExpandoAttribute(ClientID, ControlToCompareAttributeName, GetControlRenderID(ControlToCompare));
            Page.ClientScript.RegisterExpandoAttribute(ClientID, TriggerValueAttributName, ValueToCompare);
            Page.ClientScript.RegisterExpandoAttribute(ClientID, OperatorAttributeName, Operator.ToString());
        }

        protected override void RegisterEvaluationFunction(string clientScriptFunctionName)
        {
            base.RegisterEvaluationFunction(ClientScriptFunctionName);
        }

        private static bool IsInStandardDateFormat(string date)
        {
            return StandardDateFormat.IsMatch(date);
        }

        private static string ConvertToShortDateString(string text)
        {
            DateTime result;
            if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
                return result.ToShortDateString();

            return text;
        }
    }
}