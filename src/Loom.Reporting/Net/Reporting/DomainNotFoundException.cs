#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class DomainNotFoundException : Exception
    {
        private string domain;

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public DomainNotFoundException(string message) : base(message) { }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        ///     Initializes with defaults.
        /// </summary>
        public DomainNotFoundException() { }

        /// <summary>
        ///     Creates a new <see cref="DomainNotFoundException" /> instance.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="message">Message.</param>
        public DomainNotFoundException(string domain, string message) : base(message)
        {
            this.domain = domain;
        }

        /// <summary>
        ///     Creates a new <see cref="DomainNotFoundException" /> instance.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public DomainNotFoundException(string domain, string message, Exception innerException)
            : base(message, innerException)
        {
            this.domain = domain;
        }

        /// <summary>
        ///     Initializes with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">
        ///     The contextual information about the source or destination.
        /// </param>
        protected DomainNotFoundException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }

        /// <summary>
        ///     Gets or sets the domain.
        /// </summary>
        /// <value></value>
        public string Domain
        {
            get => domain;
            set => domain = value;
        }
    }
}