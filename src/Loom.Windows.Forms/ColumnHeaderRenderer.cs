#region Using Directives

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ColumnHeaderRenderer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ColumnHeaderRenderer" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="header">The header.</param>
        /// <param name="state">The state.</param>
        /// <param name="foreColor">Color of the fore.</param>
        /// <param name="backColor">Color of the back.</param>
        /// <param name="font">The font.</param>
        public ColumnHeaderRenderer(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
        {
            Graphics = graphics;
            Bounds = bounds;
            ColumnIndex = columnIndex;
            Header = header;
            State = state;
            ForeColor = foreColor;
            BackColor = backColor;
            Font = font;
        }

        /// <summary>
        ///     Gets the color for the background.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor { get; }

        /// <summary>
        ///     Gets or sets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds { get; set; }

        /// <summary>
        ///     Gets the index of the column.
        /// </summary>
        /// <value>The index of the column.</value>
        public int ColumnIndex { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether [draw default].
        /// </summary>
        /// <value><c>true</c> if [draw default]; otherwise, <c>false</c>.</value>
        public bool DrawDefault { get; set; }

        /// <summary>
        ///     Gets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font { get; }

        /// <summary>
        ///     Gets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor { get; }

        /// <summary>
        ///     Gets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        public Graphics Graphics { get; }

        /// <summary>
        ///     Gets the header.
        /// </summary>
        /// <value>The header.</value>
        public ColumnHeader Header { get; }

        /// <summary>
        ///     Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public ListViewItemStates State { get; }

        /// <summary>
        ///     Draws the background.
        /// </summary>
        public virtual void DrawBackground()
        {
            if (Application.RenderWithVisualStyles)
            {
                VisualStyleRenderer renderer1 = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
                renderer1.DrawBackground(Graphics, Bounds);
            }
            else
            {
                using (Brush brush1 = new SolidBrush(BackColor))
                {
                    Graphics.FillRectangle(brush1, Bounds);
                }
                Rectangle rectangle1 = Bounds;
                rectangle1.Width--;
                rectangle1.Height--;
                Graphics.DrawRectangle(SystemPens.ControlDarkDark, rectangle1);
                rectangle1.Width--;
                rectangle1.Height--;
                Graphics.DrawLine(SystemPens.ControlLightLight, rectangle1.X, rectangle1.Y, rectangle1.Right, rectangle1.Y);
                Graphics.DrawLine(SystemPens.ControlLightLight, rectangle1.X, rectangle1.Y, rectangle1.X, rectangle1.Bottom);
                Graphics.DrawLine(SystemPens.ControlDark, rectangle1.X + 1, rectangle1.Bottom, rectangle1.Right, rectangle1.Bottom);
                Graphics.DrawLine(SystemPens.ControlDark, rectangle1.Right, rectangle1.Y + 1, rectangle1.Right, rectangle1.Bottom);
            }
        }

        /// <summary>
        ///     Draws the text.
        /// </summary>
        public virtual void DrawText()
        {
            HorizontalAlignment alignment1 = Header.TextAlign;
            TextFormatFlags flags1 = alignment1 == HorizontalAlignment.Left ? TextFormatFlags.GlyphOverhangPadding : (alignment1 == HorizontalAlignment.Center ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right);
            flags1 |= TextFormatFlags.WordEllipsis;
            DrawText(flags1);
        }

        /// <summary>
        ///     Draws the text.
        /// </summary>
        /// <param name="flags">The flags.</param>
        public virtual void DrawText(TextFormatFlags flags)
        {
            string text = Header.Text;
            Size textSize = TextRenderer.MeasureText(" ", Font);
            int textWidth = textSize.Width;
            Rectangle newBounds = Rectangle.Inflate(Bounds, -textWidth, 0);
            TextRenderer.DrawText(Graphics, text, Font, newBounds, ForeColor, flags);
        }
    }
}