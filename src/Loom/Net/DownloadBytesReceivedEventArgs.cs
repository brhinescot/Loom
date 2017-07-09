#region Using Directives

using System;
using Loom.Annotations;

#endregion

namespace Loom.Net
{
    /// <summary>
    ///     Summary description for DownloadProgressEventHandler.
    /// </summary>
    public sealed class DownloadBytesReceivedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadBytesReceivedEventArgs" /> class.
        /// </summary>
        [PublicAPI]
        public DownloadBytesReceivedEventArgs() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadBytesReceivedEventArgs" /> class.
        /// </summary>
        /// <param name="bytesRead">The bytes read.</param>
        /// <param name="totalBytes">The total bytes.</param>
        /// <param name="data">The data.</param>
        public DownloadBytesReceivedEventArgs(int bytesRead, int totalBytes, byte[] data)
        {
            BytesRead = bytesRead;
            TotalBytes = totalBytes;
            Data = data;
        }

        /// <summary>
        ///     Gets or sets the bytes read.
        /// </summary>
        /// <value>The bytes read.</value>
        [PublicAPI]
        public int BytesRead { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [PublicAPI]
        public byte[] Data { get; set; }

        /// <summary>
        ///     Gets or sets the total bytes.
        /// </summary>
        /// <value>The total bytes.</value>
        [PublicAPI]
        public int TotalBytes { get; set; }
    }
}