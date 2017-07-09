#region Using Directives

using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;

#endregion

namespace Loom.Net.Servers
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("IPAddress={IPAddress}; Port={Port}; UseSsl={UseSsl}; HostName={HostName}")]
    public class ServerBinding
    {
        /// <summary>
        /// </summary>
        public static readonly ServerBinding Default = new ServerBinding(IPAddress.Any, 10000, false, null);

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerBinding" /> class.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="portNumber">The port number.</param>
        /// <param name="useSsl">
        ///     if set to <c>true</c> [use SSL].
        /// </param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="hostName">Name of the host.</param>
        public ServerBinding(IPAddress ipAddress, int portNumber, bool useSsl, X509Certificate certificate, string hostName = null)
        {
            IPAddress = ipAddress;
            Port = portNumber;
            UseSsl = useSsl;
            Certificate = certificate;
            HostName = hostName;
        }

        /// <summary>
        ///     Gets the certificate.
        /// </summary>
        /// <value>The certificate.</value>
        public X509Certificate Certificate { get; }

        /// <summary>
        ///     Gets or sets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName { get; }

        /// <summary>
        ///     Gets the IP.
        /// </summary>
        /// <value>The IP.</value>
        public IPAddress IPAddress { get; }

        /// <summary>
        ///     Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; }

        /// <summary>
        ///     Gets a value indicating whether to use SSL.
        /// </summary>
        /// <value>
        ///     <c>true</c> if use SSL; otherwise, <c>false</c>.
        /// </value>
        public bool UseSsl { get; }
    }
}