#region Using Directives

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Loom.Drawing.Filters;

#endregion

namespace Loom.Drawing
{
    /// <summary>
    ///     Summary description for JpgFormat.
    /// </summary>
    public static class JpgFormat
    {
        private const string EncoderType = "image/jpeg";
        private const int EncoderParameterCount = 1;

        public static void Save(string fileName, Image image, int imageQuality)
        {
            Save(fileName, image, imageQuality, null);
        }

        public static void Save(string fileName, Image image, int imageQuality, params IFilter[] filters)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {
                Save(stream, image, imageQuality, filters);
            }
        }

        public static void Save(Stream stream, Image image, int imageQuality)
        {
            Save(stream, image, imageQuality, null);
        }

        public static void Save(Stream stream, Image image, int imageQuality, params IFilter[] filters)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));
            Argument.Assert.IsNotNull(image, nameof(image));
            Argument.Assert.IsInRange(0, 100, imageQuality, nameof(imageQuality));

            if (filters != null && filters.Length > 0)
                for (int i = 0; i < filters.Length; i++)
                    filters[i].Apply(image);

            ImageCodecInfo codecInfo = GetEncoderInfo(EncoderType);
            Encoder jpgEncoder = Encoder.Quality;
            EncoderParameters jpgEncoderParameters = new EncoderParameters(EncoderParameterCount);
            EncoderParameter jpgEncoderParameter = new EncoderParameter(jpgEncoder, imageQuality);
            jpgEncoderParameters.Param[0] = jpgEncoderParameter;

            try
            {
                image.Save(stream, codecInfo, jpgEncoderParameters);
                stream.Seek(0, SeekOrigin.Begin);
            }
            finally
            {
                jpgEncoderParameters.Dispose();
                jpgEncoderParameter.Dispose();
            }
        }

        public static byte[] ToByteArray(Image image, int imageQuality)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Save(stream, image, imageQuality);
                return stream.ToArray();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            return encoders.FirstOrDefault(t => t.MimeType == mimeType);
        }
    }
}