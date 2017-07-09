#region Using Directives

using System.Collections.Generic;
using System.Text;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Providers
{
    // WIP: Subclassed to move SQL Server specific generation into this class.
    public class SqlCommandWriter : DbTextWriter
    {
        private readonly StringBuilder builder = new StringBuilder();
        private readonly Dictionary<ISchema, string> tableAliases = new Dictionary<ISchema, string>();

        public void WriteSelect(params IQueryableColumn[] tables)
        {
            builder.Append("SELECT");
            if (tables == null || tables.Length == 0)
            {
                builder.Append(" *");
                return;
            }

            bool addSeparator = false;
            foreach (IQueryableColumn column in tables)
            {
                if (addSeparator)
                    builder.Append(",");

                AppendSelectClauseAlias(column, !addSeparator);
                builder.Append(".[" + column.Name + "]");

                if (!addSeparator)
                    addSeparator = true;
            }
        }

        public void WriteFrom(params ITable[] tables)
        {
            builder.Append(" FROM ");
            bool addSeparator = false;
            foreach (ITable table in tables)
            {
                if (addSeparator)
                    builder.Append(", ");
                builder.Append("[" + table.Owner + "].[" + table.Name + "]");
                AppendFromClauseAlias(table);

                if (!addSeparator)
                    addSeparator = true;
            }
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        private void AppendSelectClauseAlias(IQueryableColumn column, bool withSpace = false)
        {
            string alias;
            tableAliases.TryGetValue(column.Table, out alias);
            if (alias == null)
            {
                alias = (withSpace ? " " : null) + "_t" + tableAliases.Count;
                tableAliases.Add(column.Table, alias);
            }

            builder.Append(alias);
        }

        private void AppendFromClauseAlias(ITable table)
        {
            if (tableAliases.Count == 0)
                return;

            string alias;
            tableAliases.TryGetValue(table, out alias);
            if (alias == null)
            {
                alias = " _t" + tableAliases.Count;
                tableAliases.Add(table, alias);
            }
            builder.Append(alias);
        }
    }
}