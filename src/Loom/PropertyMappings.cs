#region Using Directives

using System.Collections.Generic;
using Loom.Annotations;

#endregion

namespace Loom
{
    /// <summary>
    ///     Represents a class for mapping columns in a data source to corresponding properties in an object.
    /// </summary>
    public sealed class PropertyMappings
    {
        private readonly Dictionary<string, string> lookup = new Dictionary<string, string>();

        /// <summary>
        ///     Gets the number of property mapping pairs contained in this <see cref="PropertyMappings" /> instance.
        /// </summary>
        public int Count => lookup.Count;

        /// <summary>
        ///     Adds a new property mapping.
        /// </summary>
        /// <param name="sourceName">The name of the column in the data source.</param>
        /// <param name="destinationName">The name of the property in the object.</param>
        public void Add([NotNull] string sourceName, [NotNull] string destinationName)
        {
            Argument.Assert.IsNotNullOrEmpty(sourceName, nameof(sourceName));
            Argument.Assert.IsNotNullOrEmpty(destinationName, nameof(destinationName));

            lookup.Add(sourceName, destinationName);
        }

        public void Add([NotNull] PropertyMappings mappings)
        {
            Argument.Assert.IsNotNull(mappings, nameof(mappings));

            foreach (KeyValuePair<string, string> pair in mappings.lookup)
                Add(pair.Key, pair.Value);
        }

        [CanBeNull]
        public string Find(string sourceName)
        {
            Argument.Assert.IsNotNullOrEmpty(sourceName, nameof(sourceName));

            return lookup.ContainsKey(sourceName) ? lookup[sourceName] : null;
        }
    }
}