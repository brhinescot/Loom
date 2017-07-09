namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TServerProtocol"></typeparam>
    /// <typeparam name="TClientProtocol"></typeparam>
    public interface IServerProtocolFactory<TServerProtocol, TClientProtocol>
    {
        /// <summary>
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        IServerProtocol<TServerProtocol, TClientProtocol> Create(ServerBinding binding);
    }
}