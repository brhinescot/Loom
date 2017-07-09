#region Using Directives

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public sealed class TcpServerResponse : IServerResponse, IDisposable
    {
        private readonly TcpClient client;
        private MemoryStream dataBuffer;

        internal TcpServerResponse(TcpClient client)
        {
            this.client = client;
        }

        private MemoryStream DataBuffer => dataBuffer ?? (dataBuffer = new MemoryStream());

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region IServerResponse Members

        /// <summary>
        ///     Buffers the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        public void Buffer(byte[] response)
        {
            Argument.Assert.IsNotNull(response, nameof(response));
            ProcessData(response, DataBuffer);
        }

        /// <summary>
        ///     Buffers the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        public void Buffer(string response)
        {
            Argument.Assert.IsNotNull(response, nameof(response));
            ProcessData(Encoding.Default.GetBytes(response), DataBuffer);
        }

        /// <summary>
        ///     Writes any buffered data to the output stream.
        /// </summary>
        public void Flush()
        {
            NetworkStream stream = client.GetStream();
            byte[] bufferedData = DataBuffer.ToArray();
            stream.Write(bufferedData, 0, bufferedData.Length);
        }

        /// <summary>
        ///     Writes the specified data to the output stream.
        /// </summary>
        /// <param name="response">The response.</param>
        public void Write(byte[] response)
        {
            Argument.Assert.IsNotNull(response, nameof(response));

            NetworkStream stream = client.GetStream();
            ProcessData(response, stream);
        }

        /// <summary>
        ///     Writes the specified text to the output stream.
        /// </summary>
        /// <param name="response">The text response.</param>
        public void Write(string response)
        {
            Argument.Assert.IsNotNull(response, nameof(response));

            NetworkStream stream = client.GetStream();
            ProcessData(Encoding.Default.GetBytes(response), stream);
        }

        /// <summary>
        ///     Writes the specified file to the output stream.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public void WriteFile(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));
            Argument.Assert.FileExists(path);

            using (FileStream fileStream = File.OpenRead(path))
            {
                ProcessFile(path, fileStream);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [end connection].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [end connection]; otherwise, <c>false</c>.
        /// </value>
        public bool EndConnection { get; set; }

        #endregion

        /// <summary>
        ///     Writes the specified file to the output stream.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public void BufferFile(string path)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));
            Argument.Assert.FileExists(path);

            ProcessFile(path, DataBuffer);
        }

        private static void ProcessData(byte[] data, Stream stream)
        {
            stream.Write(data, 0, data.Length);
        }

        private static void ProcessFile(string path, Stream output)
        {
            using (FileStream fileStream = File.OpenRead(path))
            {
                // PERF: Add bandwith limiting to TcpServerResponse.WriteFile(string)
                int bytesRead = 1;
                while (bytesRead > 0)
                {
                    byte[] data = new byte[10000];
                    bytesRead = fileStream.Read(data, 0, data.Length);
                    output.Write(data, 0, bytesRead);
                }
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (disposing)
                if (dataBuffer != null)
                    dataBuffer.Close();
        }
    }
}