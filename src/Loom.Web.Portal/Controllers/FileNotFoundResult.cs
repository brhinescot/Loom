#region Using Directives

using System.IO;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal sealed class FileNotFoundResult : ActionResult
    {
        protected override void OnExecute(ControllerContext context)
        {
            PortalTrace.Write("FileNotFoundResult", "GetStream", "Throwing FileNotFoundException.");
            throw new FileNotFoundException();
        }
    }
}