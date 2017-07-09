#region Using Directives

using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    public interface IWriteEnumerable<T> : IEnumerable<T>
    {
        bool Add(T item);
    }
}