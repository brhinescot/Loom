#region License Information

// ******************************************************************
// Devinterop Framework 
// 
// Copyright © 2004, 2008 by Brian Scott (DevInterop)
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.devinterop.com
// http://blogs.geekdojo.net/brian
//  
// ******************************************************************

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    public class DbTextWriter : StringWriter
    {
        #region Type Fields

        private struct Formats
        {
            public const string EmptySpace = " ";
            public const string TableFormat = "[{0}].[{1}]{2}{3}";
            public const string ColumnFormatNoAlias = "[{0}].[{1}].[{2}]";
            public const string ColumnFormatAlias = "{0}.[{1}]";
            public const string ColumnFormatSubquery = "[{0}]";
            public const string Prefix = "_";
            public const string Seperator = ", ";
            public const string FromStatement = " FROM ";
            public const string WhereStatement = " WHERE ";
            public const string HavingStatement = " HAVING ";
            public const string OrOperator = " OR ";
            public const string AndOperator = " AND ";
            public const string SelectStatement = "SELECT ";
            public const string SelectCountStatement = "SELECT COUNT(*)";
            public const string SelectTopStatement = "SELECT TOP {0} ";
            public const string SelectTopPercentStatement = "SELECT TOP {0} PERCENT ";
            public const string SelectDistinctStatement = "SELECT DISTINCT ";
            public const string SelectDistinctCountStatement = "SELECT DISTINCT COUNT(*)";
            public const string SelectDistinctTopStatement = "SELECT DISTINCT TOP {0} ";
            public const string SelectDistinctTopPercentStatement = "SELECT DISTINCT TOP {0} PERCENT ";
            public const string SelectAllColumns = "*";
            public const string OrderByStatement = " ORDER BY ";
            public const string Asc = "ASC";
            public const string Desc = "DESC";
            public const string RangePredicateFormat = "{0} {1} {2} AND {3}";
            public const string DefaultPredicateFormat = "{0} {1} {2}";
            public const string NullComparisonPredicateFormat = "{0} {1} NULL";
        }

        private static readonly int IsWhereStartedFlag = BitVector32.CreateMask();
        private static readonly int IsFromStartedFlag = BitVector32.CreateMask(IsWhereStartedFlag);
        private static readonly int IsSelectionSetFlag = BitVector32.CreateMask(IsFromStartedFlag);
        private static readonly int IsHavingStartedFlag = BitVector32.CreateMask(IsSelectionSetFlag);
        private static readonly int IsJoinStartedFlag = BitVector32.CreateMask(IsHavingStartedFlag);

        #endregion

        #region Instance Fields

        private BitVector32 flags;
        private readonly Dictionary<string, string> tableAliases = new Dictionary<string, string>();

        #endregion

        #region Property Accessors

        protected virtual string UniquePrefix
        {
            get { return Formats.Prefix; }
        }

        protected virtual string ColumnSeparator
        {
            get { return Formats.Seperator; }
        }

        protected bool IsJoinStarted
        {
            get { return flags[IsJoinStartedFlag]; }
        }

        protected bool IsWhereStarted
        {
            get { return flags[IsWhereStartedFlag]; }
            private set { flags[IsWhereStartedFlag] = value; }
        }

        protected bool IsHavingStarted
        {
            get { return flags[IsHavingStartedFlag]; }
            private set { flags[IsHavingStartedFlag] = value; }
        }

        protected bool IsFromStarted
        {
            get { return flags[IsFromStartedFlag]; }
            private set { flags[IsFromStartedFlag] = value; }
        }

        protected internal bool IsSelectionSet
        {
            get { return flags[IsSelectionSetFlag]; }
            private set { flags[IsSelectionSetFlag] = value; }
        }

        #endregion

        #region Virtual Writes

        public virtual void WriteCount(bool distinct = false)
        {
            IsSelectionSet = true;

            Write(distinct ? Formats.SelectDistinctCountStatement : Formats.SelectCountStatement);
        }

        public virtual void WriteSelect(IEnumerable<IQueryableColumn> columns, Constraint constraint, bool distinct = false)
        {
            IsSelectionSet = true;

            switch (constraint.ConstraintType)
            {
                case ConstraintType.None:
                    Write(distinct ? Formats.SelectDistinctStatement : Formats.SelectStatement);
                    break;
                case ConstraintType.Top:
                case ConstraintType.Random:
                    Write(distinct ? Formats.SelectDistinctTopStatement : Formats.SelectTopStatement, constraint.Count);
                    break;
                case ConstraintType.TopPercent:
                    Write(distinct ? Formats.SelectDistinctTopPercentStatement  : Formats.SelectTopPercentStatement, constraint.Count);
                    break;
            }

            WriteSelectColumns(columns, true, distinct);
        }

        public virtual void WriteSelectColumns(IEnumerable<IQueryableColumn> columns, bool useAlias = false, bool distinct = false)
        {
            int i = 0;
            foreach (IQueryableColumn column in columns)
            {
                if (i++ >= 1)
                    Write(ColumnSeparator);
                Write(FormatPredicateSelectColumnName(column, useAlias));
            }

            if (i == 0)
                Write(Formats.SelectAllColumns);
        }

        public virtual void WriteFrom(IEnumerable<TableData> tables, bool useTableAlias = false)
        {
            if (!IsSelectionSet)
            {
                Write(Formats.SelectAllColumns);
                IsSelectionSet = true;
            }

            if (!IsFromStarted)
                Write(Formats.FromStatement);

            foreach (TableData table in tables)
            {
                if (IsFromStarted)
                    Write(", ");
                Write(FormatTableName(table, useTableAlias));

                if (!IsFromStarted)
                    IsFromStarted = true;
            }
        }

        public virtual void WriteWhere()
        {
            if (!IsWhereStarted)
                base.Write(Formats.WhereStatement);
            else
                WriteAnd();

            IsWhereStarted = true;
        }

        public virtual void WriteHaving()
        {
            if (!IsHavingStarted)
                base.Write(Formats.HavingStatement);
            else
                WriteAnd();

            IsHavingStarted = true;
        }

        public virtual void WriteOrderBy(ICollection<OrderBy> orderBys, Constraint constraint)
        {
            if (orderBys == null || orderBys.Count == 0)
            {
                if (constraint.ConstraintType == ConstraintType.Random)
                    base.Write(" ORDER BY NEWID()");
                return;
            }

            string orderByOperator = constraint.ConstraintType == ConstraintType.Random ? " ORDER BY NEWID(), " : Formats.OrderByStatement;
            foreach (OrderBy orderBy in orderBys)
            { 
                string direction;
                switch (orderBy.Direction)
                {
                    case OrderByDirection.Asc:
                        direction = Formats.Asc;
                        break;
                    case OrderByDirection.Desc:
                        direction = Formats.Desc;
                        break;
                    default:
                        continue;
                }

                if (!Compare.IsNullOrEmpty(GetTableAlias(orderBy.Column.Table)))
                    base.Write("{0}{1}.[{2}] {3}", orderByOperator, GetTableAlias(orderBy.Column.Table), orderBy.Column.Name, direction);
                else
                    base.Write("{0}[{1}].[{2}].[{3}] {4}", orderByOperator, orderBy.Column.Table.Owner, orderBy.Column.Table.Name, orderBy.Column.Name, direction);
                orderByOperator = ", ";
            }
        }

        public virtual void WriteGroupBy(IEnumerable<IQueryableColumn> groupByColumns)
        {
            string groupByOperator = " GROUP BY ";
            foreach (IQueryableColumn column in groupByColumns)
            {
                base.Write("{0}{1}.[{2}]", groupByOperator, GetTableAlias(column.Table), column.Name);
                groupByOperator = ", ";
            }
        }

        public virtual void WritePredicate(JoinPredicate current, string comparisonOperator)
        {
            string fromColumnName = FormatPredicateColumnName(current.FromColumn);
            string toColumnName = FormatPredicateColumnName(current.ToColumn);

            if (Compare.IsNullOrEmpty(toColumnName))
                Write(Formats.NullComparisonPredicateFormat, fromColumnName, comparisonOperator);
            else
                Write(Formats.DefaultPredicateFormat, fromColumnName, comparisonOperator, toColumnName);
        }

        public virtual void WritePredicate(IQueryableColumn column, string comparisonOperator, ParameterNames parameterNames)
        {
            string columText = FormatPredicateColumnName(column);
            if (column.ColumnFormat != null)
                columText = string.Format(column.ColumnFormat, columText);

            if (parameterNames == ParameterNames.Empty)
                Write(Formats.NullComparisonPredicateFormat, columText, comparisonOperator);
            else if (parameterNames.Parameter1 != null && parameterNames.Parameter2 != null)
                Write(Formats.RangePredicateFormat, columText, comparisonOperator, parameterNames.Parameter1, parameterNames.Parameter2);
            else
                Write(Formats.DefaultPredicateFormat, columText, comparisonOperator, parameterNames.Parameter1);
        }

        protected virtual string FormatPredicateColumnName(IQueryableColumn column)
        {
            if (column == null)
                return null;
            if (Compare.IsNullOrEmpty(GetTableAlias(column.Table)))
                return string.Format("[{0}].[{1}].[{2}]", column.Table.Owner, column.Table.Name, column.Name);

            return string.Format("{0}.[{1}]", GetTableAlias(column.Table), column.Name);
        }

        public virtual void WriteAnd()
        {
            base.Write(Formats.AndOperator);
        }

        public virtual void WriteOr()
        {
            base.Write(Formats.OrOperator);
        }

        #endregion

        #region Formatting

        internal string GetTableAlias(TableData table)
        {
            if (tableAliases.ContainsKey(table.Owner + table.Name))
                return tableAliases[table.Owner + table.Name];

            string alias = "_t" + tableAliases.Count;
            tableAliases.Add(table.Owner + table.Name, alias);
            return alias;
        }

        /// <summary>
        /// Formats a table name for use in a FROM statement.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default format is [Owner].[Table] Alias</para>
        /// </remarks>
        /// <param name="table">A <see cref="TableData"/> instance containing information about the table.</param>
        /// <param name="needsAlias"></param>
        /// <returns>A <see cref="string"/> containing the formatted table name.</returns>
        protected virtual string FormatTableName(TableData table, bool needsAlias)
        {
            return string.Format(Formats.TableFormat, table.Owner, table.Name, needsAlias ? Formats.EmptySpace : string.Empty, needsAlias ? GetTableAlias(table) : string.Empty);
        }

        /// <summary>
        /// Formats a column name for use in a predicate expression such as JOIN, WHERE, and HAVING.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default format is [Owner].[Table].[Column] if no alias is provided or Alias.[Column] if an alias
        /// is provided.</para>
        /// </remarks>
        /// <param name="column">A <see cref="IQueryableColumn"/> implementation containing information about the column.</param>
        /// <param name="useTableAlias"></param>
        /// <returns>A <see cref="string"/> containing the formatted column name.</returns>
        protected virtual string FormatPredicateSelectColumnName(IQueryableColumn column, bool useTableAlias)
        {
            return FormatPredicateSelectColumnName(column, useTableAlias, true);
        }

        protected virtual string FormatPredicateSelectColumnName(IQueryableColumn column, bool useTableAlias, bool useColumnAlias)
        {
            string name;
            if (Compare.IsNullOrEmpty(GetTableAlias(column.Table)))
                name = string.Format(Formats.ColumnFormatNoAlias, column.Table.Owner, column.Table.Name, column.Name);
            else if (useTableAlias)
                name = string.Format(Formats.ColumnFormatAlias, GetTableAlias(column.Table), column.Name);
            else
                name = string.Format(Formats.ColumnFormatSubquery, column.Name);

            if (column.ColumnFormat != null)
                name = string.Format(column.ColumnFormat, name);

            if (column.LocalizeFallbackColumn != null)
            {
                name = string.Format("ISNULL({0}, {1}) AS {2}", 
                                     name, 
                                     FormatPredicateSelectColumnName(column.LocalizeFallbackColumn, true, false),
                                     column.Alias ?? column.Name);
                useColumnAlias = false;
            }

            if(useColumnAlias && !Compare.IsNullOrEmpty(column.Alias))
                name += " AS " + column.Alias;

            return name;
        }

        #endregion
    }
}
