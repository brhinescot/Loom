#region File Header

// *************************************************************************
// Colossus Framework 
// 
// Copyright © 2008 Colossus Interactive, LLC
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// licensing@colossusinteractive.com
//  
// *************************************************************************

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using Colossus.Framework.Dynamic;

#endregion

namespace Colossus.Framework.Data.Entities
{
    /// <summary>
    /// Represents a class used to dynamically convert one object to another.
    /// </summary>
    /// <remarks>
    /// By default, the conversion uses properties with matching names in each object. This behavior may be overridden
    /// by calling the constructor override and passing an <see cref="PropertyMappingCollection"/> or the 
    /// <see cref="AddMapping"/> method.
    /// </remarks>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type to be created.</typeparam>
    public class EntityConverter<TSource, TDestination> : IEntityConverter<TSource,TDestination> where TDestination : new()
    {
        #region Member Fields

        private readonly Dictionary<string, DynamicProperty<TDestination>> destinationProperties = new Dictionary<string, DynamicProperty<TDestination>>();
        private PropertyMappingCollection mappingCollection;
        private readonly DynamicProperty<TSource>[] sourceProperties = DynamicType<TSource>.CreateDynamicProperties();
        private readonly Action<TSource, TDestination> mappingAction;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConverter{TSource,TDestination}"/> class.
        /// </summary>
        public EntityConverter()
        {
            foreach (DynamicProperty<TDestination> toProperty in DynamicType<TDestination>.CreateDynamicProperties())
                destinationProperties.Add(toProperty.Name, toProperty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConverter{TSource,TDestination}"/> class.
        /// </summary>
        /// <param name="mappingCollection">An <see cref="PropertyMappingCollection"/> containing source and destination 
        /// property mappings.</param>
        public EntityConverter(PropertyMappingCollection mappingCollection) : this()
        {
            this.mappingCollection = mappingCollection;
        }

        public EntityConverter(Action<TSource, TDestination> mappingAction) : this()
        {
            this.mappingAction = mappingAction;
        }

        #endregion
        
        public PropertyMapping AddMapping(string sourceName, string destinationName)
        {
            if (mappingCollection == null)
                mappingCollection = new PropertyMappingCollection();

            PropertyMapping mapping = new PropertyMapping(sourceName, destinationName);
            mappingCollection.Add(mapping);
            return mapping;
        }

        public IList<TDestination> ConvertFrom(IEnumerable<TSource> source)
        {
            Argument.Assert.IsNotNull(source, Argument.Names.source);

            List<TDestination> destinationList = new List<TDestination>();

            foreach (TSource sourceItem in source)
                destinationList.Add(ConvertFrom(sourceItem));

            return destinationList;
        }

        public TDestination ConvertFrom(TSource source) 
        {
            Argument.Assert.IsNotNull(source, Argument.Names.source);

            TDestination destination = new TDestination();

            for(int i = 0; i < sourceProperties.Length; i++)
            {
                DynamicProperty<TSource> sourceProperty = sourceProperties[i];
                if(!sourceProperty.HasGetter)
                    continue;

                string destinationPropertyName = mappingCollection != null
                                                     ? (mappingCollection.FindPropertyMapping(sourceProperty.Name) ?? sourceProperty.Name)
                                                     : sourceProperty.Name;

                if(!destinationProperties.ContainsKey(destinationPropertyName))
                    continue;

                DynamicProperty<TDestination> destinationProperty = destinationProperties[destinationPropertyName];

                if(!destinationProperty.HasSetter)
                    continue;

                destinationProperty.InvokeSetter(destination, sourceProperty.InvokeGetter(source));
            }

            if (mappingAction != null)
                mappingAction(source, destination);

            return destination;
        }
    }
}