#region Using Directives

using System.Net.Sockets;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public abstract class TcpServer : SocketServer<TcpListener, TcpClient>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpServer" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="bindings">The bindings.</param>
        protected TcpServer(ISessionProcessor processor, params ServerBinding[] bindings)
            : base(new TcpSessionFactory(), processor, new TcpProtocolFactory(), bindings) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpServer" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="protocolFactory">The protocol factory.</param>
        protected TcpServer(ISessionProcessor processor, IServerProtocolFactory<TcpListener, TcpClient> protocolFactory)
            : base(new TcpSessionFactory(), processor, protocolFactory) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpServer" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="bindings">The bindings.</param>
        protected TcpServer(ISessionProcessor processor, IServerProtocolFactory<TcpListener, TcpClient> protocolFactory, params ServerBinding[] bindings)
            : base(new TcpSessionFactory(), processor, protocolFactory, bindings) { }
    }
}