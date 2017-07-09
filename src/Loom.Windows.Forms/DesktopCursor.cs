#region Using Directives

using System.Drawing;
using System.Windows.Forms;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    public class DesktopCursor : BitmapCursor
    {
        public DesktopCursor(Point point) : base((Bitmap) Desktop.CaptureWindowAtPoint(point)) { }

        public DesktopCursor(Point point, Cursor overlayCursor) : base((Bitmap) Desktop.CaptureWindowAtPoint(point), overlayCursor) { }

        public static implicit operator Cursor(DesktopCursor bc)
        {
            return bc.InnerCursor;
        }
    }
}