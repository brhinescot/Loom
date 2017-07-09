#region Using Directives

using System;
using System.IO;
using System.Runtime.Serialization;

#endregion

namespace Loom.Threading
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class LockTimeoutException : Exception
    {
        /// <summary>
        ///     Creates a new <see cref="InvalidDataException" /> instance.
        /// </summary>
        public LockTimeoutException() : base(SR.ExceptionTimeoutWaitingForLock) { }

        /// <summary>
        ///     Creates a new <see cref="InvalidDataException" /> instance.
        /// </summary>
        /// <param name="message">Message.</param>
        public LockTimeoutException(string message) : base(message) { }

        /// <summary>
        ///     Creates a new <see cref="InvalidDataException" /> instance.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        protected LockTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Creates a new <see cref="InvalidDataException" /> instance.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public LockTimeoutException(string message, Exception innerException) : base(message, innerException) { }
    }
}