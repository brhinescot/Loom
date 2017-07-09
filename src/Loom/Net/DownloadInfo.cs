#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;

#endregion

namespace Loom.Net
{
    internal class DownloadInfo
    {
        private const int BufferSize = 1024;

        private readonly byte[] bufferRead;
        private byte[] dataBufferFast;

        public DownloadInfo()
        {
            bufferRead = new byte[BufferSize];
            DataLength = -1;

            UseFastBuffers = true;
        }

        public int BytesProcessed { get; set; }
        public EventHandler<DownloadBytesReceivedEventArgs> BytesReceivedCallback { get; set; }

        // TODO: Needs to be changed to a byte array (byte[]).
        public Collection<byte> DataBufferSlow { get; set; } = new Collection<byte>();

        public int DataLength { get; set; }

        public WebRequest Request { get; set; }

        public Stream ResponseStream { get; set; }
        public bool UseFastBuffers { get; set; }

        public byte[] GetBufferRead()
        {
            return bufferRead;
        }

        public byte[] GetDataBufferFast()
        {
            return dataBufferFast;
        }

        internal void SetDataBufferFast(byte[] value)
        {
            dataBufferFast = value;
        }
    }
}