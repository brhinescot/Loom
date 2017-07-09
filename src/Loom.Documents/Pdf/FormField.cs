#region Using Directives

using System;

#endregion

namespace Loom.Documents.Pdf
{
    public sealed class FormField
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FormField" /> class.
        /// </summary>
        /// <param name="value">The name of the column in the data source.</param>
        /// <param name="name">The name of the property on the entity.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value" /> or
        ///     <paramref name="name" /> is null or empty.
        /// </exception>
        public FormField(string name, string value)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));

            Value = value;
            Name = name;
        }

        /// <summary>
        ///     Gets the name of the column in the data source.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Gets the name of the property on the entity.
        /// </summary>
        public string Name { get; }
    }
}