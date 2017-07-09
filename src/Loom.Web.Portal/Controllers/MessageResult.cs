#region Using Directives

using System.Globalization;
using System.Web.Script.Serialization;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class MessageResult : ActionResult
    {
        public MessageResult(MessageResultType type, string title, string message)
        {
            Type = type;
            Title = title;
            Message = message;
        }

        public string Message { get; }
        public string Title { get; }
        public MessageResultType Type { get; }

        protected override void OnExecute(ControllerContext context)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IHttpResponse response = context.PortalContext.HttpContext.Response;

            response.Clear();
            response.ContentType = "application/json; charset=utf-8";
            response.Write(serializer.Serialize(new {d = new {messageBox = new {type = Type.ToString().ToLower(CultureInfo.InvariantCulture), title = Title, message = Message}}}));
            response.Complete();
        }
    }
}