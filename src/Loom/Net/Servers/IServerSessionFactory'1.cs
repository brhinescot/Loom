namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TClientProtocol"></typeparam>
    public interface IServerSessionFactory<TClientProtocol>
    {
        /// <summary>
        /// </summary>
        /// <param name="clientProtocol"></param>
        /// <param name="serverBinding"></param>
        /// <param name="sessionProcessor"></param>
        /// <returns></returns>
        IServerSession<TClientProtocol> Create(TClientProtocol clientProtocol, ServerBinding serverBinding, ISessionProcessor sessionProcessor);
    }
}