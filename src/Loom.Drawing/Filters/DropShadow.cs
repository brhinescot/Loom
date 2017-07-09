#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;

#endregion

namespace Loom.Drawing.Filters
{
    /// <summary>
    ///     Summary description for DropShadow.
    /// </summary>
    public class DropShadow : IFilter
    {
        private readonly int left;
        private readonly float lightness;
        private readonly int top;

        public DropShadow() : this(4, 4, .2f) { }

        public DropShadow(int left, int top, float lightness)
        {
            this.left = left;
            this.top = top;
            this.lightness = lightness;
        }

        #region IFilter Members

        /// <summary>
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image Apply(Image image)
        {
            Argument.Assert.IsNotNull(image, "image");

            Rectangle bounds = new Rectangle(left, top, image.Width, image.Height);
            ImageAttributes attributes = new ImageAttributes();
            ColorMatrix matrix = new ColorMatrix
            {
                Matrix00 = 0,
                Matrix11 = 0,
                Matrix22 = 0,
                Matrix33 = lightness
            };

            attributes.SetColorMatrix(matrix);

            Bitmap dropShadowVersion = new Bitmap(image.Width + left, image.Height + top);
            using (Graphics graphics = Graphics.FromImage(dropShadowVersion))
            {
                graphics.DrawImage(image, bounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);

                return dropShadowVersion;
            }
        }

        #endregion
    }
}