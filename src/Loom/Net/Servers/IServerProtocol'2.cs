#region Using Directives

using System;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TServerProtocol"></typeparam>
    /// <typeparam name="TClientProtocol"></typeparam>
    public interface IServerProtocol<TServerProtocol, TClientProtocol>
    {
        /// <summary>
        /// </summary>
        ServerBinding Binding { get; set; }

        /// <summary>
        /// </summary>
        event EventHandler<ProtocolEventArgs<TServerProtocol, TClientProtocol>> ClientConnected;

        /// <summary>
        /// </summary>
        void Start();

        /// <summary>
        /// </summary>
        void Stop();
    }
}