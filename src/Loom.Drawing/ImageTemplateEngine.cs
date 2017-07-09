#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Summary description for ImageTemplateEngine.
    /// </summary>
    public static class ImageTemplateEngine
    {
        public static Image GenerateImage(string xmlDefinition, object templateValues = null)
        {
            return GenerateImage(ImageTemplate.FromXml(xmlDefinition), templateValues);
        }

        public static Image GenerateImage(ImageTemplate imageTemplate, object templateValues = null)
        {
            if (templateValues == null)
                templateValues = new Dictionary<string, object>();

            Image bitmap = string.IsNullOrEmpty(imageTemplate.Inherits)
                ? RenderNonInheritedImage(imageTemplate, templateValues)
                : RenderInheritedImages(imageTemplate, templateValues);

            return imageTemplate.Transform.Rotate != 0
                ? RotateImage(bitmap, bitmap.Width / 2, bitmap.Height / 2, imageTemplate.Transform.Rotate, true)
                : bitmap;
        }

        private static Image RenderNonInheritedImage(ImageTemplate imageTemplate, object templateValues)
        {
            Image bitmap = new Bitmap(imageTemplate.Width, imageTemplate.Height);
            RenderImage(imageTemplate, bitmap, templateValues);
            return bitmap;
        }

        private static Image RenderInheritedImages(ImageTemplate imageTemplate, object templateValues)
        {
            Stack<ImageTemplate> stack = new Stack<ImageTemplate>();
            for (ImageTemplate current = imageTemplate; current != null; current = current.GetInherited())
                stack.Push(current);

            Image bitmap = null;
            foreach (ImageTemplate template in stack)
            {
                if (bitmap == null)
                    bitmap = new Bitmap(template.Width, template.Height);
                RenderImage(template, bitmap, templateValues);
            }

            return bitmap;
        }

        private static void RenderImage(ImageTemplate imageTemplate, Image baseImage, object templateValues)
        {
            using (Graphics myGraphics = Graphics.FromImage(baseImage))
            {
                myGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                StyleTemplate style = imageTemplate.Style;
                int borderWidth = style.BorderWidth;

                //draw rectangles for border and background
                if (style.BorderColor != Color.Empty && style.BackColor != Color.Empty)
                {
                    Rectangle borderBounds = new Rectangle(0, 0, imageTemplate.Width, imageTemplate.Height);
                    Rectangle backBounds = new Rectangle(borderWidth, borderWidth, imageTemplate.Width - borderWidth * 2, imageTemplate.Height - borderWidth * 2);
                    Rectangle gradientBounds = new Rectangle(borderWidth, borderWidth, imageTemplate.Width, imageTemplate.Height + 60);

                    using (SolidBrush solidBrush = new SolidBrush(style.BorderColor))
                    {
                        myGraphics.FillRectangle(solidBrush, borderBounds);
                    }

                    using (LinearGradientBrush brush = new LinearGradientBrush(gradientBounds, style.BackColor, style.BackColor2, style.GradientAngle, true))
                    {
                        myGraphics.FillRectangle(brush, backBounds);
                    }
                }
                else if (style.BackColor != Color.Empty)
                {
                    Rectangle backBounds = new Rectangle(borderWidth, borderWidth, imageTemplate.Width, imageTemplate.Height);
                    Rectangle gradientBounds = new Rectangle(borderWidth, borderWidth, imageTemplate.Width, imageTemplate.Height + 60);

                    using (LinearGradientBrush gradientBrush = new LinearGradientBrush(gradientBounds, style.BackColor, style.BackColor2, style.GradientAngle, true))
                    {
                        myGraphics.FillRectangle(gradientBrush, backBounds);
                    }
                }

                //draw background if available
                if (style.Image != null)
                    using (Image image = Image.FromFile(style.Image))
                    {
                        ImageAttributes attributes = null;
                        float a = imageTemplate.Style.ImageAlpha;
                        if (a < 1)
                        {
                            float[][] matrixPoints =
                            {
                                new[] {1f, 0, 0, 0, 0},
                                new[] {0, 1f, 0, 0, 0},
                                new[] {0, 0, 1f, 0, 0},
                                new[] {0, 0, 0, a, 0},
                                new[] {0, 0, 0, 0, 1f}
                            };

                            ColorMatrix alphaMatrix = new ColorMatrix(matrixPoints);
                            attributes = new ImageAttributes();
                            attributes.SetColorMatrix(alphaMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        }
                        Rectangle imageBounds = new Rectangle(borderWidth, borderWidth, imageTemplate.Width - borderWidth * 2, imageTemplate.Height - borderWidth * 2);
                        myGraphics.DrawImage(image, imageBounds, 0, 0, imageBounds.Width, imageBounds.Height, GraphicsUnit.Pixel, attributes);
                    }

                foreach (BoxTemplate box in imageTemplate.Canvas)
                    RenderBoxes(box, myGraphics, templateValues);
            }
        }

        private static void RenderBoxes(BoxTemplate box, Graphics myGraphics, object templateValues)
        {
            StyleTemplate style = box.Style;
            int borderWidth = style.BorderWidth;

            if (style.BorderColor != Color.Empty && style.BackColor != Color.Empty)
                RenderBoxBorderAndBackground(box, style, borderWidth, myGraphics);
            else if (style.BackColor != Color.Empty)
                RenderBoxBackground(box, style, myGraphics);

            if (style.Image != null)
                RenderBoxImage(box, style, myGraphics, templateValues);

            if (box.Text != null)
                RenderBoxText(box, style, myGraphics, templateValues);
        }

        private static void RenderBoxBackground(BoxTemplate box, StyleTemplate style, Graphics myGraphics)
        {
            Rectangle backBounds = new Rectangle(box.Left, box.Top, box.Width, box.Height);
            Rectangle gradientBounds = new Rectangle(box.Left, box.Top, box.Width, box.Height + 60);

            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(gradientBounds, style.BackColor, style.BackColor2, style.GradientAngle, true))
            {
                myGraphics.FillRectangle(gradientBrush, backBounds);
            }
        }

        private static void RenderBoxBorderAndBackground(BoxTemplate box, StyleTemplate style, int borderWidth, Graphics myGraphics)
        {
            Rectangle borderBounds = new Rectangle(0, 0, box.Width, box.Height);
            Rectangle backBounds = new Rectangle(borderWidth, borderWidth, box.Width - borderWidth * 2, box.Height - borderWidth * 2);

            using (SolidBrush brush = new SolidBrush(style.BorderColor))
            {
                myGraphics.FillRectangle(brush, borderBounds);
            }

            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(new Rectangle(style.BorderWidth, style.BorderWidth, box.Width, box.Height + 60), style.BackColor, style.BackColor2, style.GradientAngle, true))
            {
                myGraphics.FillRectangle(gradientBrush, backBounds);
            }
        }

        private static void RenderBoxImage(BoxTemplate box, StyleTemplate style, Graphics myGraphics, object templateValues)
        {
            //find image max size
            int maxSize = box.Width < box.Height ? box.Width : box.Height;

            Dictionary<string, object> dictionary = templateValues.ToDictionary();
            string path = templateValues == null ? style.Image : FormattableObject.ToString(dictionary, style.Image, null);

            using (Image image = Image.FromFile(path))
            using (Image thumbnail = GetThumbnail(image, maxSize))
            using (Image rotated = RotateImage(thumbnail, thumbnail.Width / 2, thumbnail.Height / 2, style.Rotation, true))
            {
                Rectangle imageRectangle = new Rectangle(new Point(box.Left, box.Top), new Size(rotated.Width, rotated.Height));
                myGraphics.DrawImage(rotated, imageRectangle, 0, 0, rotated.Width, rotated.Height, GraphicsUnit.Pixel);
            }
        }

        private static void RenderBoxText(BoxTemplate box, StyleTemplate style, Graphics myGraphics, object templateValues)
        {
            //string format for image text
            StringFormat stringFormat = new StringFormat {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near};

            Dictionary<string, object> dictionary = templateValues.ToDictionary();
            dictionary.Add("DATETIME", DateTime.Now);
            dictionary.Add("TIME", DateTime.Now.ToShortTimeString());
            dictionary.Add("DATE", DateTime.Now.ToShortDateString());
            dictionary.Add("MACHINENAME", Environment.MachineName);
            dictionary.Add("OSVERSION", Environment.OSVersion);
            dictionary.Add("USERDOMAINNAME", Environment.UserDomainName);
            dictionary.Add("USERNAME", Environment.UserName);
            dictionary.Add("VERSION", Environment.Version);

            //draw text on image
            string text = templateValues == null ? box.Text : FormattableObject.ToString(dictionary, box.Text, null);

            Rectangle textBounds = new Rectangle(box.Left, box.Top, box.Width, box.Height);
            using (SolidBrush textBrush = new SolidBrush(style.ForeColor))
            {
                myGraphics.DrawString(text, style.Font, textBrush, textBounds, stringFormat);
            }
        }

        // TODO: Use thumbnail class.
        private static Image GetThumbnail(Image image, double maxSize)
        {
            if (image != null)
            {
                double ratio;
                if (image.Height > image.Width)
                    ratio = image.Height / maxSize;
                else
                    ratio = image.Width / maxSize;

                int newWidth = Convert.ToInt32(image.Width / ratio);
                int newHeight = Convert.ToInt32(image.Height / ratio);

                IntPtr ip = new IntPtr();
                return image.GetThumbnailImage(newWidth, newHeight, () => false, ip);
            }

            return null;
        }

        private static Bitmap RotateImage(Image image, int rotateAtX, int rotateAtY, float angle, bool bNoClip)
        {
            int w, h, x, y;
            if (bNoClip)
            {
                double dW = image.Width;
                double dH = image.Height;

                double degrees = Math.Abs(angle);
                if (degrees <= 90)
                {
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    w = (int) (dH * dSin + dW * dCos);
                    h = (int) (dW * dSin + dH * dCos);
                    x = (w - image.Width) / 2;
                    y = (h - image.Height) / 2;
                }
                else
                {
                    degrees -= 90;
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    w = (int) (dW * dSin + dH * dCos);
                    h = (int) (dH * dSin + dW * dCos);
                    x = (w - image.Width) / 2;
                    y = (h - image.Height) / 2;
                }
            }
            else
            {
                w = image.Width;
                h = image.Height;
                x = 0;
                y = 0;
            }

            //create a new empty bitmap to hold rotated image
            Bitmap bmpRet = new Bitmap(w, h);
            bmpRet.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(bmpRet);

            //Put the rotation point in the "center" of the image
            g.TranslateTransform(rotateAtX + x, rotateAtY + y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-rotateAtX - x, -rotateAtY - y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0 + x, 0 + y));

            return bmpRet;
        }
    }
}