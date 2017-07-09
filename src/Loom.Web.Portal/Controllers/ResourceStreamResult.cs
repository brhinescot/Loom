#region Using Directives

using System;
using System.Reflection;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Controllers
{
    public class ResourceStreamResult : ActionResult
    {
        public ResourceStreamResult([NotNull] string path) : this(path, Assembly.GetExecutingAssembly()) { }

        public ResourceStreamResult([NotNull] string path, [NotNull] Assembly assembly)
        {
            Argument.Assert.IsNotNullOrEmpty(path, nameof(path));
            Argument.Assert.IsNotNull(assembly, "assembly");

            Path = path;
            Assembly = assembly;

            PortalTrace.Write("ResourceResult", "ResourceResult", "Instantiated with path '{0}' and assembly '{1}'.", path, assembly.ToString());
        }

        public Assembly Assembly { get; }
        public string Path { get; private set; }

        protected override void OnExecute(ControllerContext context)
        {
            PortalTrace.Write("ResourceResult", "GetStream", "Getting resource stream at '{0}'.", Path);

            string applicationPath = context.PortalContext.HttpContext.Request.ApplicationPath + "/";
            if (Path.StartsWith(applicationPath, StringComparison.OrdinalIgnoreCase))
                Path = Path.Substring(applicationPath.Length);

            context.ResponseStream = Assembly.GetManifestResourceStream(Path);
        }
    }
}