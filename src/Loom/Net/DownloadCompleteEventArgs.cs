#region Using Directives

using System;
using Loom.Annotations;

#endregion

namespace Loom.Net
{
    /// <summary>
    ///     Summary description for DownloadCompleteEventArgs.
    /// </summary>
    public sealed class DownloadCompleteEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadCompleteEventArgs" /> class.
        /// </summary>
        [PublicAPI]
        public DownloadCompleteEventArgs() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadCompleteEventArgs" /> class.
        /// </summary>
        /// <param name="dataDownloaded">The data downloaded.</param>
        [PublicAPI]
        public DownloadCompleteEventArgs(byte[] dataDownloaded)
        {
            Data = dataDownloaded;
        }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [PublicAPI]
        public byte[] Data { get; set; }
    }
}