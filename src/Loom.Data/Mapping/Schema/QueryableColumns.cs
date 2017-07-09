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

#endregion

namespace Loom.Data.Mapping.Schema
{
    public sealed class QueryableColumns : IEnumerable<QueryColumn>
    {
        #region Member Fields

        private readonly Dictionary<string, QueryColumn> innerList = new Dictionary<string, QueryColumn>();

        #endregion

        #region .ctor

        internal QueryableColumns() {}

        #endregion

        #region Add

        internal void Add(QueryColumn item)
        {
            Add(item.Name, item);
        }

        private void Add(string key, QueryColumn item)
        {
            if(innerList.ContainsKey(key))
                throw new ArgumentException();
            innerList.Add(key, item);
        }

        #endregion

        public QueryColumn FindColumn(string columnName)
        {
            if (columnName == null)
                return null;

            QueryColumn column;
            return innerList.TryGetValue(columnName, out column) ? column : null;
        }
        
        #region IEnumerable<T> Implementation

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<QueryColumn> GetEnumerator()
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
