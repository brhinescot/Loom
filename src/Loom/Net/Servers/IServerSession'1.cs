#region Using Directives

using System;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TClientProtocol"></typeparam>
    public interface IServerSession<TClientProtocol> : IDisposable
    {
        /// <summary>
        ///     Gets the client protocol.
        /// </summary>
        /// <value>The client protocol.</value>
        TClientProtocol ClientProtocol { get; }

        /// <summary>
        ///     Gets the session id.
        /// </summary>
        /// <value>The session id.</value>
        string SessionId { get; }

        /// <summary>
        ///     Gets the start time.
        /// </summary>
        /// <value>The start time.</value>
        DateTime StartTime { get; }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        ///     Stops the specified timeout.
        /// </summary>
        /// <param name="timeout">
        ///     if set to <c>true</c> [timeout].
        /// </param>
        void Stop(bool timeout);
    }
}