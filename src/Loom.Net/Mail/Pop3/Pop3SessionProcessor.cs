#region Using Directives

using System;
using System.Text;
using Loom.Net.Servers;

#endregion

namespace Loom.Net.Mail.Pop3
{
    /// <summary>
    ///     Represents a class for processing Pop3 sessions and the message exchange between the server and client.
    /// </summary>
    public class Pop3SessionProcessor : ISessionProcessor, IDisposable
    {
        private readonly EventHandler<UserAuthenticatedEventArgs> onUserAuthenticated;

        private readonly EventHandler<UserAuthenticatingEventArgs> onUserAuthenticating;
        private readonly Pop3CommandProcessor pop3CommandProcessor;

        /// <summary>
        ///     Creates a new instance of the <see cref="Pop3SessionProcessor" /> class.
        /// </summary>
        public Pop3SessionProcessor()
        {
            onUserAuthenticating = HandleUserAuthenticating;
            onUserAuthenticated = HandleUserAuthenticated;

            pop3CommandProcessor = new Pop3CommandProcessor(this);
            pop3CommandProcessor.UserAuthenticating += onUserAuthenticating;
            pop3CommandProcessor.UserAuthenticated += onUserAuthenticated;
        }

        internal bool IsLoggedIn { get; set; }

        internal Pop3SessionState State { get; set; }

        internal bool IsAuthenticated { get; private set; }

        internal string UserName { get; private set; }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing,
        ///     or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (pop3CommandProcessor == null)
                return;

            pop3CommandProcessor.UserAuthenticating -= onUserAuthenticating;
            pop3CommandProcessor.UserAuthenticated -= onUserAuthenticated;
        }

        #endregion

        #region ISessionProcessor Members

        public string ServerReadyMessage { get; set; }

        public string LineLengthExceededMessage { get; set; }

        public string TimeoutMessage { get; set; }

        /// <summary>
        ///     Processes a client request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void Process(IClientRequest request, IServerResponse response)
        {
            Argument.Assert.IsNotNull(request, "request");
            Argument.Assert.IsNotNull(response, "response");

            string[] commandArgs = Encoding.Default.GetString(request.Read()).Split(' ');
            ProcessCommand(commandArgs, response);
        }

        #endregion

        private void HandleUserAuthenticating(object sender, UserAuthenticatingEventArgs e)
        {
            IsAuthenticated = true;
            UserName = e.UserName;
        }

        private void HandleUserAuthenticated(object sender, UserAuthenticatedEventArgs e) { }

        private void ProcessCommand(string[] command, IServerResponse response)
        {
            response.Write(pop3CommandProcessor.Process(command[0], command.Segment(1)));
        }
    }
}