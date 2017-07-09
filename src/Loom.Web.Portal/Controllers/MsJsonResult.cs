namespace Loom.Web.Portal.Controllers
{
    public class MsJsonResult : JsonResult
    {
        public MsJsonResult(object result) : base(result) { }

        protected override void OnExecute(ControllerContext context)
        {
            context.PortalContext.Response.WriteJson(new {d = Result});
        }
    }
}