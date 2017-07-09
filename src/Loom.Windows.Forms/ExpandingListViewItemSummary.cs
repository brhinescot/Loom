#region Using Directives

using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ExpandingListViewItemSummary
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItemSummary" /> class.
        /// </summary>
        public ExpandingListViewItemSummary() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpandingListViewItemSummary" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public ExpandingListViewItemSummary(string text)
        {
            Text = text;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font { get; set; }

        /// <summary>
        ///     Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor { get; set; }
    }
}