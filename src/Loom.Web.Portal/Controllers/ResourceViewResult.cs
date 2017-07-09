#region Using Directives

using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class ResourceViewResult : ResourceStreamResult, IViewResult
    {
        public ResourceViewResult(string path) : base(path)
        {
            Tiles = new List<TileDefinition>();
        }

        public ResourceViewResult(string path, Assembly assembly) : base(path, assembly)
        {
            Tiles = new List<TileDefinition>();
        }

        public IList<TileDefinition> Tiles { get; }

        #region IViewResult Members

        public string DependencyPath => "~/bin/Loom.Web.Portal.dll";

        #endregion
    }
}