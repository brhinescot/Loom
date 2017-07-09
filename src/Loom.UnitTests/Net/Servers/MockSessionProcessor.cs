#region Using Directives

using System;
using System.Diagnostics;
using System.Text;

#endregion

namespace Loom.Net.Servers
{
    public class MockSessionProcessor : ISessionProcessor
    {
        public int MessageCount { get; private set; }

        #region ISessionProcessor Members

        /// <summary>
        /// </summary>
        public string ServerReadyMessage { get; set; } = "+OK Welcome.";

        /// <summary>
        /// </summary>
        public string LineLengthExceededMessage { get; set; } = "-ERR The line length is exceeded!";

        /// <summary>
        /// </summary>
        public string TimeoutMessage { get; set; } = "-ERR The connection has timed out!";

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void Process(IClientRequest request, IServerResponse response)
        {
            string[] clientMessage = Encoding.Default.GetString(request.Read()).TrimEnd('\0').Split(' ');

            if (clientMessage.Length == 1 && clientMessage[0] != "BYE")
            {
                response.Write("-Error Unknown Command");
                return;
            }

            Debug.WriteLine("Message received by mock server: " + clientMessage[0] + " " + (clientMessage.Length == 2 ? clientMessage[1] : string.Empty));

            switch (clientMessage[0])
            {
                case "CONN":
                    response.Write(string.Format("+OK Hello {0}", clientMessage[1]));
                    break;
                case "MESG":
                    MessageCount++;
                    response.Write("+OK Msg Received");
                    OnMessageReceived(new MessageReceivedEventArgs(clientMessage[1]));
                    break;
                case "BYE":
                    response.Write("+OK GoodBye");
                    response.EndConnection = true;
                    break;
            }
        }

        #endregion

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            EventHandler<MessageReceivedEventArgs> handler = MessageReceived;
            if (handler != null)
                handler(this, e);
        }
    }
}