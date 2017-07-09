#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Web.Portal
{
    [DebuggerStepThrough]
    [Serializable]
    public sealed class RouteException : Exception
    {
        [DebuggerStepThrough]
        public RouteException() { }

        [DebuggerStepThrough]
        public RouteException(string message) : base(message) { }

        [DebuggerStepThrough]
        public RouteException(string message, Exception innerException) : base(message, innerException) { }
    }
}