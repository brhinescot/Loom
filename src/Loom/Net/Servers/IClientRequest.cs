#region Using Directives

using System.Net;

#endregion

namespace Loom.Net.Servers
{
    public interface IClientRequest
    {
        IPAddress IPAddress { get; }

        int Port { get; }

        byte[] Read();
    }
}