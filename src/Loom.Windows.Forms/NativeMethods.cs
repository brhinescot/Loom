#region Using Directives

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace Loom.Windows.Forms
{
    internal static class NativeMethods
    {
        #region AnimateWindowFlags enum

        [Flags]
        public enum AnimateWindowFlags : uint
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        #endregion

        #region InvalidHotKeyModifiers enum

        public enum InvalidHotKeyModifiers
        {
            HKCOMB_NONE = 1,
            HKCOMB_S = 2,
            HKCOMB_C = 4,
            HKCOMB_A = 8,
            HKCOMB_SC = 16,
            HKCOMB_SA = 32,
            HKCOMB_CA = 64,
            HKCOMB_SCA = 128
        }

        #endregion

        internal const int WM_USER = 0x0400;

        internal const int HKM_SETHOTKEY = WM_USER + 1;
        internal const int HKM_GETHOTKEY = WM_USER + 2;
        internal const int HKM_SETRULES = WM_USER + 3;
        internal const int HOTKEYF_SHIFT = 0x01;
        internal const int HOTKEYF_CONTROL = 0x02;
        internal const int HOTKEYF_ALT = 0x04;
        internal const int HOTKEYF_EXT = 0x08;
        internal const int MOD_ALT = 0x0001;
        internal const int MOD_CONTROL = 0x0002;
        internal const int MOD_SHIFT = 0x0004;
        internal const int MOD_WIN = 0x0008;
        internal const string HOTKEY_CLASS = "msctls_hotkey32";

        internal const int WS_CHILD = 0x40000000;
        internal const int WS_VISIBLE = 0x10000000;

        internal const int WS_BORDER = 0x800000;
        internal const int WS_EX_CLIENTEDGE = 0x200;
        internal const int WS_EX_CONTROLPARENT = 0x00010000;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_NOACTIVATE = 0x08000000;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_LAYERED = 0x80000;
        internal const int WS_EX_SHOWTASKBAR = 0x00000088;
        internal const int WS_POPUP = -2147483648;

        internal const short WM_PAINT = 0x00f;
        internal const int WM_SETICON = 0x80;
        internal const int WM_SETTEXT = 0x000c;
        internal const int WM_HOTKEY = 0x0312;

        internal const int ICON_SMALL = 0;
        internal const int ICON_BIG = 1;

        internal const int ECM_FIRST = 0x1500;
        internal const int EM_SHOWBALLOONTIP = ECM_FIRST + 3;
        internal const int GW_OWNER = 4;

        internal const byte AC_SRC_OVER = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;

        internal const int SW_HIDE = 0;
        internal const int SW_SHOWNOFOCUS = 4;
        internal const int SW_SHOW = 5;

        internal const int ES_AUTOHSCROLL = 0x80;
        internal const int ES_AUTOVSCROLL = 0x40;
        internal const int ES_CENTER = 1;
        internal const int ES_LEFT = 0;
        internal const int ES_LOWERCASE = 0x10;
        internal const int ES_MULTILINE = 4;
        internal const int ES_NOHIDESEL = 0x100;
        internal const int ES_PASSWORD = 0x20;
        internal const int ES_READONLY = 0x800;
        internal const int ES_RIGHT = 2;
        internal const int ES_UPPERCASE = 8;

        internal const int DISPID_FORECOLOR = -513;

        internal const int MAPVK_VK_TO_VSC = 0;
        internal const int MAPVK_VSC_TO_VK = 1;
        internal const int MAPVK_VK_TO_CHAR = 2;
        internal const int MAPVK_VSC_TO_VK_EX = 3;
        internal const uint KLF_NOTELLSHELL = 0x00000080;
        internal static readonly HandleRef NullHandleRef;

        static NativeMethods()
        {
            NullHandleRef = new HandleRef(null, IntPtr.Zero);
        }

        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, ref EDITBALLOONTIP lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        internal static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        // HotKeys
        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vic);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern ushort GlobalDeleteAtom(ushort nAtom);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MapVirtualKeyExW", ExactSpelling = true)]
        internal static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetKeyNameText(uint lParam, StringBuilder lpString, int nSize);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetKeyboardLayout(uint idThread);

        // Windows
        [DllImport("user32.dll")]
        public static extern int MsgWaitForMultipleObjects(
            int nCount, // number of handles in array
            int pHandles, // object-handle array
            bool bWaitAll, // wait option
            int dwMilliseconds, // time-out interval
            int dwWakeMask // input-event type
        );

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetWindow(IntPtr hwnd, int cmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int newWidth, int newHeight, bool repaint);

        [DllImport("user32.dll")]
        internal static extern bool AnimateWindow(IntPtr hwnd, uint dwTime, AnimateWindowFlags dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern int ShowWindow(IntPtr hWnd, short nCmdShow);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll")]
        internal static extern IntPtr CreateIconIndirect(ref ICONINFO iconinfo);

        [DllImport("user32.dll")]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        internal static extern IntPtr GetWindowLongPtr(HandleRef hWnd, int nIndex);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);

            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        internal static int MakeHiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return number >> 16;
            return (number >> 16) & 0xffff;
        }

        internal static int MakeLoWord(int number)
        {
            return number & 0xffff;
        }

        internal static int MakeLong(int loWord, int hiWord)
        {
            return (hiWord << 16) | (loWord & 0xffff);
        }

        internal static IntPtr MakeLParam(int loWord, int hiWord)
        {
            return (IntPtr) ((hiWord << 16) | (loWord & 0xffff));
        }

        internal static IntPtr GetTopLevelOwner(IntPtr hWnd)
        {
            IntPtr hwndOwner = hWnd;
            IntPtr hwndCurrent = hWnd;
            while (hwndCurrent != (IntPtr) 0)
            {
                hwndCurrent = GetWindow(hwndCurrent, GW_OWNER);
                if (hwndCurrent != (IntPtr) 0)
                    hwndOwner = hwndCurrent;
            }
            return hwndOwner;
        }

        #region Nested type: BLENDFUNCTION

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        #endregion

        #region Nested type: EDITBALLOONTIP

        [StructLayout(LayoutKind.Sequential)]
        internal struct EDITBALLOONTIP
        {
            internal int cbStruct;
            [MarshalAs(UnmanagedType.LPWStr)] internal string pszTitle;
            [MarshalAs(UnmanagedType.LPWStr)] internal string pszText;
            internal int ttiIcon;

            internal EDITBALLOONTIP(string title, string text, int icon)
            {
                cbStruct = Marshal.SizeOf(typeof(EDITBALLOONTIP));
                pszTitle = title;
                pszText = text;
                ttiIcon = icon;
            }
        }

        #endregion

        #region Nested type: ICONINFO

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public uint xHotspot;
            public uint yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        #endregion

        #region Nested type: POINT

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static explicit operator POINT(Point pt)
            {
                return new POINT(pt.X, pt.Y);
            }
        }

        #endregion

        #region Nested type: RECT

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public static RECT Empty = new RECT(0, 0, 0, 0);

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Height => Bottom - Top + 1;

            public int Width => Right - Left + 1;

            public SIZE Size => new SIZE(Width, Height);

            public Point Location => new Point(Left, Top);

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                       ^ ((Width << 0x1a) | (Width >> 6))
                       ^ ((Height << 7) | (Height >> 0x19));
            }

            public Rectangle Bounds => Rectangle.FromLTRB(Left, Top, Right, Bottom);
        }

        #endregion

        #region Nested type: SIZE

        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            public int Width;
            public int Height;

            public SIZE(int w, int h)
            {
                Width = w;
                Height = h;
            }
        }

        #endregion
    }
}