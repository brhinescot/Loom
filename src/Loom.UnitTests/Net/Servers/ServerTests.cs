#region Using Directives

using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Loom.Net.Servers
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void MultipleClientConnections()
        {
            MockServer mockServer = new MockServer(new MockSessionProcessor(), new ServerBinding(IPAddress.Loopback, 200, false, null));

            try
            {
                mockServer.Start();

                const int protocolCount = 20;
                List<TcpClient> clients = new List<TcpClient>();

                // Create and connect all clients and read welcome message
                for (int i = 1; i <= protocolCount; i++)
                {
                    TcpClient tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 60000 + i));
                    tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 200));
                    clients.Add(tcpClient);

                    byte[] response = new byte[20];
                    tcpClient.Client.Receive(response);
                    string welcomeMessage = Encoding.Default.GetString(response).TrimEnd('\0');

                    Assert.AreEqual("+OK Welcome.", welcomeMessage);
                }

                // Send goodbye message from all clients to server and read response.
                for (int i = 0; i <= protocolCount - 1; i++)
                {
                    clients[i].Client.Send(Encoding.Default.GetBytes("BYE"));
                    byte[] response = new byte[20];
                    clients[i].Client.Receive(response);
                    string message3 = Encoding.Default.GetString(response).TrimEnd('\0');

                    Assert.AreEqual("+OK GoodBye", message3);
                }

                // Close all client connections;
                for (int i = 0; i <= protocolCount - 1; i++)
                    clients[i].Close();
                Thread.Sleep(5000);
                Assert.AreEqual(protocolCount, mockServer.MockClients.Count);
            }
            finally
            {
                mockServer.Stop();
            }
        }

        [Test]
        public void SendMessage()
        {
            TcpClient tcpClient = null;
            MockServer mockServer = null;
            string message1;
            string message2;
            string message3;
            string welcomeMessage;
            try
            {
                mockServer = new MockServer(new MockSessionProcessor(), new ServerBinding(IPAddress.Loopback, 200, false, null));
                mockServer.Start();

                tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 60050));
                tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 200));

                byte[] response = new byte[20];
                tcpClient.Client.Receive(response);
                welcomeMessage = Encoding.Default.GetString(response).TrimEnd('\0');

                tcpClient.Client.Send(Encoding.Default.GetBytes("CONN Brian"));
                response = new byte[20];
                tcpClient.Client.Receive(response);
                message1 = Encoding.Default.GetString(response).TrimEnd('\0');

                tcpClient.Client.Send(Encoding.Default.GetBytes("MESG Zach"));
                response = new byte[20];
                tcpClient.Client.Receive(response);
                message2 = Encoding.Default.GetString(response).TrimEnd('\0');

                tcpClient.Client.Send(Encoding.Default.GetBytes("BYE"));
                response = new byte[20];
                tcpClient.Client.Receive(response);
                message3 = Encoding.Default.GetString(response).TrimEnd('\0');
            }
            finally
            {
                Thread.Sleep(3000);
                if (tcpClient != null && tcpClient.Connected)
                    tcpClient.Close();
                if (mockServer != null)
                    mockServer.Stop();
            }

            Assert.AreEqual("+OK Welcome.", welcomeMessage);
            Assert.AreEqual("+OK Hello Brian", message1);
            Assert.AreEqual("+OK Msg Received", message2);
            Assert.AreEqual("+OK GoodBye", message3);
        }

        [Test]
        public void InvalidClients()
        {
            MockServer mockServer = new MockServer(new MockSessionProcessor(), new ServerBinding(IPAddress.Loopback, 200, false, null));
            mockServer.Start();

            try
            {
                TcpClient tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 61000));
                tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 200));
                tcpClient.Client.Close();

                Thread.Sleep(100);
                mockServer.IsValid = false;

                TcpClient tcpClient2 = new TcpClient(new IPEndPoint(IPAddress.Loopback, 61001));
                tcpClient2.Connect(new IPEndPoint(IPAddress.Loopback, 200));
                tcpClient2.Client.Close();

                Thread.Sleep(2000);
                Assert.AreEqual(1, mockServer.MockClients.Count);
            }
            finally
            {
                mockServer.Stop();
            }
        }

        [Test]
        public void RestartServer()
        {
            MockServer mockServer = new MockServer(new MockSessionProcessor(), new ServerBinding(IPAddress.Loopback, 200, false, null));
            mockServer.Start();

            try
            {
                const int protocolCount = 10;
                for (int i = 1; i <= protocolCount; i++)
                {
                    TcpClient tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 62000 + i));
                    tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 200));

                    tcpClient.Client.Close();
                }
                Thread.Sleep(1000);
                Assert.AreEqual(protocolCount, mockServer.MockClients.Count);

                mockServer.Restart();

                for (int i = 1; i <= protocolCount; i++)
                {
                    TcpClient tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 62000 + protocolCount + i));
                    tcpClient.Connect(new IPEndPoint(IPAddress.Loopback, 200));

                    tcpClient.Client.Close();
                }
                Thread.Sleep(1000);
                Assert.AreEqual(protocolCount, mockServer.MockClients.Count);
            }
            finally
            {
                mockServer.Stop();
            }
        }

        [Test]
        [ExpectedException(typeof(SocketException))]
        public void ConnectAfterStop()
        {
            MockServer localServer = new MockServer(new MockSessionProcessor(), new ServerBinding(IPAddress.Loopback, 201, false, null));
            localServer.Start();

            TcpClient tcpClient2 = new TcpClient(new IPEndPoint(IPAddress.Loopback, 50000));
            tcpClient2.Connect(new IPEndPoint(IPAddress.Loopback, 201));
            tcpClient2.Client.Close();

            Thread.Sleep(100);
            localServer.Stop();
            Thread.Sleep(100);

            // Should throw a socket exception because of timeout since server is stopped.
            tcpClient2 = new TcpClient(new IPEndPoint(IPAddress.Loopback, 50001));
            tcpClient2.Connect(new IPEndPoint(IPAddress.Loopback, 201));
            tcpClient2.Client.Close();
        }
    }
}