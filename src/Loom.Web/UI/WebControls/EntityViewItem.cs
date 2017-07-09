#region Using Directives

using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

#endregion

namespace Loom.Web.UI.WebControls
{
    [ToolboxItem(false)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class EntityViewItem : Control, IDataItemContainer
    {
        private readonly object dataItem;

        private readonly int dataItemIndex;

        private readonly int displayIndex;

        internal EntityViewItem(object entity)
        {
            dataItem = entity;
            dataItemIndex = displayIndex = 0;
        }

        #region IDataItemContainer Members

        object IDataItemContainer.DataItem => dataItem;

        int IDataItemContainer.DataItemIndex => dataItemIndex;

        int IDataItemContainer.DisplayIndex => displayIndex;

        #endregion
    }
}