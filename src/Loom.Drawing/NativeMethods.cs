#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Class for getting images of the desktop.
    /// </summary>
    /// <threadsafety static="false" instance="false" />
    /// <note type="caution">This class is not thread safe.</note>
    /// <remarks>
    ///     This class has been scaled back to the essentials for capturing a segment of
    ///     the desktop in order to keep Cropper as small as possible.
    /// </remarks>
    internal static class NativeMethods
    {
        internal const uint SRCCOPY = 0x00CC0020;
        internal const uint CAPTUREBLT = 1073741824;

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowDC", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr GetWindowDC([In] IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        internal static extern int ReleaseDC([In] IntPtr hWnd, [In] IntPtr hDC);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt([In] IntPtr hdc, int x, int y, int cx, int cy, [In] IntPtr hdcSrc, int x1, int y1, uint rop);

        [DllImport("user32.dll", EntryPoint = "GetWindowRgn")]
        internal static extern int GetWindowRgn([In] IntPtr hWnd, [In] IntPtr hRgn);

        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn")]
        internal static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

        #region Nested type: Point

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// LONG->int
            public int x;

            /// LONG->int
            public int y;

            public static Point FromManaged(System.Drawing.Point point)
            {
                Point nativePoint = new Point {x = point.X, y = point.Y};
                return nativePoint;
            }
        }

        #endregion
    }
}