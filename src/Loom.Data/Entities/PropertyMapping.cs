using System;

namespace Colossus.Framework.Data.Entities
{
    /// <summary>
    /// Represents a class used to map columns in a data source to the properties on an entity.
    /// </summary>
    public sealed class PropertyMapping
    {
        #region Instance Fields

        private readonly string sourceName;
        private readonly string destinationName;

        #endregion

        #region Property Accessors

        /// <summary>
        /// Gets the name of the column in the datasource.
        /// </summary>
        public string SourceName
        {
            get { return sourceName; }
        }

        /// <summary>
        /// Gets the name of the property on the entity.
        /// </summary>
        public string DestinationName
        {
            get { return destinationName; }
        }

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMapping"/> class.
        /// </summary>
        /// <param name="sourceName">The name of the column in the data source.</param>
        /// <param name="destinationName">The name of the property on the entity.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sourceName"/> or 
        /// <paramref name="destinationName"/> is null or empty.</exception>
        public PropertyMapping(string sourceName, string destinationName)
        {
            Argument.Assert.IsNotNullOrEmpty(sourceName, Argument.Names.sourceName);
            Argument.Assert.IsNotNullOrEmpty(destinationName, Argument.Names.destinationName);

            this.sourceName = sourceName;
            this.destinationName = destinationName;
        }

        #endregion
    }
}