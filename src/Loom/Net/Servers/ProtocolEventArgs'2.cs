#region Using Directives

using System;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TServerProtocol"></typeparam>
    /// <typeparam name="TClientProtocol"></typeparam>
    public sealed class ProtocolEventArgs<TServerProtocol, TClientProtocol> : EventArgs
    {
        /// <summary>
        /// </summary>
        public new static readonly ProtocolEventArgs<TServerProtocol, TClientProtocol> Empty = new ProtocolEventArgs<TServerProtocol, TClientProtocol>(default(TServerProtocol), default(TClientProtocol), null);

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProtocolEventArgs{TServerProtocol,TClientProtocol}" /> class.
        /// </summary>
        /// <param name="serverProtocol">The server protocol.</param>
        /// <param name="clientProtocol">The client protocol.</param>
        /// <param name="binding">The binding.</param>
        public ProtocolEventArgs(TServerProtocol serverProtocol, TClientProtocol clientProtocol, ServerBinding binding)
        {
            ServerProtocol = serverProtocol;
            ClientProtocol = clientProtocol;
            Binding = binding;
        }

        /// <summary>
        ///     Gets the binding.
        /// </summary>
        /// <value>The binding.</value>
        public ServerBinding Binding { get; }

        /// <summary>
        ///     Gets the client protocol.
        /// </summary>
        /// <value>The client protocol.</value>
        public TClientProtocol ClientProtocol { get; }

        /// <summary>
        ///     Gets the server protocol.
        /// </summary>
        /// <value>The server protocol.</value>
        public TServerProtocol ServerProtocol { get; }
    }
}