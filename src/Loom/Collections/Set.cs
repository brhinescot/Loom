#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Represents a class that supports the creation of new <see cref="Set{T}" /> types.
    /// </summary>
    /// <remarks>
    ///     <p>
    ///         This object overrides the <c>Equals()</c> object method, but not the <c>GetHashCode()</c>, because
    ///         the <c>Set</c> class is mutable.  Therefore, it is not safe to use as a key value in a hash table.
    ///     </p>
    ///     <p>
    ///         To make a <c>Set</c> typed based on your own <c>IDictionary</c>, simply derive a
    ///         new class with a constructor that takes no parameters. Some <c>Set</c> implementations
    ///         cannot be defined with a default constructor.  If this is the case for your class,
    ///         you will need to override <c>Clone()</c> as well.
    ///     </p>
    ///     <p>
    ///         It is also standard practice that at least one of your constructors takes an <c>ICollection</c> or
    ///         an <c>ISet</c> as an argument.
    ///     </p>
    /// </remarks>
    public class Set<T> : ISet<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Set{T}" /> class.
        /// </summary>
        public Set()
        {
            InternalDictionary = new Dictionary<T, object>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Set{T}" /> class.
        /// </summary>
        public Set(IEnumerable<T> initialValues) : this()
        {
            AddAllPrivate(initialValues);
        }

        /// <summary>
        ///     Provides the storage for elements in the <c>Set</c>, stored as the key-set
        ///     of the <c>IDictionary</c> object.  Set this object in the constructor
        ///     if you create your own <c>Set</c> class.
        /// </summary>
        protected IDictionary<T, object> InternalDictionary { get; }

        #region ISet<T> Members

        /// <summary>
        ///     Performs a "union" of the two sets, where all the elements
        ///     in both sets are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        ///     Neither this set nor the input set are modified during the operation.  The return value
        ///     is a <c>Clone()</c> of this set with the extra elements added in.
        /// </summary>
        /// <param name="a">A collection of elements.</param>
        /// <returns>
        ///     A new <c>Set</c> containing the union of this <c>Set</c> with the specified collection.
        ///     Neither of the input objects is modified by the union.
        /// </returns>
        public virtual ISet<T> Union(ISet<T> a)
        {
            ISet<T> resultSet = Clone();
            if (a != null)
                resultSet.AddAll(a);
            return resultSet;
        }

        /// <summary>
        ///     Performs an "intersection" of the two sets, where only the elements
        ///     that are present in both sets remain.  That is, the element is included if it exists in
        ///     both sets.  The <c>Intersect()</c> operation does not modify the input sets.  It returns
        ///     a <c>Clone()</c> of this set with the appropriate elements removed.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>
        ///     The intersection of this set with <c>a</c>.
        /// </returns>
        public virtual ISet<T> Intersect(ISet<T> a)
        {
            ISet<T> resultSet = Clone();
            if (a != null)
                resultSet.RetainAll(a);
            else
                resultSet.Clear();
            return resultSet;
        }

        /// <summary>
        ///     Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        ///     the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        ///     The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        ///     of this <c>Set</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>
        ///     A set containing the elements from this set with the elements in <c>a</c> removed.
        /// </returns>
        public virtual ISet<T> Minus(ISet<T> a)
        {
            ISet<T> resultSet = Clone();
            if (a != null)
                resultSet.RemoveAll(a);
            return resultSet;
        }

        /// <summary>
        ///     Performs an "exclusive-or" of the two sets, keeping only the elements that
        ///     are in one of the sets, but not in both.  The original sets are not modified
        ///     during this operation.  The result set is a <c>Clone()</c> of this set containing
        ///     the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>
        ///     A set containing the result of <c>a ^ b</c>.
        /// </returns>
        public virtual ISet<T> ExclusiveOr(ISet<T> a)
        {
            ISet<T> resultSet = Clone();
            foreach (T element in a)
                if (resultSet.Contains(element))
                    resultSet.Remove(element);
                else
                    resultSet.Add(element);
            return resultSet;
        }

        /// <summary>
        ///     Returns a clone of the <c>Set</c> instance.  This will work for derived <c>Set</c>
        ///     classes if the derived class implements a constructor that takes no arguments.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public virtual ISet<T> Clone()
        {
            Set<T> newSet = (Set<T>) Activator.CreateInstance(GetType());
            newSet.AddAll(this);
            return newSet;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds the specified element to this set if it is not already present.
        /// </summary>
        /// <param name="item">The object to add to the set.</param>
        /// <returns>
        ///     <c>true</c> is the object was added, <c>false</c> if it was already present.
        /// </returns>
        public void Add(T item)
        {
            if (InternalDictionary.ContainsKey(item))
                throw new ArgumentException("An element with the same key already exists in the Set<T> object.", "item");

            //The object we are adding is just a placeholder.  The thing we are
            //really concerned with is 'o', the key.
            InternalDictionary.Add(item, null);
        }

        /// <summary>
        ///     Adds all the elements in the specified collection to the set if they are not already present.
        /// </summary>
        /// <param name="items">A collection of objects to add to the set.</param>
        /// <returns>
        ///     <c>true</c> is the set changed as a result of this operation, <c>false</c> if not.
        /// </returns>
        public bool AddAll(IEnumerable<T> items)
        {
            return AddAllPrivate(items);
        }

        /// <summary>
        ///     Removes all objects from the set.
        /// </summary>
        public void Clear()
        {
            InternalDictionary.Clear();
        }

        /// <summary>
        ///     Returns <c>true</c> if this set contains the specified element.
        /// </summary>
        /// <param name="item">The element to look for.</param>
        /// <returns>
        ///     <c>true</c> if this set contains the specified element, <c>false</c> otherwise.
        /// </returns>
        public bool Contains(T item)
        {
            return InternalDictionary.ContainsKey(item);
        }

        /// <summary>
        ///     Returns <c>true</c> if the set contains all the elements in the specified collection.
        /// </summary>
        /// <param name="items">A collection of objects.</param>
        /// <returns>
        ///     <c>true</c> if the set contains all the elements in the specified collection, <c>false</c> otherwise.
        /// </returns>
        public bool ContainsAll(ICollection<T> items)
        {
            foreach (T o in items)
                if (!Contains(o))
                    return false;
            return true;
        }

        /// <summary>
        ///     Removes the specified element from the set.
        /// </summary>
        /// <param name="item">The element to be removed.</param>
        /// <returns>
        ///     <c>true</c> if the set contained the specified element, <c>false</c> otherwise.
        /// </returns>
        public bool Remove(T item)
        {
            bool contained = Contains(item);
            if (contained)
                InternalDictionary.Remove(item);

            return contained;
        }

        /// <summary>
        ///     Remove all the specified elements from this set, if they exist in this set.
        /// </summary>
        /// <param name="items">A collection of elements to remove.</param>
        /// <returns>
        ///     <c>true</c> if the set was modified as a result of this operation.
        /// </returns>
        public bool RemoveAll(ICollection<T> items)
        {
            bool changed = false;
            foreach (T o in items)
                changed |= Remove(o);
            return changed;
        }

        /// <summary>
        ///     Retains only the elements in this set that are contained in the specified collection.
        /// </summary>
        /// <param name="items">Collection that defines the set of elements to be retained.</param>
        /// <returns>
        ///     <c>true</c> if this set changed as a result of this operation.
        /// </returns>
        public bool RetainAll(ICollection<T> items)
        {
            //Put data from C into a set so we can use the Contains() method.
            Set<T> cSet = new Set<T>();

            //We are going to build a set of elements to remove.
            Set<T> removeSet = new Set<T>();

            foreach (T o in this)
                //If C does not contain O, then we need to remove O from our
                //set.  We can't do this while iterating through our set, so
                //we put it into RemoveSet for later.
                if (!cSet.Contains(o))
                    removeSet.Add(o);

            return RemoveAll(removeSet);
        }

        /// <summary>
        ///     Copies the elements in the <c>Set</c> to an array.  The type of array needs
        ///     to be compatible with the objects in the <c>Set</c>, obviously.
        /// </summary>
        /// <param name="array">An array that will be the target of the copy operation.</param>
        /// <param name="arrayIndex">The zero-based index where copying will start.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            InternalDictionary.Keys.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Gets an enumerator for the elements in the <c>Set</c>.
        /// </summary>
        /// <returns>
        ///     An <c>IEnumerator</c> over the elements in the <c>Set</c>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InternalDictionary.Keys.GetEnumerator();
        }

        /// <summary>
        ///     Returns <c>true</c> if this set contains no elements.
        /// </summary>
        public bool IsEmpty => InternalDictionary.Count == 0;

        /// <summary>
        ///     The number of elements contained in this collection.
        /// </summary>
        public int Count => InternalDictionary.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly => InternalDictionary.IsReadOnly;

        #endregion

        /// <summary>
        ///     Performs a "union" of two sets, where all the elements
        ///     in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        ///     The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <c>null</c>) with elements of the
        ///     other set
        ///     added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing the union of the input sets.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static ISet<T> Union(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b.Clone();
            return b == null ? a.Clone() : a.Union(b);
        }

        /// <summary>
        ///     Performs an "intersection" of the two sets, where only the elements
        ///     that are present in both sets remain.  That is, the element is included only if it exists in
        ///     both <c>a</c> and <c>b</c>.  Neither input object is modified by the operation.
        ///     The result object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <c>null</c>) containing
        ///     the
        ///     elements from the intersect operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     The intersection of the two input sets.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static ISet<T> Intersect(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
                return null;

            return a == null ? b.Intersect(a) : a.Intersect(b);
        }

        /// <summary>
        ///     Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        ///     the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        ///     The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        ///     of set <c>a</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing <c>A - B</c> elements.  <c>null</c> if <c>a</c> is <c>null</c>.
        /// </returns>
        public static ISet<T> Minus(ISet<T> a, ISet<T> b)
        {
            return a?.Minus(b);
        }

        /// <summary>
        ///     Performs an "exclusive-or" of the two sets, keeping only the elements that
        ///     are in one of the sets, but not in both.  The original sets are not modified
        ///     during this operation.  The result set is a <c>Clone()</c> of one of the sets
        ///     (<c>a</c> if it is not <c>null</c>) containing
        ///     the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing the result of <c>a ^ b</c>.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static ISet<T> ExclusiveOr(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b.Clone();
            return b == null ? a.Clone() : a.ExclusiveOr(b);
        }

        /// <summary>
        ///     Performs a "union" of two sets, where all the elements
        ///     in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        ///     The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <c>null</c>) with elements of the
        ///     other set
        ///     added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing the union of the input sets.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static Set<T> operator |(Set<T> a, Set<T> b)
        {
            return (Set<T>) Union(a, b);
        }

        /// <summary>
        ///     Performs an "intersection" of the two sets, where only the elements
        ///     that are present in both sets remain.  That is, the element is included only if it exists in
        ///     both <c>a</c> and <c>b</c>.  Neither input object is modified by the operation.
        ///     The result object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <c>null</c>) containing
        ///     the
        ///     elements from the intersect operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     The intersection of the two input sets.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static Set<T> operator &(Set<T> a, Set<T> b)
        {
            return (Set<T>) Intersect(a, b);
        }

        /// <summary>
        ///     Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        ///     the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        ///     The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        ///     of set <c>a</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing <c>A - B</c> elements.  <c>null</c> if <c>a</c> is <c>null</c>.
        /// </returns>
        public static Set<T> operator -(Set<T> a, Set<T> b)
        {
            return (Set<T>) Minus(a, b);
        }

        /// <summary>
        ///     Performs an "exclusive-or" of the two sets, keeping only the elements that
        ///     are in one of the sets, but not in both.  The original sets are not modified
        ///     during this operation.  The result set is a <c>Clone()</c> of one of the sets
        ///     (<c>a</c> if it is not <c>null</c>) containing
        ///     the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>
        ///     A set containing the result of <c>a ^ b</c>.  <c>null</c> if both sets are <c>null</c>.
        /// </returns>
        public static Set<T> operator ^(Set<T> a, Set<T> b)
        {
            return (Set<T>) ExclusiveOr(a, b);
        }

        /// <summary>
        ///     This method will test the <c>Set</c> against another <c>Set</c> for "equality".
        ///     In this case, "equality" means that the two sets contain the same elements.
        ///     The "==" and "!=" operators are not overridden by design.  If you wish to check
        ///     for "equivalent" <c>Set</c> instances, use <c>Equals()</c>.  If you wish to check
        ///     to see if two references are actually the same object, use "==" and "!=".
        /// </summary>
        /// <param name="obj">
        ///     A <c>Set</c> object to compare to.
        /// </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Set<T>) || ((Set<T>) obj).Count != Count)
                return false;
            foreach (T elt in (Set<T>) obj)
                if (!Contains(elt))
                    return false;
            return true;
        }

        /// <summary>
        ///     Gets the hashcode for the object.  Not implemented.
        /// </summary>
        /// <returns>An exception.</returns>
        /// <exception cref="NotImplementedException">This feature is not implemented.</exception>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        private bool AddAllPrivate(IEnumerable<T> c)
        {
            bool changed = false;
            foreach (T o in c)
            {
                if (InternalDictionary[o] != null)
                    continue;

                changed = true;
                Add(o);
            }
            return changed;
        }
    }
}