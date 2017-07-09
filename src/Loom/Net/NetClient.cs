#region Using Directives

using System;
using System.ComponentModel;
using System.IO;
using System.Net;

#endregion

namespace Loom.Net
{
    /// <summary>
    /// </summary>
    public partial class NetClient : Component
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NetClient" /> class.
        /// </summary>
        public NetClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        public string DownloadDirectory { get; set; }

        /// <summary>
        /// </summary>
        public bool ShowCompleteDialog { get; set; }

        /// <summary>
        /// </summary>
        public event AsyncCompletedEventHandler DownloadFileCompleted;

        /// <summary>
        /// </summary>
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        ///     Downloads the file async.
        /// </summary>
        /// <param name="address">The address.</param>
        public void DownloadFileAsync(Uri address)
        {
            Argument.Assert.IsNotNull(address, "address");
            string fileName = Path.GetFileName(address.AbsolutePath);
            DownloadFileAsync(address, Path.Combine(DownloadDirectory, fileName));
        }

        /// <summary>
        ///     Downloads the file async.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="fileName">Name of the file.</param>
        public void DownloadFileAsync(Uri address, string fileName)
        {
            if (!Path.IsPathRooted(fileName) && !Compare.IsNullOrEmpty(DownloadDirectory))
                fileName = Path.Combine(DownloadDirectory, fileName);

            client.DownloadFileAsync(address, fileName);
        }

        /// <summary>
        ///     Raises the <see cref="NetClient.DownloadProgressChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="System.Net.DownloadProgressChangedEventArgs" /> instance containing the event data.
        /// </param>
        protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChangedEventHandler handler = DownloadProgressChanged;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="NetClient.DownloadFileCompleted" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.AsyncCompletedEventArgs" /> instance containing the event data.
        /// </param>
        protected virtual void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            AsyncCompletedEventHandler handler = DownloadFileCompleted;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        ///     Handles the download file completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.ComponentModel.AsyncCompletedEventArgs" /> instance containing the event data.
        /// </param>
        private void HandleDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            OnDownloadFileCompleted(e);
        }

        /// <summary>
        ///     Handles the download progress changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Net.DownloadProgressChangedEventArgs" /> instance containing the event data.
        /// </param>
        private void HandleDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnDownloadProgressChanged(e);
        }
    }
}