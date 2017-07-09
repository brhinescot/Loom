#region Using Directives

using System;

#endregion

namespace Loom.Net.Servers
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs() { }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}