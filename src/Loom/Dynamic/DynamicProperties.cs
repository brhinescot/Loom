#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Dynamic
{
    public sealed class DynamicProperties<T> : IEnumerable<DynamicProperty<T>>
    {
        private readonly Dictionary<string, DynamicProperty<T>> properties = new Dictionary<string, DynamicProperty<T>>();

        public DynamicProperties() : this(DynamicType<T>.CreateDynamicProperties(true)) { }

        private DynamicProperties(IList<DynamicProperty<T>> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                DynamicProperty<T> property = properties[i];
                this.properties.Add(property.AttributeName, property);
            }
        }

        public DynamicProperty<T> this[string name]
        {
            get
            {
                DynamicProperty<T> property;
                return properties.TryGetValue(name, out property) ? property : null;
            }
            set
            {
                if (properties.ContainsKey(name))
                    properties[name] = value;
                else
                    properties.Add(name, value);
            }
        }

        public object InvokeGetter(T obj, string propertyName)
        {
            DynamicProperty<T> property = properties.TryGetValue(propertyName, out property) ? property : null;

            if (property == null)
                throw new ArgumentException("A property named " + propertyName + " does not exist.", "propertyName");

            return property.InvokeGetterOn(obj);
        }

        public void InvokeSetter(T obj, string propertyName, object value)
        {
            DynamicProperty<T> property = properties.TryGetValue(propertyName, out property) ? property : null;

            if (property == null)
                throw new ArgumentException("A property named " + propertyName + " does not exist.", "propertyName");

            property.InvokeSetterOn(obj, value);
        }

        #region IEnumerable<DynamicProperty<T>> Members

        public IEnumerator<DynamicProperty<T>> GetEnumerator()
        {
            return properties.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}