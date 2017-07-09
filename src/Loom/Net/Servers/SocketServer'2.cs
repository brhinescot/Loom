#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Loom.Threading;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TServerProtocol"></typeparam>
    /// <typeparam name="TClientProtocol"></typeparam>
    public abstract class SocketServer<TServerProtocol, TClientProtocol>
    {
        private readonly ISessionProcessor processor;
        private readonly List<IServerProtocol<TServerProtocol, TClientProtocol>> protocols = new List<IServerProtocol<TServerProtocol, TClientProtocol>>();
        private readonly List<IServerSession<TClientProtocol>> serverSessions = new List<IServerSession<TClientProtocol>>();
        private readonly IServerSessionFactory<TClientProtocol> sessionFactory;
        private bool isRunning;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocketServer{TServerProtocol,TClientProtocol}" /> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        /// <param name="processor">The processor.</param>
        /// <param name="protocolFactory">The protocol factory.</param>
        protected SocketServer(IServerSessionFactory<TClientProtocol> sessionFactory, ISessionProcessor processor, IServerProtocolFactory<TServerProtocol, TClientProtocol> protocolFactory)
            : this(sessionFactory, processor, protocolFactory, ServerBinding.Default) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocketServer{TServerProtocol,TClientProtocol}" /> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        /// <param name="processor">The processor.</param>
        /// <param name="protocolFactory">The protocol factory.</param>
        /// <param name="bindings">The bindings.</param>
        protected SocketServer(IServerSessionFactory<TClientProtocol> sessionFactory, ISessionProcessor processor, IServerProtocolFactory<TServerProtocol, TClientProtocol> protocolFactory, params ServerBinding[] bindings)
        {
            this.sessionFactory = sessionFactory;
            this.processor = processor;
            CreateProtocols(bindings, protocolFactory);
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!isRunning)
            {
                OnStarting(EventArgs.Empty);
                isRunning = true;
                StartAllProtocols();
            }
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (isRunning)
            {
                OnStoping(EventArgs.Empty);
                isRunning = false;
                StopAllProtocols();
            }
        }

        /// <summary>
        ///     Restarts this instance.
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        ///     Determines whether [is client valid] [the specified client protocol].
        /// </summary>
        /// <param name="clientProtocol">The client protocol.</param>
        /// <returns>
        ///     <c>true</c> if [is client valid] [the specified client protocol]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsClientValid(TClientProtocol clientProtocol)
        {
            return true;
        }

        /// <summary>
        ///     Starts the session.
        /// </summary>
        /// <param name="clientProtocol">The client protocol.</param>
        /// <param name="binding">The binding.</param>
        protected virtual void StartSession(TClientProtocol clientProtocol, ServerBinding binding)
        {
            IServerSession<TClientProtocol> session = sessionFactory.Create(clientProtocol, binding, processor);
            serverSessions.Add(session);
            session.Start();
        }

        /// <summary>
        ///     Ends the session.
        /// </summary>
        /// <param name="clientProtocol">The client protocol.</param>
        /// <param name="binding">The binding.</param>
        protected virtual void EndSession(TClientProtocol clientProtocol, ServerBinding binding)
        {
            foreach (IServerSession<TClientProtocol> session in serverSessions.Where(s => s.ClientProtocol.Equals(clientProtocol)))
                session.Stop();
        }

        private void CreateProtocols(IEnumerable<ServerBinding> bindings, IServerProtocolFactory<TServerProtocol, TClientProtocol> protocolFactory)
        {
            foreach (ServerBinding bindInfo in bindings)
                protocols.Add(protocolFactory.Create(bindInfo));
        }

        private void StartAllProtocols()
        {
            foreach (IServerProtocol<TServerProtocol, TClientProtocol> protocol in protocols)
            {
                protocol.ClientConnected += HandleClientConnected;
                new Thread(protocol.Start).Start();
            }
        }

        private void StopAllProtocols()
        {
            foreach (IServerProtocol<TServerProtocol, TClientProtocol> protocol in protocols)
            {
                protocol.Stop();
                protocol.ClientConnected -= HandleClientConnected;
            }
        }

        private void HandleClientConnected(object sender, ProtocolEventArgs<TServerProtocol, TClientProtocol> e)
        {
            if (IsClientValid(e.ClientProtocol))
                DelegateAdapter.StartThread(new StartSessionHandler(StartSession), e.ClientProtocol, e.Binding);
            else
                EndSession(e.ClientProtocol, e.Binding);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStarting(EventArgs e)
        {
            EventHandler handler = Starting;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStoping(EventArgs e)
        {
            EventHandler handler = Stoping;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// </summary>
        public event EventHandler Stoping;

        #region Nested type: StartSessionHandler

        private delegate void StartSessionHandler(TClientProtocol clientProtocol, ServerBinding binding);

        #endregion
    }
}