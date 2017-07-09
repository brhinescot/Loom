#region Using Directives

using System;
using System.IO;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public abstract class FileStreamResult : ActionResult
    {
        protected FileStreamResult([NotNull] string path)
        {
            Path = path;
            PortalTrace.Write("FileStreamResult", "FileStreamResult", "Instantiated with path '{0}'.", path);
        }

        public virtual string Path { get; private set; }

        protected override void OnExecute(ControllerContext context)
        {
            PortalTrace.Write("ResourceResult", "GetStream", "Getting resource stream at '{0}'.", Path);

            string applicationPath = context.PortalContext.HttpContext.Request.ApplicationPath + "/";
            if (Path.StartsWith(applicationPath, StringComparison.OrdinalIgnoreCase))
                Path = Path.Substring(applicationPath.Length);

            context.ResponseStream = File.Open(context.HttpContext.Server.MapPath(Path), FileMode.Open, FileAccess.Read);
        }
    }
}