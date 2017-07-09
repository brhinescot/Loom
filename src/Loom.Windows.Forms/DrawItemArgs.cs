#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class DrawItemArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawItemArgs" /> class.
        /// </summary>
        public DrawItemArgs() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawItemArgs" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="selected">if set to <c>true</c> [selected].</param>
        /// <param name="focused">if set to <c>true</c> [focused].</param>
        public DrawItemArgs(ExpandingListViewItem item, Rectangle bounds, Graphics graphics, bool selected, bool focused)
        {
            Item = item;
            Bounds = bounds;
            Graphics = graphics;
            Selected = selected;
            Focused = focused;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DrawItemArgs" /> is focused.
        /// </summary>
        /// <value><c>true</c> if focused; otherwise, <c>false</c>.</value>
        public bool Focused { get; set; }

        /// <summary>
        ///     Gets or sets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        public Graphics Graphics { get; set; }

        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public ExpandingListViewItem Item { get; set; }

        /// <summary>
        ///     Gets or sets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DrawItemArgs" /> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get; set; }
    }
}