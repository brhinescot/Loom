#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Data.Diff
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("Name={Name}, BaselineValue={BaselineValue}, NewValue={NewValue}")]
    public sealed class DiffEntry : DiffBaselineEntry
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffEntry" /> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="baselineValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        internal DiffEntry(string name, string baselineValue, string newValue) : base(name, baselineValue)
        {
            NewValue = newValue;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffEntry" /> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="baselineValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="diffType"></param>
        internal DiffEntry(string name, string baselineValue, string newValue, DiffType diffType) : base(name, baselineValue)
        {
            NewValue = newValue;
            DiffType = diffType;
        }

        /// <summary>
        ///     Gets or sets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public string NewValue { get; set; }

        public DiffType DiffType { get; set; }
    }
}