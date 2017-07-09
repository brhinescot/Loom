#region Using Directives

using System.Reflection;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public sealed class ControllerErrorResult : ResourceViewResult
    {
        public ControllerErrorResult() : base("Loom.Web.Portal.Resources.Templates.Error.aspx")
        {
            Tiles.Add(new TileDefinition("middlePart", "~/moduleresource/Admin/Message.ascx"));
        }

        public ControllerErrorResult([NotNull] string path, [NotNull] Assembly assembly) : base(path, assembly) { }
    }
}