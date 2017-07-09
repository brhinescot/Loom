#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Data.Mapping.Schema
{
    public sealed class QueryableColumns : IEnumerable<QueryColumn>
    {
        private readonly Dictionary<string, QueryColumn> innerList = new Dictionary<string, QueryColumn>();

        internal QueryableColumns() { }

        #region IEnumerable<QueryColumn> Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<QueryColumn> GetEnumerator()
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

        internal void Add(QueryColumn item)
        {
            Add(item.Name, item);
        }

        private void Add(string key, QueryColumn item)
        {
            if (innerList.ContainsKey(key))
                throw new ArgumentException();
            innerList.Add(key, item);
        }

        public QueryColumn FindColumn(string columnName)
        {
            if (columnName == null)
                return null;

            QueryColumn column;
            return innerList.TryGetValue(columnName, out column) ? column : null;
        }
    }
}