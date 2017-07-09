#region Using Directives

using System.Web.UI;

#endregion

namespace Loom.Web.Portal.UI.Controls
{
    public class ScriptItem : Control, IDataItemContainer
    {
        private readonly object dataItem;

        internal ScriptItem(object entity)
        {
            dataItem = entity;
        }

        public JavaScriptType ScriptType { get; set; }

        #region IDataItemContainer Members

        object IDataItemContainer.DataItem => dataItem;

        int IDataItemContainer.DataItemIndex => 0;

        int IDataItemContainer.DisplayIndex => 0;

        #endregion
    }
}