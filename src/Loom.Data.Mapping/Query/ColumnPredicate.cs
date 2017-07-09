#region Using Directives

using System;
using System.Diagnostics;
using System.Globalization;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    [DebuggerDisplay("{Column.Table.Name, nq}.{Column.Name, nq} {Comparison} {Value}, OrNextPredicate={OrNextPredicate}")]
    public class ColumnPredicate : IPredicate<ColumnPredicate>, IBindablePredicate, IEquatable<ColumnPredicate>
    {
        private readonly IConvertible convertibleValue;
        private readonly IConvertible convertibleValue2;

        public ColumnPredicate(IQueryableColumn column, Comparison comparison, object value)
        {
            //BUG: What if parameter value is not a supported SqlParameter type.
            if (comparison == Comparison.Between)
                throw new NotSupportedException("Between comparison requires two values. Use the ColumnPredicate(IQueryableColumn, object, object) overload.");

            Comparison = comparison;
            Column = column;
            Value = value;
            convertibleValue = Value as IConvertible;
        }

        public ColumnPredicate(IQueryableColumn column, object value, object value2)
        {
            Comparison = Comparison.Between;
            Column = column;
            Value = value;
            Value2 = value2;
            convertibleValue = Value as IConvertible;
            convertibleValue2 = Value2 as IConvertible;
        }

        public IQueryableColumn Column { get; }

        public object Value { get; }

        public object Value2 { get; }

        public Comparison Comparison { get; }

        public ColumnPredicate NextInGroup { get; private set; }

        public bool IsGroup => NextInGroup != null;

        public bool IsJoinPredicate => Value is IQueryableColumn;

        #region IBindablePredicate Members

        void IBindablePredicate.BindWhere(ICommandBuilder builder)
        {
            builder.AppendFrom(Column.Table);

            if (IsJoinPredicate)
                builder.JoinPredicates.Add(this);
            else
                builder.WherePredicates.Add(this);
        }

        void IBindablePredicate.BindHaving(ICommandBuilder builder)
        {
            builder.AppendFrom(Column.Table);

            builder.HavingPredicates.Add(this);
        }

        #endregion

        #region IEquatable<ColumnPredicate> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="obj">An object to compare with this object.</param>
        public bool Equals(ColumnPredicate obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj.Column, Column) &&
                   Equals(obj.Value, Value) &&
                   Equals(obj.Value2, Value2) &&
                   Equals(obj.Comparison, Comparison) &&
                   Equals(obj.NextInGroup, NextInGroup) &&
                   obj.OrNextPredicate.Equals(OrNextPredicate) &&
                   obj.OrToPreviousGroup.Equals(OrToPreviousGroup);
        }

        #endregion

        #region IPredicate<ColumnPredicate> Members

        public bool OrNextPredicate { get; set; }

        public bool OrToPreviousGroup { get; set; }

        /// <exception cref="ArgumentException">
        ///     Only one instance of each <see cref="ColumnPredicate" />
        ///     can exist in a predicate group.
        /// </exception>
        public ColumnPredicate Or(ColumnPredicate predicate)
        {
            if (ReferenceEquals(this, predicate))
                throw new ArgumentException("Only one instance of each ColumnPredicate can exist in a predicate group.");

            ColumnPredicate last = this;
            while (last.NextInGroup != null)
            {
                if (last.NextInGroup == predicate)
                    throw new ArgumentException("Only one instance of each ColumnPredicate can exist in a predicate group.");

                last = last.NextInGroup;
            }

            last.NextInGroup = predicate;
            last.OrNextPredicate = true;
            return this;
        }

        /// <exception cref="ArgumentException">
        ///     Only one instance of each <see cref="ColumnPredicate" />
        ///     can exist in a predicate group.
        /// </exception>
        public ColumnPredicate And(ColumnPredicate predicate)
        {
            if (this == predicate)
                throw new ArgumentException("Only one instance of each ColumnPredicate can exist in a predicate group.");

            ColumnPredicate last = this;
            while (last.NextInGroup != null)
            {
                if (last.NextInGroup == predicate)
                    throw new ArgumentException("Only one instance of each ColumnPredicate can exist in a predicate group.");

                last = last.NextInGroup;
            }

            last.NextInGroup = predicate;
            last.OrNextPredicate = false;
            return this;
        }

        #endregion

        public JoinPredicate AsJoin()
        {
            if (!IsJoinPredicate)
                throw new InvalidOperationException();

            JoinPredicate predicate = new JoinPredicate(Column, Comparison, (IQueryableColumn) Value);
            predicate.OrNextPredicate = OrNextPredicate;
            predicate.NextInGroup = NextInGroup == null ? null : NextInGroup.ToExplicitJoinPredicate();

            return predicate;
        }

        private JoinPredicate ToExplicitJoinPredicate()
        {
            if (Value != null && !IsJoinPredicate)
                throw new InvalidOperationException();

            JoinPredicate predicate = new JoinPredicate(Column, Comparison, (IQueryableColumn) Value);
            predicate.OrNextPredicate = OrNextPredicate;
            predicate.NextInGroup = NextInGroup;

            return predicate;
        }

        public static implicit operator JoinPredicate(ColumnPredicate q)
        {
            return q == null ? null : q.AsJoin();
        }

        public static ColumnPredicate operator |(ColumnPredicate comparison1, ColumnPredicate comparison2)
        {
            return comparison1 == null ? comparison2 : comparison1.Or(comparison2);
        }

        public static ColumnPredicate operator &(ColumnPredicate comparison1, ColumnPredicate comparison2)
        {
            return comparison1 == null ? comparison2 : comparison1.And(comparison2);
        }

        public bool Evaluate(object compare)
        {
            bool result = false;
            switch (Comparison)
            {
                case Comparison.Equal:
                    result = CompareToFirstValue(compare) == 0;
                    break;
                case Comparison.NotEqual:
                    result = CompareToFirstValue(compare) != 0;
                    break;
                case Comparison.Greater:
                    result = CompareToFirstValue(compare) < 0;
                    break;
                case Comparison.GreaterOrEqual:
                    result = CompareToFirstValue(compare) <= 0;
                    break;
                case Comparison.Less:
                    result = CompareToFirstValue(compare) > 0;
                    break;
                case Comparison.LessOrEqual:
                    result = CompareToFirstValue(compare) >= 0;
                    break;
                case Comparison.Between:
                    result = CompareToSecondValue(compare) >= 0 && CompareToFirstValue(compare) <= 0;
                    break;
                case Comparison.True:
                    if (compare is bool)
                        result = (bool) compare;
                    break;
                case Comparison.False:
                    if (compare is bool)
                        result = !(bool) compare;
                    break;
                case Comparison.StartsWith:
                    result = EvalStartsWith(compare);
                    break;
                case Comparison.EndsWith:
                    result = EvalEndsWith(compare);
                    break;
                case Comparison.Contains:
                    result = EvalContains(compare);
                    break;
                case Comparison.DoesNotStartWith:
                    result = !EvalStartsWith(compare);
                    break;
                case Comparison.DoesNotEndWith:
                    result = !EvalEndsWith(compare);
                    break;
                case Comparison.DoesNotContain:
                    result = !EvalContains(compare);
                    break;
            }

            if (NextInGroup == null)
                return result;
            if (OrNextPredicate)
                return result || NextInGroup.Evaluate(compare);
            return result && NextInGroup.Evaluate(compare);
        }

        /// <exception cref="ArgumentNullException">compare is null.</exception>
        private int CompareToFirstValue(object compare)
        {
            return CompareValues(compare, Value, convertibleValue);
        }

        private int CompareToSecondValue(object compare)
        {
            return CompareValues(compare, Value2, convertibleValue2);
        }

        /// <exception cref="ArgumentNullException">compare is null.</exception>
        private static int CompareValues(object compare, object value, IConvertible valueConvertible)
        {
            if (compare == null && value == null)
                return 0;

            IConvertible compareConvertible = compare as IConvertible;
            if (compareConvertible != null && valueConvertible != null)
                return CompareConvertible(compareConvertible, valueConvertible);

            IComparable comparable = compare as IComparable;
            if (comparable != null)
                return comparable.CompareTo(value);

            if (compare == null)
                return 1;

            throw new ArgumentException("Predicate evaluation failed. The argument must be of type IComparable.");
        }

        private static int CompareConvertible(IConvertible comparisonValue, IConvertible predicateValue)
        {
//            try
//            {
            TypeCode comparisonType = comparisonValue.GetTypeCode();
            switch (predicateValue.GetTypeCode())
            {
                case TypeCode.DBNull:
                    return comparisonType == TypeCode.DBNull ? 0 : 1;
                case TypeCode.Boolean:
                    if (comparisonType != TypeCode.Boolean)
                        return 1;
                    return predicateValue.ToBoolean(CultureInfo.CurrentCulture)
                        .CompareTo(comparisonValue.ToBoolean(CultureInfo.CurrentCulture));
                case TypeCode.Char:
                    return predicateValue.ToChar(CultureInfo.CurrentCulture)
                        .CompareTo(comparisonValue.ToChar(CultureInfo.CurrentCulture));
                case TypeCode.SByte:
                    return predicateValue.ToSByte(CultureInfo.CurrentCulture)
                        .CompareTo(comparisonValue.ToSByte(CultureInfo.CurrentCulture));
                case TypeCode.Byte:
                    return predicateValue.ToByte(CultureInfo.CurrentCulture)
                        .CompareTo(comparisonValue.ToByte(CultureInfo.CurrentCulture));
                case TypeCode.Int16:
                    return predicateValue.ToInt16(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToInt16(NumberFormatInfo.CurrentInfo));
                case TypeCode.UInt16:
                    return predicateValue.ToUInt16(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToUInt16(NumberFormatInfo.CurrentInfo));
                case TypeCode.Int32:
                    return predicateValue.ToInt32(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToInt32(NumberFormatInfo.CurrentInfo));
                case TypeCode.UInt32:
                    return predicateValue.ToUInt32(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToUInt32(NumberFormatInfo.CurrentInfo));
                case TypeCode.Int64:
                    return predicateValue.ToInt64(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToInt64(NumberFormatInfo.CurrentInfo));
                case TypeCode.UInt64:
                    return predicateValue.ToUInt64(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToUInt64(NumberFormatInfo.CurrentInfo));
                case TypeCode.Single:
                    return predicateValue.ToSingle(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToSingle(NumberFormatInfo.CurrentInfo));
                case TypeCode.Double:
                    return predicateValue.ToDouble(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToDouble(NumberFormatInfo.CurrentInfo));
                case TypeCode.Decimal:
                    return predicateValue.ToDecimal(NumberFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToDecimal(NumberFormatInfo.CurrentInfo));
                case TypeCode.DateTime:
                    return predicateValue.ToDateTime(DateTimeFormatInfo.CurrentInfo)
                        .CompareTo(comparisonValue.ToDateTime(DateTimeFormatInfo.CurrentInfo));
                default:
                    return string.CompareOrdinal(predicateValue.ToString(CultureInfo.CurrentCulture), comparisonValue.ToString(CultureInfo.CurrentCulture));
            }
//            }
//            catch (FormatException)
//            {
//                return 1;
//            }
        }

        private bool EvalContains(object compare)
        {
            if (compare == null || Value == null)
                return false;

            return Convert.ToString(compare).Contains(Convert.ToString(Value));
        }

        private bool EvalStartsWith(object compare)
        {
            if (compare == null || Value == null)
                return false;

            return Convert.ToString(compare).StartsWith(Convert.ToString(Value));
        }

        private bool EvalEndsWith(object compare)
        {
            if (compare == null || Value == null)
                return false;

            return Convert.ToString(compare).StartsWith(Convert.ToString(Value));
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
        ///     <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == typeof(ColumnPredicate) && Equals((ColumnPredicate) obj);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = Column != null ? Column.GetHashCode() : 0;
                result = (result * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                result = (result * 397) ^ (Value2 != null ? Value2.GetHashCode() : 0);
                result = (result * 397) ^ Comparison.GetHashCode();
                result = (result * 397) ^ (NextInGroup != null ? NextInGroup.GetHashCode() : 0);
                result = (result * 397) ^ OrNextPredicate.GetHashCode();
                result = (result * 397) ^ OrToPreviousGroup.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(ColumnPredicate left, ColumnPredicate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ColumnPredicate left, ColumnPredicate right)
        {
            return !Equals(left, right);
        }
    }
}