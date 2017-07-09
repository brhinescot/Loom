#region Using Directives

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

#endregion

namespace Loom.Windows.Forms
{
    ///<summary>
    ///</summary>
    public class HatchStyleEditor : UITypeEditor
    {
        /// <summary>
        ///     Overloaded. Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">
        ///     An ITypeDescriptorContext
        ///     that can be used to gain additional
        ///     context information.
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
        ///     An ITypeDescriptorContext that
        ///     can be used to gain additional context information.
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
                HatchStyle style = (HatchStyle) e.Value;
                Rectangle bounds = e.Bounds;
                using (Brush brush = new HatchBrush(style, SystemColors.WindowText, SystemColors.Window))
                {
                    GraphicsState state = e.Graphics.Save();
                    e.Graphics.RenderingOrigin = new Point(bounds.X, bounds.Y);
                    e.Graphics.FillRectangle(brush, bounds);
                    e.Graphics.Restore(state);
                }
            }
        }
    }
}