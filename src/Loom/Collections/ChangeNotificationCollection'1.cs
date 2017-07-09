#region Using Directives

using System;
using System.Collections.ObjectModel;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Represents a collection of objects that raises an event when the contents of the collection has changed.
    /// </summary>
    public class ChangeNotificationCollection<T> : Collection<T>
    {
        /// <summary>
        ///     The event that is raised when the contents of the collection have changed.
        /// </summary>
        public event EventHandler<CollectionChangedEventArgs<T>> Changed;

        /// <summary>
        ///     Inserts an element into the <see cref="System.Collections.ObjectModel.Collection{T}"></see>
        ///     at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is less than zero.-or-index is
        ///     greater than <see cref="System.Collections.ObjectModel.Collection{T}.Count"></see>.
        /// </exception>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnChanged(new CollectionChangedEventArgs<T>(CollectionChangeType.Inserted, item));
        }

        /// <summary>
        ///     Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">
        ///     The new value for the element at the specified index. The value can be
        ///     null for reference types.
        /// </param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is less than zero.-or-index is
        ///     greater than <see cref="System.Collections.ObjectModel.Collection{T}.Count"></see>.
        /// </exception>
        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            OnChanged(new CollectionChangedEventArgs<T>(CollectionChangeType.Replaced, item));
        }

        /// <summary>
        ///     Removes the element at the specified index of the <see cref="System.Collections.ObjectModel.Collection{T}"></see>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is less than zero.-or-index is equal
        ///     to or greater than <see cref="System.Collections.ObjectModel.Collection{T}.Count"></see>.
        /// </exception>
        protected override void RemoveItem(int index)
        {
            T item = base[index];
            base.RemoveItem(index);
            OnChanged(new CollectionChangedEventArgs<T>(CollectionChangeType.Removed, item));
        }

        /// <summary>
        ///     Removes all elements from the <see cref="System.Collections.ObjectModel.Collection{T}"></see>.
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            OnChanged(new CollectionChangedEventArgs<T>(CollectionChangeType.Cleared, default(T)));
        }

        /// <summary>
        ///     Raises the <see cref="Changed" /> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="CollectionChangedEventArgs{T}" /> instance containing data
        ///     about the event.
        /// </param>
        protected virtual void OnChanged(CollectionChangedEventArgs<T> e)
        {
            EventHandler<CollectionChangedEventArgs<T>> handler = Changed;
            if (handler != null)
                handler(this, e);
        }
    }
}