#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Web.Localization
{
    public class LocalizationException : Exception
    {
        public LocalizationException() { }
        public LocalizationException(string message) : base(message) { }
        public LocalizationException(string message, Exception innerException) : base(message, innerException) { }
        protected LocalizationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}