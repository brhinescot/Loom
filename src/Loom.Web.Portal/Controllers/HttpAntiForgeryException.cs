#region Using Directives

using System;
using System.Runtime.Serialization;
using System.Web;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [Serializable]
    public sealed class HttpAntiForgeryException : HttpException
    {
        public HttpAntiForgeryException() : this("A potential request forgery was encountered. Unable to process the request.") { }
        public HttpAntiForgeryException(string message) : base(message) { }
        private HttpAntiForgeryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public HttpAntiForgeryException(string message, Exception innerException) : base(message, innerException) { }
    }
}