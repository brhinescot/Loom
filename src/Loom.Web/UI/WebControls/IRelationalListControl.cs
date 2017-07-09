#region Using Directives

using System.Web.UI.WebControls;

#endregion

namespace Loom.Web.UI.WebControls
{
    /// <summary>
    ///     Exposes common properties and methods of the relational list controls.
    /// </summary>
    /// <remarks>
    ///     This interface brings the controls back together under a common set of expanded methods.
    /// </remarks>
    public interface IRelationalListControl
    {
        /// <summary>
        ///     Gets a referance to the controls parent <see cref="System.Web.UI.WebControls.ListControl" />.
        /// </summary>
        /// <remarks>
        ///     <runtimeonly />
        /// </remarks>
        ListControl ParentListControl { get; }

        /// <summary>
        ///     Gets a <b>boolean</b> value indicating if the control has a parent
        ///     <see cref="System.Web.UI.WebControls.ListControl" /> defined.
        /// </summary>
        /// <remarks>
        ///     <para>A top level <b>ListControl</b> will not have a parent defined.</para>
        ///     <runtimeonly />
        /// </remarks>
        bool HasParent { get; }

        /// <summary>
        ///     Gets or sets the ID of the parent <see cref="System.Web.UI.WebControls.ListControl" />.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The control automatically sets up the relationship between it and the
        ///         parent <b>ListControl</b>.
        ///     </para>
        ///     <para>
        ///         A design time list of compatible controls is exposed as a list in the controls property
        ///         inspector.  The list is provided by the <see cref="ListControlConverter" /> class.
        ///     </para>
        /// </remarks>
        string ParentListId { get; set; }
    }
}