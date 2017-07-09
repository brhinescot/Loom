#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Loom.Dynamic;

#endregion

namespace Loom
{
    /// <summary>
    ///     Represents a class used to dynamically convert one object to another.
    /// </summary>
    /// <remarks>
    ///     By default, the conversion uses properties with matching names in each object. This behavior may be overridden
    ///     by calling the constructor override and passing an <see cref="PropertyMappings" /> or the
    ///     <see cref="AddMapping" /> method.
    /// </remarks>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type to be created.</typeparam>
    public class EntityConverter<TSource, TDestination> : IEntityConverter<TSource, TDestination> where TDestination : new()
    {
        private readonly Dictionary<string, DynamicProperty<TDestination>> destinationProperties = new Dictionary<string, DynamicProperty<TDestination>>();
        private readonly Action<TSource, TDestination> mappingAction;
        private readonly DynamicProperty<TSource>[] sourceProperties = DynamicType<TSource>.CreateDynamicProperties();
        private PropertyMappings mappings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityConverter{TSource,TDestination}" /> class.
        /// </summary>
        public EntityConverter()
        {
            foreach (DynamicProperty<TDestination> toProperty in DynamicType<TDestination>.CreateDynamicProperties())
                destinationProperties.Add(toProperty.Name, toProperty);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityConverter{TSource,TDestination}" /> class.
        /// </summary>
        /// <param name="mappings">
        ///     An <see cref="PropertyMappings" /> containing source and destination
        ///     property mappings.
        /// </param>
        public EntityConverter(PropertyMappings mappings) : this()
        {
            this.mappings = mappings;
        }

        public EntityConverter(Action<TSource, TDestination> mappingAction) : this()
        {
            this.mappingAction = mappingAction;
        }

        #region IEntityConverter<TSource,TDestination> Members

        public IList<TDestination> Convert(IEnumerable<TSource> source)
        {
            Argument.Assert.IsNotNull(source, nameof(source));

            return source.Select(Convert).ToList();
        }

        public TDestination Convert(TSource source)
        {
            TDestination destination = new TDestination();
            Merge(source, destination);
            return destination;
        }

        #endregion

        public void AddMapping(string sourceName, string destinationName)
        {
            if (mappings == null)
                mappings = new PropertyMappings();

            mappings.Add(sourceName, destinationName);
        }

        public void Merge(TSource source, TDestination destination, bool allowNullOverride = false)
        {
            Argument.Assert.IsNotNull(source, nameof(source));
            Argument.Assert.IsNotNull(destination, nameof(destination));

            for (int i = 0; i < sourceProperties.Length; i++)
            {
                DynamicProperty<TSource> sourceProperty = sourceProperties[i];
                if (!sourceProperty.HasGetter)
                    continue;

                string destinationPropertyName = mappings != null
                    ? (mappings.Find(sourceProperty.Name) ?? sourceProperty.Name)
                    : sourceProperty.Name;

                if (!destinationProperties.ContainsKey(destinationPropertyName))
                    continue;

                DynamicProperty<TDestination> destinationProperty = destinationProperties[destinationPropertyName];

                if (!destinationProperty.HasSetter)
                    continue;

                object valueToSet = sourceProperty.InvokeGetterOn(source);
                if (!allowNullOverride && valueToSet == null)
                    continue;

                destinationProperty.InvokeSetterOn(destination, valueToSet);
            }

            mappingAction?.Invoke(source, destination);
        }
    }
}