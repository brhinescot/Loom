#region Using Directives

using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;

#endregion

namespace Loom.Drawing
{
    [TestFixture]
    public class ThumbnailTests
    {
        [Test]
        public void SameHeightWidthConstraintSquareImage()
        {
            using (Image image = new Bitmap(300, 300))
            using (Image thumb = Thumbnail.FromImage(image, 50))
            {
                Assert.AreEqual(new Size(50, 50), thumb.Size);
            }
        }

        [Test]
        public void SameHeightWidthConstraintRectangleImage()
        {
            using (Image image = new Bitmap(300, 400))
            using (Image thumb = Thumbnail.FromImage(image, 50))
            {
                Assert.AreEqual(new Size(38, 50), thumb.Size);
            }
        }

        [Test]
        public void SameHeightWidthConstraintRectangleImage2()
        {
            using (Image image = new Bitmap(400, 300))
            using (Image thumb = Thumbnail.FromImage(image, 50))
            {
                Assert.AreEqual(new Size(50, 38), thumb.Size);
            }
        }

        [Test]
        public void DifferentHeightWidthConstraintSquareImage()
        {
            using (Image image = new Bitmap(300, 300))
            using (Image thumb = Thumbnail.FromImage(image, 70, 50))
            {
                Assert.AreEqual(new Size(50, 50), thumb.Size);
            }
        }

        [Test]
        public void DifferentHeightWidthConstraintSquareImage2()
        {
            using (Image image = new Bitmap(300, 300))
            using (Image thumb = Thumbnail.FromImage(image, 50, 70))
            {
                Assert.AreEqual(new Size(50, 50), thumb.Size);
            }
        }

        [Test]
        public void DifferentHeightWidthConstraintRectangleImage()
        {
            using (Image image = new Bitmap(72, 58))
            using (Image thumb = Thumbnail.FromImage(image, 70, 54))
            {
                Assert.AreEqual(new Size(67, 54), thumb.Size);
            }
        }

        [Test]
        public void DifferentHeightWidthConstraintRectangleImage2()
        {
            using (Image image = new Bitmap(58, 72))
            using (Image thumb = Thumbnail.FromImage(image, 54, 70))
            {
                Assert.AreEqual(new Size(54, 67), thumb.Size);
            }
        }

        [Test]
        public void CreateThumbnailFromJpg()
        {
            using (Image image = Image.FromFile(@"Drawing\Resources\ULPT-L.jpg"))
            using (Image thumb = Thumbnail.FromImage(image, 134, 107))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    JpgFormat.Save(stream, thumb, 80);
                    using (Bitmap bitmap = MemoryBitmap.FromStream(stream))
                    {
                        Assert.AreEqual(new Size(134, 107), bitmap.Size);
                    }
                }
            }
        }

        [Test]
        public void CreateThumbnailFromFolder()
        {
            const string path = @"Drawing\Resources\Averaging Test Images";
            const int size = 100;

            File.Delete(@"CreateThumbnailFromFolder.jpg");
            using (Image thumb1 = Thumbnail.CreateStack(path, size, ThumbnailStackType.Fan))
            {
                Assert.IsNotNull(thumb1);
                JpgFormat.Save(@"CreateThumbnailFromFolder.jpg", thumb1, 100);
            }
        }

        public void FromGif()
        {
            using (Image thumb1 = Thumbnail.FromFile(@"C:\Users\Brian\Desktop\Awards\architech08.gif", 80))
            {
                Assert.IsNotNull(thumb1);
            }
        }

        public void FromGifImage()
        {
            using (Image image = Image.FromFile(@"C:\Users\Brian\Desktop\Awards\architech08.gif"))
            using (Image thumb1 = Thumbnail.FromImage(image, 80))
            {
                Assert.IsNotNull(thumb1);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullImageArgument()
        {
            Thumbnail.FromImage(null, 50);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullImageWithHeightAndWidth()
        {
            Thumbnail.FromImage(null, 50, 50);
        }

//        [Test, ExpectedException(typeof (ArgumentOutOfRangeException))]
//        public void InvalidMaxSize()
//        {
//            using (Image image = new Bitmap(300, 300))
//                Thumbnail.FromImage(image, 0);
//        }
//
//        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
//        public void InvalidWidthAndHeight()
//        {
//            using (Image image = new Bitmap(300, 300))
//                Thumbnail.FromImage(image, 0, 0);
//        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AllInvalidArguments()
        {
            Thumbnail.FromImage(null, 0, 0);
        }
    }
}