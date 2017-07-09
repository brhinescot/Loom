#region Using Directives

using System;
using System.Collections;
using System.IO;

#endregion

namespace Loom.Web
{
    public interface IBuildManager
    {
        Stream CreateCachedFile(string fileName);

        bool FileExists(string virtualPath);

        Type GetCompiledType(string virtualPath);

        ICollection GetReferencedAssemblies();

        Stream ReadCachedFile(string fileName);
    }
}