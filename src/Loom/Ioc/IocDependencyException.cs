#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Ioc
{
    public class IocDependencyException : Exception
    {
        public IocDependencyException() { }
        public IocDependencyException(string message) : base(message) { }
        public IocDependencyException(string message, Exception innerException) : base(message, innerException) { }
        protected IocDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}