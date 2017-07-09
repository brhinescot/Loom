#region Using Directives

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     A class representing the exception that is thrown when a possible hijacked session is detected.
    /// </summary>
    [Serializable]
    public class InvalidSessionException : HttpException
    {
        private const string IpAddressKey = "IPAddress";
        private string ipAddress;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidSessionException" /> class.
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
        public InvalidSessionException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidSessionException" /> class.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
        public InvalidSessionException(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidSessionException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that
        ///     holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext"></see> that
        ///     holds the contextual information about the source or destination.
        /// </param>
        [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
        protected InvalidSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ipAddress = info.GetString(IpAddressKey);
        }

        /// <summary>
        ///     Gets or sets the IP address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress
        {
            get => ipAddress;
            set => ipAddress = value;
        }

        /// <summary>
        ///     Gets information about the exception and adds it to the
        ///     <see cref="System.Runtime.Serialization.SerializationInfo"></see> object.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="System.Runtime.Serialization.SerializationInfo"></see> that
        ///     holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="System.Runtime.Serialization.StreamingContext"></see> that
        ///     holds the contextual information about the source or destination.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(IpAddressKey, ipAddress);
            base.GetObjectData(info, context);
        }
    }
}