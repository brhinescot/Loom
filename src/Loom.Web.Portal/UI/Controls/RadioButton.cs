#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    [ToolboxData("<{0}:RadioButton runat=\"server\"></{0}:RadioButton>")]
    public class RadioButton : CheckBox
    {
        protected override string InputType => "radio";
    }
}