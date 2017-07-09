#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class LayeredWindow : NativeWindow, IDisposable
    {
        private int animationFrames;
        private int animationTime;

        private double windowOpacity = 1.0;

        /// <summary>
        ///     Gets or sets the opacity.
        /// </summary>
        /// <value>The opacity.</value>
        public double Opacity
        {
            get => windowOpacity;
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0;

                windowOpacity = value;
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets the opacity as byte.
        /// </summary>
        /// <value>The opacity as byte.</value>
        private byte OpacityAsByte => (byte) (windowOpacity * 255);

        /// <summary>
        ///     Window bounds
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds
        {
            get
            {
                NativeMethods.RECT rect = NativeMethods.RECT.Empty;
                NativeMethods.GetWindowRect(Handle, ref rect);

                return rect.Bounds;
            }
            set
            {
                NativeMethods.MoveWindow(Handle, value.X, value.Y, value.Width, value.Height, true);
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets or sets the top.
        /// </summary>
        /// <value>The top.</value>
        public int Top
        {
            get => Bounds.Top;
            set
            {
                Bounds = new Rectangle(Bounds.X, value, Bounds.Width, Bounds.Height);
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        public int Left
        {
            get => Bounds.Left;
            set
            {
                Bounds = new Rectangle(value, Bounds.Y, Bounds.Width, Bounds.Height);
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get => Bounds.Height;
            set
            {
                Bounds = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, value);
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get => Bounds.Width;
            set
            {
                Bounds = new Rectangle(Bounds.X, Bounds.Y, value, Bounds.Height);
                PaintLayer();
            }
        }

        /// <summary>
        ///     Gets the create params.
        /// </summary>
        /// <value>The create params.</value>
        public virtual CreateParams CreateParams
        {
            get
            {
                CreateParams cp = new CreateParams {Caption = "LoomLayeredWindow"};

                cp.Caption = string.Empty;
                cp.ClassStyle = 0;
                cp.Width = Bounds.Width;
                cp.Height = Bounds.Height;
                cp.X = Bounds.X;
                cp.Y = Bounds.Y;
                cp.Style = NativeMethods.WS_POPUP;
                cp.ExStyle =
                    NativeMethods.WS_EX_TOOLWINDOW |
                    NativeMethods.WS_EX_TOPMOST |
                    NativeMethods.WS_EX_LAYERED;

                return cp;
            }
        }

        #region IDisposable Members

        /// <summary>
        ///     Dispose all allocated resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// </summary>
        [Category("Appearance")]
        public event EventHandler<LayerPaintEventArgs> Paint;

        /// <summary>
        /// </summary>
        [Category("Behavior")]
        public event MouseEventHandler MouseDown;

        /// <summary>
        ///     Shows the <see cref="LayeredWindow" />.
        /// </summary>
        public void Show()
        {
            if (Handle == IntPtr.Zero)
                Create();

            NativeMethods.ShowWindow(Handle, NativeMethods.SW_SHOWNOFOCUS);
            Invalidate();
        }

        /// <summary>
        ///     Shows the <see cref="LayeredWindow" /> using the specified animation effect.
        /// </summary>
        /// <param name="style">The style of the animation.</param>
        /// <param name="duration">The duration of the animation in milliseconds.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The <paramref name="frameCount" />
        ///     exceeds the <paramref name="duration" />.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Show(AnimationStyle style, int duration = 800, int frameCount = 30)
        {
            // If there is no animation time or no frames, just show the window.
            // Otherwise, start the animation.
            if ((AnimationStyle.None & style) == AnimationStyle.None || frameCount <= 0 || duration <= 0)
                Show();
            else if (frameCount < duration)
                AnimateWindow(style, duration, frameCount);
            else
                throw new ArgumentOutOfRangeException("frameCount", frameCount, string.Format("The number of frames in the animation must be less than the number of milliseconds in the duration of the animation. The duration is {0}.", duration));
        }

        private void AnimateWindow(AnimationStyle style, int duration, int frameCount)
        {
            ThreadStart threadStart = GetThreadStartForAnimationStyle(style);
            if (threadStart == null)
            {
                Show();
                return;
            }

            if (Handle == IntPtr.Zero)
                Create();

            animationFrames = frameCount;
            animationTime = duration;

            Thread t = new Thread(threadStart) {IsBackground = true};
            t.Start();
        }

        private ThreadStart GetThreadStartForAnimationStyle(AnimationStyle style)
        {
            ThreadStart threadStart;
            switch (style)
            {
                case AnimationStyle.Fade:
                    threadStart = FadeIn;
                    break;
                default:
                    threadStart = null;
                    break;
            }
            return threadStart;
        }

        private void FadeIn()
        {
            int sleepTime = animationTime / animationFrames;
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            double targetOpacity = Opacity;
            double interval = Opacity / animationFrames;
            double fadeOpacity = interval;
            Opacity = fadeOpacity;
            NativeMethods.ShowWindow(Handle, NativeMethods.SW_SHOWNOFOCUS);
            while (Opacity < targetOpacity)
            {
                Thread.Sleep(sleepTime);
                Opacity = fadeOpacity;
                fadeOpacity += interval;
            }
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void Hide()
        {
            NativeMethods.ShowWindow(Handle, NativeMethods.SW_HIDE);
        }

        /// <summary>
        ///     Invalidates this instance.
        /// </summary>
        public void Invalidate()
        {
            PaintLayer();
        }

        /// <summary>
        ///     Paints the layered window.
        /// </summary>
        protected void PaintLayer()
        {
            if (Bounds.Size == Size.Empty)
                return;

            using (Bitmap surface = new Bitmap(Bounds.Width, Bounds.Height, PixelFormat.Format32bppArgb))
            {
                PaintLayer(surface, windowOpacity);
            }
        }

        /// <summary>
        ///     Paints the layered window.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="opacity">The opacity.</param>
        protected void PaintLayer(Bitmap bitmap, double opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("The bitmap must be 32bpp with an alpha-channel.", "bitmap");

            windowOpacity = opacity;
            using (LayerPaintEventArgs args = new LayerPaintEventArgs(bitmap))
            {
                // Give clients the opportunity to paint to this bitmap.
                OnPaint(args);
                PaintNative(bitmap, OpacityAsByte);
            }
        }

        private void PaintNative(Bitmap bitmap, byte opacity)
        {
            IntPtr hdcDestination = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr hdcSource = NativeMethods.CreateCompatibleDC(hdcDestination);
            IntPtr hdcBitmap = IntPtr.Zero;
            IntPtr previousBitmap = IntPtr.Zero;

            try
            {
                // Get the GDI bitmap;
                hdcBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                // Create a device context from the GDI bitmap.
                previousBitmap = NativeMethods.SelectObject(hdcSource, hdcBitmap);

                // Create the native drawing options.
                NativeMethods.SIZE size = new NativeMethods.SIZE(bitmap.Width, bitmap.Height);
                NativeMethods.POINT source = new NativeMethods.POINT(0, 0);
                NativeMethods.POINT destination = new NativeMethods.POINT(Bounds.Left, Bounds.Top);
                NativeMethods.BLENDFUNCTION blend = new NativeMethods.BLENDFUNCTION
                {
                    BlendOp = NativeMethods.AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = opacity,
                    AlphaFormat = NativeMethods.AC_SRC_ALPHA
                };

                NativeMethods.UpdateLayeredWindow(
                    Handle,
                    hdcDestination,
                    ref destination,
                    ref size,
                    hdcSource,
                    ref source,
                    0,
                    ref blend,
                    2);
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, hdcDestination);
                if (hdcBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(hdcSource, previousBitmap);
                    NativeMethods.DeleteObject(hdcBitmap);
                }
                NativeMethods.DeleteDC(hdcSource);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (Handle != IntPtr.Zero)
                DestroyHandle();
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="LayeredWindow" /> is reclaimed by garbage collection.
        /// </summary>
        ~LayeredWindow()
        {
            Dispose(false);
        }

        private void Create()
        {
            CreateParams p = CreateParams;
            CreateHandle(p);
        }

        /// <summary>
        ///     Invokes the default window procedure associated with this window.
        /// </summary>
        /// <param name="m">A <see cref="System.Windows.Forms.Message"></see> that is associated with the current Windows message.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x132:
                case 0x133:
                case 0x134:
                case 0x135:
                case 310:
                case 0x137:
                case 0x200:
                {
                    //this.WmMouseMove(ref m);
                    return;
                }
                case 0x201:
                {
                    WmMouseDown(ref m, MouseButtons.Left, 1);
                    return;
                }
                case 0x202:
                {
                    //this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Left, 1);
                    return;
                }
                case 0x203:
                {
                    WmMouseDown(ref m, MouseButtons.Left, 2);
                    //if (this.GetStyle(ControlStyles.StandardDoubleClick))
                    //{
                    //    this.SetState(0x4000000, true);
                    //}
                    return;
                }
                case 0x204:
                {
                    WmMouseDown(ref m, MouseButtons.Right, 1);
                    return;
                }
                case 0x205:
                {
                    //this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Right, 1);
                    return;
                }
                case 0x206:
                {
                    WmMouseDown(ref m, MouseButtons.Right, 2);
                    //if (this.GetStyle(ControlStyles.StandardDoubleClick))
                    //{
                    //    this.SetState(0x4000000, true);
                    //}
                    return;
                }
                case 0x207:
                {
                    WmMouseDown(ref m, MouseButtons.Middle, 1);
                    return;
                }
                case 520:
                {
                    //this.WmMouseUp(ref m, System.Windows.Forms.MouseButtons.Middle, 1);
                    return;
                }
                case 0x209:
                {
                    WmMouseDown(ref m, MouseButtons.Middle, 2);
                    //if (this.GetStyle(ControlStyles.StandardDoubleClick))
                    //{
                    //    this.SetState(0x4000000, true);
                    //}
                    return;
                }
                case 0x20a:
                {
                    //this.WmMouseWheel(ref m);
                    return;
                }
                case 0x20b:
                {
                    //this.WmMouseDown(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 1);
                    return;
                }
                case 0x20c:
                {
                    //this.WmMouseUp(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 1);
                    return;
                }
                case 0x20d:
                {
                    //this.WmMouseDown(ref m, this.GetXButton(System.Windows.Forms.NativeMethods.Util.HIWORD(m.WParam)), 2);
                    //if (this.GetStyle(ControlStyles.StandardDoubleClick))
                    //{
                    //    this.SetState(0x4000000, true);
                    //}
                    return;
                }
            }
        }

        private void WmMouseDown(ref Message m, MouseButtons mouseButtons, int p)
        {
            // TODO: Add native MouseDown handling.
            OnMouseDown(new MouseEventArgs(mouseButtons, 0, 0, 0, 0));
        }

        /// <summary>
        ///     Raises the <see cref="MouseDown" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="MouseEventArgs" /> instance containing the
        ///     event data.
        /// </param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseEventHandler handler = MouseDown;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Paint" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="LayerPaintEventArgs" /> instance containing the
        ///     event data.
        /// </param>
        protected virtual void OnPaint(LayerPaintEventArgs e)
        {
            EventHandler<LayerPaintEventArgs> handler = Paint;
            if (handler != null)
                handler(this, e);
        }
    }
}