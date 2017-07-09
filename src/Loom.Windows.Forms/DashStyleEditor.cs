#region Using Directives

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     The UITypeEditor of DashStyle
    /// </summary>
    public class DashStyleEditor : UITypeEditor
    {
        /// <summary>
        ///     Overloaded. Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">
        ///     An ITypeDescriptorContext that
        ///     can be used to gain additional context information.
        /// </param>
        /// <returns>
        ///     A UITypeEditorEditStyle value that indicates
        ///     the style of editor used by the EditValue method.
        /// </returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        /// <summary>
        ///     Indicates whether the specified context supports painting
        ///     a representation of an object's value within the specified context.
        /// </summary>
        /// <param name="context">
        ///     An ITypeDescriptorContext
        ///     that can be used to gain additional
        ///     context information.
        /// </param>
        /// <returns>
        ///     true if PaintValue is implemented;
        ///     otherwise, false.
        /// </returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        ///     Paints a representation of the value of an object
        ///     using the specified PaintValueEventArgs.
        /// </summary>
        /// <param name="e">
        ///     A PaintValueEventArgs that
        ///     indicates what to paint and where to paint it.
        /// </param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                DashStyle style = (DashStyle) e.Value;
                Rectangle bounds = e.Bounds;

                int y = bounds.Y + bounds.Height / 2;
                Point start = new Point(bounds.Left, y);
                Point end = new Point(bounds.Right, y);

                using (Brush brush = new SolidBrush(SystemColors.Window))
                using (Pen pen = new Pen(SystemColors.WindowText))
                {
                    pen.DashStyle = style;
                    pen.Width = 2;

                    GraphicsState state = e.Graphics.Save();
                    e.Graphics.FillRectangle(brush, bounds);
                    e.Graphics.DrawLine(pen, start, end);
                    e.Graphics.Restore(state);
                }
            }
        }
    }
}