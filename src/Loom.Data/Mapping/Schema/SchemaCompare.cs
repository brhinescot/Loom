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

namespace Loom.Data.Mapping.Schema
{
    internal static class SchemaCompare
    {
        public static bool TablesAreSame(TableData a, TableData b)
        {
            if (a == null || b == null)
                return false;

            if (ReferenceEquals(a, b))
                return true;

            return Compare.AreSameOrdinal(a.Name, b.Name) &&
                   Compare.AreSameOrdinal(a.Owner, b.Owner);
        }

        public static bool ColumnsAreSame(IQueryableColumn a, IQueryableColumn b)
        {
            if (a == null || b == null)
                return false;

            if (ReferenceEquals(a, b))
                return true;

            return Compare.AreSameOrdinal(a.Name, b.Name) &&
                   Compare.AreSameOrdinal(a.Table.Name, b.Table.Name) &&
                   Compare.AreSameOrdinal(a.Table.Owner, b.Table.Owner);
        }

        public static bool ColumnsAreSame(IQueryableColumn column, PrimaryKeys keys)
        {
            foreach (IQueryableColumn b in keys)
            {
                if (column == null || b == null)
                    return false;

                if (ReferenceEquals(column, b))
                    return true;

                if (Compare.AreSameOrdinal(column.Name, b.Name) &&
                       Compare.AreSameOrdinal(column.Table.Name, b.Table.Name) &&
                       Compare.AreSameOrdinal(column.Table.Owner, b.Table.Owner))
                    return true;
            }

            return false;
        }
    }
}
