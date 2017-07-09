#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Web.UI.PageParts
{
    [DebuggerDisplay("FunctionKey={FunctionKey}")]
    public class InlineClientScript : IClientScriptProvider, IEquatable<InlineClientScript>, IComparable<InlineClientScript>, IComparable
    {
        private InlineClientScript(string inlineScript) : this(inlineScript, null, null) { }

        private InlineClientScript(string inlineScript, string functionKey, string function)
        {
            InlineScript = inlineScript;
            FunctionKey = functionKey;
            Function = function;
        }

        #region IClientScriptProvider Members

        public string InlineScript { get; }

        public string FunctionKey { get; }

        public string Function { get; }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is double))
                throw new ArgumentException("obj");

            InlineClientScript script = (InlineClientScript) obj;

            return CompareTo(script);
        }

        #endregion

        #region IComparable<InlineClientScript> Members

        public int CompareTo(InlineClientScript other)
        {
            return FunctionKey.CompareTo(other.FunctionKey);
        }

        #endregion

        #region IEquatable<InlineClientScript> Members

        public bool Equals(InlineClientScript other)
        {
            if (!Equals(InlineScript, other.InlineScript))
                return false;
            if (!Equals(FunctionKey, other.FunctionKey))
                return false;
            if (!Equals(Function, other.Function))
                return false;
            return true;
        }

        #endregion

        public static InlineClientScript ConfirmAction(string message)
        {
            const string script = "return confirm('{0}')";
            return new InlineClientScript(string.Format(script, message));
        }

        public static InlineClientScript UpdateCssClass(string className)
        {
            const string script = "this.class='{0}';this.className='{0}'";
            return new InlineClientScript(string.Format(script, className));
        }

        public static InlineClientScript ChangeImage(string imageUrl)
        {
            const string script = "if (document.images) this.src= '{0}';";
            return new InlineClientScript(string.Format(script, imageUrl));
        }

        public static InlineClientScript ToggleVisibility(string controlId)
        {
            string clientFunction = Environment.NewLine +
                                    "    function ToggleVisibility(controlId){" + Environment.NewLine +
                                    "        control = document.all ? document.all[controlId] : document.getElementById(controlId);" + Environment.NewLine +
                                    "        if(control.style.display == 'none')" + Environment.NewLine +
                                    "            control.style.display = 'block';" + Environment.NewLine +
                                    "        else" + Environment.NewLine +
                                    "           control.style.display = 'none';" + Environment.NewLine +
                                    "    }" + Environment.NewLine;

            string inlineScript = "ToggleVisibility('" + controlId + "');";
            return new InlineClientScript(inlineScript, "ToggleVisibility", clientFunction);
        }

        public static InlineClientScript UpdateWindowStatus(string message)
        {
            const string script = "window.status='{0}';return true";
            return new InlineClientScript(string.Format(script, message));
        }

        public static InlineClientScript UpdateWindowLocation(string location)
        {
            const string script = "window.location='{0}';return true";
            return new InlineClientScript(string.Format(script, location));
        }

        public static InlineClientScript GoToHistory()
        {
            const string script = "javascript.history.back();return false;";
            return new InlineClientScript(script);
        }

        public static InlineClientScript GoToHistory(int steps)
        {
            const string script = "javascript.history.back({0});return false;";
            return new InlineClientScript(string.Format(script, steps));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is InlineClientScript))
                return false;

            return Equals((InlineClientScript) obj);
        }

        public override int GetHashCode()
        {
            int result = InlineScript != null ? InlineScript.GetHashCode() : 0;
            result = 29 * result + (FunctionKey != null ? FunctionKey.GetHashCode() : 0);
            result = 29 * result + (Function != null ? Function.GetHashCode() : 0);
            return result;
        }
    }
}