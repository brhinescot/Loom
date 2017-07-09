#region Using Directives

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    /// </summary>
    public static class Desktop
    {
        /// <summary>
        ///     Creates an <see cref="Image" /> of the full desktop.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Image" /> containing an image of the full desktop.</returns>
        public static Image Capture()
        {
            return Capture(NativeMethods.GetDesktopWindow());
        }

        /// <summary>
        ///     Gets a segment of the desktop as an image.
        /// </summary>
        /// <param name="rectangle">The rectangular area to capture.</param>
        /// <returns>
        ///     A <see cref="System.Drawing.Image" /> containing an image of the desktop
        ///     at the specified coordinates
        /// </returns>
        public static Image Capture(Rectangle rectangle)
        {
            return Capture(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        ///     Retrieves an image of the specified part of your screen.
        /// </summary>
        /// <param name="x">The X coordinate of the requested area</param>
        /// <param name="y">The Y coordinate of the requested area</param>
        /// <param name="width">The width of the requested area</param>
        /// <param name="height">The height of the requested area</param>
        /// <returns>
        ///     A <see cref="System.Drawing.Image" /> of the desktop at
        ///     the specified coordinates.
        /// </returns>
        public static Image Capture(int x, int y, int width, int height)
        {
            //Create the image and graphics to capture the portion of the desktop.
            Image destinationImage = new Bitmap(width, height);
            Graphics destinationGraphics = Graphics.FromImage(destinationImage);

            IntPtr destinationGraphicsHandle = IntPtr.Zero;
            IntPtr desktopWindow = IntPtr.Zero;
            IntPtr windowDC = IntPtr.Zero;

            try
            {
                //Pointers for window handles
                destinationGraphicsHandle = destinationGraphics.GetHdc();
                desktopWindow = NativeMethods.GetDesktopWindow();
                windowDC = NativeMethods.GetWindowDC(desktopWindow);

                //Get the screen capture
                NativeMethods.BitBlt(destinationGraphicsHandle, 0, 0, width, height, windowDC, x, y, NativeMethods.SRCCOPY);
            }
            finally
            {
                //Release and dispose the graphics object so we have an image to work with.
                NativeMethods.ReleaseDC(desktopWindow, windowDC);
                destinationGraphics.ReleaseHdc(destinationGraphicsHandle);
            }

            // Don't forget to dispose this image
            return destinationImage;
        }

        /// <summary>
        ///     Captures the foreground window.
        /// </summary>
        /// <returns></returns>
        public static Image CaptureForegroundWindow()
        {
            return Capture(NativeMethods.GetForegroundWindow());
        }

        /// <summary>
        ///     Captures the foreground window.
        /// </summary>
        /// <param name="fillColor">The fillColor.</param>
        /// <returns></returns>
        public static Image CaptureForegroundWindow(Color fillColor)
        {
            return Capture(NativeMethods.GetForegroundWindow(), fillColor);
        }

        /// <summary>
        ///     Captures the window at the specified point.
        /// </summary>
        /// <param name="windowLocation">The window location.</param>
        /// <returns></returns>
        public static Image CaptureWindowAtPoint(Point windowLocation)
        {
            return Capture(NativeMethods.WindowFromPoint(NativeMethods.Point.FromManaged(windowLocation)));
        }

        /// <summary>
        ///     Captures the window at the specified point.
        /// </summary>
        /// <param name="windowLocation">The window location.</param>
        /// <param name="fillColor">The fillColor.</param>
        /// <returns></returns>
        public static Image CaptureWindowAtPoint(Point windowLocation, Color fillColor)
        {
            return Capture(NativeMethods.WindowFromPoint(NativeMethods.Point.FromManaged(windowLocation)), fillColor);
        }

        /// <summary>
        ///     Creates an <see cref="Image" /> of the control specified by the <paramref name="hWnd" /> parameter.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>
        ///     A <see cref="System.Drawing.Image" /> containing an image of the control.
        /// </returns>
        internal static Image Capture(IntPtr hWnd)
        {
            return Capture(hWnd, Color.Empty);
        }

        /// <summary>
        ///     Creates an <see cref="Image" /> of the control specified by the <paramref name="hWnd" /> parameter.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="fillColor">Color of the background.</param>
        /// <returns>
        ///     A <see cref="System.Drawing.Image" /> containing an image of the control.
        /// </returns>
        internal static Image Capture(IntPtr hWnd, Color fillColor)
        {
            IntPtr sourceDeviceContext = IntPtr.Zero;
            Bitmap capture = null;

            try
            {
                sourceDeviceContext = NativeMethods.GetWindowDC(hWnd);
                using (Graphics sourceGraphics = Graphics.FromHdc(sourceDeviceContext))
                {
                    capture = new Bitmap((int) sourceGraphics.VisibleClipBounds.Width, (int) sourceGraphics.VisibleClipBounds.Height);
                    using (Graphics captureGraphics = Graphics.FromImage(capture))
                    {
                        IntPtr captureGraphicsHdc = captureGraphics.GetHdc();

                        //Get the screen capture
                        NativeMethods.BitBlt(captureGraphicsHdc, 0, 0, (int) sourceGraphics.VisibleClipBounds.Width, (int) sourceGraphics.VisibleClipBounds.Height, sourceDeviceContext, 0, 0, NativeMethods.SRCCOPY | NativeMethods.CAPTUREBLT);
                        captureGraphics.ReleaseHdc(captureGraphicsHdc);
                    }
                }

                if (fillColor != Color.Empty)
                    return ColorNonRegionFormArea(hWnd, capture, fillColor);
                else
                    return capture;
            }
            finally
            {
                if (capture != null && fillColor != Color.Empty)
                    capture.Dispose();
                //Release and dispose the graphics object so we have an image to work with.
                NativeMethods.ReleaseDC(hWnd, sourceDeviceContext);
            }
        }

        private static Bitmap ColorNonRegionFormArea(IntPtr hWnd, Image capture, Color color)
        {
            Bitmap finalCapture;
            IntPtr windowRegion = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            NativeMethods.GetWindowRgn(hWnd, windowRegion);

            using (Region region = Region.FromHrgn(windowRegion))
            using (Graphics drawGraphics = Graphics.FromImage(capture))
            using (SolidBrush brush = new SolidBrush(color))
            {
                RectangleF bounds = region.GetBounds(drawGraphics);
                if (bounds == RectangleF.Empty)
                {
                    GraphicsUnit unit = GraphicsUnit.Pixel;
                    bounds = capture.GetBounds(ref unit);
                }
                else
                {
                    region.Complement(bounds);
                    drawGraphics.FillRegion(brush, region);
                }
                finalCapture = new Bitmap((int) bounds.Width, (int) bounds.Height);
                using (Graphics finalGraphics = Graphics.FromImage(finalCapture))
                {
                    finalGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    finalGraphics.DrawImage(capture, new RectangleF(new PointF(0, 0), finalCapture.Size), bounds, GraphicsUnit.Pixel);
                }
            }
            return finalCapture;
        }
    }
}