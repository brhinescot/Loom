#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class ControllerResultException : Exception
    {
        public ControllerResultException() { }
        public ControllerResultException(string message) : base(message) { }
        public ControllerResultException(string message, Exception innerException) : base(message, innerException) { }
        protected ControllerResultException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ActionResult GetResult()
        {
            ControllerErrorResult result = new ControllerErrorResult();
#if DEBUG
            ExceptionFormatter formatter = new ExceptionFormatter("Colossus Portal", Message);
            result.ViewData.Message = formatter.Generate(this, "<br/>");
#endif
#if !DEBUG
            result.ViewData.Message = "An error occurred.";
#endif
            return result;
        }
    }
}