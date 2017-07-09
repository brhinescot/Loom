#region Using Directives

using System;

#endregion

namespace Loom.Collections
{
    /// <summary>
    /// </summary>
    public sealed class CollectionChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// </summary>
        /// <param name="changeType"></param>
        /// <param name="item"></param>
        public CollectionChangedEventArgs(CollectionChangeType changeType, T item)
        {
            ChangeType = changeType;
            Item = item;
        }

        /// <summary>
        ///     Gets or sets the type of the change.
        /// </summary>
        /// <value>The type of the change.</value>
        public CollectionChangeType ChangeType { get; }

        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public T Item { get; }
    }
}