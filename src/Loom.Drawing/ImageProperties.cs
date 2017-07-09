#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Summary description for ImageProperties.
    /// </summary>
    public class ImageProperties : IDisposable
    {
        private bool disposed;

        /// <summary>
        ///     Creates a new <see cref="ImageProperties" /> from the supplied <see cref="Bitmap" />.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns>A new <see cref="ImageProperties" />.</returns>
        public ImageProperties(Bitmap bitmap)
        {
            Argument.Assert.IsNotNull(bitmap, "bitmap");

            Bitmap = bitmap;
        }

        /// <summary>
        ///     Creates a new <see cref="ImageProperties" /> from the supplied <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A new <see cref="ImageProperties" />.</returns>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="stream" /> does not
        ///     contain a valid  <see cref="Bitmap" /> object.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="stream" /> is
        ///     null.
        /// </exception>
        public ImageProperties(Stream stream)
        {
            Argument.Assert.IsNotNull(stream, "stream");

            Bitmap = MemoryBitmap.FromStream(stream);
            if (Bitmap == null)
                throw new ArgumentException("The stream does not contain a valid bitmap object.", "stream");
        }

        /// <summary>
        ///     Creates a new <see cref="ImageProperties" /> from the supplied file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A new <see cref="ImageProperties" />.</returns>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="ArgumentException">
        ///     The file does not
        ///     contain a valid  <see cref="Bitmap" /> object.
        /// </exception>
        public ImageProperties(string fileName)
        {
            Argument.Assert.IsNotNullOrEmpty(fileName, "fileName");
            Argument.Assert.FileExists(fileName);

            Bitmap = Image.FromFile(fileName) as Bitmap;
            if (Bitmap == null)
                throw new ArgumentException(string.Format("The file '{0}' is not a valid image file.", fileName), "fileName");
        }

        /// <summary>
        ///     Gets the Bitmap image.
        /// </summary>
        /// <value></value>
        public Bitmap Bitmap { get; }

        public bool IsIndexed
        {
            get
            {
                PixelFormat format = Bitmap.PixelFormat;
                return format == PixelFormat.Indexed ||
                       format == PixelFormat.Format1bppIndexed ||
                       format == PixelFormat.Format4bppIndexed ||
                       format == PixelFormat.Format8bppIndexed;
            }
        }

        public bool Is1bpp => Bitmap.PixelFormat == PixelFormat.Format1bppIndexed;

        public bool Is8bpp => Bitmap.PixelFormat == PixelFormat.Format8bppIndexed;

        public bool Is16bpp
        {
            get
            {
                PixelFormat format = Bitmap.PixelFormat;
                return format == PixelFormat.Format16bppArgb1555 ||
                       format == PixelFormat.Format16bppGrayScale ||
                       format == PixelFormat.Format16bppRgb555 ||
                       format == PixelFormat.Format16bppRgb565;
            }
        }

        public bool Is24bpp => Bitmap.PixelFormat == PixelFormat.Format24bppRgb;

        public bool Is32bpp
        {
            get
            {
                PixelFormat format = Bitmap.PixelFormat;
                return format == PixelFormat.Format32bppArgb ||
                       format == PixelFormat.Format32bppPArgb ||
                       format == PixelFormat.Format32bppRgb;
            }
        }

        public bool Is48bpp => Bitmap.PixelFormat == PixelFormat.Format48bppRgb;

        public bool Is64bpp
        {
            get
            {
                PixelFormat format = Bitmap.PixelFormat;
                return format == PixelFormat.Format64bppArgb ||
                       format == PixelFormat.Format64bppPArgb;
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

        /// <summary>
        ///     Calculates the average color of the image.
        /// </summary>
        /// <returns></returns>
        public Color CalculateAverageColor()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().ToString());

            int red = 0,
                green = 0,
                blue = 0,
                pixels = 0;

            // Walk each column of pixels in the image and add the 
            // red, green and blue value to the running total.
            // 
            for (int x = 0; x < Bitmap.Width; x++)
            for (int y = 0; y < Bitmap.Height; y++)
            {
                Color pixel = Bitmap.GetPixel(x, y);
                red += pixel.R;
                green += pixel.G;
                blue += pixel.B;
                pixels++;
            }

            // Divide the total amount of each color by the number of pixels
            // inspected to get the 8bit (single  byte sized) value of the colors.
            return Color.FromArgb(red / pixels, green / pixels, blue / pixels);
        }

        public Color[] CalculateAverageColor(int segments)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().ToString());

            if (segments < 1)
                throw new ArgumentException("The segments parameter must be 1 or more.");

            int red = 0,
                green = 0,
                blue = 0,
                pixels = 0;

            int segmentWidth = Bitmap.Width / segments;
            int segmentHeight = Bitmap.Height / segments;
            List<Color> colors = new List<Color>();

            int currentX = 0;
            int currentY = 0;

            while (currentX < Bitmap.Width)
            {
                while (currentY < Bitmap.Height)
                {
                    int cropWidth = segmentWidth;
                    int cropHeight = segmentHeight;

                    if (currentX + segmentWidth > Bitmap.Width)
                        cropWidth = Bitmap.Width - currentX;

                    if (currentY + segmentHeight > Bitmap.Height)
                        cropHeight = Bitmap.Height - currentY;

                    Rectangle rectangle = new Rectangle(currentX, currentY, cropWidth, cropHeight);
                    Bitmap cropped = Bitmap.Clone(rectangle, Bitmap.PixelFormat);

                    for (int x = 0; x < cropped.Width; x++)
                    for (int y = 0; y < cropped.Height; y++)
                    {
                        Color pixel = cropped.GetPixel(x, y);
                        red += pixel.R;
                        green += pixel.G;
                        blue += pixel.B;
                        pixels++;
                    }
                    if (pixels == 0)
                        continue;

                    colors.Add(Color.FromArgb(red / pixels, green / pixels, blue / pixels));
                    red = 0;
                    blue = 0;
                    green = 0;
                    pixels = 0;
                    currentY += segmentHeight;
                    if (currentY >= Bitmap.Height)
                        currentY = Bitmap.Height;
                }
                currentX += segmentWidth;
                if (currentX >= Bitmap.Width)
                    currentX = Bitmap.Width;
                currentY = 0;
            }

            // Walk each column of pixels in the image and add the 
            // red, green and blue value to the running total.

//            for (int x = 0; x < cropped.Width; x++)
//            {
//                for (int y = 0; y < cropped.Height; y++)
//                {
//                    Color pixel = cropped.GetPixel(x, y);
//                    red += pixel.R;
//                    green += pixel.G;
//                    blue += pixel.B;
//                    pixels++;
//                }
//            }

            // Divide the total amount of each color by the number of pixels
            // inspected to get the 8bit (single  byte sized) value of the colors.
            return colors.ToArray();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                if (Bitmap != null)
                    Bitmap.Dispose();

            disposed = true;
        }
    }
}