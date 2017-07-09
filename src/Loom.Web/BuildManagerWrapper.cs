#region Using Directives

using System;
using System.Collections;
using System.IO;
using System.Web.Compilation;

#endregion

namespace Loom.Web
{
    public sealed class BuildManagerWrapper : IBuildManager
    {
        #region IBuildManager Members

        bool IBuildManager.FileExists(string virtualPath)
        {
            return BuildManager.GetObjectFactory(virtualPath, false) != null;
        }

        Type IBuildManager.GetCompiledType(string virtualPath)
        {
            return BuildManager.GetCompiledType(virtualPath);
        }

        ICollection IBuildManager.GetReferencedAssemblies()
        {
            return BuildManager.GetReferencedAssemblies();
        }

        Stream IBuildManager.ReadCachedFile(string fileName)
        {
            return BuildManager.ReadCachedFile(fileName);
        }

        Stream IBuildManager.CreateCachedFile(string fileName)
        {
            return BuildManager.CreateCachedFile(fileName);
        }

        #endregion
    }
}