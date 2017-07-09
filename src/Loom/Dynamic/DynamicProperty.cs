#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Dynamic
{
    [DebuggerDisplay("Name={Name}, Type={Type}")]
    public class DynamicProperty<T>
    {
        private readonly PropertyGetter<T, object> getter;
        private readonly PropertySetter<T, object> setter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DynamicProperty{T}" /> class.
        /// </summary>
        /// <param name="setter">
        ///     The <see cref="PropertySetter{TObjectType,TPropertyType}" />.
        /// </param>
        /// <param name="getter">
        ///     The <see cref="PropertyGetter{TObjectType,TPropertyType}" />.
        /// </param>
        /// <param name="name"></param>
        /// <param name="attributeName">
        ///     A <see cref="string" /> representing the name of the property.
        /// </param>
        /// <param name="propertyType">
        ///     The <see cref="Type" /> of the property.
        /// </param>
        public DynamicProperty(PropertySetter<T, object> setter, PropertyGetter<T, object> getter, string name, string attributeName, Type propertyType)
        {
            this.setter = setter;
            this.getter = getter;
            Name = name;
            AttributeName = attributeName;
            Type = propertyType;
        }

        /// <summary>
        ///     Gets or sets an alternate name for the property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This value may be specified by decorating a property with the
        ///         <see cref="DynamicPropertyAttribute" />.
        ///     </para>
        ///     <para>
        ///         When created by the <see cref="DynamicType{TObjectType}" />
        ///         class, this property will represent the value contained in the <see cref="DynamicPropertyAttribute.Name" />
        ///         property of the <see cref="DynamicPropertyAttribute" />.
        ///     </para>
        /// </remarks>
        public string AttributeName { get; set; }

        /// <summary>
        ///     Gets a value indicating if the property has a getter.
        /// </summary>
        /// <remarks>
        ///     If this property is <see langword="false" />, a call to <see cref="InvokeGetterOn" /> will fail.
        /// </remarks>
        public bool HasGetter => getter != null;

        /// <summary>
        ///     Gets a value indicating if the property has a setter.
        /// </summary>
        /// <remarks>
        ///     If this property is <see langword="false" />, a call to <see cref="InvokeSetterOn" /> will fail.
        /// </remarks>
        public bool HasSetter => setter != null;

        /// <summary>
        ///     Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the <see cref="Type" /> of the property.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        ///     Invokes the property setter represented by this instance with the
        ///     specified <paramref name="value" />.
        /// </summary>
        /// <param name="obj">The object on which to invoke the property setter.</param>
        /// <param name="value">The value the property will contain.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="obj" /> is <i>null</i>; <i>nothing</i>
        ///     in Visual Basic.
        /// </exception>
        /// <exception cref="DynamicPropertyException">The property does not have a set accessor.</exception>
        public void InvokeSetterOn(T obj, object value)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            if (setter == null)
                throw new DynamicPropertyException("This property does not have a set accessor.");

            if (Type.IsValueType && (value == null || value == DBNull.Value))
                if (Nullable.GetUnderlyingType(Type) == null)
                    throw new DynamicPropertyException("Cannot set this ValueType property to null. (" + setter.Method.Name + ")");

            setter(obj, value == DBNull.Value ? null : value);
        }
        /*
        static bool IsNullable<T>(T obj)
        {
            if (obj == null) 
                return true; // obvious

            Type type = typeof(T);
            if (!type.IsValueType) 
                return true; // ref-type

            if (Nullable.GetUnderlyingType(type) != null) 
                return true; // Nullable<T>

            return false; // value-type
        }
        */

        /// <summary>
        ///     Invokes the property getter represented by this instance.
        /// </summary>
        /// <param name="obj">The object on which to invoke the property getter.</param>
        /// <returns>
        ///     An <see cref="object" /> representing the value contained in the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="obj" /> is <i>null</i>; <i>nothing</i>
        ///     in Visual Basic.
        /// </exception>
        /// <exception cref="DynamicPropertyException">The property does not have a get accessor.</exception>
        public object InvokeGetterOn(T obj)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            if (getter == null)
                throw new DynamicPropertyException("This property does not have a get accessor.");

            return getter(obj);
        }
    }
}