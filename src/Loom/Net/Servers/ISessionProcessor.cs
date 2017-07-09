namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    public interface ISessionProcessor
    {
        /// <summary>
        /// </summary>
        string LineLengthExceededMessage { get; set; }

        /// <summary>
        /// </summary>
        string ServerReadyMessage { get; set; }

        /// <summary>
        /// </summary>
        string TimeoutMessage { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        void Process(IClientRequest request, IServerResponse response);
    }
}