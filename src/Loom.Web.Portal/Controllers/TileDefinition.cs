#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Web.Portal.Controllers
{
    [DebuggerDisplay("VirtualPath={VirtualPath}, LayoutId={LayoutId}")]
    public class TileDefinition
    {
        public TileDefinition(string layoutId, string virtualPath, string settings = null, string data = null)
        {
            LayoutId = layoutId;
            VirtualPath = virtualPath;
            Settings = settings;
            Data = data;
        }

        public string LayoutId { get; set; }
        public string VirtualPath { get; set; }
        public string Settings { get; set; }
        public string Data { get; set; }
    }
}