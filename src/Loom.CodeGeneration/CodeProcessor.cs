#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.CodeGeneration
{
    public abstract class CodeProcessor : IFileProcessor
    {
        protected CodeProcessor(string inputFileName)
        {
            InputFileName = inputFileName;
        }

        public string InputFileName { get; }

        #region IFileProcessor Members

        public abstract IEnumerable<GeneratedObject> GenerateObjects();

        #endregion
    }
}