namespace Loom.Web.Services
{
    /// <summary>
    ///     Used by ChainStream functions to keep
    ///     track of which stream is being processed
    /// </summary>
    internal enum SerializationPass
    {
        /// <summary>
        ///     Specifies that first phase of
        ///     processing of stream has begun
        /// </summary>
        Serialize,

        /// <summary>
        ///     Specifies that second phase of
        ///     processing of stream has begun
        /// </summary>
        Deserialize,

        /// <summary>
        ///     specifies that processing of stream
        ///     has not yet started
        /// </summary>
        None
    }
}