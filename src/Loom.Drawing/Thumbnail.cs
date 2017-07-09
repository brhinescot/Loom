#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Represents a class used to create thumbnails of images or from folders.
    /// </summary>
    public static class Thumbnail
    {
        public static Bitmap FromFile(string path, int maxSize)
        {
            using (Image src = Image.FromFile(path))
            {
                return FromImage(src, maxSize);
            }
        }

        /// <summary>
        ///     Creates a thumbnail from the specified image.
        /// </summary>
        /// <param name="image">The <see cref="Image" /> to thumbnail.</param>
        /// <param name="maxSize">The maximum height or width of the thumbnail.</param>
        /// <returns></returns>
        public static Bitmap FromImage(Image image, int maxSize)
        {
            Argument.Assert.IsNotNull(image, nameof(image));

            if (maxSize <= 0 || maxSize > int.MaxValue)
                throw new ArgumentOutOfRangeException("maxSize", maxSize, SR.ThumbnailSizeOutOfRange);

            return CreatePrivate(image, maxSize, maxSize);
        }

        /// <summary>
        ///     Creates a thumbnail from the specified image.
        /// </summary>
        /// <param name="image">The <see cref="Image" /> to thumbnail.</param>
        /// <param name="maxHeight">The maximum height of the thumbnail.</param>
        /// <param name="maxWidth">The maximum width of the thumbnail.</param>
        /// <returns></returns>
        public static Bitmap FromImage(Image image, int maxWidth, int maxHeight)
        {
            Argument.Assert.IsNotNull(image, nameof(image));

            if (maxHeight <= 0 || maxHeight > int.MaxValue)
                throw new ArgumentOutOfRangeException("maxHeight", maxHeight, SR.ThumbnailHeightOutOfRange);

            if (maxWidth <= 0 || maxWidth > int.MaxValue)
                throw new ArgumentOutOfRangeException("maxWidth", maxWidth, SR.ThumbnailWidthOutOfRange);

            return CreatePrivate(image, maxWidth, maxHeight);
        }

        /// <summary>
        ///     Creates for folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="maxSize">Size of the max.</param>
        /// <returns></returns>
        public static IEnumerable<Bitmap> FromFolder(string path, int maxSize)
        {
            Argument.Assert.IsNotNull(path, nameof(path));

            if (maxSize <= 0 || maxSize > int.MaxValue)
                throw new ArgumentOutOfRangeException("maxSize", maxSize, SR.ThumbnailSizeOutOfRange);

            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; i++)
                // BUG: Non-image files will throw a OutOfMemory exception in Bitmap.FromFile(string). 
                // Refactor to not attempt loading of non-image files.
                if (Compare.AreSameOrdinalIgnoreCase(files[i].Extension, ".jpg"))
                    using (Image image = Image.FromFile(files[i].FullName))
                    {
                        yield return CreatePrivate(image, maxSize, maxSize);
                    }
        }

        /// <summary>
        ///     Creates for folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <returns></returns>
        public static IEnumerable<Bitmap> FromFolder(string path, int maxHeight, int maxWidth)
        {
            Argument.Assert.IsNotNull(path, nameof(path));

            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; i++)
                // BUG: Non-image files will throw a OutOfMemory exception in Bitmap.FromFile(string).
                // Refactor to not attempt loading of non-image files.
                if (Compare.AreSameOrdinalIgnoreCase(files[i].Extension, ".jpg"))
                    using (Image image = Image.FromFile(files[i].FullName))
                    {
                        yield return CreatePrivate(image, maxWidth, maxHeight);
                    }
        }

        public static Bitmap CreateStack(string directory, int maxSize, ThumbnailStackType stackType)
        {
            switch (stackType)
            {
                case ThumbnailStackType.None:
                    throw new ArgumentException("The 'ThumbnailStackType' has not been initialized.", "stackType");
                case ThumbnailStackType.Pile:
                    throw new NotImplementedException("ThumbnailStackType.Pile is not implemented.");
                case ThumbnailStackType.Fan:
                    return CreateFan(directory, maxSize);
                default:
                    throw new ArgumentOutOfRangeException("stackType");
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="directory"></param>
        ///<param name="maxSize"></param>
        ///<returns></returns>
        private static Bitmap CreateFan(string directory, int maxSize)
        {
            Argument.Assert.IsNotNull(directory, nameof(directory));
            Argument.Assert.IsNotNegative(maxSize, nameof(maxSize));
            const int stackHeight = 6;
            Bitmap newImage = new Bitmap(maxSize * 2, maxSize * 2);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                Random rnd = new Random();
                List<string> imagesToDraw = new List<string>();
                int nbFound;
                string[] images = Directory.GetFiles(directory, "default.jpg", SearchOption.AllDirectories);

                for (nbFound = 0; nbFound < Math.Min(stackHeight, images.Length); nbFound++)
                    imagesToDraw.Insert(0, images[nbFound]);

                if (nbFound < stackHeight)
                {
                    images = Directory.GetFiles(directory, "*.jpg", SearchOption.AllDirectories);
                    for (int i = 0; i < Math.Min(stackHeight - nbFound, images.Length); i++)
                        imagesToDraw.Insert(0, images[rnd.Next(images.Length)]);
                }

                const float rotation = 15f;
                const float fadeStep = .13f;
                float angle = rotation - imagesToDraw.Count * rotation;
                float fade = (imagesToDraw.Count - 1) * fadeStep;
                Rectangle drawRectangle = new Rectangle(maxSize / 2, maxSize / 2, maxSize, maxSize);
                foreach (string path in imagesToDraw)
                    using (Bitmap folderImage = new Bitmap(path))
                    {
                        Matrix transformMatrix = new Matrix();
                        transformMatrix.RotateAt(angle, new PointF(maxSize, maxSize));
                        g.Transform = transformMatrix;

                        ColorMatrix colorMatrix = new ColorMatrix(new[]
                        {
                            new[] {1f, 0, 0, 0, 0},
                            new[] {0, 1f, 0, 0, 0},
                            new[] {0, 0, 1f, 0, 0},
                            new[] {0, 0, 0, 1f, 0},
                            new[] {fade, fade, fade, 0, 1f}
                        });

                        // Create an ImageAttributes object and set its color matrix
                        ImageAttributes imageAtt = new ImageAttributes();
                        imageAtt.SetColorMatrix(
                            colorMatrix,
                            ColorMatrixFlag.Default,
                            ColorAdjustType.Bitmap);

                        g.DrawImage(folderImage, drawRectangle, 0, 0, folderImage.Width, folderImage.Height, GraphicsUnit.Pixel, imageAtt);

                        angle += rotation;
                        fade -= fadeStep;
                    }
            }
            return newImage;
        }

        private static Bitmap CreatePrivate(Image image, int maxWidth, int maxHeight)
        {
            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                Size newSize = CalculateNewSize(image.Size, maxWidth, maxHeight);

                if (image.PixelFormat == PixelFormat.Indexed ||
                    image.PixelFormat == PixelFormat.Format8bppIndexed ||
                    image.PixelFormat == PixelFormat.Format1bppIndexed ||
                    image.PixelFormat == PixelFormat.Format4bppIndexed)
                    return CreateFromIndexed(image, newSize.Width, newSize.Height);

                Bitmap thumbnail = new Bitmap(newSize.Width, newSize.Height, image.PixelFormat);

                using (Graphics graphics = Graphics.FromImage(thumbnail))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle bounds = new Rectangle(-1, -1, newSize.Width + 1, newSize.Height + 1);
                    graphics.DrawImage(image, bounds);
                }

                return thumbnail;
            }

            return new Bitmap(image);
        }

        private static Bitmap CreateFromIndexed(Image image, int maxWidth, int maxHeight)
        {
            using (Bitmap dst = new Bitmap(maxWidth, maxHeight))
            {
                using (Graphics g = Graphics.FromImage(dst))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, dst.Width, dst.Height);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    dst.Save(stream, ImageFormat.Png);
                    return MemoryBitmap.FromStream(stream);
                }
            }
        }

        public static Size CalculateNewSize(Size original, int maxWidth, int maxHeight)
        {
            double widthRatio = original.Width / (double) maxWidth;
            double heightRatio = original.Height / (double) maxHeight;
            double ratio = Math.Max(widthRatio, heightRatio);

            return new Size((int) Math.Round(original.Width / ratio), (int) Math.Round(original.Height / ratio));
        }
    }
}