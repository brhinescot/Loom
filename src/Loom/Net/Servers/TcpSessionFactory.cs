#region Using Directives

using System.Net.Sockets;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public class TcpSessionFactory : IServerSessionFactory<TcpClient>
    {
        #region IServerSessionFactory<TcpClient> Members

        /// <summary>
        /// </summary>
        /// <param name="clientProtocol"></param>
        /// <param name="serverBinding"></param>
        /// <param name="sessionProcessor"></param>
        /// <returns></returns>
        public IServerSession<TcpClient> Create(TcpClient clientProtocol, ServerBinding serverBinding, ISessionProcessor sessionProcessor)
        {
            return new TcpSession(clientProtocol, serverBinding, sessionProcessor);
        }

        #endregion
    }
}