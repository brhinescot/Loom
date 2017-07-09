#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Loom.Dynamic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Represents a dictionary that auto generates a key based on the hash values of
    ///     properties specified by supplied property names.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the values in the <see cref="AutoKeyDictionary{T}" />
    /// </typeparam>
    [Serializable]
    public class AutoKeyDictionary<T> : IDictionary<int, T>, IDictionary, ISerializable
    {
        private readonly Dictionary<int, T> internalDictionary;
        private PropertyGetter<T, int>[] getters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoKeyDictionary{T}" />.
        /// </summary>
        /// <param name="keyProperties">The property names used to generate a key.</param>
        public AutoKeyDictionary(params string[] keyProperties)
        {
            InitializeGetters(keyProperties);
            internalDictionary = new Dictionary<int, T>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoKeyDictionary{T}" />.
        /// </summary>
        /// <param name="capacity">
        ///     The initial number of elements that the
        ///     <see cref="AutoKeyDictionary{T}" /> can contain.
        /// </param>
        /// <param name="keyProperties">The property names used to generate a key.</param>
        public AutoKeyDictionary(int capacity, params string[] keyProperties)
        {
            InitializeGetters(keyProperties);
            internalDictionary = new Dictionary<int, T>(capacity);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoKeyDictionary{T}" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="ICollection{T}" /> whose elements are copied
        ///     to the new <see cref="AutoKeyDictionary{T}" />.
        /// </param>
        /// <param name="keyProperties">The property names used to generate a key.</param>
        public AutoKeyDictionary(ICollection<T> collection, params string[] keyProperties)
        {
            InitializeGetters(keyProperties);

            if (collection == null)
                return;

            internalDictionary = new Dictionary<int, T>(collection.Count);
            foreach (T value in collection)
                Add(value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoKeyDictionary{T}" />.
        /// </summary>
        /// <param name="dictionary">
        ///     The <see cref="AutoKeyDictionary{T}" /> whose elements are copied
        ///     to the new <see cref="AutoKeyDictionary{T}" />.
        /// </param>
        /// <param name="keyProperties">The property names used to generate a key.</param>
        public AutoKeyDictionary(AutoKeyDictionary<T> dictionary, params string[] keyProperties)
        {
            InitializeGetters(keyProperties);

            if (dictionary == null)
                return;

            internalDictionary = new Dictionary<int, T>(dictionary.Count);
            foreach (KeyValuePair<int, T> pair in dictionary)
                internalDictionary.Add(pair.Key, pair.Value);
        }

        /// <summary>
        ///     Gets the <see cref="IEqualityComparer{T}"></see> that is used to determine equality of keys
        ///     for the dictionary.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEqualityComparer{T}"></see> generic interface implementation that is used to
        ///     determine equality of keys for the current <see cref="Dictionary{TKey,TValue}"></see> and
        ///     to provide hash values for the keys.
        /// </returns>
        public IEqualityComparer<int> Comparer => internalDictionary.Comparer;

        #region IDictionary Members

        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        ///     The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set. </param>
        /// <exception cref="NotSupportedException">
        ///     The property is set and the
        ///     <see cref="IDictionary">
        ///     </see>
        ///     object is read-only.-or- The property is set, key does not exist in the collection,
        ///     and the <see cref="IDictionary"></see> has a fixed size.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null. </exception>
        /// <filterpriority>2</filterpriority>
        object IDictionary.this[object key]
        {
            get => ((IDictionary) internalDictionary)[key];
            set => ((IDictionary) internalDictionary)[key] = value;
        }

        /// <summary>
        ///     Determines whether the <see cref="IDictionary"></see> object contains an
        ///     element with the specified key.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="IDictionary"></see> contains an element with the
        ///     key; otherwise, false.
        /// </returns>
        /// <param name="key">
        ///     The key to locate in the <see cref="IDictionary"></see>
        ///     object.
        /// </param>
        /// <exception cref="ArgumentNullException">key is null. </exception>
        /// <filterpriority>2</filterpriority>
        bool IDictionary.Contains(object key)
        {
            return ((IDictionary) internalDictionary).Contains(key);
        }

        /// <summary>
        ///     Adds an element with the provided key and value to the
        ///     <see cref="IDictionary">
        ///     </see>
        ///     object.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Object"></see> to use as the value of the
        ///     element to add.
        /// </param>
        /// <param name="key">
        ///     The <see cref="Object"></see> to use as the key of the element
        ///     to add.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists
        ///     in the <see cref="IDictionary"></see> object.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null. </exception>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IDictionary"></see> is
        ///     read-only.-or- The <see cref="IDictionary"></see> has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        void IDictionary.Add(object key, object value)
        {
            ((IDictionary) internalDictionary).Add(key, value);
        }

        /// <summary>
        ///     Returns an <see cref="IDictionaryEnumerator"></see> object for the <see cref="IDictionary"></see> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="IDictionaryEnumerator"></see> object for the <see cref="IDictionary"></see> object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary) internalDictionary).GetEnumerator();
        }

        /// <summary>
        ///     Removes the element with the specified key from the <see cref="IDictionary"></see> object.
        /// </summary>
        /// <param name="key">The key of the element to remove. </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IDictionary"></see> object is
        ///     read-only.-or- The <see cref="IDictionary"></see> has a fixed size.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null. </exception>
        /// <filterpriority>2</filterpriority>
        void IDictionary.Remove(object key)
        {
            ((IDictionary) internalDictionary).Remove(key);
        }

        /// <summary>
        ///     Gets an <see cref="ICollection"></see> object containing the keys of the
        ///     <see cref="IDictionary"></see> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="ICollection"></see> object containing the keys of the
        ///     <see cref="IDictionary"></see> object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        ICollection IDictionary.Keys => ((IDictionary) internalDictionary).Keys;

        /// <summary>
        ///     Gets an <see cref="ICollection"></see> object containing the values in the
        ///     <see cref="IDictionary"></see> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="ICollection"></see> object containing the values in the
        ///     <see cref="IDictionary"></see> object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        ICollection IDictionary.Values => ((IDictionary) internalDictionary).Values;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="IDictionary"></see> object has a fixed size.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="IDictionary"></see> object has a fixed size; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        bool IDictionary.IsFixedSize => ((IDictionary) internalDictionary).IsFixedSize;

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"></see> that is the destination
        ///     of the elements copied from <see cref="ICollection"></see>. The <see cref="Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in array at which copying begins. </param>
        /// <exception cref="ArgumentNullException">array is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException">index is less than zero. </exception>
        /// <exception cref="ArgumentException">
        ///     array is multidimensional.-or- index is equal to or
        ///     greater than the length of array.-or- The number of elements in the source
        ///     <see cref="ICollection"></see> is greater than the available space from index to
        ///     the end of the destination array.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     The type of the source
        ///     <see cref="ICollection">
        ///     </see>
        ///     cannot be cast automatically to the type of the destination array.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) internalDictionary).CopyTo(array, index);
        }

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="ICollection"></see>.
        /// </summary>
        /// <returns>
        ///     An object that can be used to synchronize access to the <see cref="ICollection"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        object ICollection.SyncRoot => ((ICollection) internalDictionary).SyncRoot;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="ICollection"></see> is
        ///     synchronized (thread safe).
        /// </summary>
        /// <returns>
        ///     true if access to the <see cref="ICollection"></see> is synchronized (thread safe);
        ///     otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        bool ICollection.IsSynchronized => ((ICollection) internalDictionary).IsSynchronized;

        #endregion

        #region IDictionary<int,T> Members

        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        ///     The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.</param>
        /// <exception cref="NotSupportedException">
        ///     The property is set and the
        ///     <see cref="IDictionary{TKey,TValue}"></see> is read-only.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        /// <exception cref="KeyNotFoundException">
        ///     The property is retrieved and
        ///     key is not found.
        /// </exception>
        public T this[int key]
        {
            get => internalDictionary[key];
            set => internalDictionary[key] = value;
        }

        /// <summary>
        ///     Determines whether the <see cref="IDictionary{TKey,TValue}"></see> contains an element with the specified key.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="IDictionary{TKey,TValue}"></see> contains an element with the key; otherwise, false.
        /// </returns>
        /// <param name="key">
        ///     The key to locate in the <see cref="IDictionary{TKey,TValue}"></see>.
        /// </param>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public bool ContainsKey(int key)
        {
            return internalDictionary.ContainsKey(key);
        }

        /// <summary>
        ///     Adds an element with the provided key and value to the <see cref="IDictionary{TKey,TValue}"></see>.
        /// </summary>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IDictionary{TKey,TValue}"></see> is read-only.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the <see cref="IDictionary{TKey,TValue}"></see>.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public void Add(int key, T value)
        {
            internalDictionary.Add(key, value);
        }

        /// <summary>
        ///     Removes the element with the specified key from the <see cref="IDictionary{TKey,TValue}"></see>.
        /// </summary>
        /// <returns>
        ///     true if the element is successfully removed; otherwise, false.  This method also returns false if key was not found
        ///     in the original
        ///     <see
        ///         cref="IDictionary{TKey,TValue}">
        ///     </see>
        ///     .
        /// </returns>
        /// <param name="key">The key of the element to remove.</param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IDictionary{TKey,TValue}"></see> is read-only.
        /// </exception>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public bool Remove(int key)
        {
            return internalDictionary.Remove(key);
        }

        public bool TryGetValue(int key, out T value)
        {
            return internalDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Gets an <see cref="ICollection{T}"></see> containing the keys of the
        ///     <see cref="IDictionary{TKey,TValue}"></see>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ICollection{T}"></see> containing the keys of the object that
        ///     implements <see cref="IDictionary{TKey,TValue}"></see>.
        /// </returns>
        public ICollection<int> Keys => internalDictionary.Keys;

        /// <summary>
        ///     Gets an <see cref="ICollection{T}"></see> containing the values in the
        ///     <see cref="IDictionary{TKey,TValue}"></see>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ICollection{T}"></see> containing the values in the object that
        ///     implements <see cref="IDictionary{TKey,TValue}"></see>.
        /// </returns>
        public ICollection<T> Values => internalDictionary.Values;

        /// <summary>
        ///     Adds an item to the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <param name="item">
        ///     The object to add to the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see>
        ///     is read-only.
        /// </exception>
        void ICollection<KeyValuePair<int, T>>.Add(KeyValuePair<int, T> item)
        {
            internalDictionary.Add(item.Key, item.Value);
        }

        /// <summary>
        ///     Removes all items from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is
        ///     read-only.
        /// </exception>
        public void Clear()
        {
            internalDictionary.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="ICollection{T}"></see> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if item is found in the <see cref="ICollection{T}"></see>; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="ICollection{T}"></see>.
        /// </param>
        bool ICollection<KeyValuePair<int, T>>.Contains(KeyValuePair<int, T> item)
        {
            return ((ICollection<KeyValuePair<int, T>>) internalDictionary).Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection{T}"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"></see> that is the destination of
        ///     the elements copied from <see cref="ICollection{T}"></see>. The <see cref="Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentException">
        ///     array is multidimensional.-or-arrayIndex is equal
        ///     to or greater than the length of array.-or-The number of elements in the source
        ///     <see cref="ICollection{T}"></see> is greater than the available space from arrayIndex to the
        ///     end of the destination array.-or-Type T cannot be cast automatically to the type of the
        ///     destination array.
        /// </exception>
        void ICollection<KeyValuePair<int, T>>.CopyTo(KeyValuePair<int, T>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<int, T>>) internalDictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <returns>
        ///     true if item was successfully removed from the <see cref="ICollection{T}"></see>;
        ///     otherwise, false. This method also returns false if item is not found in the original
        ///     <see cref="ICollection{T}"></see>.
        /// </returns>
        /// <param name="item">
        ///     The object to remove from the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see>
        ///     is read-only.
        /// </exception>
        bool ICollection<KeyValuePair<int, T>>.Remove(KeyValuePair<int, T> item)
        {
            return ((ICollection<KeyValuePair<int, T>>) internalDictionary).Remove(item);
        }

        /// <summary>
        ///     The number of elements contained in this collection.
        /// </summary>
        public int Count => internalDictionary.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="ICollection{T}"></see> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="ICollection{T}"></see> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly => ((ICollection<KeyValuePair<int, T>>) internalDictionary).IsReadOnly;

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEnumerator{T}"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<KeyValuePair<int, T>> IEnumerable<KeyValuePair<int, T>>.GetEnumerator()
        {
            return internalDictionary.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalDictionary.GetEnumerator();
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        ///     Implements the <see cref="ISerializable"></see> interface and
        ///     returns the data needed to serialize the <see cref="Dictionary{TKey,TValue}"></see> instance.
        /// </summary>
        /// <param name="context">
        ///     A <see cref="StreamingContext"></see>
        ///     structure that contains the source and destination of the serialized stream associated with
        ///     the <see cref="Dictionary{TKey,TValue}"></see> instance.
        /// </param>
        /// <param name="info">
        ///     A <see cref="SerializationInfo"></see>
        ///     object that contains the information required to serialize the
        ///     <see cref="Dictionary{TKey,TValue}">
        ///     </see>
        ///     instance.
        /// </param>
        /// <exception cref="ArgumentNullException">info is null.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            internalDictionary.GetObjectData(info, context);
        }

        #endregion

        /// <summary>
        ///     Adds the specified <paramref name="value" /> to the dictionary.
        /// </summary>
        /// <param name="value">The value to add. The value can not be null.</param>
        /// <returns>
        ///     The <see cref="int" /> dictionary key generated from the key properties of the
        ///     specified <paramref name="value" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The key generated from the supplied
        ///     <paramref name="value" /> already exists in the <see cref="AutoKeyDictionary{T}" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="value" /> is null.
        /// </exception>
        public int Add(T value)
        {
            Argument.Assert.IsNotNull(value, nameof(value));

            int key = 0;
            unchecked
            {
                for (int i = 0; i < getters.Length; i++)
                    key = (key * 397) ^ getters[i](value);
            }
            if (internalDictionary.ContainsKey(key))
                throw new ArgumentException("The key generated from the supplied value already exists in the dictionary.");

            internalDictionary.Add(key, value);
            return key;
        }

        /// <summary>
        ///     Determines whether the <see cref="AutoKeyDictionary{T}"></see> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="AutoKeyDictionary{T}"></see> contains an element with the specified
        ///     value; otherwise, false.
        /// </returns>
        /// <param name="value">
        ///     The value to locate in the <see cref="AutoKeyDictionary{T}"></see>.
        ///     The value can be null for reference types.
        /// </param>
        public bool ContainsValue(T value)
        {
            return internalDictionary.ContainsValue(value);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="AutoKeyDictionary{T}" />.
        /// </summary>
        /// <returns>
        ///     A Enumerator structure for the <see cref="AutoKeyDictionary{T}" />.
        /// </returns>
        public Dictionary<int, T>.Enumerator GetEnumerator()
        {
            return internalDictionary.GetEnumerator();
        }

        private void InitializeGetters(params string[] keyProperties)
        {
            getters = new PropertyGetter<T, int>[keyProperties.Length];
            for (int i = 0; i < keyProperties.Length; i++)
                getters[i] = DynamicType<T>.CreatePropertyGetterGetHashCode(keyProperties[i]);
        }
    }
}