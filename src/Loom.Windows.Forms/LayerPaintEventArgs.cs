#region Using Directives

using System;
using System.Drawing;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public sealed class LayerPaintEventArgs : EventArgs, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LayerPaintEventArgs" /> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        internal LayerPaintEventArgs(Bitmap bitmap)
        {
            SetBitmap(bitmap);
        }

        /// <summary>
        ///     Gets the bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Rectangle Bounds => new Rectangle(Point.Empty, Size - new Size(1, 1));

        /// <summary>
        ///     Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public Size Size { get; private set; }

        /// <summary>
        ///     Gets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        public Graphics Graphics { get; private set; }

        /// <summary>
        ///     Gets the image.
        /// </summary>
        /// <value>The image.</value>
        internal Bitmap Image { get; private set; }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Graphics != null)
                Graphics.Dispose();
            if (Image != null)
                Image.Dispose();
        }

        #endregion

        /// <summary>
        ///     Sets the bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        internal void SetBitmap(Bitmap bitmap)
        {
            Image = bitmap;
            Graphics = Graphics.FromImage(Image);
            Size = new Size(bitmap.Width, bitmap.Height);
        }
    }
}