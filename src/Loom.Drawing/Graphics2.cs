#region Using Directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Remoting;
using System.Security.Permissions;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    /// </summary>
    public sealed class Graphics2 : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Graphics2" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public Graphics2(Graphics graphics)
        {
            BaseGraphics = graphics;
        }

        /// <summary>
        ///     Gets the base graphics.
        /// </summary>
        /// <value>The base graphics.</value>
        public Graphics BaseGraphics { get; }

        /// <summary>
        ///     Gets a value that specifies how composited images are drawn to this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <returns>
        ///     This property specifies a member of the <see cref="System.Drawing.Drawing2D.CompositingMode"></see> enumeration.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CompositingMode CompositingMode
        {
            get => BaseGraphics.CompositingMode;
            set => BaseGraphics.CompositingMode = value;
        }

        /// <summary>
        ///     Gets or sets the rendering origin of this <see cref="System.Drawing.Graphics"></see> object for dithering and for
        ///     hatch brushes.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Point"></see> structure that represents the dither origin for 8-bits-per-pixel and
        ///     16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Point RenderingOrigin
        {
            get => BaseGraphics.RenderingOrigin;
            set => BaseGraphics.RenderingOrigin = value;
        }

        /// <summary>
        ///     Gets or sets the rendering quality of composited images drawn to this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <returns>
        ///     This property specifies a member of the <see cref="System.Drawing.Drawing2D.CompositingQuality"></see> enumeration.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public CompositingQuality CompositingQuality
        {
            get => BaseGraphics.CompositingQuality;
            set => BaseGraphics.CompositingQuality = value;
        }

        /// <summary>
        ///     Gets or sets the rendering mode for text associated with this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="System.Drawing.Text.TextRenderingHint"></see> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public TextRenderingHint TextRenderingHint
        {
            get => BaseGraphics.TextRenderingHint;
            set => BaseGraphics.TextRenderingHint = value;
        }

        /// <summary>
        ///     Gets or sets the gamma correction value for rendering text.
        /// </summary>
        /// <returns>
        ///     The gamma correction value used for rendering antialiased and ClearType text.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public int TextContrast
        {
            get => BaseGraphics.TextContrast;
            set => BaseGraphics.TextContrast = value;
        }

        /// <summary>
        ///     Gets or sets the rendering quality for this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="System.Drawing.Drawing2D.SmoothingMode"></see> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public SmoothingMode SmoothingMode
        {
            get => BaseGraphics.SmoothingMode;
            set => BaseGraphics.SmoothingMode = value;
        }

        /// <summary>
        ///     Gets or set a value specifying how pixels are offset during rendering of this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     This property specifies a member of the <see cref="System.Drawing.Drawing2D.PixelOffsetMode"></see> enumeration
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public PixelOffsetMode PixelOffsetMode
        {
            get => BaseGraphics.PixelOffsetMode;
            set => BaseGraphics.PixelOffsetMode = value;
        }

        /// <summary>
        ///     Gets or sets the interpolation mode associated with this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="System.Drawing.Drawing2D.InterpolationMode"></see> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public InterpolationMode InterpolationMode
        {
            get => BaseGraphics.InterpolationMode;
            set => BaseGraphics.InterpolationMode = value;
        }

        /// <summary>
        ///     Gets or sets the world transformation for this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Drawing2D.Matrix"></see> object that represents the world transformation for this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Matrix Transform
        {
            get => BaseGraphics.Transform;
            set => BaseGraphics.Transform = value;
        }

        /// <summary>
        ///     Gets or sets the unit of measure used for page coordinates in this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="System.Drawing.GraphicsUnit"></see> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsUnit PageUnit
        {
            get => BaseGraphics.PageUnit;
            set => BaseGraphics.PageUnit = value;
        }

        /// <summary>
        ///     Gets or sets the scaling between world units and page units for this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <returns>
        ///     This property specifies a value for the scaling between world units and page units for this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float PageScale
        {
            get => BaseGraphics.PageScale;
            set => BaseGraphics.PageScale = value;
        }

        /// <summary>
        ///     Gets the horizontal resolution of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     The value, in dots per inch, for the horizontal resolution supported by this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiX => BaseGraphics.DpiX;

        /// <summary>
        ///     Gets the vertical resolution of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     The value, in dots per inch, for the vertical resolution supported by this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public float DpiY => BaseGraphics.DpiY;

        /// <summary>
        ///     Gets or sets a <see cref="System.Drawing.Region"></see> object that limits the drawing region of this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Region"></see> object that limits the portion of this
        ///     <see cref="System.Drawing.Graphics"></see> object that is currently available for drawing.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public Region Clip
        {
            get => BaseGraphics.Clip;
            set => BaseGraphics.Clip = value;
        }

        /// <summary>
        ///     Gets a <see cref="System.Drawing.RectangleF"></see> structure that bounds the clipping region of this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.RectangleF"></see> structure that represents a bounding rectangle for the clipping
        ///     region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public RectangleF ClipBounds => BaseGraphics.ClipBounds;

        /// <summary>
        ///     Gets a value indicating whether the clipping region of this <see cref="System.Drawing.Graphics"></see> object is
        ///     empty.
        /// </summary>
        /// <returns>
        ///     true if the clipping region of this <see cref="System.Drawing.Graphics"></see> object is empty; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsClipEmpty => BaseGraphics.IsClipEmpty;

        /// <summary>
        ///     Gets the bounding rectangle of the visible clipping region of this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.RectangleF"></see> structure that represents a bounding rectangle for the visible
        ///     clipping region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public RectangleF VisibleClipBounds => BaseGraphics.VisibleClipBounds;

        /// <summary>
        ///     Gets a value indicating whether the visible clipping region of this <see cref="System.Drawing.Graphics"></see>
        ///     object is empty.
        /// </summary>
        /// <returns>
        ///     true if the visible portion of the clipping region of this <see cref="System.Drawing.Graphics"></see> object is
        ///     empty; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisibleClipEmpty => BaseGraphics.IsVisibleClipEmpty;

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            BaseGraphics.Dispose();
        }

        #endregion

        /// <summary>
        ///     Releases a device context handle obtained by a previous call to the
        ///     <see cref="System.Drawing.Graphics.GetHdc"></see> method of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="hdc">
        ///     Handle to a device context obtained by a previous call to the
        ///     <see cref="System.Drawing.Graphics.GetHdc"></see> method of this <see cref="System.Drawing.Graphics"></see> object.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void ReleaseHdc(IntPtr hdc)
        {
            BaseGraphics.ReleaseHdc(hdc);
        }

        /// <summary>
        ///     Releases a handle to a device context.
        /// </summary>
        /// <param name="hdc">Handle to a device context. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public void ReleaseHdcInternal(IntPtr hdc)
        {
            BaseGraphics.ReleaseHdcInternal(hdc);
        }

        /// <summary>
        ///     Forces execution of all pending graphics operations and returns immediately without waiting for the operations to
        ///     finish.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flush()
        {
            BaseGraphics.Flush();
        }

        /// <summary>
        ///     Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return
        ///     before the operations finish.
        /// </summary>
        /// <param name="intention">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FlushIntention"></see> enumeration that
        ///     specifies whether the method returns immediately or waits for any existing operations to finish.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Flush(FlushIntention intention)
        {
            BaseGraphics.Flush(intention);
        }

        /// <summary>
        ///     Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing
        ///     surface of the <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        {
            BaseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize);
        }

        /// <summary>
        ///     Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the
        ///     drawing surface of the <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
        {
            BaseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize);
        }

        /// <summary>
        ///     Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing
        ///     surface of the <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
        /// <param name="copyPixelOperation">One of the <see cref="System.Drawing.CopyPixelOperation"></see> values.</param>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     copyPixelOperation is not a member of
        ///     <see cref="System.Drawing.CopyPixelOperation"></see>.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            BaseGraphics.CopyFromScreen(upperLeftSource, upperLeftDestination, blockRegionSize, copyPixelOperation);
        }

        /// <summary>
        ///     Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the
        ///     drawing surface of the <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="blockRegionSize">The size of the area to be transferred.</param>
        /// <param name="copyPixelOperation">One of the <see cref="System.Drawing.CopyPixelOperation"></see> values.</param>
        /// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
        /// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
        /// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        ///     copyPixelOperation is not a member of
        ///     <see cref="System.Drawing.CopyPixelOperation"></see>.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="AllWindows" />
        /// </PermissionSet>
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            BaseGraphics.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
        }

        /// <summary>
        ///     Resets the world transformation matrix of this <see cref="System.Drawing.Graphics"></see> object to the identity
        ///     matrix.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetTransform()
        {
            BaseGraphics.ResetTransform();
        }

        /// <summary>
        ///     Multiplies the world transformation of this <see cref="System.Drawing.Graphics"></see> object and specified the
        ///     <see cref="System.Drawing.Drawing2D.Matrix"></see> object.
        /// </summary>
        /// <param name="matrix">
        ///     4x4 <see cref="System.Drawing.Drawing2D.Matrix"></see> object that multiplies the world
        ///     transformation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void MultiplyTransform(Matrix matrix)
        {
            BaseGraphics.MultiplyTransform(matrix);
        }

        /// <summary>
        ///     Multiplies the world transformation of this <see cref="System.Drawing.Graphics"></see> object and specified the
        ///     <see cref="System.Drawing.Drawing2D.Matrix"></see> object in the specified order.
        /// </summary>
        /// <param name="matrix">
        ///     4x4 <see cref="System.Drawing.Drawing2D.Matrix"></see> object that multiplies the world
        ///     transformation.
        /// </param>
        /// <param name="order">
        ///     Member of the <see cref="System.Drawing.Drawing2D.MatrixOrder"></see> enumeration that determines
        ///     the order of the multiplication.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            BaseGraphics.MultiplyTransform(matrix, order);
        }

        /// <summary>
        ///     Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of
        ///     this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="dx">x component of the translation. </param>
        /// <param name="dy">y component of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy)
        {
            BaseGraphics.TranslateTransform(dx, dy);
        }

        /// <summary>
        ///     Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of
        ///     this <see cref="System.Drawing.Graphics"></see> object in the specified order.
        /// </summary>
        /// <param name="dx">x component of the translation. </param>
        /// <param name="dy">y component of the translation. </param>
        /// <param name="order">
        ///     Member of the <see cref="System.Drawing.Drawing2D.MatrixOrder"></see> enumeration that specifies
        ///     whether the translation is prepended or appended to the transformation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            BaseGraphics.TranslateTransform(dx, dy, order);
        }

        /// <summary>
        ///     Applies the specified scaling operation to the transformation matrix of this
        ///     <see cref="System.Drawing.Graphics"></see> object by prepending it to the object's transformation matrix.
        /// </summary>
        /// <param name="sy">Scale factor in the y direction. </param>
        /// <param name="sx">Scale factor in the x direction. </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy)
        {
            BaseGraphics.ScaleTransform(sx, sy);
        }

        /// <summary>
        ///     Applies the specified scaling operation to the transformation matrix of this
        ///     <see cref="System.Drawing.Graphics"></see> object in the specified order.
        /// </summary>
        /// <param name="sy">Scale factor in the y direction. </param>
        /// <param name="sx">Scale factor in the x direction. </param>
        /// <param name="order">
        ///     Member of the <see cref="System.Drawing.Drawing2D.MatrixOrder"></see> enumeration that specifies
        ///     whether the scaling operation is prepended or appended to the transformation matrix.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            BaseGraphics.ScaleTransform(sx, sy, order);
        }

        /// <summary>
        ///     Applies the specified rotation to the transformation matrix of this <see cref="System.Drawing.Graphics"></see>
        ///     object.
        /// </summary>
        /// <param name="angle">Angle of rotation in degrees. </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle)
        {
            BaseGraphics.RotateTransform(angle);
        }

        /// <summary>
        ///     Applies the specified rotation to the transformation matrix of this <see cref="System.Drawing.Graphics"></see>
        ///     object in the specified order.
        /// </summary>
        /// <param name="angle">Angle of rotation in degrees. </param>
        /// <param name="order">
        ///     Member of the <see cref="System.Drawing.Drawing2D.MatrixOrder"></see> enumeration that specifies
        ///     whether the rotation is appended or prepended to the matrix transformation.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void RotateTransform(float angle, MatrixOrder order)
        {
            BaseGraphics.RotateTransform(angle, order);
        }

        /// <summary>
        ///     Transforms an array of points from one coordinate space to another using the current world and page transformations
        ///     of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="pts">Array of <see cref="System.Drawing.PointF"></see> structures that represent the points to transform. </param>
        /// <param name="srcSpace">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CoordinateSpace"></see> enumeration that
        ///     specifies the source coordinate space.
        /// </param>
        /// <param name="destSpace">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CoordinateSpace"></see> enumeration that
        ///     specifies the destination coordinate space.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
        {
            BaseGraphics.TransformPoints(destSpace, srcSpace, pts);
        }

        /// <summary>
        ///     Transforms an array of points from one coordinate space to another using the current world and page transformations
        ///     of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="pts">
        ///     Array of <see cref="System.Drawing.Point"></see> structures that represents the points to
        ///     transformation.
        /// </param>
        /// <param name="srcSpace">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CoordinateSpace"></see> enumeration that
        ///     specifies the source coordinate space.
        /// </param>
        /// <param name="destSpace">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CoordinateSpace"></see> enumeration that
        ///     specifies the destination coordinate space.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
        {
            BaseGraphics.TransformPoints(destSpace, srcSpace, pts);
        }

        /// <summary>
        ///     Gets the nearest color to the specified <see cref="System.Drawing.Color"></see> structure.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Drawing.Color"></see> structure that represents the nearest color to the one specified with the
        ///     color parameter.
        /// </returns>
        /// <param name="color"><see cref="System.Drawing.Color"></see> structure for which to find a match. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public Color GetNearestColor(Color color)
        {
            return BaseGraphics.GetNearestColor(color);
        }

        /// <summary>
        ///     Draws a line connecting the two points specified by coordinate pairs.
        /// </summary>
        /// <param name="x1">x-coordinate of the first point. </param>
        /// <param name="y2">y-coordinate of the second point. </param>
        /// <param name="y1">y-coordinate of the first point. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line. </param>
        /// <param name="x2">x-coordinate of the second point. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            BaseGraphics.DrawLine(pen, x1, y1, x2, y2);
        }

        /// <summary>
        ///     Draws a line connecting two <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="pt2"><see cref="System.Drawing.PointF"></see> structure that represents the second point to connect. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line. </param>
        /// <param name="pt1"><see cref="System.Drawing.PointF"></see> structure that represents the first point to connect. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            BaseGraphics.DrawLine(pen, pt1, pt2);
        }

        /// <summary>
        ///     Draws a series of line segments that connect an array of <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that represent the points to connect. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line
        ///     segments.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLines(Pen pen, PointF[] points)
        {
            BaseGraphics.DrawLines(pen, points);
        }

        /// <summary>
        ///     Draws a line connecting the two points specified by coordinate pairs.
        /// </summary>
        /// <param name="x1">x-coordinate of the first point. </param>
        /// <param name="y2">y-coordinate of the second point. </param>
        /// <param name="y1">y-coordinate of the first point. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line. </param>
        /// <param name="x2">x-coordinate of the second point. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            BaseGraphics.DrawLine(pen, x1, y1, x2, y2);
        }

        /// <summary>
        ///     Draws a line connecting two <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="pt2"><see cref="System.Drawing.Point"></see> structure that represents the second point to connect. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line. </param>
        /// <param name="pt1"><see cref="System.Drawing.Point"></see> structure that represents the first point to connect. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            BaseGraphics.DrawLine(pen, pt1, pt2);
        }

        /// <summary>
        ///     Draws a series of line segments that connect an array of <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that represent the points to connect. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the line
        ///     segments.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawLines(Pen pen, Point[] points)
        {
            BaseGraphics.DrawLines(pen, points);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc. </param>
        /// <param name="width">Width of the rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the rectangle that defines the ellipse. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the arc. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a <see cref="System.Drawing.RectangleF"></see>
        ///     structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure that defines the boundaries of the ellipse. </param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the arc. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc. </param>
        /// <param name="width">Width of the rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the rectangle that defines the ellipse. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the arc. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            BaseGraphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws an arc representing a portion of an ellipse specified by a <see cref="System.Drawing.Rectangle"></see>
        ///     structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure that defines the boundaries of the ellipse. </param>
        /// <param name="sweepAngle">Angle in degrees measured clockwise from the startAngle parameter to ending point of the arc. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the arc. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.
        /// </summary>
        /// <param name="x3">x-coordinate of the second control point of the curve. </param>
        /// <param name="x1">x-coordinate of the starting point of the curve. </param>
        /// <param name="x4">x-coordinate of the ending point of the curve. </param>
        /// <param name="y4">y-coordinate of the ending point of the curve. </param>
        /// <param name="y3">y-coordinate of the second control point of the curve. </param>
        /// <param name="y2">y-coordinate of the first control point of the curve. </param>
        /// <param name="y1">y-coordinate of the starting point of the curve. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     curve.
        /// </param>
        /// <param name="x2">x-coordinate of the first control point of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            BaseGraphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
        }

        /// <summary>
        ///     Draws a Bézier spline defined by four <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="pt4"><see cref="System.Drawing.PointF"></see> structure that represents the ending point of the curve. </param>
        /// <param name="pt2">
        ///     <see cref="System.Drawing.PointF"></see> structure that represents the first control point for the
        ///     curve.
        /// </param>
        /// <param name="pt3">
        ///     <see cref="System.Drawing.PointF"></see> structure that represents the second control point for the
        ///     curve.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     curve.
        /// </param>
        /// <param name="pt1"><see cref="System.Drawing.PointF"></see> structure that represents the starting point of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            BaseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        /// <summary>
        ///     Draws a series of Bézier splines from an array of <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.PointF"></see> structures that represent the points that
        ///     determine the curve.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBeziers(Pen pen, PointF[] points)
        {
            BaseGraphics.DrawBeziers(pen, points);
        }

        /// <summary>
        ///     Draws a Bézier spline defined by four <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="pt4"><see cref="System.Drawing.Point"></see> structure that represents the ending point of the curve. </param>
        /// <param name="pt2">
        ///     <see cref="System.Drawing.Point"></see> structure that represents the first control point for the
        ///     curve.
        /// </param>
        /// <param name="pt3">
        ///     <see cref="System.Drawing.Point"></see> structure that represents the second control point for the
        ///     curve.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> structure that determines the color, width, and style of the
        ///     curve.
        /// </param>
        /// <param name="pt1"><see cref="System.Drawing.Point"></see> structure that represents the starting point of the curve. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        {
            BaseGraphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        /// <summary>
        ///     Draws a series of Bézier splines from an array of <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.Point"></see> structures that represent the points that
        ///     determine the curve.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawBeziers(Pen pen, Point[] points)
        {
            BaseGraphics.DrawBeziers(pen, points);
        }

        /// <summary>
        ///     Draws a rectangle specified by a <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect">A <see cref="System.Drawing.Rectangle"></see> structure that represents the rectangle to draw. </param>
        /// <param name="pen">
        ///     A <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     rectangle.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            BaseGraphics.DrawRectangle(pen, rect);
        }

        /// <summary>
        ///     Draws a rectangle specified by a coordinate pair, a width, and a height.
        /// </summary>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="width">The width of the rectangle to draw. </param>
        /// <param name="height">The height of the rectangle to draw. </param>
        /// <param name="pen">
        ///     A <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     rectangle.
        /// </param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            BaseGraphics.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>
        ///     Draws a rectangle specified by a coordinate pair, a width, and a height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <param name="width">Width of the rectangle to draw. </param>
        /// <param name="height">Height of the rectangle to draw. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     rectangle.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the rectangle to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            BaseGraphics.DrawRectangle(pen, x, y, width, height);
        }

        /// <summary>
        ///     Draws a series of rectangles specified by <see cref="System.Drawing.RectangleF"></see> structures.
        /// </summary>
        /// <param name="rects">
        ///     Array of <see cref="System.Drawing.RectangleF"></see> structures that represent the rectangles to
        ///     draw.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     outlines of the rectangles.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-rects is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangles(Pen pen, RectangleF[] rects)
        {
            BaseGraphics.DrawRectangles(pen, rects);
        }

        /// <summary>
        ///     Draws a series of rectangles specified by <see cref="System.Drawing.Rectangle"></see> structures.
        /// </summary>
        /// <param name="rects">
        ///     Array of <see cref="System.Drawing.Rectangle"></see> structures that represent the rectangles to
        ///     draw.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     outlines of the rectangles.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-rects is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawRectangles(Pen pen, Rectangle[] rects)
        {
            BaseGraphics.DrawRectangles(pen, rects);
        }

        /// <summary>
        ///     Draws an ellipse defined by a bounding <see cref="System.Drawing.RectangleF"></see>.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure that defines the boundaries of the ellipse. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            BaseGraphics.DrawEllipse(pen, rect);
        }

        /// <summary>
        ///     Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     ellipse.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            BaseGraphics.DrawEllipse(pen, x, y, width, height);
        }

        /// <summary>
        ///     Draws an ellipse specified by a bounding <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure that defines the boundaries of the ellipse. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     ellipse.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawEllipse(Pen pen, Rectangle rect)
        {
            BaseGraphics.DrawEllipse(pen, rect);
        }

        /// <summary>
        ///     Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     ellipse.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            BaseGraphics.DrawEllipse(pen, x, y, width, height);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a <see cref="System.Drawing.RectangleF"></see> structure and
        ///     two radial lines.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that represents the bounding rectangle that
        ///     defines the ellipse from which the pie shape comes.
        /// </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the startAngle parameter to the second side of the
        ///     pie shape.
        /// </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the pie
        ///     shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, and a height and two radial lines.
        /// </summary>
        /// <param name="y">
        ///     y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie shape comes.
        /// </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the startAngle parameter to the second side of the
        ///     pie shape.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the pie
        ///     shape.
        /// </param>
        /// <param name="x">
        ///     x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie shape comes.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a <see cref="System.Drawing.Rectangle"></see> structure and
        ///     two radial lines.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that represents the bounding rectangle that
        ///     defines the ellipse from which the pie shape comes.
        /// </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the startAngle parameter to the second side of the
        ///     pie shape.
        /// </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the pie
        ///     shape.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            BaseGraphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, and a height and two radial lines.
        /// </summary>
        /// <param name="y">
        ///     y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie shape comes.
        /// </param>
        /// <param name="sweepAngle">
        ///     Angle measured in degrees clockwise from the startAngle parameter to the second side of the
        ///     pie shape.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes. </param>
        /// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the pie
        ///     shape.
        /// </param>
        /// <param name="x">
        ///     x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie shape comes.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            BaseGraphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Draws a polygon defined by an array of <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.PointF"></see> structures that represent the vertices of the
        ///     polygon.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     polygon.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPolygon(Pen pen, PointF[] points)
        {
            BaseGraphics.DrawPolygon(pen, points);
        }

        /// <summary>
        ///     Draws a polygon defined by an array of <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.Point"></see> structures that represent the vertices of the
        ///     polygon.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the
        ///     polygon.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawPolygon(Pen pen, Point[] points)
        {
            BaseGraphics.DrawPolygon(pen, points);
        }

        /// <summary>
        ///     Draws a <see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object.
        /// </summary>
        /// <param name="path"><see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object to draw. </param>
        /// <param name="pen"><see cref="System.Drawing.Pen"></see> object that determines the color, width, and style of the path. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-path is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawPath(Pen pen, GraphicsPath path)
        {
            BaseGraphics.DrawPath(pen, path);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points)
        {
            BaseGraphics.DrawCurve(pen, points);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.PointF"></see> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.PointF"></see> structures that represent the points that define
        ///     the curve.
        /// </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, float tension)
        {
            BaseGraphics.DrawCurve(pen, points, tension);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.PointF"></see> structures. The
        ///     drawing begins offset from the beginning of the array.
        /// </summary>
        /// <param name="offset">
        ///     Offset from the first element in the array of the points parameter to the starting point in the
        ///     curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
        {
            BaseGraphics.DrawCurve(pen, points, offset, numberOfSegments);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.PointF"></see> structures using a
        ///     specified tension. The drawing begins offset from the beginning of the array.
        /// </summary>
        /// <param name="offset">
        ///     Offset from the first element in the array of the points parameter to the starting point in the
        ///     curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
        {
            BaseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points)
        {
            BaseGraphics.DrawCurve(pen, points);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.Point"></see> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points, float tension)
        {
            BaseGraphics.DrawCurve(pen, points, tension);
        }

        /// <summary>
        ///     Draws a cardinal spline through a specified array of <see cref="System.Drawing.Point"></see> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="offset">
        ///     Offset from the first element in the array of the points parameter to the starting point in the
        ///     curve.
        /// </param>
        /// <param name="numberOfSegments">Number of segments after the starting point to include in the curve. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
        {
            BaseGraphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="System.Drawing.PointF"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, PointF[] points)
        {
            BaseGraphics.DrawClosedCurve(pen, points);
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="System.Drawing.PointF"></see> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled. This parameter is required but is ignored.
        /// </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
        {
            BaseGraphics.DrawClosedCurve(pen, points, tension, fillmode);
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="System.Drawing.Point"></see> structures.
        /// </summary>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, Point[] points)
        {
            BaseGraphics.DrawClosedCurve(pen, points);
        }

        /// <summary>
        ///     Draws a closed cardinal spline defined by an array of <see cref="System.Drawing.Point"></see> structures using a
        ///     specified tension.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled. This parameter is required but ignored.
        /// </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <param name="pen">
        ///     <see cref="System.Drawing.Pen"></see> object that determines the color, width, and height of the
        ///     curve.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
        {
            BaseGraphics.DrawClosedCurve(pen, points, tension, fillmode);
        }

        /// <summary>
        ///     Clears the entire drawing surface and fills it with the specified background color.
        /// </summary>
        /// <param name="color">
        ///     <see cref="System.Drawing.Color"></see> structure that represents the background color of the
        ///     drawing surface.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public void Clear(Color color)
        {
            BaseGraphics.Clear(color);
        }

        /// <summary>
        ///     Fills the interior of a rectangle specified by a <see cref="System.Drawing.RectangleF"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure that represents the rectangle to fill. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRectangle(Brush brush, RectangleF rect)
        {
            BaseGraphics.FillRectangle(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="width">Width of the rectangle to fill. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the rectangle to fill. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            BaseGraphics.FillRectangle(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interior of a rectangle specified by a <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure that represents the rectangle to fill. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRectangle(Brush brush, Rectangle rect)
        {
            BaseGraphics.FillRectangle(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <param name="width">Width of the rectangle to fill. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the rectangle to fill. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the rectangle to fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            BaseGraphics.FillRectangle(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interiors of a series of rectangles specified by <see cref="System.Drawing.RectangleF"></see> structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="rects">
        ///     Array of <see cref="System.Drawing.RectangleF"></see> structures that represent the rectangles to
        ///     fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            BaseGraphics.FillRectangles(brush, rects);
        }

        /// <summary>
        ///     Fills the interiors of a series of rectangles specified by <see cref="System.Drawing.Rectangle"></see> structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="rects">
        ///     Array of <see cref="System.Drawing.Rectangle"></see> structures that represent the rectangles to
        ///     fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            BaseGraphics.FillRectangles(brush, rects);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by <see cref="System.Drawing.PointF"></see>
        ///     structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.PointF"></see> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, PointF[] points)
        {
            BaseGraphics.FillPolygon(brush, points);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by <see cref="System.Drawing.PointF"></see>
        ///     structures using the specified fill mode.
        /// </summary>
        /// <param name="fillMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     the style of the fill.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.PointF"></see> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
        {
            BaseGraphics.FillPolygon(brush, points, fillMode);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by <see cref="System.Drawing.Point"></see>
        ///     structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.Point"></see> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, Point[] points)
        {
            BaseGraphics.FillPolygon(brush, points);
        }

        /// <summary>
        ///     Fills the interior of a polygon defined by an array of points specified by <see cref="System.Drawing.Point"></see>
        ///     structures using the specified fill mode.
        /// </summary>
        /// <param name="fillMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     the style of the fill.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">
        ///     Array of <see cref="System.Drawing.Point"></see> structures that represent the vertices of the
        ///     polygon to fill.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
        {
            BaseGraphics.FillPolygon(brush, points, fillMode);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a
        ///     <see cref="System.Drawing.RectangleF"></see> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that represents the bounding rectangle that
        ///     defines the ellipse.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillEllipse(Brush brush, RectangleF rect)
        {
            BaseGraphics.FillEllipse(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a
        ///     height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            BaseGraphics.FillEllipse(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a
        ///     <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that represents the bounding rectangle that
        ///     defines the ellipse.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillEllipse(Brush brush, Rectangle rect)
        {
            BaseGraphics.FillEllipse(brush, rect);
        }

        /// <summary>
        ///     Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a
        ///     height.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            BaseGraphics.FillEllipse(brush, x, y, width, height);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a
        ///     <see cref="System.Drawing.RectangleF"></see> structure and two radial lines.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that represents the bounding rectangle that
        ///     defines the ellipse from which the pie section comes.
        /// </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the startAngle parameter to the second side of the
        ///     pie section.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
        {
            BaseGraphics.FillPie(brush, rect, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, and a height
        ///     and two radial lines.
        /// </summary>
        /// <param name="y">
        ///     y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie section comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the startAngle parameter to the second side of the
        ///     pie section.
        /// </param>
        /// <param name="x">
        ///     x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie section comes.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            BaseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, and a height
        ///     and two radial lines.
        /// </summary>
        /// <param name="y">
        ///     y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie section comes.
        /// </param>
        /// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes. </param>
        /// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section. </param>
        /// <param name="sweepAngle">
        ///     Angle in degrees measured clockwise from the startAngle parameter to the second side of the
        ///     pie section.
        /// </param>
        /// <param name="x">
        ///     x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the
        ///     pie section comes.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            BaseGraphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        /// <summary>
        ///     Fills the interior of a <see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="path"><see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object that represents the path to fill. </param>
        /// <exception cref="T:System.ArgumentNullException">pen is null.-or-path is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillPath(Brush brush, GraphicsPath path)
        {
            BaseGraphics.FillPath(brush, path);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.PointF"></see>
        ///     structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points)
        {
            BaseGraphics.FillClosedCurve(brush, points);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.PointF"></see>
        ///     structures using the specified fill mode.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
        {
            BaseGraphics.FillClosedCurve(brush, points, fillmode);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.PointF"></see>
        ///     structures using the specified fill mode and tension.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled.
        /// </param>
        /// <param name="brush">A <see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.PointF"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
        {
            BaseGraphics.FillClosedCurve(brush, points, fillmode, tension);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.Point"></see>
        ///     structures.
        /// </summary>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points)
        {
            BaseGraphics.FillClosedCurve(brush, points);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.Point"></see>
        ///     structures using the specified fill mode.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
        {
            BaseGraphics.FillClosedCurve(brush, points, fillmode);
        }

        /// <summary>
        ///     Fills the interior a closed cardinal spline curve defined by an array of <see cref="System.Drawing.Point"></see>
        ///     structures using the specified fill mode and tension.
        /// </summary>
        /// <param name="fillmode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.FillMode"></see> enumeration that determines
        ///     how the curve is filled.
        /// </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve. </param>
        /// <param name="points">Array of <see cref="System.Drawing.Point"></see> structures that define the spline. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-points is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
        {
            BaseGraphics.FillClosedCurve(brush, points, fillmode, tension);
        }

        /// <summary>
        ///     Fills the interior of a <see cref="System.Drawing.Region"></see> object.
        /// </summary>
        /// <param name="region"><see cref="System.Drawing.Region"></see> object that represents the area to fill. </param>
        /// <param name="brush"><see cref="System.Drawing.Brush"></see> object that determines the characteristics of the fill. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-region is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void FillRegion(Brush brush, Region region)
        {
            BaseGraphics.FillRegion(brush, region);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects.
        /// </summary>
        /// <param name="y">y coordinate of the upper-left corner of the drawn text. </param>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="x">x coordinate of the upper-left corner of the drawn text. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            BaseGraphics.DrawString(s, font, brush, x, y);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects.
        /// </summary>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="point">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the upper-left corner of the
        ///     drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            BaseGraphics.DrawString(s, font, brush, point);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects using the formatting
        ///     attributes of the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn text. </param>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="format">
        ///     <see cref="System.Drawing.StringFormat"></see> object that specifies formatting attributes, such
        ///     as line spacing and alignment, that are applied to the drawn text.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn text. </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            BaseGraphics.DrawString(s, font, brush, x, y, format);
        }

        /// <summary>
        ///     Draws the specified text string at the specified location with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects using the formatting
        ///     attributes of the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="format">
        ///     <see cref="System.Drawing.StringFormat"></see> object that specifies formatting attributes, such
        ///     as line spacing and alignment, that are applied to the drawn text.
        /// </param>
        /// <param name="point">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the upper-left corner of the
        ///     drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            BaseGraphics.DrawString(s, font, brush, point, format);
        }

        /// <summary>
        ///     Draws the specified text string in the specified rectangle with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects.
        /// </summary>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="layoutRectangle">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location of the
        ///     drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            BaseGraphics.DrawString(s, font, brush, layoutRectangle);
        }

        /// <summary>
        ///     Draws the specified text string in the specified rectangle with the specified
        ///     <see cref="System.Drawing.Brush"></see> and <see cref="System.Drawing.Font"></see> objects using the formatting
        ///     attributes of the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="s">String to draw. </param>
        /// <param name="brush">
        ///     <see cref="System.Drawing.Brush"></see> object that determines the color and texture of the drawn
        ///     text.
        /// </param>
        /// <param name="format">
        ///     <see cref="System.Drawing.StringFormat"></see> object that specifies formatting attributes, such
        ///     as line spacing and alignment, that are applied to the drawn text.
        /// </param>
        /// <param name="layoutRectangle">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location of the
        ///     drawn text.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">brush is null.-or-s is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            BaseGraphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object and
        ///     formatted with the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size of the string, in
        ///     the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the text parameter as
        ///     drawn with the font parameter and the stringFormat parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="charactersFitted">Number of characters in the string. </param>
        /// <param name="linesFilled">Number of text lines in the string. </param>
        /// <param name="stringFormat">
        ///     <see cref="System.Drawing.StringFormat"></see> object that represents formatting
        ///     information, such as line spacing, for the string.
        /// </param>
        /// <param name="layoutArea">
        ///     <see cref="System.Drawing.SizeF"></see> structure that specifies the maximum layout area for
        ///     the text.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
        {
            return BaseGraphics.MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object and
        ///     formatted with the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified by the
        ///     text parameter as drawn with the font parameter and the stringFormat parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object defines the text format of the string. </param>
        /// <param name="origin">
        ///     <see cref="System.Drawing.PointF"></see> structure that represents the upper-left corner of the
        ///     string.
        /// </param>
        /// <param name="stringFormat">
        ///     <see cref="System.Drawing.StringFormat"></see> object that represents formatting
        ///     information, such as line spacing, for the string.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
        {
            return BaseGraphics.MeasureString(text, font, origin, stringFormat);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object within
        ///     the specified layout area.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified by the
        ///     text parameter as drawn with the font parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object defines the text format of the string. </param>
        /// <param name="layoutArea">
        ///     <see cref="System.Drawing.SizeF"></see> structure that specifies the maximum layout area for
        ///     the text.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea)
        {
            return BaseGraphics.MeasureString(text, font, layoutArea);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object and
        ///     formatted with the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified in the
        ///     text parameter as drawn with the font parameter and the stringFormat parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object defines the text format of the string. </param>
        /// <param name="stringFormat">
        ///     <see cref="System.Drawing.StringFormat"></see> object that represents formatting
        ///     information, such as line spacing, for the string.
        /// </param>
        /// <param name="layoutArea">
        ///     <see cref="System.Drawing.SizeF"></see> structure that specifies the maximum layout area for
        ///     the text.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            return BaseGraphics.MeasureString(text, font, layoutArea, stringFormat);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified by the
        ///     text parameter as drawn with the font parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font)
        {
            return BaseGraphics.MeasureString(text, font);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified in the
        ///     text parameter as drawn with the font parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the format of the string. </param>
        /// <param name="width">Maximum width of the string in pixels. </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, int width)
        {
            return BaseGraphics.MeasureString(text, font, width);
        }

        /// <summary>
        ///     Measures the specified string when drawn with the specified <see cref="System.Drawing.Font"></see> object and
        ///     formatted with the specified <see cref="System.Drawing.StringFormat"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.SizeF"></see> structure that represents the size, in the units
        ///     specified by the <see cref="P:System.Drawing.Graphics.PageUnit"></see> property, of the string specified in the
        ///     text parameter as drawn with the font parameter and the stringFormat parameter.
        /// </returns>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="width">Maximum width of the string. </param>
        /// <param name="format">
        ///     <see cref="System.Drawing.StringFormat"></see> object that represents formatting information, such
        ///     as line spacing, for the string.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return BaseGraphics.MeasureString(text, font, width, format);
        }

        /// <summary>
        ///     Gets an array of <see cref="System.Drawing.Region"></see> objects, each of which bounds a range of character
        ///     positions within the specified string.
        /// </summary>
        /// <returns>
        ///     This method returns an array of <see cref="System.Drawing.Region"></see> objects, each of which bounds a range of
        ///     character positions within the specified string.
        /// </returns>
        /// <param name="layoutRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the layout rectangle for
        ///     the string.
        /// </param>
        /// <param name="font"><see cref="System.Drawing.Font"></see> object that defines the text format of the string. </param>
        /// <param name="stringFormat">
        ///     <see cref="System.Drawing.StringFormat"></see> object that represents formatting
        ///     information, such as line spacing, for the string.
        /// </param>
        /// <param name="text">String to measure. </param>
        /// <filterpriority>1</filterpriority>
        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {
            return BaseGraphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
        }

        /// <summary>
        ///     Draws the image represented by the specified <see cref="System.Drawing.Icon"></see> object at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="icon"><see cref="System.Drawing.Icon"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">icon is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawIcon(Icon icon, int x, int y)
        {
            BaseGraphics.DrawIcon(icon, x, y);
        }

        /// <summary>
        ///     Draws the image represented by the specified <see cref="System.Drawing.Icon"></see> object within the area
        ///     specified by a <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="icon"><see cref="System.Drawing.Icon"></see> object to draw. </param>
        /// <param name="targetRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the resulting image on the display surface. The image contained in the icon parameter is scaled to the dimensions
        ///     of this rectangular area.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">icon is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            BaseGraphics.DrawIcon(icon, targetRect);
        }

        /// <summary>
        ///     Draws the image represented by the specified <see cref="System.Drawing.Icon"></see> object without scaling the
        ///     image.
        /// </summary>
        /// <param name="icon"><see cref="System.Drawing.Icon"></see> object to draw. </param>
        /// <param name="targetRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the resulting image. The image is not scaled to fit this rectangle, but retains its original size. If the image is
        ///     larger than the rectangle, it is clipped to fit inside it.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">icon is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        {
            BaseGraphics.DrawIconUnstretched(icon, targetRect);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object, using its original physical size, at the
        ///     specified location.
        /// </summary>
        /// <param name="point">
        ///     <see cref="System.Drawing.PointF"></see> structure that represents the upper-left corner of the
        ///     drawn image.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, PointF point)
        {
            BaseGraphics.DrawImage(image, point);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object, using its original physical size, at the
        ///     specified location.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y)
        {
            BaseGraphics.DrawImage(image, x, y);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of the
        ///     drawn image.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, RectangleF rect)
        {
            BaseGraphics.DrawImage(image, rect);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Width of the drawn image. </param>
        /// <param name="height">Height of the drawn image. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            BaseGraphics.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object, using its original physical size, at the
        ///     specified location.
        /// </summary>
        /// <param name="point">
        ///     <see cref="System.Drawing.Point"></see> structure that represents the location of the upper-left
        ///     corner of the drawn image.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, Point point)
        {
            BaseGraphics.DrawImage(image, point);
        }

        /// <summary>
        ///     Draws the specified image, using its original physical size, at the location specified by a coordinate pair.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y)
        {
            BaseGraphics.DrawImage(image, x, y);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of the
        ///     drawn image.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImage(Image image, Rectangle rect)
        {
            BaseGraphics.DrawImage(image, rect);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     size.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Width of the drawn image. </param>
        /// <param name="height">Height of the drawn image. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            BaseGraphics.DrawImage(image, x, y, width, height);
        }

        /// <summary>
        ///     Draws a specified image using its original physical size at a specified location.
        /// </summary>
        /// <param name="point">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the upper-left corner of the drawn
        ///     image.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaled(Image image, Point point)
        {
            BaseGraphics.DrawImageUnscaled(image, point);
        }

        /// <summary>
        ///     Draws the specified image using its original physical size at the location specified by a coordinate pair.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            BaseGraphics.DrawImageUnscaled(image, x, y);
        }

        /// <summary>
        ///     Draws a specified image using its original physical size at a specified location.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> that specifies the upper-left corner of the drawn image.
        ///     The X and Y properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            BaseGraphics.DrawImageUnscaled(image, rect);
        }

        /// <summary>
        ///     Draws a specified image using its original physical size at a specified location.
        /// </summary>
        /// <param name="y">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="width">Not used. </param>
        /// <param name="height">Not used. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        {
            BaseGraphics.DrawImageUnscaled(image, x, y, width, height);
        }

        /// <summary>
        ///     Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.
        /// </summary>
        /// <param name="rect">The <see cref="System.Drawing.Rectangle"></see> in which to draw the image.</param>
        /// <param name="image">The <see cref="System.Drawing.Image"></see> to draw.</param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
        {
            BaseGraphics.DrawImageUnscaledAndClipped(image, rect);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     shape and size.
        /// </summary>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints)
        {
            BaseGraphics.DrawImage(image, destPoints);
        }

        /// <summary>
        ///     Draws the specified <see cref="System.Drawing.Image"></see> object at the specified location and with the specified
        ///     shape and size.
        /// </summary>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints)
        {
            BaseGraphics.DrawImage(image, destPoints);
        }

        /// <summary>
        ///     Draws a portion of an image at a specified location.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     <see cref="System.Drawing.Image"></see> object to draw.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, x, y, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws a portion of an image at a specified location.
        /// </summary>
        /// <param name="y">y-coordinate of the upper-left corner of the drawn image. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <param name="x">x-coordinate of the upper-left corner of the drawn image. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, x, y, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate to use
        ///     when checking whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)">
        ///     </see>
        ///     method.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram. </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used by the srcRect parameter.
        /// </param>
        /// <param name="callbackData">
        ///     Value specifying additional data for the <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate to use
        ///     when checking whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)">
        ///     </see>
        ///     method.
        /// </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a
        ///     parallelogram.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the image
        ///     object to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            BaseGraphics.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr, callback, callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="imageAttrs">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="imageAttrs">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="callbackData">
        ///     Value specifying additional data for the
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate to use when checking whether to stop execution
        ///     of the DrawImage method.
        /// </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="imageAttrs">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, callbackData);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for image.
        /// </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr, callback);
        }

        /// <summary>
        ///     Draws the specified portion of the specified <see cref="System.Drawing.Image"></see> object at the specified
        ///     location and with the specified size.
        /// </summary>
        /// <param name="callbackData">
        ///     Value specifying additional data for the
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate to use when checking whether to stop execution
        ///     of the DrawImage method.
        /// </param>
        /// <param name="srcHeight">Height of the portion of the source image to draw. </param>
        /// <param name="imageAttrs">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies recoloring and
        ///     gamma information for the image object.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.DrawImageAbort"></see> delegate that specifies a method to call during the
        ///     drawing of the image. This method is called frequently to check whether to stop execution of the
        ///     <see
        ///         cref="System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)">
        ///     </see>
        ///     method according to application-determined criteria.
        /// </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the units
        ///     of measure used to determine the source rectangle.
        /// </param>
        /// <param name="srcWidth">Width of the portion of the source image to draw. </param>
        /// <param name="image"><see cref="System.Drawing.Image"></see> object to draw. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn image. The image is scaled to fit the rectangle.
        /// </param>
        /// <param name="srcX">x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <param name="srcY">y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
        /// <exception cref="T:System.ArgumentNullException">image is null.</exception>
        /// <filterpriority>1</filterpriority>
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            BaseGraphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttrs, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records of the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram.
        /// </summary>
        /// <returns>
        ///     This method does not return a value.
        /// </returns>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in the specified <see cref="System.Drawing.Imaging.Metafile"></see> object, one at a time, to a
        ///     callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point.
        /// </summary>
        /// <returns>
        ///     This method does not return a value.
        /// </returns>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <returns>
        ///     This method does not return a value.
        /// </returns>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.PointF"></see> structure that specifies the location of the
        ///     upper-left corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display at a specified point using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="destPoint">
        ///     <see cref="System.Drawing.Point"></see> structure that specifies the location of the upper-left
        ///     corner of the drawn metafile.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoint, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records of a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified rectangle using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="destRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the location and size of
        ///     the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destRect, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structures that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.PointF"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that specifies the portion of the
        ///     metafile, relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="srcUnit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit
        ///     of measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, srcUnit, callback, callbackData);
        }

        /// <summary>
        ///     Sends the records in a selected rectangle from a <see cref="System.Drawing.Imaging.Metafile"></see> object, one at
        ///     a time, to a callback method for display in a specified parallelogram using specified image attributes.
        /// </summary>
        /// <param name="callbackData">
        ///     Internal pointer that is required, but ignored. You can pass
        ///     <see cref="F:System.IntPtr.Zero"></see> for this parameter.
        /// </param>
        /// <param name="callback">
        ///     <see cref="System.Drawing.Graphics.EnumerateMetafileProc"></see> delegate that specifies the
        ///     method to which the metafile records are sent.
        /// </param>
        /// <param name="metafile"><see cref="System.Drawing.Imaging.Metafile"></see> object to enumerate. </param>
        /// <param name="imageAttr">
        ///     <see cref="System.Drawing.Imaging.ImageAttributes"></see> object that specifies image attribute
        ///     information for the drawn image.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure used to determine the portion of the metafile that the rectangle specified by the srcRect parameter
        ///     contains.
        /// </param>
        /// <param name="destPoints">
        ///     Array of three <see cref="System.Drawing.Point"></see> structures that define a parallelogram
        ///     that determines the size and location of the drawn metafile.
        /// </param>
        /// <param name="srcRect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the portion of the metafile,
        ///     relative to its upper-left corner, to draw.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
        {
            BaseGraphics.EnumerateMetafile(metafile, destPoints, srcRect, unit, callback, callbackData, imageAttr);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the Clip property of the
        ///     specified <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="g"><see cref="System.Drawing.Graphics"></see> object from which to take the new clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Graphics g)
        {
            BaseGraphics.SetClip(g);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the result of the specified
        ///     combining operation of the current clip region and the <see cref="P:System.Drawing.Graphics.Clip"></see> property
        ///     of the specified <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <param name="g"><see cref="System.Drawing.Graphics"></see> object that specifies the clip region to combine. </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CombineMode"></see> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Graphics g, CombineMode combineMode)
        {
            BaseGraphics.SetClip(g, combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the rectangle specified by a
        ///     <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure that represents the new clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Rectangle rect)
        {
            BaseGraphics.SetClip(rect);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the result of the specified
        ///     operation combining the current clip region and the rectangle specified by a
        ///     <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure to combine. </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CombineMode"></see> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Rectangle rect, CombineMode combineMode)
        {
            BaseGraphics.SetClip(rect, combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the rectangle specified by a
        ///     <see cref="System.Drawing.RectangleF"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure that represents the new clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(RectangleF rect)
        {
            BaseGraphics.SetClip(rect);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the result of the specified
        ///     operation combining the current clip region and the rectangle specified by a
        ///     <see cref="System.Drawing.RectangleF"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure to combine. </param>
        /// <param name="combineMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CombineMode"></see> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(RectangleF rect, CombineMode combineMode)
        {
            BaseGraphics.SetClip(rect, combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the specified
        ///     <see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object.
        /// </summary>
        /// <param name="path"><see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object that represents the new clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(GraphicsPath path)
        {
            BaseGraphics.SetClip(path);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the result of the specified
        ///     operation combining the current clip region and the specified
        ///     <see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object.
        /// </summary>
        /// <param name="combineMode">
        ///     Member of the <see cref="System.Drawing.Drawing2D.CombineMode"></see> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <param name="path"><see cref="System.Drawing.Drawing2D.GraphicsPath"></see> object to combine. </param>
        /// <filterpriority>1</filterpriority>
        public void SetClip(GraphicsPath path, CombineMode combineMode)
        {
            BaseGraphics.SetClip(path, combineMode);
        }

        /// <summary>
        ///     Sets the clipping region of this <see cref="System.Drawing.Graphics"></see> object to the result of the specified
        ///     operation combining the current clip region and the specified <see cref="System.Drawing.Region"></see> object.
        /// </summary>
        /// <param name="region"><see cref="System.Drawing.Region"></see> object to combine. </param>
        /// <param name="combineMode">
        ///     Member from the <see cref="System.Drawing.Drawing2D.CombineMode"></see> enumeration that
        ///     specifies the combining operation to use.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void SetClip(Region region, CombineMode combineMode)
        {
            BaseGraphics.SetClip(region, combineMode);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="System.Drawing.Graphics"></see> object to the intersection of the
        ///     current clip region and the specified <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure to intersect with the current clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(Rectangle rect)
        {
            BaseGraphics.IntersectClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="System.Drawing.Graphics"></see> object to the intersection of the
        ///     current clip region and the specified <see cref="System.Drawing.RectangleF"></see> structure.
        /// </summary>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure to intersect with the current clip region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(RectangleF rect)
        {
            BaseGraphics.IntersectClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="System.Drawing.Graphics"></see> object to the intersection of the
        ///     current clip region and the specified <see cref="System.Drawing.Region"></see> object.
        /// </summary>
        /// <param name="region"><see cref="System.Drawing.Region"></see> object to intersect with the current region. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void IntersectClip(Region region)
        {
            BaseGraphics.IntersectClip(region);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="System.Drawing.Graphics"></see> object to exclude the area specified by
        ///     a <see cref="System.Drawing.Rectangle"></see> structure.
        /// </summary>
        /// <param name="rect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that specifies the rectangle to exclude from
        ///     the clip region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ExcludeClip(Rectangle rect)
        {
            BaseGraphics.ExcludeClip(rect);
        }

        /// <summary>
        ///     Updates the clip region of this <see cref="System.Drawing.Graphics"></see> object to exclude the area specified by
        ///     a <see cref="System.Drawing.Region"></see> object.
        /// </summary>
        /// <param name="region">
        ///     <see cref="System.Drawing.Region"></see> object that specifies the region to exclude from the clip
        ///     region.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ExcludeClip(Region region)
        {
            BaseGraphics.ExcludeClip(region);
        }

        /// <summary>
        ///     Resets the clip region of this <see cref="System.Drawing.Graphics"></see> object to an infinite region.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void ResetClip()
        {
            BaseGraphics.ResetClip();
        }

        /// <summary>
        ///     Translates the clipping region of this <see cref="System.Drawing.Graphics"></see> object by specified amounts in
        ///     the horizontal and vertical directions.
        /// </summary>
        /// <param name="dx">x component of the translation. </param>
        /// <param name="dy">y component of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateClip(float dx, float dy)
        {
            BaseGraphics.TranslateClip(dx, dy);
        }

        /// <summary>
        ///     Translates the clipping region of this <see cref="System.Drawing.Graphics"></see> object by specified amounts in
        ///     the horizontal and vertical directions.
        /// </summary>
        /// <param name="dx">x component of the translation. </param>
        /// <param name="dy">y component of the translation. </param>
        /// <filterpriority>1</filterpriority>
        public void TranslateClip(int dx, int dy)
        {
            BaseGraphics.TranslateClip(dx, dy);
        }

        /// <summary>
        ///     Non-browsable member.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [StrongNameIdentityPermission(SecurityAction.LinkDemand, Name = "System.Windows.Forms", PublicKey = "0x00000000000000000400000000000000")]
        public object GetContextInfo()
        {
            return BaseGraphics.GetContextInfo();
        }

        /// <summary>
        ///     Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the point defined by the x and y parameters is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="y">y coordinate of the point to test for visibility. </param>
        /// <param name="x">x coordinate of the point to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y)
        {
            return BaseGraphics.IsVisible(x, y);
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="System.Drawing.Point"></see> structure is contained within the visible
        ///     clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the point specified by the point parameter is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="point"><see cref="System.Drawing.Point"></see> structure to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Point point)
        {
            return BaseGraphics.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the point defined by the x and y parameters is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="y">y coordinate of the point to test for visibility. </param>
        /// <param name="x">x coordinate of the point to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y)
        {
            return BaseGraphics.IsVisible(x, y);
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="System.Drawing.PointF"></see> structure is contained within the visible
        ///     clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the point specified by the point parameter is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="point"><see cref="System.Drawing.PointF"></see> structure to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(PointF point)
        {
            return BaseGraphics.IsVisible(point);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the
        ///     visible clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the rectangle defined by the x, y, width, and height parameters is contained within the visible clip region
        ///     of this <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="y">y coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="width">Width of the rectangle to test for visibility. </param>
        /// <param name="height">Height of the rectangle to test for visibility. </param>
        /// <param name="x">x coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(int x, int y, int width, int height)
        {
            return BaseGraphics.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a <see cref="System.Drawing.Rectangle"></see> structure is contained
        ///     within the visible clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the rectangle specified by the rect parameter is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="rect"><see cref="System.Drawing.Rectangle"></see> structure to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(Rectangle rect)
        {
            return BaseGraphics.IsVisible(rect);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the
        ///     visible clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the rectangle defined by the x, y, width, and height parameters is contained within the visible clip region
        ///     of this <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="y">y coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <param name="width">Width of the rectangle to test for visibility. </param>
        /// <param name="height">Height of the rectangle to test for visibility. </param>
        /// <param name="x">x coordinate of the upper-left corner of the rectangle to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        public bool IsVisible(float x, float y, float width, float height)
        {
            return BaseGraphics.IsVisible(x, y, width, height);
        }

        /// <summary>
        ///     Indicates whether the rectangle specified by a <see cref="System.Drawing.RectangleF"></see> structure is contained
        ///     within the visible clip region of this <see cref="System.Drawing.Graphics"></see> object.
        /// </summary>
        /// <returns>
        ///     true if the rectangle specified by the rect parameter is contained within the visible clip region of this
        ///     <see cref="System.Drawing.Graphics"></see> object; otherwise, false.
        /// </returns>
        /// <param name="rect"><see cref="System.Drawing.RectangleF"></see> structure to test for visibility. </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public bool IsVisible(RectangleF rect)
        {
            return BaseGraphics.IsVisible(rect);
        }

        /// <summary>
        ///     Saves the current state of this <see cref="System.Drawing.Graphics"></see> object and identifies the saved state
        ///     with a <see cref="System.Drawing.Drawing2D.GraphicsState"></see> object.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.Drawing2D.GraphicsState"></see> object that represents the saved
        ///     state of this <see cref="System.Drawing.Graphics"></see> object.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsState Save()
        {
            return BaseGraphics.Save();
        }

        /// <summary>
        ///     Restores the state of this <see cref="System.Drawing.Graphics"></see> object to the state represented by a
        ///     <see cref="System.Drawing.Drawing2D.GraphicsState"></see> object.
        /// </summary>
        /// <param name="gstate">
        ///     <see cref="System.Drawing.Drawing2D.GraphicsState"></see> object that represents the state to
        ///     which to restore this <see cref="System.Drawing.Graphics"></see> object.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void Restore(GraphicsState gstate)
        {
            BaseGraphics.Restore(gstate);
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="System.Drawing.Graphics"></see> object and
        ///     opens and uses a new graphics container with the specified scale transformation.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.Drawing2D.GraphicsContainer"></see> object that represents the
        ///     state of this <see cref="System.Drawing.Graphics"></see> object at the time of the method call.
        /// </returns>
        /// <param name="srcrect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that, together with the dstrect parameter,
        ///     specifies a scale transformation for the new graphics container.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure for the container.
        /// </param>
        /// <param name="dstrect">
        ///     <see cref="System.Drawing.RectangleF"></see> structure that, together with the srcrect parameter,
        ///     specifies a scale transformation for the new graphics container.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
        {
            return BaseGraphics.BeginContainer(dstrect, srcrect, unit);
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="System.Drawing.Graphics"></see> object and
        ///     opens and uses a new graphics container.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.Drawing2D.GraphicsContainer"></see> object that represents the
        ///     state of this <see cref="System.Drawing.Graphics"></see> object at the time of the method call.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public GraphicsContainer BeginContainer()
        {
            return BaseGraphics.BeginContainer();
        }

        /// <summary>
        ///     Closes the current graphics container and restores the state of this <see cref="System.Drawing.Graphics"></see>
        ///     object to the state saved by a call to the <see cref="Graphics.BeginContainer()"></see> method.
        /// </summary>
        /// <param name="container">
        ///     <see cref="System.Drawing.Drawing2D.GraphicsContainer"></see> object that represents the
        ///     container this method restores.
        /// </param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="UnmanagedCode, ControlEvidence" />
        /// </PermissionSet>
        public void EndContainer(GraphicsContainer container)
        {
            BaseGraphics.EndContainer(container);
        }

        /// <summary>
        ///     Saves a graphics container with the current state of this <see cref="System.Drawing.Graphics"></see> object and
        ///     opens and uses a new graphics container with the specified scale transformation.
        /// </summary>
        /// <returns>
        ///     This method returns a <see cref="System.Drawing.Drawing2D.GraphicsContainer"></see> object that represents the
        ///     state of this <see cref="System.Drawing.Graphics"></see> object at the time of the method call.
        /// </returns>
        /// <param name="srcrect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that, together with the dstrect parameter,
        ///     specifies a scale transformation for the container.
        /// </param>
        /// <param name="unit">
        ///     Member of the <see cref="System.Drawing.GraphicsUnit"></see> enumeration that specifies the unit of
        ///     measure for the container.
        /// </param>
        /// <param name="dstrect">
        ///     <see cref="System.Drawing.Rectangle"></see> structure that, together with the srcrect parameter,
        ///     specifies a scale transformation for the container.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
        {
            return BaseGraphics.BeginContainer(dstrect, srcrect, unit);
        }

        /// <summary>
        ///     Adds a comment to the current <see cref="System.Drawing.Imaging.Metafile"></see> object.
        /// </summary>
        /// <param name="data">Array of bytes that contains the comment. </param>
        /// <filterpriority>1</filterpriority>
        public void AddMetafileComment(byte[] data)
        {
            BaseGraphics.AddMetafileComment(data);
        }

        /// <summary>
        ///     Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        ///     An object of type <see cref="System.Runtime.Remoting.Lifetime.ILease"></see> used to control the lifetime policy
        ///     for this instance.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="RemotingConfiguration, Infrastructure" />
        /// </PermissionSet>
        public object GetLifetimeService()
        {
            return BaseGraphics.GetLifetimeService();
        }

        /// <summary>
        ///     Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        ///     An object of type <see cref="System.Runtime.Remoting.Lifetime.ILease"></see> used to control the lifetime policy
        ///     for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new
        ///     lifetime service object initialized to the value of the
        ///     <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"></see> property.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="RemotingConfiguration, Infrastructure" />
        /// </PermissionSet>
        public object InitializeLifetimeService()
        {
            return BaseGraphics.InitializeLifetimeService();
        }

        /// <summary>
        ///     Creates an object that contains all the relevant information required to generate a proxy used to communicate with
        ///     a remote object.
        /// </summary>
        /// <returns>
        ///     Information required to generate a proxy.
        /// </returns>
        /// <param name="requestedType">
        ///     The <see cref="System.Type"></see> of the object that the new
        ///     <see cref="System.Runtime.Remoting.ObjRef"></see> will reference.
        /// </param>
        /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
        /// <exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object. </exception>
        /// <filterpriority>2</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Flags="Infrastructure" />
        /// </PermissionSet>
        public ObjRef CreateObjRef(Type requestedType)
        {
            return BaseGraphics.CreateObjRef(requestedType);
        }

        /// <summary>
        ///     Returns the handle to a Windows device context.
        /// </summary>
        /// <returns>
        ///     An <see cref="System.IntPtr"></see> representing the handle of a device context.
        /// </returns>
        public IntPtr GetHdc()
        {
            return BaseGraphics.GetHdc();
        }

        /// <summary>
        ///     Releases the handle of a Windows device context.
        /// </summary>
        public void ReleaseHdc()
        {
            BaseGraphics.ReleaseHdc();
        }
    }
}