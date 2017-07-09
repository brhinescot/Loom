#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    /// </summary>
    public class Tree<T>
    {
        private BranchCollection<T> branches;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tree{T}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parent">The parent.</param>
        internal Tree(T value, Tree<T> parent) : this(value, null, parent) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tree{T}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="branches">The branches.</param>
        /// <param name="parent">The parent.</param>
        public Tree(T value, BranchCollection<T> branches = null, Tree<T> parent = null)
        {
            this.branches = branches;
            Parent = parent;
            Value = value;
        }

        /// <summary>
        /// </summary>
        public BranchCollection<T> Branches => branches ?? (branches = new BranchCollection<T>(this));

        /// <summary>
        /// </summary>
        public Tree<T> Parent { get; internal set; }

        /// <summary>
        /// </summary>
        public T Value { get; }

        public Tree<T> BreathFirstSearch(Predicate<Tree<T>> filter)
        {
            return Search.BreadthFirst(this, filter, t => t.Branches);
        }

        public Tree<T> DepthFirstSearch(Predicate<Tree<T>> filter)
        {
            return Search.DepthFirst(this, filter, t => t.Branches);
        }

        public Tree<T> AddBranch(T branch)
        {
            Tree<T> item = new Tree<T>(branch);
            Branches.Add(item);
            return item;
        }

        public void AddBranch(Tree<T> branch)
        {
            Branches.Add(branch);
        }

        public IEnumerable<Tree<T>> AddBranches(IEnumerable<T> newBranches)
        {
            foreach (T branch in newBranches)
                yield return AddBranch(branch);
        }

        public void AddBranches(IEnumerable<Tree<T>> newBranches)
        {
            Branches.AddRange(newBranches);
        }
    }

    /// <summary>
    /// </summary>
    public class BranchCollection<T> : IList<Tree<T>>
    {
        private readonly List<Tree<T>> innerList = new List<Tree<T>>();
        private readonly Tree<T> owner;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BranchCollection{T}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        internal BranchCollection(Tree<T> owner)
        {
            this.owner = owner;
        }

        #region IList<Tree<T>> Members

        /// <summary>
        ///     Gets or sets the <see cref="Tree{T}" /> at the specified index.
        /// </summary>
        /// <value></value>
        public Tree<T> this[int index]
        {
            get => innerList[index];
            set
            {
                innerList[index] = value;
                value.Parent = owner;
            }
        }

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="IList{T}"></see>.
        /// </summary>
        /// <param name="item">
        ///     The object to locate in the <see cref="IList{T}"></see>.
        /// </param>
        /// <returns>
        ///     The index of item if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(Tree<T> item)
        {
            return innerList.IndexOf(item);
        }

        /// <summary>
        ///     Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">
        ///     The object to insert into the <see cref="IList{T}"></see>.
        /// </param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IList{T}"></see> is read-only.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is not a valid index in the <see cref="IList{T}"></see>.
        /// </exception>
        public void Insert(int index, Tree<T> item)
        {
            innerList.Insert(index, item);
            item.Parent = owner;
        }

        /// <summary>
        ///     Removes the <see cref="IList{T}"></see> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="IList{T}"></see> is read-only.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     index is not a valid index in the <see cref="IList{T}"></see>.
        /// </exception>
        public void RemoveAt(int index)
        {
            Tree<T> item = innerList[index];
            innerList.RemoveAt(index);
            item.Parent = null;
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
        public void Add(Tree<T> item)
        {
            innerList.Add(item);
            item.Parent = owner;
        }

        /// <summary>
        ///     Removes all items from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is read-only.
        /// </exception>
        public void Clear()
        {
            innerList.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="ICollection{T}"></see> contains a specific value.
        /// </summary>
        /// <param name="item">
        ///     The object to locate in the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <returns>
        ///     true if item is found in the <see cref="ICollection{T}"></see>; otherwise, false.
        /// </returns>
        public bool Contains(Tree<T> item)
        {
            return innerList.Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection{T}"></see> to an <see cref="System.Array"></see>, starting at a
        ///     particular
        ///     <see
        ///         cref="System.Array">
        ///     </see>
        ///     index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="System.Array"></see> that is the destination of the elements copied from
        ///     <see
        ///         cref="ICollection{T}">
        ///     </see>
        ///     . The <see cref="System.Array"></see> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentException">
        ///     array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements
        ///     in the source
        ///     <see
        ///         cref="ICollection{T}">
        ///     </see>
        ///     is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast
        ///     automatically to the type of the destination array.
        /// </exception>
        public void CopyTo(Tree<T>[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <param name="item">
        ///     The object to remove from the <see cref="ICollection{T}"></see>.
        /// </param>
        /// <returns>
        ///     true if item was successfully removed from the <see cref="ICollection{T}"></see>; otherwise, false. This method
        ///     also returns false if item is not found in the original
        ///     <see
        ///         cref="ICollection{T}">
        ///     </see>
        ///     .
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     The <see cref="ICollection{T}"></see> is read-only.
        /// </exception>
        public bool Remove(Tree<T> item)
        {
            bool removed = innerList.Remove(item);
            if (removed)
                item.Parent = null;
            return removed;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEnumerator{T}"></see> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Tree<T>> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) innerList).GetEnumerator();
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The number of elements contained in the <see cref="ICollection{T}"></see>.
        /// </returns>
        public int Count => innerList.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="ICollection{T}"></see> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     true if the <see cref="ICollection{T}"></see> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly => ((ICollection<Tree<T>>) innerList).IsReadOnly;

        #endregion

        /// <summary>
        ///     Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Tree<T> Add(T value)
        {
            Tree<T> item = new Tree<T>(value, owner);
            Add(item);
            return item;
        }

        /// <summary>
        ///     Adds the range.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<Tree<T>> collection)
        {
            innerList.AddRange(collection);
        }

        internal int BinarySearch(Tree<T> item)
        {
            return innerList.BinarySearch(item);
        }

        internal int BinarySearch(Tree<T> item, IComparer<Tree<T>> comparer)
        {
            return innerList.BinarySearch(item, comparer);
        }

        internal int BinarySearch(int index, int count, Tree<T> item, IComparer<Tree<T>> comparer)
        {
            return innerList.BinarySearch(index, count, item, comparer);
        }
    }
}