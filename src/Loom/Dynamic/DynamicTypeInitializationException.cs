#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Dynamic
{
    public class DynamicTypeInitializationException : Exception
    {
        public DynamicTypeInitializationException() { }
        public DynamicTypeInitializationException(string message) : base(message) { }
        public DynamicTypeInitializationException(string message, Exception innerException) : base(message, innerException) { }
        protected DynamicTypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}