#region Using Directives

using System.Security.Permissions;
using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI
{
    /// <summary>
    ///     Converts a CheckBox control on the Web Forms page to a string representing the controls name.
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand)]
    [PermissionSet(SecurityAction.InheritanceDemand)]
    public sealed class CheckBoxControlConverter : ControlConverter<CheckBox> { }
}