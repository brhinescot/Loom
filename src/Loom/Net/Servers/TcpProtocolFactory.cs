#region Using Directives

using System.Net.Sockets;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public class TcpProtocolFactory : IServerProtocolFactory<TcpListener, TcpClient>
    {
        #region IServerProtocolFactory<TcpListener,TcpClient> Members

        /// <summary>
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public IServerProtocol<TcpListener, TcpClient> Create(ServerBinding binding)
        {
            TcpProtocol protocol = new TcpProtocol
            {
                Binding = binding
            };
            return protocol;
        }

        #endregion
    }
}