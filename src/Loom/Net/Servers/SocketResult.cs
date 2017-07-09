namespace Loom.Net.Servers
{
    /// <summary>
    ///     Asynchronous command execute result.
    /// </summary>
    public enum SocketResult
    {
        /// <summary>
        ///     Operation was successfull.
        /// </summary>
        Ok,

        /// <summary>
        ///     Exceeded maximum allowed size.
        /// </summary>
        LengthExceeded,

        /// <summary>
        ///     Connected client closed connection.
        /// </summary>
        SocketClosed,

        /// <summary>
        ///     Exception happened.
        /// </summary>
        Exception
    }
}