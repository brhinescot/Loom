#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Data.Diff
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("Name={Name}, Entries=[{Entries.Count}]")]
    public sealed class Diff<T>
    {
        /// <summary>
        ///     Gets a collection of <see cref="DiffEntry" /> objects that represent the changed properties
        ///     in the <see cref="Diff{T}" />.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="DiffEntry" /> objects representing the changed properties.
        /// </value>
        public DiffEntryCollection Entries { get; } = new DiffEntryCollection();

        /// <summary>
        ///     Gets the name of the object that the <see cref="Diff{T}" /> was performed on.
        /// </summary>
        /// <value>The name of the object the <see cref="Diff{T}" /> was performed on.</value>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets the baseline item of the comparison.
        /// </summary>
        public T BaselineItem { get; internal set; }

        /// <summary>
        ///     Gets the working item of the comparison.
        /// </summary>
        public T WorkingItem { get; internal set; }

        internal void AddEntry(string propertyName, string baselineValue, string newValue)
        {
            Entries.Add(new DiffEntry(propertyName, baselineValue, newValue, DiffType.Changed));
        }

        internal void AddEntry(DiffEntry entry)
        {
            Entries.Add(entry);
        }
    }
}