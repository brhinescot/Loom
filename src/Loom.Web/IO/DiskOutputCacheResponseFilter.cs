#region Using Directives

using System;
using System.IO;

#endregion

namespace Loom.Web.IO
{
    // helper class to capture the response into a file
    internal class DiskOutputCacheResponseFilter : Stream
    {
        private readonly Stream chainedStream;
        private FileStream captureFileStream;

        internal DiskOutputCacheResponseFilter(Stream filterChain, string captureFilename)
        {
            chainedStream = filterChain;
            CaptureFilename = captureFilename;
            captureFileStream = new FileStream(CaptureFilename, FileMode.CreateNew, FileAccess.Write);
        }

        internal string CaptureFilename { get; }

        // Stream implementation

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        internal void StopFiltering(bool deleteDataFile)
        {
            if (captureFileStream != null)
            {
                captureFileStream.Close();
                captureFileStream = null;
            }

            if (deleteDataFile)
                File.Delete(CaptureFilename);
        }

        public override void Flush()
        {
            chainedStream.Flush();

            if (captureFileStream != null)
                captureFileStream.Flush();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            chainedStream.Write(buffer, offset, count);

            if (captureFileStream != null)
                captureFileStream.Write(buffer, offset, count);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}