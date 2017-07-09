#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionList<T> : IList<T>, IList
    {
        private readonly List<T> items = new List<T>();

        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        public CollectionList(IEnumerable<T> list)
        {
            Argument.Assert.IsNotNull(list, "list");
            items.AddRange(list);
        }

        /// <summary>
        /// </summary>
        public CollectionList() { }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        /// <value>The items.</value>
        protected IList<T> Items => items;

        #region IList Members

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     index is not a valid index in the
        ///     <see cref="System.Collections.IList"></see>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The property is set and the
        ///     <see cref="System.Collections.IList"></see> is read-only.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        object IList.this[int index]
        {
            get => items[index];
            set
            {
                VerifyValueType(value);
                this[index] = (T) value;
            }
        }

        /// <summary>
        ///     Determines whether the <see cref="System.Collections.IList"></see> contains a specific
        ///     value.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="System.Object"></see> is found in the
        ///     <see cref="System.Collections.IList"></see>; otherwise, false.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="System.Object"></see> to locate in the
        ///     <see cref="System.Collections.IList"></see>.
        /// </param>
        /// <filterpriority>2</filterpriority>
        bool IList.Contains(object value)
        {
            if (IsCompatibleObject(value))
                return Contains((T) value);
            return false;
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="System.Collections.IList"></see>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.Object"></see> to remove from the
        ///     <see cref="System.Collections.IList"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The
        ///     <see cref="System.Collections.IList">
        ///     </see>
        ///     is read-only.-or- The <see cref="System.Collections.IList"></see> has a
        ///     fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        void IList.Remove(object value)
        {
            if (((IList) items).IsReadOnly)
                throw new NotSupportedException(SR.ExceptionNotSupportedReadOnlyCollection);

            if (IsCompatibleObject(value))
                Remove((T) value);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"></see> that is the
        ///     destination of the elements copied from <see cref="ICollection"></see>.
        ///     The <see cref="Array"></see> must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in array at which copying begins. </param>
        /// <exception cref="ArgumentNullException">array is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException">index is less than zero. </exception>
        /// <exception cref="ArgumentException">
        ///     array is multidimensional.-or- index is equal to
        ///     or greater than the length of array.-or- The number of elements in the source
        ///     <see cref="ICollection"></see> is greater than the available space from index to the end
        ///     of the destination array.
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
            #region Fail Fast Validation

            Argument.Assert.IsNotNull(array, "array");

            if (array.Rank != 1)
                throw new ArgumentException("Multi-dimensional arrays are not supported.");
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("Array has a non-zero lower bound.");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be a non-negative number.");
            if (array.Length - index < Count)
                throw new ArgumentException(SR.ExceptionArgArrayPlusOffTooSmall);

            #endregion

            T[] localArray = array as T[];
            if (localArray != null)
            {
                items.CopyTo(localArray, index);
            }

            else
            {
                Type arrayElementType = array.GetType().GetElementType();
                Type itemType = typeof(T);
                if (!arrayElementType.IsAssignableFrom(itemType) && !itemType.IsAssignableFrom(arrayElementType))
                    throw new ArgumentException(SR.ExceptionArgInvalidArrayType);

                try
                {
                    for (int num2 = 0; num2 < items.Count; num2++)
                        array.SetValue(items[num2], index++);
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(SR.ExceptionArgInvalidArrayType);
                }
            }
        }

        /// <summary>
        ///     Inserts an item to the <see cref="System.Collections.IList"></see> at the specified index.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="System.Object"></see> to insert into the <see cref="IList"></see>.
        /// </param>
        /// <param name="index">The zero-based index at which value should be inserted. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     index is not a valid index in the
        ///     <see cref="IList"></see>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="System.Collections.IList"></see>
        ///     is read-only.-or- The <see cref="System.Collections.IList"></see> has a fixed size.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        ///     value is null reference in the
        ///     <see cref="System.Collections.IList"></see>.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        void IList.Insert(int index, object value)
        {
            if (((IList) items).IsReadOnly)
                throw new NotSupportedException(SR.ExceptionNotSupportedReadOnlyCollection);

            VerifyValueType(value);
            Insert(index, (T) value);
        }

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="System.Collections.IList"></see>.
        /// </summary>
        /// <returns>
        ///     The index of value if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="System.Object"></see> to locate in the
        ///     <see cref="System.Collections.IList"></see>.
        /// </param>
        /// <filterpriority>2</filterpriority>
        int IList.IndexOf(object value)
        {
            if (IsCompatibleObject(value))
                return IndexOf((T) value);
            return -1;
        }

        /// <summary>
        ///     Adds an item to the <see cref="System.Collections.IList"></see>.
        /// </summary>
        /// <returns>
        ///     The position into which the new element was inserted.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="System.Object"></see> to add to the <see cref="System.Collections.IList"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="System.Collections.IList"></see> is read-only.-or- The <see cref="System.Collections.IList"></see>
        ///     has a fixed size.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        int IList.Add(object value)
        {
            VerifyValueType(value);
            int index = Count + 1;
            Insert(index, (T) value);
            return index;
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="System.Collections.IList"></see> has a fixed size.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="System.Collections.IList"></see> has a fixed size; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsFixedSize => ((IList) items).IsFixedSize;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="IList"></see> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="IList"></see> is read-only; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsReadOnly => ((IList) items).IsReadOnly;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="ICollection"></see>
        ///     is synchronized (thread safe).
        /// </summary>
        /// <returns>
        ///     true if access to the <see cref="ICollection"></see> is synchronized (thread safe);
        ///     otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsSynchronized => false;

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the
        ///     <see cref="ICollection"></see>.
        /// </summary>
        /// <returns>
        ///     An object that can be used to synchronize access to the <see cref="ICollection"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object SyncRoot => ((ICollection) items).SyncRoot;

        #endregion

        #region IList<T> Members

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     index is not a valid index in the
        ///     <see cref="IList{T}">
        ///     </see>
        ///     .
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     The property is set and the <see cref="IList{T}"></see>
        ///     is read-only.
        /// </exception>
        public T this[int index]
        {
            get => items[index];
            set => SetItem(index, value);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEnumerator{T}"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        ///     Adds an item to the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <param name="item">
        ///     The object to add to the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is read-only.
        /// </exception>
        public void Add(T item)
        {
            int index = items.Count;
            InsertItem(index, item);
        }

        /// <summary>
        ///     Removes all items from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is read-only.
        /// </exception>
        public void Clear()
        {
            ClearItems();
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
        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection{T}"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="System.Array"></see> that is the destination
        ///     of the elements copied from <see cref="ICollection{T}"></see>. The <see cref="System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentException">
        ///     array is multidimensional.-or-arrayIndex is equal to
        ///     or greater than the length of array.-or-The number of elements in the source
        ///     <see cref="ICollection{T}"></see> is greater than the available space from arrayIndex to the
        ///     end of the destination array.-or-Type T cannot be cast automatically to the type of the
        ///     destination array.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <returns>
        ///     true if item was successfully removed from the <see cref="ICollection{T}"></see>; otherwise,
        ///     false. This method also returns false if item is not found in the original
        ///     <see cref="ICollection{T}"></see>.
        /// </returns>
        /// <param name="item">
        ///     The object to remove from the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is read-only.
        /// </exception>
        public bool Remove(T item)
        {
            int index = items.IndexOf(item);
            if (index < 0)
                return false;

            RemoveItem(index);
            return true;
        }

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="IList{T}"></see>.
        /// </summary>
        /// <returns>
        ///     The index of item if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="IList{T}"></see>.
        /// </param>
        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        /// <summary>
        ///     Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// </summary>
        /// <param name="item">
        ///     The object to insert into the <see cref="IList{T}"></see>.
        /// </param>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IList{T}"></see> is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     index is not a valid index in the
        ///     <see cref="IList{T}"></see>.
        /// </exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > items.Count)
                throw new ArgumentOutOfRangeException("index");

            InsertItem(index, item);
        }

        /// <summary>
        ///     Removes the <see cref="IList{T}"></see> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IList{T}"></see> is read-only.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     index is not a valid index in the
        ///     <see cref="IList{T}"></see>.
        /// </exception>
        public void RemoveAt(int index)
        {
            RemoveItem(index);
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="ICollection{T}"></see> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="ICollection{T}"></see> is read-only; otherwise, false.
        /// </returns>
        bool ICollection<T>.IsReadOnly => ((IList) items).IsReadOnly;

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="ICollection{T}"></see>.
        /// </returns>
        public int Count => items.Count;

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            items.AddRange(collection);
        }

        /// <summary>
        ///     Clears the items.
        /// </summary>
        protected virtual void ClearItems()
        {
            items.Clear();
        }

        /// <summary>
        ///     Inserts the item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected virtual void InsertItem(int index, T item)
        {
            items.Insert(index, item);
        }

        /// <summary>
        ///     Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected virtual void RemoveItem(int index)
        {
            items.RemoveAt(index);
        }

        /// <summary>
        ///     Sets the item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected virtual void SetItem(int index, T item)
        {
            items[index] = item;
        }

        /// <summary>
        /// </summary>
        protected void Reverse()
        {
            items.Reverse();
        }

        /// <summary>
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        protected void Reverse(int index, int count)
        {
            items.Reverse(index, count);
        }

        /// <summary>
        /// </summary>
        protected void Sort()
        {
            Sort(0, Count, null);
        }

        /// <summary>
        ///     Sorts the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        protected void Sort(IComparer<T> comparer)
        {
            Sort(0, Count, comparer);
        }

        /// <summary>
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        protected void Sort(Comparison<T> comparison)
        {
            items.Sort(comparison);
        }

        /// <summary>
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <param name="comparer">The comparer.</param>
        protected void Sort(int index, int count, IComparer<T> comparer)
        {
            items.Sort(index, count, comparer);
        }

        private static bool IsCompatibleObject(object value)
        {
            if (!(value is T) && (value != null || typeof(T).IsValueType))
                return false;

            return true;
        }

        private static void VerifyValueType(object value)
        {
            if (!IsCompatibleObject(value))
                throw new ArgumentException(SR.ExceptionArgumentWrongType("value", typeof(T)));
        }
    }
}