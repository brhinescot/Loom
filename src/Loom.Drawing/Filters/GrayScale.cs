#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;

#endregion

namespace Loom.Drawing.Filters
{
    /// <summary>
    ///     Summary description for GrayScaleFilter.
    /// </summary>
    public class Grayscale : IFilter
    {
        #region IFilter Members

        /// <summary>
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image Apply(Image image)
        {
            Argument.Assert.IsNotNull(image, "image");

            Rectangle bounds = new Rectangle(0, 0, image.Width, image.Height);
            ColorMatrix matrix = GetGreyscaleMatrix();
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);

            Bitmap greyVersion = new Bitmap(image.Width, image.Height);
            using (Graphics graphics = Graphics.FromImage(greyVersion))
            {
                graphics.DrawImage(image, bounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

                return greyVersion;
            }
        }

        #endregion

        /// <summary>
        ///     Gets the greyscale matrix.
        /// </summary>
        /// <returns></returns>
        private static ColorMatrix GetGreyscaleMatrix()
        {
            ColorMatrix matrix = new ColorMatrix();

            matrix.Matrix00 = 1 / 3f;
            matrix.Matrix01 = 1 / 3f;
            matrix.Matrix02 = 1 / 3f;
            matrix.Matrix10 = 1 / 3f;
            matrix.Matrix11 = 1 / 3f;
            matrix.Matrix12 = 1 / 3f;
            matrix.Matrix20 = 1 / 3f;
            matrix.Matrix21 = 1 / 3f;
            matrix.Matrix22 = 1 / 3f;
            matrix.Matrix33 = 1f;
            matrix.Matrix44 = 1f;

            return matrix;
        }
    }
}