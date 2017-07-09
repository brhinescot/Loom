#region Using Directives

using System.Drawing;
using Loom.Collections;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewItemCollection : ChangeNotificationCollection<ExpandingListViewItem>
    {
        private readonly ExpandingListViewGroup parent;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItemCollection" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ExpandingListViewItemCollection(ExpandingListViewGroup parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is less than zero.-or-index is
        ///     greater than <see cref="System.Collections.ObjectModel.Collection{T}.Count"></see>.
        /// </exception>
        protected override void InsertItem(int index, ExpandingListViewItem item)
        {
            item.Parent = parent;
            base.InsertItem(index, item);
        }

        /// <summary>
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        ///     A new <see cref="ExpandingListViewGroupCollection" />.
        /// </returns>
        internal ExpandingListViewItemBase BinaryBoundsSearch(Point point)
        {
            int left = 0;
            int right = Items.Count - 1;

            while (right >= 0)
            {
                int index = (left + right) / 2;
                ExpandingListViewItemBase item = Items[index];
                if (item.RenderBounds.Contains(point))
                    return item;

                if (point.Y < item.RenderBounds.Y)
                    right = index - 1;
                else
                    left = index + 1;
            }
            return null;
        }
    }
}