#region Using Directives

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms.Tests
{
    internal class LayeredFormTest : LayeredWindow
    {
        private Image image;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Dispose();
        }

        protected override void OnPaint(LayerPaintEventArgs e)
        {
            if (image == null)
                image = Thumbnail.FromImage(Image.FromFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Files\\search.png"), true), Bounds.Height - 4);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, e.Bounds.Width, e.Bounds.Height);
            e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
            e.Graphics.DrawImage(image, 2, 2, image.Width, image.Height);
            e.Graphics.DrawString("Your search is in progress.\n\nSearching in all local drives.\n\n6% Complete", new Font(FontFamily.GenericSansSerif, 8.5f), SystemBrushes.WindowText, image.Width + 5, 3);
            e.Graphics.DrawLine(SystemPens.ControlDark, image.Width + 5, 18, e.Bounds.Width - 2, 18);

            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            base.Dispose(disposing);
        }
    }
}