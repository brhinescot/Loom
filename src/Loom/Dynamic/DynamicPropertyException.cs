#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Dynamic
{
    public class DynamicPropertyException : Exception
    {
        public DynamicPropertyException() { }
        public DynamicPropertyException(string message) : base(message) { }
        public DynamicPropertyException(string message, Exception innerException) : base(message, innerException) { }
        protected DynamicPropertyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}