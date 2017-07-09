#region Using Directives

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public sealed class TcpSession : IServerSession<TcpClient>
    {
        private readonly ServerBinding binding;
        private readonly ISessionProcessor processor;
        private bool isActive = true;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpSession" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="serverBinding">The server binding.</param>
        /// <param name="sessionProcessor">The session processor.</param>
        public TcpSession(TcpClient client, ServerBinding serverBinding, ISessionProcessor sessionProcessor)
        {
            Argument.Assert.IsNotNull(client, "client");

            ClientProtocol = client;
            binding = serverBinding;
            processor = sessionProcessor;
            SessionId = Guid.NewGuid().ToString();
            StartTime = DateTime.Now;
        }

        #region IServerSession<TcpClient> Members

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Gets the session id.
        /// </summary>
        /// <value>The session id.</value>
        public string SessionId { get; }

        /// <summary>
        ///     Gets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; }

        /// <summary>
        ///     Gets the client protocol.
        /// </summary>
        /// <value>The client protocol.</value>
        public TcpClient ClientProtocol { get; }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!isActive)
                throw new InvalidOperationException(string.Format("Session {0} is no longer alive.", SessionId));

            try
            {
                byte[] bytes = Encoding.Default.GetBytes(processor.ServerReadyMessage);
                ClientProtocol.GetStream().Write(bytes, 0, bytes.Length);

                BeginClientCommunication();
            }
            catch (ArgumentNullException)
            {
                Stop();
                throw;
            }
            catch (ArgumentException)
            {
                Stop();
                throw;
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            Stop(false);
        }

        /// <summary>
        ///     Stops the specified timeout.
        /// </summary>
        /// <param name="timeout">
        ///     if set to <c>true</c> [timeout].
        /// </param>
        public void Stop(bool timeout)
        {
            if (!isActive)
                return;

            if (timeout)
            {
                Debug.WriteLine("Session timed out on IP address {0} port {1}.", binding.IPAddress, binding.Port);
                if (ClientProtocol.Connected)
                {
                    IServerResponse response = new TcpServerResponse(ClientProtocol);
                    response.Write(processor.TimeoutMessage);
                }
            }
            isActive = false;
        }

        #endregion

        private static void LogCommunicationException(Exception ex)
        {
            Debug.WriteLine("Closing Session: " + ex.Message);
        }

        private void BeginClientCommunication()
        {
            try
            {
                NetworkStream stream = ClientProtocol.GetStream();
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    while (isActive && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        ProcessData(buffer.Segment(0, bytesRead), SocketResult.Ok);
                }
                catch (SocketException ex)
                {
                    LogCommunicationException(ex);
                }
                catch (IOException ex)
                {
                    LogCommunicationException(ex);
                }
                finally
                {
                    stream.Dispose();
                }
            }
            catch (SocketException ex)
            {
                LogCommunicationException(ex);
            }
            catch (IOException ex)
            {
                LogCommunicationException(ex);
            }
            finally
            {
                Debug.WriteLine("Closing Session: Client disconnected");
                Stop();
            }
        }

        private void ProcessData(byte[] data, SocketResult result)
        {
            try
            {
                using (TcpServerResponse response = new TcpServerResponse(ClientProtocol))
                {
                    switch (result)
                    {
                        case SocketResult.Ok:
                            processor.Process(new TcpClientRequest(data, ClientProtocol), response);
                            if (response.EndConnection)
                                Stop();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                Stop(false);
        }
    }
}