#region File Header

// *************************************************************************
// Copyright © 2008 Colossus Interactive, LLC
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// licensing@colossusinteractive.com
//  
// *************************************************************************

#endregion

#region Using Directives

using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    public static class Function
    {
        #region Static Factories

        public static ColumnAggregate Count(this IQueryableColumn column)
        {
            return new CountAggregate(column);
        }

        public static ColumnAggregate Sum(this IQueryableColumn column)
        {
            return new SumAggregate(column);
        }

        public static ColumnAggregate Min(this IQueryableColumn column)
        {
            return new MinAggregate(column);
        }

        public static ColumnAggregate Max(this IQueryableColumn column)
        {
            return new MaxAggregate(column);
        }

        public static ColumnAggregate Average(this IQueryableColumn column)
        {
            return new AverageAggregate(column);
        }

        public static ColumnAggregate StandardDeviation(this IQueryableColumn column)
        {
            return new StandardDeviationAggregate(column);
        }

        public static ColumnAggregate IsNull(this IQueryableColumn column, object defaultValue)
        {
//            throw new NotImplementedException("Still needs some work on format of default value.");
            return new IsNullFunction(column, defaultValue);
        }

        #endregion

        #region Nested Column Aggregates

        private class IsNullFunction : ColumnAggregate
        {
            private readonly object defaultValue;

            public IsNullFunction(IQueryableColumn column, object defaultValue) : base(column)
            {
                this.defaultValue = defaultValue;
            }

            public override string ColumnFormat
            {
                get
                {
                    if (Column.ColumnFormat != null)
                        return string.Format("ISNULL({0}, " + defaultValue + ")", Column.ColumnFormat);
                    return "ISNULL({0}, " + defaultValue + ")";
                }
                set { Column.ColumnFormat = value; }
            }
        }

        private class AverageAggregate : ColumnAggregate
        {
            public AverageAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("AVG({0})", Column.ColumnFormat) : "AVG({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        private class CountAggregate : ColumnAggregate
        {
            public CountAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("COUNT({0})", Column.ColumnFormat) : "COUNT({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        private class SumAggregate : ColumnAggregate
        {
            public SumAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("SUM({0})", Column.ColumnFormat) : "SUM({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        private class MinAggregate : ColumnAggregate
        {
            public MinAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("MIN({0})", Column.ColumnFormat) : "MIN({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        private class MaxAggregate : ColumnAggregate
        {
            public MaxAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("MAX({0})", Column.ColumnFormat) : "MAX({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        private class StandardDeviationAggregate : ColumnAggregate
        {
            public StandardDeviationAggregate(IQueryableColumn column) : base(column) {}

            public override string ColumnFormat
            {
                get { return Column.ColumnFormat != null ? string.Format("STDEV({0})", Column.ColumnFormat) : "STDEV({0})"; }
                set { Column.ColumnFormat = value; }
            }
        }

        #endregion
    }
}
