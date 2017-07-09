#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Loom.Data.Mapping.Configuration;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [Serializable, DebuggerDisplay("ParentTable = {ParentTable.FullNameBracketed,nq}, Count = {Count}")]
    public sealed class TableColumnDefinitionCollection : ICollection<TableColumnDefinition>
    {
        #region Member Fields

        private readonly TableDefinition parentTable;
        private readonly ActiveMapCodeGenConfigurationSection configuration;
        private readonly Dictionary<string, TableColumnDefinition> innerList = new Dictionary<string, TableColumnDefinition>(); 

        #endregion

        #region Property Accessors

        public TableDefinition ParentTable
        {
            get { return parentTable; }
        }

        public int Count
        {
            get { return innerList.Count; }
        }

        #endregion

        #region .ctor

        public TableColumnDefinitionCollection(TableDefinition parentTable, ActiveMapCodeGenConfigurationSection configuration)
        {
            this.parentTable = parentTable;
            this.configuration = configuration;
        }

        #endregion

        public void Add(TableColumnDefinition item)
        {
            item.ParentTable = parentTable;
            if (string.Compare(item.Name, configuration.AuditMapping.DeletedColumn, StringComparison.OrdinalIgnoreCase) == 0)
                item.ParentTable.DeletedColumn = item;
            
            innerList.Add(item.Name, item);
        }

        public TableColumnDefinition FindColumn(string columnName)
        {
            if (innerList.ContainsKey(columnName))
                return innerList[columnName];
            return null;
        }

        #region Explicit ICollection<T> Implementation

        ///<summary>
        ///Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        void ICollection<TableColumnDefinition>.Clear()
        {
            innerList.Clear();
        }

        ///<summary>
        ///Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        ///</summary>
        ///
        ///<returns>
        ///true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        ///</returns>
        ///
        ///<param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        bool ICollection<TableColumnDefinition>.Contains(TableColumnDefinition item)
        {
            return innerList.ContainsKey(item.Name);
        }

        ///<summary>
        ///Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        ///</summary>
        ///
        ///<param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        ///<param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ///<exception cref="T:System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ///<exception cref="T:System.ArgumentNullException">array is null.</exception>
        ///<exception cref="T:System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        void ICollection<TableColumnDefinition>.CopyTo(TableColumnDefinition[] array, int arrayIndex)
        {
            innerList.Values.CopyTo(array, arrayIndex);
        }

        ///<summary>
        ///Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</summary>
        ///
        ///<returns>
        ///true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        ///</returns>
        ///
        ///<param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ///<exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        bool ICollection<TableColumnDefinition>.Remove(TableColumnDefinition item)
        {
            return innerList.Remove(item.Name);
        }

        ///<summary>
        ///Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        ///</summary>
        ///
        ///<returns>
        ///true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.
        ///</returns>
        ///
        bool ICollection<TableColumnDefinition>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable<T> Implementation

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<TableColumnDefinition> GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.Values.GetEnumerator();
        }

        #endregion
    }
}
