#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Flags]
    public enum CodeFormatOptions
    {
        None = 0,
        RemoveFKPrefix = 1,
        RemoveTblPrefix = 2,
        AddIdSuffixForKeys = 4
    }
}