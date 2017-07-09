#region Using Directives

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public class TcpProtocol : Protocol<TcpListener, TcpClient>
    {
        private bool isRunning;
        private TcpListener listener;

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public override void Start()
        {
            Debug.Assert(Binding != null, SR.ExceptionServerBindingIsNull);

            if (Binding == null)
                throw new InvalidOperationException(SR.ExceptionServerBindingIsNull);

            try
            {
                listener = new TcpListener(Binding.IPAddress, Binding.Port);
                listener.Start(500);
                isRunning = true;
                Debug.WriteLine("Loom.Net.Servers.TcpProtocol: TCP listener started on IP address {0} port {1}.", Binding.IPAddress, Binding.Port);

                Thread connectionQueueWorker = new Thread(AcceptAndQueueIncomingConnections);
                connectionQueueWorker.Start();
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public override void Stop()
        {
            isRunning = false;
            listener.Stop();
            Debug.WriteLine("Loom.Net.Servers.TcpProtocol: TCP listener stopped on IP address {0} port {1}.", Binding.IPAddress, Binding.Port);
        }

        /// <summary>
        ///     Accepts the and queue incoming connections.
        /// </summary>
        private void AcceptAndQueueIncomingConnections()
        {
            while (isRunning)
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Debug.WriteLine("Loom.Net.Servers.TcpProtocol: Client connected from {0}:{1}.", ((IPEndPoint) client.Client.RemoteEndPoint).Address, ((IPEndPoint) client.Client.RemoteEndPoint).Port);
                    OnClientConnected(new ProtocolEventArgs<TcpListener, TcpClient>(listener, client, Binding));
                    Thread.Sleep(2);
                }
                catch (SocketException ex)
                {
                    // Expected error code when listener is stopped during a blocking operation 
                    // such as TcpListener.AcceptTcpClient(). Safe to ignore. Only happens when
                    // server is shutting down.
                    if (ex.ErrorCode != 10004)
                        Debug.WriteLine("Loom.Net.Servers.TcpProtocol: Socket error {0} attempting to accept a client connection:\r\n{1}", ex.ErrorCode, ex);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Loom.Net.Servers.TcpProtocol: Error attempting to accept a client connection:\r\n{0}", ex);
                }
        }
    }
}