#region Using Directives

using System;

#endregion

namespace Loom.Data
{
    [Flags]
    public enum ColumnProperties
    {
        None = 0,
        PrimaryKey = 1,
        Unique = 2,
        ForeignKey = 4,
        Nullable = 8,
        Computed = 16,
        Identity = 32
    }
}