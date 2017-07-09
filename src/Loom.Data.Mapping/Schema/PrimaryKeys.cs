#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Schema
{
    /// <summary>
    /// </summary>
    public class PrimaryKeys : IEnumerable<QueryColumn>
    {
        private readonly QueryColumn[] keys;

        internal PrimaryKeys(ITable table)
        {
            List<QueryColumn> primaryKeys = new List<QueryColumn>();
            foreach (QueryColumn column in table.Columns)
                if ((column.ColumnProperties & ColumnProperties.PrimaryKey) == ColumnProperties.PrimaryKey)
                    primaryKeys.Add(column);
            keys = primaryKeys.ToArray();
        }

        public bool Exists => keys != null && keys.Length > 0 && !Equals(keys[0], null) && !Compare.IsNullOrEmpty(keys[0].Name);

        public QueryColumn this[int index]
        {
            get
            {
                if (index >= keys.Length)
                    throw new ArgumentOutOfRangeException("index", index, null);

                return keys[index];
            }
        }

        public bool Multiple => keys.Length > 1;

        #region IEnumerable<QueryColumn> Members

        public IEnumerator<QueryColumn> GetEnumerator()
        {
            for (int i = 0; i < keys.Length; i++)
                yield return keys[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return keys.GetEnumerator();
        }

        #endregion

        public ColumnPredicate CreatePredicate<TDataRecord>(TDataRecord record) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ColumnPredicate predicate = null;
            foreach (QueryColumn key in keys)
            {
                object value = record[key];
                if (value == null)
                    throw new InvalidOperationException(SR.ExPrimaryKeyValueNotSet);

                if (Equals(predicate, null))
                    predicate = key == value;
                else
                    predicate = predicate & (key == value);
            }

            return predicate;
        }

        public QueryColumn GetIdentityColumn()
        {
            foreach (QueryColumn key in keys)
                if ((key.ColumnProperties & ColumnProperties.Identity) == ColumnProperties.Identity)
                    return key;

            return null;
        }
    }
}