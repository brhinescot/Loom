#region Using Directives

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

#endregion

namespace Loom.Net
{
    /// <summary>
    ///     Summary description for HttpFileTransfer.
    /// </summary>
    public partial class HttpFileTransfer : Component
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpFileTransfer" /> class.
        /// </summary>
        public HttpFileTransfer()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpFileTransfer" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public HttpFileTransfer(IContainer container)
        {
            Argument.Assert.IsNotNull(container, "container");

            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        ///     The location of the file to download.
        /// </summary>
        public Uri DownloadLocation { get; set; }

        /// <summary>
        ///     Raised when the download is completed.
        /// </summary>
        public event EventHandler<DownloadCompleteEventArgs> Complete;

        /// <summary>
        ///     Raised each time the byte buffer is filled during the download.
        /// </summary>
        public event EventHandler<DownloadBytesReceivedEventArgs> BytesReceived;

        /// <summary>
        ///     Downloads the file.
        /// </summary>
        public void Download()
        {
            if (DownloadLocation == null)
                throw new InvalidOperationException("The DownloadLocation property has not been set. Set the property before calling download or use the overload that takes a Uri as a parameter.");

            Thread t = new Thread(StartDownload);
            t.Start();
        }

        /// <summary>
        ///     Downloads the file at the specified <see cref="Uri" />.
        /// </summary>
        /// <param name="url">
        ///     The <see cref="Uri" /> to download from.
        /// </param>
        public void Download(Uri url)
        {
            DownloadLocation = url;
            Download();
        }

        private void StartDownload()
        {
            if (Complete != null && DownloadLocation != null)
            {
                DownloadWorker downloadWorker = new DownloadWorker();
                byte[] downloadedData = downloadWorker.Download(DownloadLocation, BytesReceived);
                Complete(this, new DownloadCompleteEventArgs(downloadedData));
            }
        }

        #region Nested type: DownloadWorker

        internal class DownloadWorker
        {
            private const int BufferSize = 1024;

            public readonly ManualResetEvent ResetEvent = new ManualResetEvent(false);

            internal byte[] Download(Uri url, EventHandler<DownloadBytesReceivedEventArgs> bytesReceivedCallback)
            {
                // Ensure flag set correctly.			
                ResetEvent.Reset();

                //// Get the URI from the command line.
                //Uri httpSite = new Uri(url);

                // Create the request object.
                WebRequest request = WebRequest.Create(url);

                // Create the state object.
                DownloadInfo info = new DownloadInfo();

                // Put the request into the state object so it can be passed around.
                info.Request = request;

                // Assign the callbacks
                info.BytesReceivedCallback += bytesReceivedCallback;

                // Issue the async request.
                request.BeginGetResponse(ResponseCallback, info);

                // Wait until the ManualResetEvent is set so that the application
                // does not exit until after the callback is called.
                ResetEvent.WaitOne();

                // Pass back the downloaded information.
                if (info.UseFastBuffers)
                    return info.GetDataBufferFast();

                byte[] data = new byte[info.DataBufferSlow.Count];
                for (int b = 0; b < info.DataBufferSlow.Count; b++)
                    data[b] = info.DataBufferSlow[b];
                return data;
            }

            private void ReadCallBack(IAsyncResult asyncResult)
            {
                // Get the DownloadInfo object from AsyncResult.
                DownloadInfo info = (DownloadInfo) asyncResult.AsyncState;

                // Retrieve the ResponseStream that was set in RespCallback.
                Stream responseStream = info.ResponseStream;

                // Read info.BufferRead to verify that it contains data.
                int bytesRead = responseStream.EndRead(asyncResult);
                if (bytesRead > 0)
                {
                    if (info.UseFastBuffers)
                        Array.Copy(info.GetBufferRead(), 0,
                            info.GetDataBufferFast(), info.BytesProcessed,
                            bytesRead);
                    else
                        for (int b = 0; b < bytesRead; b++)
                            info.DataBufferSlow.Add(info.GetBufferRead()[b]);
                    info.BytesProcessed += bytesRead;

                    // If a registered progress-callback, inform it of our
                    // download progress so far.
                    EventHandler<DownloadBytesReceivedEventArgs> handler = info.BytesReceivedCallback;
                    if (handler != null)
                        handler(this, new DownloadBytesReceivedEventArgs(info.BytesProcessed, info.DataLength, info.GetBufferRead()));

                    // Continue reading data until responseStream.EndRead returns –1.
                    responseStream.BeginRead(
                        info.GetBufferRead(), 0, BufferSize,
                        ReadCallBack, info);
                }
                else
                {
                    responseStream.Close();
                    ResetEvent.Set();
                }
            }

            private void ResponseCallback(IAsyncResult ar)
            {
                // Get the DownloadInfo object from the async result were
                // we're storing all of the temporary data and the download
                // buffer.
                DownloadInfo info = (DownloadInfo) ar.AsyncState;

                // Get the WebRequest from RequestState.
                WebRequest webRequest = info.Request;

                // Call EndGetResponse, which produces the WebResponse object
                // that came from the request issued above.
                WebResponse response = webRequest.EndGetResponse(ar);

                // Find the data size from the headers.
                string strContentLength = response.Headers["Content-Length"];
                if (strContentLength != null)
                {
                    info.DataLength = Convert.ToInt32(strContentLength);
                    info.SetDataBufferFast(new byte[info.DataLength]);
                }
                else
                {
                    info.UseFastBuffers = false;
                }

                //  Start reading data from the response stream.
                Stream ResponseStream = response.GetResponseStream();

                // Store the response stream in RequestState to read
                // the stream asynchronously.
                info.ResponseStream = ResponseStream;

                //  Pass do.BufferRead to BeginRead.
                ResponseStream.BeginRead(info.GetBufferRead(),
                    0,
                    BufferSize,
                    ReadCallBack,
                    info);
            }
        }

        #endregion
    }
}