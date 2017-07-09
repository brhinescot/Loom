#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class DrawGroupHeaderArgs
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DrawGroupHeaderArgs" /> is focused.
        /// </summary>
        /// <value><c>true</c> if focused; otherwise, <c>false</c>.</value>
        public bool Focused { get; set; }

        /// <summary>
        ///     Gets or sets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        public Graphics Graphics { get; set; }

        /// <summary>
        ///     Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public ExpandingListViewGroup Group { get; set; }

        /// <summary>
        ///     Gets or sets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DrawGroupHeaderArgs" /> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get; set; }
    }
}