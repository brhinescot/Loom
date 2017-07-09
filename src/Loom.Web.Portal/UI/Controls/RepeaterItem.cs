#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class RepeaterItem : Control, IDataItemContainer
    {
        private readonly object dataItem;

        private readonly int dataItemIndex;

        private readonly int displayIndex;

        internal RepeaterItem(object entity, int index = 0)
        {
            dataItem = entity;
            dataItemIndex = displayIndex = index;
        }

        #region IDataItemContainer Members

        object IDataItemContainer.DataItem => dataItem;

        int IDataItemContainer.DataItemIndex => dataItemIndex;

        int IDataItemContainer.DisplayIndex => displayIndex;

        #endregion
    }
}