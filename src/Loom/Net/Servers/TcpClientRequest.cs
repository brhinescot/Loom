#region Using Directives

using System.Net;
using System.Net.Sockets;

#endregion

namespace Loom.Net.Servers
{
    public class TcpClientRequest : IClientRequest
    {
        private readonly byte[] data;

        internal TcpClientRequest(byte[] data, TcpClient client)
        {
            this.data = data;
            IPAddress = ((IPEndPoint) client.Client.RemoteEndPoint).Address;
            Port = ((IPEndPoint) client.Client.RemoteEndPoint).Port;
        }

        #region IClientRequest Members

        public byte[] Read()
        {
            byte[] buffer = new byte[data.Length];
            data.CopyTo(buffer, 0);
            return buffer;
        }

        public IPAddress IPAddress { get; }

        public int Port { get; }

        #endregion
    }
}