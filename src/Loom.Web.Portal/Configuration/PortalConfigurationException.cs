#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Web.Portal.Configuration
{
    [DebuggerStepThrough]
    [Serializable]
    public sealed class PortalConfigurationException : Exception
    {
        public PortalConfigurationException() { }
        public PortalConfigurationException(string message) : base(message) { }
        public PortalConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}