#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TServerProtocol"></typeparam>
    /// <typeparam name="TClientProtocol"></typeparam>
    [DebuggerDisplay("IPAddress={Binding.IPAddress}; Port={Binding.Port}.")]
    public abstract class Protocol<TServerProtocol, TClientProtocol> : IServerProtocol<TServerProtocol, TClientProtocol>
    {
        #region IServerProtocol<TServerProtocol,TClientProtocol> Members

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public abstract void Start();

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// </summary>
        public event EventHandler<ProtocolEventArgs<TServerProtocol, TClientProtocol>> ClientConnected;

        /// <summary>
        ///     Gets or sets the binding.
        /// </summary>
        /// <value>The binding.</value>
        public ServerBinding Binding { get; set; }

        #endregion

        /// <summary>
        ///     Raises the <see cref="Protocol{TServerProtocol, TClientProtocol}.ClientConnected" /> event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="ProtocolEventArgs{TServerProtocol, TClientProtocol}" />
        ///     instance containing the event data.
        /// </param>
        protected virtual void OnClientConnected(ProtocolEventArgs<TServerProtocol, TClientProtocol> e)
        {
            EventHandler<ProtocolEventArgs<TServerProtocol, TClientProtocol>> handler = ClientConnected;
            if (handler != null)
                handler(this, e);
        }
    }
}