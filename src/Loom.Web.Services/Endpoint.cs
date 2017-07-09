namespace Loom.Web.Services
{
    /// <summary>
    ///     Used to identity whether message
    ///     is being processed on server
    ///     or on client (proxy)
    /// </summary>
    internal enum Endpoint
    {
        /// <summary>
        ///     Specifies endpoint as being web service
        ///     side of connection
        /// </summary>
        Server,

        /// <summary>
        ///     Specifies endpoint as being client
        ///     side of connection
        /// </summary>
        Client
    }
}