#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable]
    [DebuggerDisplay("Procedure = {ParentProcedure.FullNameBracketed,nq}, Count = {Count}")]
    public sealed class ProcedureParameterDefinitionCollection : ICollection<ProcedureParameterDefinition>
    {
        private readonly Dictionary<string, ProcedureParameterDefinition> innerList = new Dictionary<string, ProcedureParameterDefinition>();

        #region ICollection<ProcedureParameterDefinition> Members

        public int Count => innerList.Count;

        public void Add(ProcedureParameterDefinition item)
        {
            innerList.Add(item.Name, item);
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        void ICollection<ProcedureParameterDefinition>.Clear()
        {
            innerList.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        bool ICollection<ProcedureParameterDefinition>.Contains(ProcedureParameterDefinition item)
        {
            return innerList.ContainsKey(item.Name);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an
        ///     <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements
        ///     copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">array is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     array is multidimensional.-or-arrayIndex is equal to or greater than the
        ///     length of array.-or-The number of elements in the source
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from arrayIndex
        ///     to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.
        /// </exception>
        void ICollection<ProcedureParameterDefinition>.CopyTo(ProcedureParameterDefinition[] array, int arrayIndex)
        {
            innerList.Values.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <returns>
        ///     true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>;
        ///     otherwise, false. This method also returns false if item is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1"></see> is
        ///     read-only.
        /// </exception>
        bool ICollection<ProcedureParameterDefinition>.Remove(ProcedureParameterDefinition item)
        {
            return innerList.Remove(item.Name);
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.
        /// </returns>
        bool ICollection<ProcedureParameterDefinition>.IsReadOnly => false;

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<ProcedureParameterDefinition> GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        #endregion

        public ProcedureParameterDefinition FindParameter(string columnName)
        {
            if (innerList.ContainsKey(columnName))
                return innerList[columnName];
            return null;
        }
    }
}