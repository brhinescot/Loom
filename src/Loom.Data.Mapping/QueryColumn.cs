#region Using Directives

using System;
using System.Diagnostics;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping
{
    [DebuggerDisplay("{Table.Owner,nq}.{Table.Name,nq}.{Name,nq}, ColumnProperties={ColumnProperties}, ForeignKeyColumn={ForeignKeyColumn == null ? \"None\" : ForeignKeyColumn.Table.Owner + \".\" + ForeignKeyColumn.Table.Name + \".\" + ForeignKeyColumn.Name, nq}")]
    public abstract class QueryColumn : ComparisonColumn<ColumnPredicate>, IEquatable<QueryColumn>
    {
        #region IEquatable<QueryColumn> Members

        public bool Equals(QueryColumn queryColumn)
        {
            return GetHashCode() == queryColumn.GetHashCode();
        }

        #endregion

        protected override ColumnPredicate Create(ComparisonColumn<ColumnPredicate> comparisonColumn, Comparison comparison, object value)
        {
            return new ColumnPredicate(comparisonColumn, comparison, value);
        }

        protected override ColumnPredicate Create(ComparisonColumn<ColumnPredicate> comparisonColumn, object lowValue, object highValue)
        {
            return new ColumnPredicate(comparisonColumn, lowValue, highValue);
        }

        protected override ColumnPredicate Create(ComparisonColumn<ColumnPredicate> comparisonColumn, Comparison comparison, bool value)
        {
            return new ColumnPredicate(comparisonColumn, comparison, value);
        }

        protected override ColumnPredicate Create(ComparisonColumn<ColumnPredicate> comparisonColumn, Comparison comparison, DBNull value)
        {
            return new ColumnPredicate(comparisonColumn, comparison, value);
        }

        public static ColumnPredicate operator %(QueryColumn column, string value)
        {
            return new ColumnPredicate(column, Comparison.Contains, value);
        }

        public static ColumnPredicate operator ==(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.Equal, value);
        }

        public static ColumnPredicate operator !=(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.NotEqual, value);
        }

        public static ColumnPredicate operator >(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.Greater, value);
        }

        public static ColumnPredicate operator <(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.Less, value);
        }

        public static ColumnPredicate operator >=(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.GreaterOrEqual, value);
        }

        public static ColumnPredicate operator <=(QueryColumn column, object value)
        {
            return new ColumnPredicate(column, Comparison.LessOrEqual, value);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as QueryColumn);
        }

        public override int GetHashCode()
        {
            return Table.Datasource.GetHashCode() +
                   29 * Table.Owner.GetHashCode() +
                   29 * Table.Name.GetHashCode() +
                   29 * Name.GetHashCode();
        }

//        public static JoinPredicate operator ==(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.Equal, column2);
//        }
//
//        public static JoinPredicate operator !=(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.NotEqualTo, column2);
//        }
//
//        public static JoinPredicate operator >(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.Greater, column2);
//        }
//
//        public static JoinPredicate operator <(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.Less, column2);
//        }
//
//        public static JoinPredicate operator >=(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.GreaterOrEqual, column2);
//        }
//
//        public static JoinPredicate operator <=(QueryColumn column1, QueryColumn column2)
//        {
//            return new JoinPredicate(column1, Comparison.LessOrEqual, column2);
//        }
    }
}