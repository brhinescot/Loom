#region Using Directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewItemComparer : IComparer<ExpandingListViewItem>
    {
        private readonly int columnIndex;
        private readonly SortOrder order;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItemComparer" /> class.
        /// </summary>
        public ExpandingListViewItemComparer() : this(0, SortOrder.Ascending) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItemComparer" /> class.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="order">The order.</param>
        public ExpandingListViewItemComparer(int column, SortOrder order)
        {
            columnIndex = column;
            this.order = order;
        }

        #region IComparer<ExpandingListViewItem> Members

        /// <summary>
        ///     Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        ///     Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
        /// </returns>
        public int Compare(ExpandingListViewItem x, ExpandingListViewItem y)
        {
            Argument.Assert.IsNotNull(x, "x");
            Argument.Assert.IsNotNull(y, "y");

            int returnVal;
            returnVal = string.Compare(x.SubItems[columnIndex].Text, y.SubItems[columnIndex].Text, StringComparison.CurrentCulture);

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                returnVal *= -1; // Invert the value returned by String.Compare.
            return returnVal;
        }

        #endregion
    }
}