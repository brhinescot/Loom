#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Encapsulates operations for creating a bitmap from an in-memory buffer.
    /// </summary>
    public static class MemoryBitmap
    {
        public static Bitmap FromStream(Stream stream)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));

            // Temporary bitmap is a workaround for bitmaps created from a stream. 
            // If not used we would need to keep the stream open for the life of the bitmap.
            using (Bitmap temp = (Bitmap) Image.FromStream(stream))
            {
                return new Bitmap(temp);
            }
        }

        /// <summary>
        ///     Creates a <see cref="MemoryStream" /> from an in-memory bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static MemoryStream ToStream(Bitmap bitmap)
        {
            Argument.Assert.IsNotNull(bitmap, nameof(bitmap));

            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            return stream;
        }

        /// <summary>
        ///     Creates a <see cref="Bitmap" /> from an in-memory buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset">The buffer offset at which the image bytes begin.</param>
        /// <returns></returns>
        public static Bitmap FromBuffer(byte[] buffer, int offset = 0)
        {
            Argument.Assert.IsNotNull(buffer, nameof(buffer));
            Argument.Assert.IsInRange(buffer, offset, nameof(buffer));

            using (Stream stream = new MemoryStream())
            {
                stream.Write(buffer, offset, buffer.Length - offset);
                return FromStream(stream);
            }
        }

        /// <summary>
        ///     Creates a <see cref="byte" /> array from an in-memory bitmap.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] ToBuffer(Bitmap bitmap)
        {
            using (MemoryStream stream = ToStream(bitmap))
            {
                return stream.ToArray();
            }
        }
    }
}