#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Represents a generic collection of key/value pairs that are ordered independently of the key and value.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary</typeparam>
    public interface IOrderedDictionary<TKey, TValue> : IOrderedDictionary, IDictionary<TKey, TValue>
    {
        /// <summary>
        ///     Inserts a new entry into the
        ///     <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> collection with the
        ///     specified key
        ///     and value at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the element should be inserted.</param>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">
        ///     The value of the entry to add. The value can be <null /> if the type of the values in the dictionary is a reference
        ///     type.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than 0.<br />
        ///     -or-<br />
        ///     <paramref name="index" /> is greater than <see cref="System.Collections.ICollection.Count" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     An element with the same key already exists in the
        ///     <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> is read-only.<br />
        ///     -or-<br />
        ///     The <see cref="IOrderedDictionary{TKey,TValue}">IOrderedDictionary&lt;TKey,TValue&gt;</see> has a fixed size.
        /// </exception>
        void Insert(int index, TKey key, TValue value);
    }
}