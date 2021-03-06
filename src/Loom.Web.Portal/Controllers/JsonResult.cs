namespace Loom.Web.Portal.Controllers
{
    public class JsonResult : ActionResult
    {
        public JsonResult(object result)
        {
            Result = result;
        }

        protected object Result { get; set; }

        protected override void OnExecute(ControllerContext context)
        {
            context.PortalContext.Response.WriteJson(Result);
        }
    }
}