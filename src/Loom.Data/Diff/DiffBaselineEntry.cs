#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Data.Diff
{
    /// <summary>
    /// </summary>
    [DebuggerDisplay("Name={Name}, BaselineValue={BaselineValue}")]
    public class DiffBaselineEntry
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiffBaselineEntry" /> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="baselineValue">The baseline value.</param>
        internal DiffBaselineEntry(string name, string baselineValue)
        {
            Name = name;
            BaselineValue = baselineValue;
        }

        /// <summary>
        ///     Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the baseline value.
        /// </summary>
        /// <value>The baseline value.</value>
        public string BaselineValue { get; set; }
    }
}