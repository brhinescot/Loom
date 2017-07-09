#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Web.Portal
{
    [DebuggerStepThrough]
    [Serializable]
    public sealed class PortalFatalException : Exception
    {
        [DebuggerStepThrough]
        public PortalFatalException() { }

        [DebuggerStepThrough]
        public PortalFatalException(string message) : base(message) { }

        [DebuggerStepThrough]
        public PortalFatalException(string message, Exception innerException) : base(message, innerException) { }
    }
}