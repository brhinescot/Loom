#region Using Directives

using System.Web.Compilation;
using Loom.Web.IO;

#endregion

namespace Loom.Web.Localization
{
    /// <summary>
    ///     Provider factory that instantiates the individual provider. The provider
    ///     passes a 'classname' which is the ResourceSet id or how a resource is identified.
    ///     For global resources it's the name of hte resource file, for local resources
    ///     it's the full Web relative virtual path
    /// </summary>
    public sealed class DbSimpleResourceProviderFactory : ResourceProviderFactory
    {
        /// <summary>
        ///     ASP.NET sets up provides the global resource name which is the
        ///     resource ResX file (without any extensions). This will become
        ///     our ResourceSet id. ie. Resource.resx becomes "Resources"
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public override System.Web.Compilation.IResourceProvider CreateGlobalResourceProvider(string classname)
        {
            return new DbSimpleResourceProvider(classname);
        }

        /// <summary>
        ///     ASP.NET passes the full page virtual path (/MyApp/subdir/test.aspx) wich is
        ///     the effective ResourceSet id. We'll store only an application relative path
        ///     (subdir/test.aspx) by stripping off the base path.
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override System.Web.Compilation.IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            // ASP.NET passes full virtual path: Strip out the virtual path 
            // leaving us just with app relative page/control path
            string resourceSetName = WebPath.GetAppRelativePath(virtualPath);

            return new DbSimpleResourceProvider(resourceSetName.ToLower());
        }
    }
}