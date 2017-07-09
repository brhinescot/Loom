#region Using Directives

using System;
using System.IO;
using System.Web.Hosting;
using Loom.Web.Portal.Controllers;

#endregion

namespace Loom.Web.Portal.IO
{
    internal sealed class RouteFile : VirtualFile
    {
        public RouteFile(string virtualPath, ActionResult result) : base(virtualPath)
        {
            Result = result;
        }

        public ActionResult Result { get; set; }

        public override Stream Open()
        {
            PortalTrace.Write("RouteFile", "Open", "Getting the stream for the current view.");

            ControllerContext context = new ControllerContext(PortalContext.Current);
            Result.Execute(context);

            if (context.ResponseStream == null)
                throw new InvalidOperationException("The ResponseStream property of the RouteFile has not been initialized.");

            return context.ResponseStream;
        }
    }
}