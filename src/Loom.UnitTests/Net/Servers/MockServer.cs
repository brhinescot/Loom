#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;

#endregion

namespace Loom.Net.Servers
{
    public class MockServer : TcpServer
    {
        internal readonly Collection<TcpClient> MockClients = new Collection<TcpClient>();
        internal bool IsValid = true;

        public MockServer(ISessionProcessor processor, params ServerBinding[] bindings) : base(processor, bindings) { }
        public MockServer(ISessionProcessor processor, IServerProtocolFactory<TcpListener, TcpClient> protocolFactory) : base(processor, protocolFactory) { }
        public MockServer(ISessionProcessor processor, IServerProtocolFactory<TcpListener, TcpClient> protocolFactory, params ServerBinding[] bindings) : base(processor, protocolFactory, bindings) { }

        protected override void StartSession(TcpClient clientProtocol, ServerBinding binding)
        {
            Console.WriteLine("Session Started");
            MockClients.Add(clientProtocol);
            base.StartSession(clientProtocol, binding);
        }

        protected override void EndSession(TcpClient clientProtocol, ServerBinding binding)
        {
            Console.WriteLine("Client Disconnected");
            base.EndSession(clientProtocol, binding);
        }

        protected override void OnStoping(EventArgs e)
        {
            base.OnStoping(e);
            MockClients.Clear();
        }

        protected override bool IsClientValid(TcpClient clientProtocol)
        {
            return IsValid;
        }
    }
}