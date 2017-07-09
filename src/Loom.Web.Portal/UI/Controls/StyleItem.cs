#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class StyleItem : Control, IDataItemContainer
    {
        private readonly object dataItem;

        internal StyleItem(object entity)
        {
            dataItem = entity;
        }

        #region IDataItemContainer Members

        object IDataItemContainer.DataItem => dataItem;

        int IDataItemContainer.DataItemIndex => 0;

        int IDataItemContainer.DisplayIndex => 0;

        #endregion
    }
}