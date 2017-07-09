#region Using Directives

using System.IO;

#endregion

namespace Loom.IO
{
    public static class StreamExtensions
    {
        public static void Write(this Stream stream, Stream output, int bufferLength = 256)
        {
            Argument.Assert.IsNotNull(stream, nameof(stream));
            Argument.Assert.IsNotNull(output, nameof(output));
            Argument.Assert.IsGreaterThanZero(bufferLength, nameof(bufferLength));

            WritePrivate(stream, output, bufferLength);
        }

        public static string ReadString(this Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static void WritePrivate(Stream stream, Stream output, int bufferLength)
        {
            byte[] buffer = new byte[bufferLength];
            int bytesRead = stream.Read(buffer, 0, bufferLength);

            while (bytesRead > 0)
            {
                output.Write(buffer, 0, bytesRead);
                bytesRead = stream.Read(buffer, 0, bufferLength);
            }
        }
    }
}