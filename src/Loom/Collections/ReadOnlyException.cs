#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Collections
{
    public class ReadOnlyException : Exception
    {
        public ReadOnlyException() { }
        public ReadOnlyException(string message) : base(message) { }
        public ReadOnlyException(string message, Exception innerException) : base(message, innerException) { }
        protected ReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}