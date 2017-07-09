#region Using Directives

using System.Collections.Generic;
using System.IO;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class FileViewResult : FileStreamResult, IViewResult
    {
        private string viewPath;

        public FileViewResult(string name) : base(name)
        {
            Tiles = new List<TileDefinition>();
        }

        public override string Path
        {
            get
            {
//                if (viewPath != null)
//                    return viewPath;

                IPortalRequest request = PortalContext.Current.Request;
                foreach (string path in ViewPaths)
                {
                    string tempPath = path + (base.Path ?? request.ActionName) + ".aspx";
                    if (File.Exists(PortalContext.Current.HttpContext.Server.MapPath(tempPath)))
                    {
                        viewPath = tempPath;
                        break;
                    }
                }

                if (Compare.IsNullOrEmpty(viewPath))
                    throw new FileNotFoundException("No view found. Looked in the following locations: " + string.Join(", ", ViewPaths));

                return viewPath;
            }
        }

        public IList<TileDefinition> Tiles { get; }

        #region IViewResult Members

        public string DependencyPath => Path;

        #endregion
    }
}