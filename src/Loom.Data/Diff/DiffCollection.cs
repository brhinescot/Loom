#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Loom.Data.Diff
{
    public class DiffCollection<T> : Collection<Diff<T>>
    {
        public IEnumerable<DiffEntry> Entries
        {
            get
            {
                for (int i = 0; i < Count; i++)
                for (int j = 0; j < this[i].Entries.Count; j++)
                    yield return this[i].Entries[j];
            }
        }
    }
}