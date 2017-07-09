#region Using Directives

using System.ComponentModel;
using System.Drawing.Design;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public interface IListControl
    {
        [DefaultValue(null)]
        [MergableProperty(false)]
        [Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        ListItemCollection Items { get; }
    }
}