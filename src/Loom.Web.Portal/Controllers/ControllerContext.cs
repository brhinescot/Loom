#region Using Directives

using System.IO;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class ControllerContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public ControllerContext(IPortalContext portalContext)
        {
            PortalContext = portalContext;
            HttpContext = PortalContext.HttpContext;
        }

        public IHttpContext HttpContext { get; internal set; }
        public IPortalContext PortalContext { get; internal set; }
        public Stream ResponseStream { get; internal set; }
    }
}