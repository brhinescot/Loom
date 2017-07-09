#region Using Directives

using System;
using System.Globalization;
using System.Runtime.InteropServices;

#endregion

namespace Loom
{
    /************************************************************************************
     * FEATURE: Add support for setting first day of the week to Week class.
     ************************************************************************************/

    /// <summary>
    ///     Represents a calendar week and the days contained within it.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Week : IFormattable, IEquatable<Week>, IComparable<Week>
    {
        #region Default Values

        /// <summary>
        ///     Represents the smallest possible value of <see cref="Week" />. This field
        ///     is read only.
        /// </summary>
        public static readonly Week MinValue = new Week(DateTime.MinValue);

        /// <summary>
        ///     Represents the largest possible value of <see cref="Week" />. This field
        ///     is read only.
        /// </summary>
        public static readonly Week MaxValue = new Week(DateTime.MaxValue.AddDays(-5));

        #endregion

        #region Type Fields

        private const string DefaultSeparator = "-";
        private const string DefaultFormat = "{0} {1} {2}";

        #endregion

        #region Instance Fields

        #endregion

        #region Property Accessors

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Monday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="DateTime" /> set to Monday's date.
        /// </value>
        public DateTime Monday { get; }

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Tuesday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="DateTime" /> set to Tuesday's date.
        /// </value>
        public DateTime Tuesday => Monday.AddDays(1);

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Wednesday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="System.DateTime" /> set to Wednesday's date.
        /// </value>
        public DateTime Wednesday => Monday.AddDays(2);

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Thursday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="DateTime" /> set to Thursday's date.
        /// </value>
        public DateTime Thursday => Monday.AddDays(3);

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Friday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="DateTime" /> set to Friday's date.
        /// </value>
        public DateTime Friday => Monday.AddDays(4);

        /// <summary>
        ///     Gets a <see cref="DateTime" /> representing Saturday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="DateTime" /> set to Saturday's date.
        /// </value>
        public DateTime Saturday => Monday.AddDays(5);

        /// <summary>
        ///     Gets a <see cref="System.DateTime" /> representing Sunday of this week.
        /// </summary>
        /// <value>
        ///     A <see cref="System.DateTime" /> set to Sunday's date.
        /// </value>
        public DateTime Sunday => Monday.AddDays(6);

        #endregion

        #region .ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Week" /> structure.
        /// </summary>
        /// <remarks>
        ///     The <see cref="Week" /> structure will be the calendar week that contains
        ///     the <see cref="System.DateTime" /> parameter.
        /// </remarks>
        /// <param name="dateInWeek">
        ///     A <see cref="DateTime" /> that is contained
        ///     in the calendar week that will be represented by the <see cref="Week" />
        ///     structure.
        /// </param>
        public Week(DateTime dateInWeek)
        {
            int offsetDays;

            // If dateInWeek is Sunday, set offsetDays to -6 to find previous Monday
            // If not, determine offset to find Monday's date
            //
            if (dateInWeek.DayOfWeek == DayOfWeek.Sunday)
                offsetDays = -6;
            else
                offsetDays = (Convert.ToInt32(dateInWeek.DayOfWeek, CultureInfo.InvariantCulture) - 1) * -1;

            Monday = dateInWeek.AddDays(offsetDays).Date;
        }

        #endregion

        #region Factories

        /// <summary>
        ///     Converts the string representation of a date to its <see cref="Week" />
        ///     equivalent object.
        /// </summary>
        /// <param name="s">A string containing a day in the week to convert.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="s" /> is null.
        /// </exception>
        public static Week Parse(string s)
        {
            Argument.Assert.IsNotNullOrEmpty(s, nameof(s));

            return new Week(DateTime.Parse(s, CultureInfo.CurrentCulture));
        }

        /// <summary>
        ///     Converts the string representation of a date to its <see cref="Week" />
        ///     equivalent object.
        /// </summary>
        /// <param name="s">A string containing a day in the week to convert.</param>
        /// <param name="cultureInfo">
        ///     The <see cref="CultureInfo" /> to use when parsing the date.
        /// </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="s" /> is null.
        /// </exception>
        public static Week Parse(string s, CultureInfo cultureInfo)
        {
            Argument.Assert.IsNotNullOrEmpty(s, nameof(s));

            return new Week(DateTime.Parse(s, cultureInfo));
        }

        /// <summary>
        ///     Gets the <see cref="Week" /> that contains the current local date and time on this computer.
        /// </summary>
        /// <value>
        ///     A <see cref="Week" /> whose value represents the current week.
        /// </value>
        public static Week ThisWeek => new Week(DateTime.Today);

        public static Week NewWeek(DateTime dateInWeek)
        {
            return new Week(dateInWeek);
        }

        public static Week NewIsoWeek(DateTime dateInWeek)
        {
            return new Week(dateInWeek);
        }

        #endregion

        /// <summary>
        ///     Adds the specified number of weeks to the value of this instance.
        /// </summary>
        /// <remarks>
        ///     This method does not change the value of this <see cref="Week" />.
        ///     Instead, a new <see cref="Week" /> is returned whose value is the
        ///     result of this operation.
        /// </remarks>
        /// <param name="value">
        ///     A number of whole weeks. The <paramref name="value" />
        ///     parameter can be positive or negative.
        /// </param>
        /// <returns>
        ///     A <see cref="Week" /> whose value is the sum of the week
        ///     represented by this instance and the number of weeks represented by
        ///     <i>value</i>.
        /// </returns>
        public Week AddWeeks(int value)
        {
            return value == 0 ? ThisWeek : new Week(Monday.AddDays(value * 7));
        }

        /// <summary>
        ///     Returns an array of <see cref="System.DateTime" /> objects representing the
        ///     days in this week.
        /// </summary>
        public DateTime[] ToArray()
        {
            return new[] {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday};
        }

        #region ToString Methods

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
        ///     by <paramref name="formatProvider" />.
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

        #region IFormattable Members

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
        ///     <i>format</i> and
        ///     <i>
        ///         <paramref name="formatProvider" />
        ///     </i>
        ///     .
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format, formatProvider, DefaultSeparator);
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
        ///     <i>format</i> and
        ///     <i>
        ///         <paramref name="formatProvider" />
        ///     </i>
        ///     .
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider, string separator)
        {
            return string.Format(formatProvider, DefaultFormat,
                Monday.ToString(format, formatProvider),
                separator,
                Sunday.ToString(format, formatProvider));
        }

        #endregion

        #endregion

        #region Operator Overloads

        /// <summary>
        /// </summary>
        /// <param name="week1"></param>
        /// <param name="week2"></param>
        /// <returns></returns>
        public static bool operator ==(Week week1, Week week2)
        {
            return week1.Equals(week2);
        }

        /// <summary>
        /// </summary>
        /// <param name="week1"></param>
        /// <param name="week2"></param>
        /// <returns></returns>
        public static bool operator !=(Week week1, Week week2)
        {
            return !week1.Equals(week2);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(Week x, Week y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(Week x, Week y)
        {
            return x.CompareTo(y) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(Week x, Week y)
        {
            return x.CompareTo(y) <= 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(Week x, Week y)
        {
            return x.CompareTo(y) >= 0;
        }

        #endregion

        #region IComparable<Week> Members

        /// <summary>
        ///     Compares this instance to a specified object and returns an indication
        ///     of their relative values.
        /// </summary>
        /// <param name="other">
        ///     An object to compare, or a <null />.
        /// </param>
        /// <returns>
        ///     A signed number indicating the relative values of this instance
        ///     and value.
        /// </returns>
        public int CompareTo(Week other)
        {
            return Monday.CompareTo(other.Monday);
        }

        #endregion

        #region IEquatable<Week> Members

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        ///     true if obj and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Week))
                return false;

            return Equals((Week) obj);
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Week other)
        {
            return Monday == other.Monday;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return Monday.GetHashCode();
        }

        #endregion
    }
}