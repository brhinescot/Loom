#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}ImageButton runat=\"server\"></{0}:ImageButton>")]
    public class HiddenField : Input
    {
        protected override string InputType => "hiddden";

        public string Value { get; set; }

        protected override string GetValue()
        {
            return Value;
        }
    }
}