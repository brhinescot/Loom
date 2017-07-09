#region Using Directives

using System.ComponentModel;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewGroup : ExpandingListViewItemBase
    {
        private ExpandingListViewItemCollection items;

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ExpandingListViewItemCollection Items
        {
            get
            {
                if (items == null)
                    items = new ExpandingListViewItemCollection(this);
                return items;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="ExpandingListViewGroup" /> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;
                foreach (ExpandingListViewItem item in items)
                    item.SetSelected(Selected);
            }
        }
    }
}