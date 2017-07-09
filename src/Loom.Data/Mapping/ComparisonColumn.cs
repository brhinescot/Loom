#region Using Directives

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Loom.Data.Mapping.Query;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping
{
    [DebuggerDisplay("{Table.Owner,nq}.{Table.Name,nq}.{Name,nq}, ColumnProperties={ColumnProperties}, ForeignKeyColumn={ForeignKeyColumn == null ? \"None\" : ForeignKeyColumn.Table.Owner + \".\" + ForeignKeyColumn.Table.Name + \".\" + ForeignKeyColumn.Name, nq}")]
    public abstract class ComparisonColumn<TPredicate> : IQueryableColumn, IEquatable<ComparisonColumn<TPredicate>>
        where TPredicate : IPredicate<TPredicate>
    {
        #region Factory Method Signatures

        protected abstract TPredicate Create(ComparisonColumn<TPredicate> comparisonColumn, Comparison comparison, object value);
        protected abstract TPredicate Create(ComparisonColumn<TPredicate> comparisonColumn, Comparison comparison, bool value);
        protected abstract TPredicate Create(ComparisonColumn<TPredicate> comparisonColumn, Comparison comparison, DBNull value);
        protected abstract TPredicate Create(ComparisonColumn<TPredicate> comparisonColumn, object lowValue, object highValue);

        #endregion
        
        #region EqualTo Operators

        public TPredicate IsEqualTo(object value)
        {
            return Create(this, Comparison.Equal, value);
        }

        public TPredicate IsEqualToAny(object value1, object value2)
        {
            return OrMultipleValues(Comparison.Equal, value1, value2);
        }

        public TPredicate IsEqualToAny(object value1, object value2, object value3)
        {
            return OrMultipleValues(Comparison.Equal, value1, value2, value3);
        }

        public TPredicate IsEqualToAny(params object[] values)
        {
            return OrMultipleValues(Comparison.Equal, values);
        }

        public TPredicate IsEqualToAny(ICollection values)
        {
            return OrMultipleValues(Comparison.Equal, values);
        }

        public TPredicate IsEqualTo(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.Equal, value);
        }

        public TPredicate IsEqualToAny(object value1, object value2, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Equal, value1, value2);
        }

        public TPredicate IsEqualToAny(object value1, object value2, object value3, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Equal, value1, value2, value3);
        }

        public TPredicate IsEqualToAny(ICollection values, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Equal, values);
        }

        #endregion

        #region NotEqualTo Operators

        public TPredicate IsNotEqualTo(object value)
        {
            return Create(this, Comparison.NotEqual, value);
        }

        public TPredicate IsEqualToNone(object value1, object value2)
        {
            return AndMultipleValues(Comparison.NotEqual, value1, value2);
        }

        public TPredicate IsEqualToNone(object value1, object value2, object value3)
        {
            return AndMultipleValues(Comparison.NotEqual, value1, value2, value3);
        }

        public TPredicate IsEqualToNone(params object[] values)
        {
            return AndMultipleValues(Comparison.NotEqual, values);
        }

        public TPredicate IsEqualToNone(ICollection values)
        {
            return AndMultipleValues(Comparison.NotEqual, values);
        }

        public TPredicate IsNotEqualTo(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.NotEqual, value);
        }

        public TPredicate IsEqualToNone(object value1, object value2, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.NotEqual, value1, value2);
        }

        public TPredicate IsEqualToNone(object value1, object value2, object value3, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.NotEqual, value1, value2, value3);
        }

        public TPredicate IsEqualToNone(ICollection values, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.NotEqual, values);
        }

        #endregion

        #region StartsWith Operators

        public TPredicate StartsWith(object value)
        {
            return Create(this, Comparison.StartsWith, value);
        }

        public TPredicate StartsWithAny(object value1, object value2)
        {
            return OrMultipleValues(Comparison.StartsWith, value1, value2);
        }

        public TPredicate StartsWithAny(object value1, object value2, object value3)
        {
            return OrMultipleValues(Comparison.StartsWith, value1, value2, value3);
        }

        public TPredicate StartsWithAny(params object[] values)
        {
            return OrMultipleValues(Comparison.StartsWith, values);
        }

        public TPredicate StartsWithAny(ICollection values)
        {
            return OrMultipleValues(Comparison.StartsWith, values);
        }

        public TPredicate StartsWith(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.StartsWith, value);
        }

        public TPredicate StartsWithAny(object value1, object value2, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.StartsWith, value1, value2);
        }

        public TPredicate StartsWithAny(object value1, object value2, object value3, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.StartsWith, value1, value2, value3);
        }

        public TPredicate StartsWithAny(ICollection values, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.StartsWith, values);
        }

        #endregion

        #region DoesNotStartWith Operators

        public TPredicate DoesNotStartWith(object value)
        {
            return Create(this, Comparison.DoesNotStartWith, value);
        }

        public TPredicate StartsWithNone(object value1, object value2)
        {
            return AndMultipleValues(Comparison.DoesNotStartWith, value1, value2);
        }

        public TPredicate StartsWithNone(object value1, object value2, object value3)
        {
            return AndMultipleValues(Comparison.DoesNotStartWith, value1, value2, value3);
        }

        public TPredicate StartsWithNone(ICollection values)
        {
            return AndMultipleValues(Comparison.DoesNotStartWith, values);
        }

        public TPredicate DoesNotStartWith(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.DoesNotStartWith, value);
        }

        public TPredicate StartsWithNone(object value1, object value2, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotStartWith, value1, value2);
        }

        public TPredicate StartsWithNone(object value1, object value2, object value3, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotStartWith, value1, value2, value3);
        }

        public TPredicate StartsWithNone(ICollection values, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotStartWith, values);
        }

        #endregion

        #region EndsWith Operators

        public TPredicate EndsWith(object value)
        {
            return Create(this, Comparison.EndsWith, value);
        }

        public TPredicate EndsWithAny(object value1, object value2)
        {
            return OrMultipleValues(Comparison.EndsWith, value1, value2);
        }

        public TPredicate EndsWithAny(object value1, object value2, object value3)
        {
            return OrMultipleValues(Comparison.EndsWith, value1, value2, value3);
        }

        public TPredicate EndsWithAny(ICollection values)
        {
            return OrMultipleValues(Comparison.EndsWith, values);
        }

        public TPredicate EndsWith(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.EndsWith, value);
        }

        public TPredicate EndsWithAny(object value1, object value2, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.EndsWith, value1, value2);
        }

        public TPredicate EndsWithAny(object value1, object value2, object value3, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.EndsWith, value1, value2, value3);
        }

        public TPredicate EndsWithAny(ICollection values, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.EndsWith, values);
        }

        #endregion

        #region DoesNotEndWith Operators

        public TPredicate DoesNotEndWith(object value)
        {
            return Create(this, Comparison.DoesNotEndWith, value);
        }

        public TPredicate EndsWithNone(object value1, object value2)
        {
            return AndMultipleValues(Comparison.DoesNotEndWith, value1, value2);
        }

        public TPredicate EndsWithNone(object value1, object value2, object value3)
        {
            return AndMultipleValues(Comparison.DoesNotEndWith, value1, value2, value3);
        }

        public TPredicate EndsWithNone(ICollection values)
        {
            return AndMultipleValues(Comparison.DoesNotEndWith, values);
        }

        public TPredicate DoesNotEndWith(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.DoesNotEndWith, value);
        }

        public TPredicate EndsWithNone(object value1, object value2, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotEndWith, value1, value2);
        }

        public TPredicate EndsWithNone(object value1, object value2, object value3, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotEndWith, value1, value2, value3);
        }

        public TPredicate EndsWithNone(ICollection values, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotEndWith, values);
        }

        #endregion

        #region Contains Operators

        public TPredicate Contains(object value)
        {
            return Create(this, Comparison.Contains, value);
        }

        public TPredicate ContainsAny(object value1, object value2)
        {
            return OrMultipleValues(Comparison.Contains, value1, value2);
        }

        public TPredicate ContainsAny(object value1, object value2, object value3)
        {
            return OrMultipleValues(Comparison.Contains, value1, value2, value3);
        }

        public TPredicate ContainsAny(ICollection values)
        {
            return OrMultipleValues(Comparison.Contains, values);
        }

        public TPredicate Contains(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.Contains, value);
        }

        public TPredicate ContainsAny(object value1, object value2, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Contains, value1, value2);
        }

        public TPredicate ContainsAny(object value1, object value2, object value3, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Contains, value1, value2, value3);
        }

        public TPredicate ContainsAny(ICollection values, Ignore ignores)
        {
            return OrMultipleValues(ignores, Comparison.Contains, values);
        }
        
        public TPredicate ContainsAll(object value1, object value2)
        {
            return AndMultipleValues(Comparison.Contains, value1, value2);
        }

        public TPredicate ContainsAll(object value1, object value2, object value3)
        {
            return AndMultipleValues(Comparison.Contains, value1, value2, value3);
        }

        public TPredicate ContainsAll(ICollection values)
        {
            return AndMultipleValues(Comparison.Contains, values);
        }

        public TPredicate ContainsAll(object value1, object value2, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.Contains, value1, value2);
        }

        public TPredicate ContainsAll(object value1, object value2, object value3, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.Contains, value1, value2, value3);
        }

        public TPredicate ContainsAll(ICollection values, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.Contains, values);
        }

        #endregion

        #region DoesNotContain Operators

        public TPredicate DoesNotContain(object value)
        {
            return Create(this, Comparison.DoesNotContain, value);
        }

        public TPredicate ContainsNone(object value1, object value2)
        {
            return AndMultipleValues(Comparison.DoesNotContain, value1, value2);
        }

        public TPredicate ContainsNone(object value1, object value2, object value3)
        {
            return AndMultipleValues(Comparison.DoesNotContain, value1, value2, value3);
        }

        public TPredicate ContainsNone(ICollection values)
        {
            return AndMultipleValues(Comparison.DoesNotContain, values);
        }

        public TPredicate DoesNotContain(object value, Ignore ignores)
        {
            return IgnoreCondition(value, ignores) ? default(TPredicate) : Create(this, Comparison.DoesNotContain, value);
        }

        public TPredicate ContainsNone(object value1, object value2, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotContain, value1, value2);
        }

        public TPredicate ContainsNone(object value1, object value2, object value3, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotContain, value1, value2, value3);
        }

        public TPredicate ContainsNone(ICollection values, Ignore ignores)
        {
            return AndMultipleValues(ignores, Comparison.DoesNotContain, values);
        }

        #endregion

        #region Misc Operators

        public TPredicate IsGreaterThan(object value)
        {
            return Create(this, Comparison.Greater, value);
        }

        public TPredicate IsGreaterOrEqualTo(object value)
        {
            return Create(this, Comparison.GreaterOrEqual, value);
        }

        public TPredicate IsLessThan(object value)
        {
            return Create(this, Comparison.Less, value);
        }

        public TPredicate IsLessOrEqualTo(object value)
        {
            return Create(this, Comparison.LessOrEqual, value);
        }

        public TPredicate IsBetween(object lowValue, object highValue)
        {
            return Create(this, lowValue, highValue);
        }

        public TPredicate IsBetween(object lowValue, object highValue, Ignore ignores)
        {
            if (IgnoreCondition(lowValue, ignores) || IgnoreCondition(highValue, ignores))
                return default(TPredicate);

            return Create(this, lowValue, highValue);
        }

        public TPredicate IsTrue()
        {
            return Create(this, Comparison.Equal, true);
        }

        public TPredicate IsFalse()
        {
            return Create(this, Comparison.Equal, false);
        }

        public TPredicate IsNull()
        {
            return Create(this, Comparison.Equal, DBNull.Value);
        }

        public TPredicate IsNotNull()
        {
            return Create(this, Comparison.NotEqual, DBNull.Value);
        }

        #endregion

        #region Multiple Values

        private TPredicate OrMultipleValues(Comparison comparison, object value1, object value2)
        {
            return OrMultipleValues(Ignore.None, comparison, value1, value2);
        }

        private TPredicate OrMultipleValues(Ignore ignores, Comparison comparison, object value1, object value2)
        {
            bool ignoreFirst = IgnoreCondition(value1, ignores);
            bool ignoreSecond = IgnoreCondition(value2, ignores);

            if (!ignoreFirst && !ignoreSecond)
            {
                TPredicate columnPredicate = Create(this, comparison, value1);
                columnPredicate.Or(Create(this, comparison, value2));
                return columnPredicate;
            }

            if (ignoreFirst && !ignoreSecond)
                return Create(this, comparison, value2);

            return !ignoreFirst ? Create(this, comparison, value1) : default(TPredicate);
        }

        private TPredicate OrMultipleValues(Comparison comparison, object value1, object value2, object value3)
        {
            return OrMultipleValues(Ignore.None, comparison, value1, value2, value3);
        }

        private TPredicate OrMultipleValues(Ignore ignores, Comparison comparison, object value1, object value2, object value3)
        {
            bool ignoreFirst = IgnoreCondition(value1, ignores);
            bool ignoreSecond = IgnoreCondition(value2, ignores);
            bool ignoreThird = IgnoreCondition(value3, ignores);

            if (!ignoreFirst)
            {
                TPredicate columnPredicate = Create(this, comparison, value1);
                if (!ignoreSecond)
                    columnPredicate.Or(Create(this, comparison, value2));
                if (!ignoreThird)
                    columnPredicate.Or(Create(this, comparison, value3));
                return columnPredicate;
            }

            if (!ignoreSecond)
            {
                TPredicate columnPredicate = Create(this, comparison, value2);
                if (!ignoreThird)
                    columnPredicate.Or(Create(this, comparison, value3));
                return columnPredicate;
            }

            return !ignoreThird ? Create(this, comparison, value3) : default(TPredicate);
        }

        private TPredicate OrMultipleValues(Comparison comparison, params object[] values)
        {
            if (values == null || values.Length == 0)
                return default(TPredicate);

            TPredicate columnPredicate = Create(this, comparison, values[0]);
            for (int i = 1; i < values.Length; i++)
                columnPredicate.Or(Create(this, comparison, values[i]));

            return columnPredicate;
        }

        private TPredicate OrMultipleValues(Comparison comparison, ICollection values)
        {
            return OrMultipleValues(Ignore.None, comparison, values);
        }

        private TPredicate OrMultipleValues(Ignore ignores, Comparison comparison, ICollection values)
        {
            if (values == null || values.Count == 0)
                return default(TPredicate);

            ArrayList items = new ArrayList(values);

            TPredicate columnPredicate = default(TPredicate);
            if (!IgnoreCondition(items[0], ignores))
                columnPredicate = Create(this, comparison, items[0]);

            for (int i = 1; i < items.Count; i++)
            {
                if (!IgnoreCondition(items[i], ignores))
                    columnPredicate.Or(Create(this, comparison, items[i]));
            }

            return columnPredicate;
        }

        //--------------------------------------------------

        private TPredicate AndMultipleValues(Comparison comparison, object value1, object value2)
        {
            return AndMultipleValues(Ignore.None, comparison, value1, value2);
        }

        private TPredicate AndMultipleValues(Ignore ignores, Comparison comparison, object value1, object value2)
        {
            bool ignoreFirst = IgnoreCondition(value1, ignores);
            bool ignoreSecond = IgnoreCondition(value2, ignores);

            if (!ignoreFirst && !ignoreSecond)
            {
                TPredicate columnPredicate = Create(this, comparison, value1);
                columnPredicate.And(Create(this, comparison, value2));
                return columnPredicate;
            }

            if (ignoreFirst && !ignoreSecond)
                return Create(this, comparison, value2);

            return !ignoreFirst ? Create(this, comparison, value1) : default(TPredicate);
        }

        private TPredicate AndMultipleValues(Comparison comparison, object value1, object value2, object value3)
        {
            return AndMultipleValues(Ignore.None, comparison, value1, value2, value3);
        }

        private TPredicate AndMultipleValues(Ignore ignores, Comparison comparison, object value1, object value2, object value3)
        {
            bool ignoreFirst = IgnoreCondition(value1, ignores);
            bool ignoreSecond = IgnoreCondition(value2, ignores);
            bool ignoreThird = IgnoreCondition(value3, ignores);

            if (!ignoreFirst)
            {
                TPredicate columnPredicate = Create(this, comparison, value1);
                if (!ignoreSecond)
                    columnPredicate.And(Create(this, comparison, value2));
                if (!ignoreThird)
                    columnPredicate.And(Create(this, comparison, value3));
                return columnPredicate;
            }

            if (!ignoreSecond)
            {
                TPredicate columnPredicate = Create(this, comparison, value2);
                if (!ignoreThird)
                    columnPredicate.And(Create(this, comparison, value3));
                return columnPredicate;
            }

            return !ignoreThird ? Create(this, comparison, value3) : default(TPredicate);
        }

        private TPredicate AndMultipleValues(Comparison comparison, params object[] values)
        {
            if (values == null || values.Length == 0)
                return default(TPredicate);

            TPredicate columnPredicate = Create(this, comparison, values[0]);
            for (int i = 1; i < values.Length; i++)
                columnPredicate.And(Create(this, comparison, values[i]));

            return columnPredicate;
        }

        private TPredicate AndMultipleValues(Comparison comparison, ICollection values)
        {
            return AndMultipleValues(Ignore.None, comparison, values);
        }

        private TPredicate AndMultipleValues(Ignore ignores, Comparison comparison, ICollection values)
        {
            if (values == null || values.Count == 0)
                return default(TPredicate);

            ArrayList items = new ArrayList(values);

            TPredicate columnPredicate = default(TPredicate);
            if (!IgnoreCondition(items[0], ignores))
                columnPredicate = Create(this, comparison, items[0]);

            for (int i = 1; i < items.Count; i++)
            {
                if (!IgnoreCondition(items[i], ignores))
                    columnPredicate.And(Create(this, comparison, items[i]));
            }

            return columnPredicate;
        }

        #endregion

        #region IQueryableColumn Members

        public abstract TableData Table { get; set; }

        /// <summary>
        /// Gets the name of the column in the data source.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the alias, if any, that has been applied to this column.
        /// </summary>
        /// <remarks>
        /// Add an alias to this instance by calling <see cref="IQueryableColumn.As"/> and passing the value for the alias.
        /// </remarks>
        public abstract string Alias { get; }

        /// <summary>
        /// Gets a reference to an <see cref="IQueryableColumn"/> instance that represents a foreign key reference
        /// in the data source.
        /// </summary>
        public abstract IQueryableColumn ForeignKeyColumn { get; }

        public abstract IQueryableColumn LocalizedColumn { get; }

        public abstract IQueryableColumn LocalizeFallbackColumn { get; set; }

        public abstract ColumnProperties ColumnProperties { get; }

        /// <summary>
        /// Gets the data source <see cref="IQueryableColumn.DbType"/> of this instance.
        /// </summary>
        public abstract DbType DbType { get; }

        public abstract int MaxLength { get; }

        public abstract string ColumnFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string DefaultValue { get; }

        public bool HasDefaultValue
        {
            get { return !Compare.IsNullOrEmpty(DefaultValue); }
        }

        /// <summary>
        /// Returns a copy of this <see cref="IQueryableColumn"/> with the supplied <paramref name="columnAlias"/> applied.
        /// </summary>
        /// <param name="columnAlias">The alias to associate with this instance in the generated query.</param>
        /// <returns>An <see cref="IQueryableColumn"/> with the specified <paramref name="columnAlias"/> applied.</returns>
        public abstract IQueryableColumn As(string columnAlias);

        #endregion

        #region IEquatable<T> Implementation

        public bool Equals(ComparisonColumn<TPredicate> comparisonColumn)
        {
            return GetHashCode() != comparisonColumn.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as ComparisonColumn<TPredicate>);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Table.Datasource.GetHashCode(), 
                Table.Owner.GetHashCode(),
                Table.Name.GetHashCode(),
                Name.GetHashCode());
        }

        #endregion

        private static bool IgnoreCondition(object value, Ignore ignores)
        {
            if (((ignores & Ignore.Null) == Ignore.Null) && Equals(value, default(object)))
                return true;

            string emptyTest = value as string;
            if (emptyTest != null && ((ignores & Ignore.Empty) == Ignore.Empty) && Compare.IsNullOrEmpty(emptyTest))
                return true;

            DateTime minDateTest;
            bool isDateTime = DateTime.TryParse(emptyTest, out minDateTest);
            if (((ignores & Ignore.MinDate) == Ignore.MinDate) && (isDateTime && minDateTest == DateTime.MinValue))
                return true;

            double zeroTest;
            bool isNumber = double.TryParse(Convert.ToString(value), out zeroTest);
            //TODO [Brian,20140603] Make sure this floating point comparison is needed and accurate enough.
            if (((ignores & Ignore.Zero) == Ignore.Zero) && (isNumber && Math.Abs(zeroTest) < .1))
                return true;

            if (value is DateTime)
            {
                if (((ignores & Ignore.MinDate) == Ignore.MinDate) && (Convert.ToDateTime(value) == DateTime.MinValue))
                    return true;
            }

            return false;
        }
    }
}
