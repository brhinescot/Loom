#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace Colossus.Framework.Data.Entities
{
    /// <summary>
    /// Represents a class for mapping columns in a data source to corresponding properties in an object.
    /// </summary>
    public sealed class PropertyMappingCollection : Collection<PropertyMapping>
    {
        /// <summary>
        /// Adds an <see cref="PropertyMapping"/> object to the end of the <see cref="PropertyMappingCollection"/>. 
        /// </summary>
        /// <param name="sourceName">The name of the column in the data source.</param>
        /// <param name="destinationName">The name of the property in the object.</param>
        public void Add(string sourceName, string destinationName)
        {
            Add(new PropertyMapping(sourceName, destinationName));
        }

        public void AddRange(IList<PropertyMapping> mappings)
        {
            for (int i = 0; i < mappings.Count; i++)
                Add(mappings[i]);
        }

        internal string FindPropertyMapping(string sourceName)
        {
            for (int i = 0; i < Count; i++)
            {
                PropertyMapping mapping = this[i];
                if (mapping.SourceName == sourceName)
                    return mapping.DestinationName;
            }
            return null;
        }
    }
}