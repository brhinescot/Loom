#region Using Directives

using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security;
using System.Windows.Forms;
using Loom.Annotations;
using Loom.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Represents an image created from a bitmap used to paint the mouse pointer.
    /// </summary>
    public class BitmapCursor : IDisposable, ISerializable, IEquatable<BitmapCursor>
    {
        private const string ObjectName = "BitmapCursor";

        [NonSerialized] private readonly Cursor innerCursor;

        [NonSerialized] private IntPtr handle;

        [NonSerialized] private bool isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitmapCursor" /> class.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap" /> to use for the cursor.</param>
        /// <param name="hotSpot">
        ///     A <see cref="Point" /> representing the location of the
        ///     <see cref="BitmapCursor" /> hotspot.
        /// </param>
        public BitmapCursor(Bitmap bitmap, Point hotSpot)
            : this(bitmap, null, hotSpot.X, hotSpot.Y) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitmapCursor" /> class.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap" /> to use for the cursor.</param>
        /// <param name="hotSpotX">The horizontal hotspot of the <see cref="BitmapCursor" />.</param>
        /// <param name="hotSpotY">The vertical hotspot of the <see cref="BitmapCursor" />.</param>
        public BitmapCursor(Bitmap bitmap, int hotSpotX, int hotSpotY)
            : this(bitmap, null, hotSpotX, hotSpotY) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitmapCursor" /> class.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap" /> to use for the cursor.</param>
        /// <param name="overlayCursor">
        ///     An optional <see cref="Cursor" /> to overlay on the
        ///     specified <paramref name="bitmap" />
        /// </param>
        /// <param name="hotSpot">
        ///     A <see cref="Point" /> representing the location of the
        ///     <see cref="BitmapCursor" /> hotspot.
        /// </param>
        /// <remarks>
        ///     <para>
        ///         If an <paramref name="overlayCursor" /> is specified, the hotspot of the <see cref="Cursor" />
        ///         is placed at the coordinates specified by the <paramref name="hotSpot" /> parameter.
        ///     </para>
        /// </remarks>
        public BitmapCursor(Bitmap bitmap, Cursor overlayCursor, Point hotSpot)
            : this(bitmap, overlayCursor, hotSpot.X, hotSpot.Y) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitmapCursor" /> class.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap" /> to use for the cursor.</param>
        /// <param name="overlayCursor">
        ///     An optional <see cref="Cursor" /> to overlay on the
        ///     specified <paramref name="bitmap" />
        /// </param>
        /// <remarks>
        ///     <para>
        ///         If an <paramref name="overlayCursor" /> is specified, the hotspot of the <see cref="BitmapCursor" />
        ///         is placed at the coordinates of the <paramref name="overlayCursor" /> hotspot.
        ///     </para>
        /// </remarks>
        [PublicAPI]
        public BitmapCursor(Bitmap bitmap, Cursor overlayCursor)
        {
            innerCursor = Create(bitmap, overlayCursor);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BitmapCursor" /> class.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap" /> to use for the cursor.</param>
        /// <param name="overlayCursor">
        ///     An optional <see cref="Cursor" /> to overlay on the
        ///     specified <paramref name="bitmap" />
        /// </param>
        /// <param name="hotSpotX">The horizontal hotspot of the <see cref="BitmapCursor" />.</param>
        /// <param name="hotSpotY">The vertical hotspot of the <see cref="BitmapCursor" />.</param>
        /// <remarks>
        ///     <para>
        ///         If an <paramref name="overlayCursor" /> is specified, the hotspot of the <see cref="Cursor" />
        ///         is placed at the coordinates specified by the <paramref name="hotSpotX" /> and
        ///         <paramref name="hotSpotY" /> parameters.
        ///     </para>
        /// </remarks>
        [PublicAPI]
        public BitmapCursor(Bitmap bitmap, Cursor overlayCursor = null, int hotSpotX = 0, int hotSpotY = 0)
        {
            innerCursor = Create(bitmap, overlayCursor, hotSpotX, hotSpotY);
        }

        [PublicAPI]
        protected internal BitmapCursor(SerializationInfo info, StreamingContext context)
        {
            innerCursor = (Cursor) info.GetValue("InnerCursor", typeof(Cursor));
            handle = innerCursor.Handle;
        }

        public BitmapCursor(string fileName, Point hotSpot)
            : this(fileName, null, hotSpot.X, hotSpot.Y) { }

        public BitmapCursor(string fileName, int hotSpotX, int hotSpotY)
            : this(fileName, null, hotSpotX, hotSpotY) { }

        public BitmapCursor(string fileName, Cursor overlayCursor, Point hotSpot)
            : this(fileName, overlayCursor, hotSpot.X, hotSpot.Y) { }

        public BitmapCursor(string fileName, Cursor overlayCursor)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, nameof(fileName));
            Argument.Assert.FileExists(fileName);

            using (Bitmap bitmap = (Bitmap) Image.FromFile(fileName))
            {
                innerCursor = Create(bitmap, overlayCursor);
            }
        }

        [PublicAPI]
        public BitmapCursor(string fileName, Cursor overlayCursor = null, int hotSpotX = 0, int hotSpotY = 0)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, nameof(fileName));
            Argument.Assert.FileExists(fileName);

            using (Bitmap bitmap = (Bitmap) Image.FromFile(fileName))
            {
                innerCursor = Create(bitmap, overlayCursor, hotSpotX, hotSpotY);
            }
        }

        protected Cursor InnerCursor => innerCursor;

        public object Tag
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return innerCursor.Tag;
            }
            set
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ObjectName);

                innerCursor.Tag = value;
            }
        }

        /// <summary>
        ///     Gets the handle of the cursor.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.IntPtr"></see> that represents the cursor's handle.
        /// </returns>
        /// <exception cref="T:System.Exception">
        ///     The handle value is
        ///     <see cref="F:System.IntPtr.Zero">
        ///     </see>
        ///     .
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public IntPtr Handle
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return innerCursor.Handle;
            }
        }

        /// <summary>
        ///     Gets the handle of the cursor.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.IntPtr"></see> that represents the cursor's handle.
        /// </returns>
        /// <exception cref="T:System.Exception">
        ///     The handle value is <see cref="F:System.IntPtr.Zero"></see>.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        public Point HotSpot
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return innerCursor.HotSpot;
            }
        }

        /// <summary>
        ///     Gets the size of the cursor object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Drawing.Size"></see> that represents the width and height of the
        ///     <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public Size Size
        {
            get
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ObjectName);

                return innerCursor.Size;
            }
        }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IEquatable<BitmapCursor> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(BitmapCursor other)
        {
            return other != null && Equals(innerCursor, other.innerCursor);
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        ///     Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"></see>
        ///     with the data needed to serialize the target object.
        /// </summary>
        /// <param name="context">
        ///     The destination (see <see cref="StreamingContext"></see>)
        ///     for this serialization.
        /// </param>
        /// <param name="info">
        ///     The <see cref="SerializationInfo"></see> to populate with
        ///     data.
        /// </param>
        /// <exception cref="SecurityException">
        ///     The caller does not have the required
        ///     permission.
        /// </exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InnerCursor", innerCursor, typeof(Cursor));
        }

        #endregion

        private Cursor Create(Bitmap bitmap, Cursor overlayCursor)
        {
            return Create(bitmap, overlayCursor,
                overlayCursor == null ? 0 : overlayCursor.HotSpot.X,
                overlayCursor == null ? 0 : overlayCursor.HotSpot.Y);
        }

        private Cursor Create(Bitmap bitmap, Cursor overlayCursor, int hotSpotX, int hotSpotY)
        {
            Argument.Assert.IsNotNull(bitmap, nameof(bitmap));
            Argument.Assert.IsNotNegative(hotSpotX, "hotSpotX");
            Argument.Assert.IsNotNegative(hotSpotY, "hotSpotY");

            using (SafeBitmap safeBitmap = new SafeBitmap(bitmap))
            {
                AddOverlay(safeBitmap, overlayCursor, hotSpotX, hotSpotY);
                NativeMethods.ICONINFO iconInfo = GetIconInfo(safeBitmap, hotSpotX, hotSpotY);
                handle = NativeMethods.CreateIconIndirect(ref iconInfo);
            }

            return new Cursor(handle);
        }

        private static NativeMethods.ICONINFO GetIconInfo(Bitmap bitmap, int hotSpotX, int hotSpotY)
        {
            NativeMethods.ICONINFO iconInfo = new NativeMethods.ICONINFO();
            iconInfo.fIcon = false;
            iconInfo.xHotspot = (uint) hotSpotX;
            iconInfo.yHotspot = (uint) hotSpotY;
            iconInfo.hbmMask = bitmap.GetHbitmap();
            iconInfo.hbmColor = bitmap.GetHbitmap();
            return iconInfo;
        }

        private static void AddOverlay(Image bitmap, Cursor overlayCursor, int hotSpotX, int hotSpotY)
        {
            if (overlayCursor == null)
                return;

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Rectangle rect = new Rectangle(
                    hotSpotX - overlayCursor.HotSpot.X,
                    hotSpotY - overlayCursor.HotSpot.Y,
                    overlayCursor.Size.Width,
                    overlayCursor.Size.Height);

                overlayCursor.Draw(g, rect);
            }
        }

        public static implicit operator Cursor(BitmapCursor bc)
        {
            if (bc.isDisposed)
                throw new ObjectDisposedException(ObjectName);

            return bc.innerCursor;
        }

        /// <summary>
        ///     Copies the handle of this <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.IntPtr"></see> that represents the cursor's handle.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IntPtr CopyHandle()
        {
            if (isDisposed)
                throw new ObjectDisposedException(ObjectName);

            return innerCursor.CopyHandle();
        }

        /// <summary>
        ///     Draws the cursor on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">
        ///     The <see cref="T:System.Drawing.Graphics"></see> surface
        ///     on which to draw the <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </param>
        /// <param name="targetRect">
        ///     The <see cref="T:System.Drawing.Rectangle"></see>
        ///     that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void Draw(Graphics g, Rectangle targetRect)
        {
            if (isDisposed)
                throw new ObjectDisposedException(ObjectName);

            innerCursor.Draw(g, targetRect);
        }

        /// <summary>
        ///     Draws the cursor in a stretched format on the specified surface, within the specified bounds.
        /// </summary>
        /// <param name="g">
        ///     The <see cref="T:System.Drawing.Graphics"></see> surface on
        ///     which to draw the <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </param>
        /// <param name="targetRect">
        ///     The <see cref="T:System.Drawing.Rectangle"></see>
        ///     that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor"></see>.
        /// </param>
        /// <filterpriority>1</filterpriority>
        public void DrawStretched(Graphics g, Rectangle targetRect)
        {
            if (isDisposed)
                throw new ObjectDisposedException(ObjectName);

            innerCursor.DrawStretched(g, targetRect);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (handle != IntPtr.Zero)
                NativeMethods.DestroyIcon(handle);

            if (disposing)
                if (innerCursor != null)
                    innerCursor.Dispose();

            isDisposed = true;
        }

        ~BitmapCursor()
        {
            Dispose(false);
        }

        public static bool operator !=(BitmapCursor left, BitmapCursor right)
        {
            return !(left == right);
        }

        public static bool operator ==(BitmapCursor left, BitmapCursor right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.innerCursor.Handle == right.innerCursor.Handle;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as BitmapCursor);
        }

        public override int GetHashCode()
        {
            return innerCursor.GetHashCode();
        }

        #region Nested type: SafeBitmap

        private sealed class SafeBitmap : IDisposable
        {
            private readonly Bitmap bitmap;
            private readonly bool bitmapOwned;

            public SafeBitmap(Bitmap bitmap)
            {
                ImageProperties properties = new ImageProperties(bitmap);
                if (properties.IsIndexed)
                {
                    Bitmap temp = new Bitmap(bitmap.Width, bitmap.Height);
                    using (Graphics g = Graphics.FromImage(temp))
                    {
                        g.DrawImage(bitmap, 0, 0);
                    }

                    this.bitmap = temp;
                    bitmapOwned = true;
                }
                else
                {
                    this.bitmap = bitmap;
                }
            }

            #region IDisposable Members

            /// <summary>
            ///     Performs application-defined tasks associated with freeing, releasing,
            ///     or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                Dispose(true);
            }

            #endregion

            public static implicit operator Bitmap(SafeBitmap safeBitmap)
            {
                return safeBitmap.bitmap;
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                    if (bitmapOwned)
                        bitmap.Dispose();
            }
        }

        #endregion
    }
}