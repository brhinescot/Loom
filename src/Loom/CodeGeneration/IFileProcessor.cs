#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.CodeGeneration
{
    public interface IFileProcessor
    {
        IEnumerable<GeneratedObject> GenerateObjects();
    }
}