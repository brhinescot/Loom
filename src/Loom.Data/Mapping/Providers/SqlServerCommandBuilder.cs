#region Using Directives

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Loom.Data.Mapping.Query;

#endregion

namespace Loom.Data.Mapping.Providers
{
    public class SqlServerCommandBuilder : CommandBuilder
    {
        private const string PagingSelectTemplate = "SELECT {0} FROM (SELECT TOP (@_pi + @_ps) ROW_NUMBER() OVER ({1}) AS _prr, {2}) AS _prt WHERE (_prr BETWEEN @_pi + 1 AND (@_pi  + @_ps))";
        private const string ParameterPrefixValue = "@";
        private const string WildcardCharacterValue = "%";

        private struct SqlComparisonOperator
        {
            public const string Is = "IS";
            public const string IsNot = "IS NOT";
            public const string Equal = "=";
            public const string NotEqual = "<>";
            public const string Greater = ">";
            public const string GreaterOrEqual = ">=";
            public const string Less = "<";
            public const string LessOrEqual = "<=";
            public const string Like = "LIKE";
            public const string NotLike = "NOT LIKE";
            public const string Between = "BETWEEN";
        }

        protected override string ParameterPrefix
        {
            get { return ParameterPrefixValue; }
        }

        protected override string WildcardCharacter
        {
            get { return WildcardCharacterValue; }
        }

        public SqlServerCommandBuilder(DataSession session) : base(session) { }
        public SqlServerCommandBuilder(QueryTree query) : base(query) { }

        protected override string GetComparisonOperator(Comparison comparison, object value)
        {
            switch (comparison)
            {
                case Comparison.Equal:
                    if (value == null || value == DBNull.Value)
                        return SqlComparisonOperator.Is;
                    return SqlComparisonOperator.Equal;
                case Comparison.NotEqual:
                    if (value == null || value == DBNull.Value)
                        return SqlComparisonOperator.IsNot;
                    return SqlComparisonOperator.NotEqual;
                case Comparison.Greater:
                    return SqlComparisonOperator.Greater;
                case Comparison.GreaterOrEqual:
                    return SqlComparisonOperator.GreaterOrEqual;
                case Comparison.Less:
                    return SqlComparisonOperator.Less;
                case Comparison.LessOrEqual:
                    return SqlComparisonOperator.LessOrEqual;
                case Comparison.StartsWith:
                case Comparison.EndsWith:
                case Comparison.Contains:
                    return SqlComparisonOperator.Like;
                case Comparison.DoesNotContain:
                case Comparison.DoesNotEndWith:
                case Comparison.DoesNotStartWith:
                    return SqlComparisonOperator.NotLike;
                case Comparison.Between:
                    return SqlComparisonOperator.Between;
                default:
                    throw new ArgumentException("Unrecognized comparison.", Argument.Names.comparison);
            }
        }

        protected override string FormatPagingSql(DbTextWriter writer, Constraint constraint)
        {
            Argument.Assert.IsNotNull(writer, Argument.Names.writer);
            
            OnWriteSelect(writer);
            OnWriteFrom(writer);
            OnWriteWhere(writer);
            OnWriteGroupBys(writer);
            OnWriteHaving(writer);

            string subQuery = writer.ToString();
            string outerSelectList;
            string orderbylist;

            if (OrderBys.Count == 0 && SelectTable != null)
            {
                if (SelectTable.HasIdentityColumn)
                    OrderBys.Add(new OrderBy(SelectTable.IdentityColumn, OrderByDirection.Asc));
                else if (SelectTable.HasPrimaryKey)
                    foreach (QueryColumn key in SelectTable.PrimaryKey)
                        OrderBys.Add(new OrderBy(key, OrderByDirection.Asc));
                else
                    throw new InvalidOperationException("The paging command has no OrderBy column specified and the table has no identity column or primary key defined.");
            }

            using (DbTextWriter outerSelectWriter = new DbTextWriter())
            {
                outerSelectWriter.WriteSelectColumns(Columns);
                outerSelectList = outerSelectWriter.ToString();
            }

            using (DbTextWriter orderByWriter = new DbTextWriter())
            {
                orderByWriter.WriteOrderBy(OrderBys, constraint);
                orderbylist = orderByWriter.ToString();
            }

            Parameters.Add("_pi", constraint.Start);
            Parameters.Add("_ps", constraint.Count);
            return string.Format(PagingSelectTemplate, outerSelectList, orderbylist, subQuery);
        }

        public override DbCommand ToSelectCommand()
        {
            return ToSelectCommand(ConstraintType.None);
        }

        public override DbCommand ToSelectCommand(ConstraintType constraintType)
        {
            SqlCommand command = new SqlCommand(ToSelectText(constraintType));

            foreach (KeyValuePair<string, object> pair in Parameters)
                command.Parameters.AddWithValue(pair.Key, pair.Value);

            return command;
        }
    }
}
