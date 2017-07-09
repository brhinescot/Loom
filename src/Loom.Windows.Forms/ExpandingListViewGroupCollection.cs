#region Using Directives

using System;
using System.Drawing;
using Loom.Collections;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewGroupCollection : ChangeNotificationCollection<ExpandingListViewGroup>
    {
        /// <summary>
        ///     The event that is raised when the contents of the collection have changed.
        /// </summary>
        public event EventHandler<CollectionChangedEventArgs<ExpandingListViewItem>> ItemChanged;

        /// <summary>
        ///     Inserts an element into the <see cref="System.Collections.ObjectModel.Collection{T}"></see>
        ///     at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is less than zero.-or-index is
        ///     greater than <see cref="System.Collections.ObjectModel.Collection{T}.Count"></see>.
        /// </exception>
        protected override void InsertItem(int index, ExpandingListViewGroup item)
        {
            item.Items.Changed += HandleItemsChanged;
            base.InsertItem(index, item);
        }

        private void HandleItemsChanged(object sender, CollectionChangedEventArgs<ExpandingListViewItem> e)
        {
            EventHandler<CollectionChangedEventArgs<ExpandingListViewItem>> handler = ItemChanged;
            if (handler != null)
                handler(sender, e);
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