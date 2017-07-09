namespace Loom.Web.Portal.Controllers
{
    public class PartialResult : ActionResult
    {
        public PartialResult(string result)
        {
            Result = result;
        }

        public string Result { get; set; }

        protected override void OnExecute(ControllerContext context)
        {
            IHttpResponse response = context.PortalContext.HttpContext.Response;
            response.Clear();
            response.Write(Result);
            response.Complete();
        }
    }

    public class ViewResult : ActionResult
    {
        protected override void OnExecute(ControllerContext context) { }
    }
}