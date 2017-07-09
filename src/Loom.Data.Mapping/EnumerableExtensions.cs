#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Returns the first item in the locally cached collection with the specified value in the specified column.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TDataRecord Find<TDataRecord>(this IEnumerable<TDataRecord> items, ColumnPredicate predicate) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            Argument.Assert.IsNotNull(predicate, nameof(predicate));

            return items.FirstOrDefault(item => predicate.Evaluate(item[predicate.Column]));
        }

        public static TDataRecord FindByKey<TDataRecord>(this IEnumerable<TDataRecord> items, object primaryKeyValue) where TDataRecord : DataRecord<TDataRecord>, new()
        {
            ITable table = DataRecord<TDataRecord>.Table;

            if (!table.HasPrimaryKey)
                throw new InvalidOperationException(SR.ExNoPrimaryKeyDefinedInTable(DataRecord<TDataRecord>.Table.Owner + "." + DataRecord<TDataRecord>.Table.Name));

            if (table.PrimaryKey.Multiple)
                throw new InvalidOperationException("The table " + table.Owner + "." + table.Name + " has more than one primary key column. Unable to find a record using this method. Use a fond method that allows specifying a ColumnPredicate value.");

            return Find(items, DataRecord<TDataRecord>.Table.PrimaryKey[0] == primaryKeyValue);
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, ColumnPredicate predicate) where T : DataRecord<T>, new()
        {
            Argument.Assert.IsNotNull(predicate, nameof(predicate));

            return items.Where(item => predicate.Evaluate(item[predicate.Column]));
        }
    }
}