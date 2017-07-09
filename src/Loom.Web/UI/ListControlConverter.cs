#region Using Directives

using System.Security.Permissions;
using System.Web.UI.WebControls;
using Loom.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI
{
    /// <summary>
    ///     Converts a control on the Web Forms page that inherits
    ///     from <see cref="ListControl">ListControl</see>
    ///     to a string representing the controls name.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Not all controls can act as a <b>parent</b> to the <see cref="IRelationalListControl" />.
    ///         This class creates a collection of controls on the Web Forms page that can be associated
    ///         with a relational list control.  This collection is commonly used in a designer to display
    ///         a list of controls on the Web Forms page that can be associated with a relational list control.
    ///     </para>
    ///     <para>
    ///         Any control that <b>inherits</b> from <see cref="ListControl">ListControl</see>
    ///         including the <see cref="DropDownList">DropDownList</see> control and
    ///         <see cref="ListBox">ListBox</see> control are converted and
    ///         listed for binding to the relational list control.  This also includes other
    ///         <see cref="RelationalDropDownList" /> and <see cref="RelationalListBox" /> controls.
    ///     </para>
    /// </remarks>
    [PermissionSet(SecurityAction.LinkDemand)]
    [PermissionSet(SecurityAction.InheritanceDemand)]
    public sealed class ListControlConverter : ControlConverter<ListControl> { }
}