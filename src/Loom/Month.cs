#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

#endregion

namespace Loom
{
    /// <summary>
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Month : IFormattable, IEquatable<Month>, IComparable<Month>, IEnumerable<Week>
    {
        #region Instance Fields

        #endregion

        private const string DefaultSeperator = "-";
        private const string DefaultFormat = "{0} {1} {2}";

        #region Property Accessors

        /// <summary>
        ///     Gets the first day of this month represented as a <see cref="DateTime" />.
        /// </summary>
        /// <value>The first day.</value>
        public DateTime FirstDay { get; }

        /// <summary>
        ///     Gets the first day of this month represented as a <see cref="DateTime" />.
        /// </summary>
        /// <value>The first day.</value>
        public DateTime LastDay { get; }

        #endregion

        /// <summary>
        ///     Converts the string representation of a date to its <see cref="Month" />
        ///     equivalent object.
        /// </summary>
        /// <param name="s">A string containing a day in the month to convert.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="s" /> is null.
        /// </exception>
        public static Month Parse(string s)
        {
            Argument.Assert.IsNotNullOrEmpty(s, nameof(s));
            return new Month(DateTime.Parse(s, CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static Month ThisMonth => new Month(DateTime.Today);

        #region IFormattable Implementation

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <returns>A string representation of value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <param name="formatProvider">
        ///     An <see cref="IFormatProvider" /> that supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <returns>
        ///     A string representation of value of this instance as specified
        ///     by <i>formatProvider</i>.
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <returns>
        ///     A string representation of value of this instance as specified
        ///     by <i>format</i>.
        /// </returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="separator">A string used to separate the dates in the date span.</param>
        /// <returns>
        ///     A string representation of value of this instance as specified
        ///     by <i>format</i>.
        /// </returns>
        public string ToString(string format, string separator)
        {
            return ToString(format, CultureInfo.CurrentCulture, separator);
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="formatProvider">
        ///     An <see cref="IFormatProvider" /> that supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <returns>
        ///     A string representation of value of this instance as specified by
        ///     <i>format</i> and <i>formatProvider</i>.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format, formatProvider, DefaultSeperator);
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation using
        ///     the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="formatProvider">
        ///     An <see cref="IFormatProvider" /> that supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <param name="separator">A string used to separate the dates in the date span.</param>
        /// <returns>
        ///     A string representation of value of this instance as specified by
        ///     <paramref name="format" /> and <paramref name="formatProvider" />.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider, string separator)
        {
            return string.Format(formatProvider, DefaultFormat,
                FirstDay.ToString(format, formatProvider),
                separator,
                LastDay.ToString(format, formatProvider));
        }

        #endregion

        #region IEquatable<Month> Implementation

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Month other)
        {
            return FirstDay == other.FirstDay;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="Object"></see> is equal to
        ///     the current <see cref="Object"></see>.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="Object"></see> to compare with the
        ///     current <see cref="Object"></see>.
        /// </param>
        /// <returns>
        ///     true if the specified <see cref="Object"></see> is equal to the current
        ///     <see cref="Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Month))
                return false;

            return Equals((Month) obj);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return FirstDay.GetHashCode();
        }

        #endregion

        #region IComparable<Month> Implementation

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relative order of the objects being compared.
        ///     The return value has the following meanings: Value Meaning Less than zero This object is
        ///     less than the other parameter.Zero This object is equal to other. Greater than zero This
        ///     object is greater than other.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Month other)
        {
            return FirstDay.CompareTo(other.FirstDay);
        }

        #endregion

        #region .ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Month" /> class.
        /// </summary>
        /// <param name="dateInMonth">The date in month.</param>
        public Month(DateTime dateInMonth) : this()
        {
            FirstDay = dateInMonth.AddDays(-(dateInMonth.Day - 1));
            LastDay = FirstDay.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Month" /> class.
        /// </summary>
        /// <param name="weekInMonth">The week in month.</param>
        public Month(Week weekInMonth) : this(weekInMonth.Monday) { }

        #endregion

        public Week[] ToArray()
        {
            return new List<Week>(this).ToArray();
        }

        #region IEnumerable<Week> Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<Week> IEnumerable<Week>.GetEnumerator()
        {
            DateTime currentDay = FirstDay;
            yield return new Week(currentDay);

            currentDay = currentDay.AddDays(7);
            while (true)
            {
                Week week = new Week(currentDay);
                if (week.Monday.Day < 7 && (week.Monday.Month > FirstDay.Month || week.Monday.Year != FirstDay.Year))
                    break;

                yield return week;
                currentDay = currentDay.AddDays(7);
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<Week>) this).GetEnumerator();
        }

        #endregion
    }
}