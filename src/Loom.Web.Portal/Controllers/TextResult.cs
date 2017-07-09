#region Using Directives

using System.Collections.Generic;
using System.Text;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class TextResult : ActionResult
    {
        public TextResult(object result)
        {
            IEnumerable<string> list = result as IEnumerable<string>;
            if (list != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in list)
                    sb.AppendLine(s);
                Result = sb.ToString();
            }
            else
            {
                Result = result;
            }
        }

        protected object Result { get; set; }

        protected override void OnExecute(ControllerContext context)
        {
            IHttpResponse response = context.PortalContext.HttpContext.Response;

            response.Clear();
            response.Headers["Content-Type"] = "text/html; charset=UTF-8";
            response.Write(Result);
            response.Complete();
        }
    }
}