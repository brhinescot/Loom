#region Using Directives

using System.Collections.Generic;
using System.Data;

#endregion

namespace Loom.Data
{
    /// <summary>
    ///     A class providing common helper methods used to work with <see cref="DataTable" /> objects.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        ///     Returns the distinct rows in a <see cref="DataTable" />
        /// </summary>
        /// <param name="table">The <see cref="DataTable" /> to select from.</param>
        /// <returns>A <see cref="DataTable" /> with only distinct rows.</returns>
        public static DataTable SelectDistinct(this DataTable table)
        {
            Argument.Assert.IsNotNull(table, nameof(table));

            return SelectDistinctPrivate(table.DefaultView, 0);
        }

        /// <summary>
        ///     Returns the distinct rows in a <see cref="DataTable" />
        /// </summary>
        /// <param name="table">The <see cref="DataTable" /> to select from.</param>
        /// <param name="maxCount">The maximum number of rows to return.</param>
        /// <returns>A <see cref="DataTable" /> with only distinct rows.</returns>
        public static DataTable SelectDistinct(this DataTable table, int maxCount)
        {
            Argument.Assert.IsNotNull(table, nameof(table));
            Argument.Assert.IsNotNegative(maxCount, nameof(maxCount));

            return SelectDistinctPrivate(table.DefaultView, maxCount);
        }

        /// <summary>
        ///     Returns the distinct rows in a <see cref="DataView" />
        /// </summary>
        /// <param name="view">The <see cref="DataView" /> to select from.</param>
        /// <returns>A <see cref="DataTable" /> with only distinct rows.</returns>
        public static DataTable SelectDistinct(this DataView view)
        {
            Argument.Assert.IsNotNull(view, nameof(view));

            return SelectDistinctPrivate(view, 0);
        }

        /// <summary>
        ///     Returns the distinct rows in a <see cref="DataView" />
        /// </summary>
        /// <param name="view">The <see cref="DataView" /> to select from.</param>
        /// <param name="maxCount">The maximum number of rows to return.</param>
        /// <returns>A <see cref="DataTable" /> with only distinct rows.</returns>
        public static DataTable SelectDistinct(this DataView view, int maxCount)
        {
            Argument.Assert.IsNotNull(view, nameof(view));
            Argument.Assert.IsNotNegative(maxCount, nameof(maxCount));

            return SelectDistinctPrivate(view, maxCount);
        }

        private static DataTable SelectDistinctPrivate(DataView view, int maxCount)
        {
            // create empty table with schema of DataView table
            DataTable distinctTable = view.Table.Clone();

            // initialize variable for row count
            int currentCount = 0;

            // iterate over every row in DataView containing duplicate values
            foreach (DataRowView drv in view)
            {
                // assume no equal rows
                bool isRowDuplicated = false;

                // iterate over every unique row in distinct table
                // no iteration occurs on first call
                foreach (DataRow rowInTable in distinctTable.Rows)
                {
                    // now assume row is duplicated
                    isRowDuplicated = true;

                    // iterate over each column value, and compare
                    for (int index = 0; index < rowInTable.ItemArray.Length; index++)
                        // compare original column with new distinct table column 
                        if (!drv.Row.ItemArray[index].Equals(rowInTable.ItemArray[index]))
                        {
                            // if column values are not equal, not a duplicate row
                            isRowDuplicated = false;
                            break; // for
                        }
                    if (isRowDuplicated)
                        break; // for
                }

                if (isRowDuplicated)
                    continue; // outer foreach

                // if a row is NOT duplicated, add it to distinct table
                distinctTable.ImportRow(drv.Row);

                // stop processing if maxCount is reached
                if (++currentCount == maxCount)
                    break;
            }

            return distinctTable;
        }

        /// <summary>
        ///     Removes the duplicates.
        /// </summary>
        /// <param name="table">TBL.</param>
        /// <param name="keyColumns">Key columns.</param>
        public static void RemoveDuplicates(this DataTable table, params DataColumn[] keyColumns)
        {
            Argument.Assert.IsNotNull(table, nameof(table));
            Argument.Assert.IsNotNull(keyColumns, nameof(keyColumns));
            Argument.Assert.IsNotZeroLength(keyColumns, nameof(keyColumns));

            RemoveDuplicatesPrivate(table, keyColumns);
        }

        private static DataRow[] FindDuplicates(DataTable table, int sourceIndex, IEnumerable<DataColumn> keyColumns)
        {
            List<DataRow> retVal = new List<DataRow>();

            DataRow sourceRow = table.Rows[sourceIndex];
            for (int i = sourceIndex + 1; i < table.Rows.Count; i++)
            {
                DataRow targetRow = table.Rows[i];
                if (IsDuplicate(sourceRow, targetRow, keyColumns))
                    retVal.Add(targetRow);
            }
            return retVal.ToArray();
        }

        private static bool IsDuplicate(DataRow sourceRow, DataRow targetRow, IEnumerable<DataColumn> keyColumns)
        {
            bool retVal = true;
            foreach (DataColumn column in keyColumns)
            {
                retVal = sourceRow[column].Equals(targetRow[column]);
                if (!retVal) break;
            }
            return retVal;
        }

        private static void RemoveDuplicatesPrivate(DataTable table, IEnumerable<DataColumn> keyColumns)
        {
            int index = 0;
            while (index < table.Rows.Count - 1)
            {
                DataRow[] dups = FindDuplicates(table, index, keyColumns);
                if (dups.Length > 0)
                    foreach (DataRow dup in dups)
                        table.Rows.Remove(dup);
                else
                    index++;
            }
        }
    }
}