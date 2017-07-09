#region Using Directives

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom
{
    /// <summary>
    ///     Represents a phone number.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct PhoneNumber : IEquatable<PhoneNumber>, IComparable<PhoneNumber>, IFormattable, IConvertible
    {
        #region Type Fields

        private const string BasicFormat = "({0:000}) {1:000}-{2:0000}";
        private const string BasicFormatPlusExtension = "({0:000}) {1:000}-{2:0000} x{3}";
        private const string InternationalFormat = "+{0} ({1:000}) {2:000}-{3:0000}";
        private const string InternationalFormatPlusExtension = "+{0} ({1:000}) {2:000}-{3:0000} x{4}";
        private static readonly Regex PhoneParser = new Regex(SR.ExpressionPhoneNumber, RegexOptions.Compiled);
        private static readonly Regex AllNumbers = new Regex(@"^\d+$", RegexOptions.Compiled);

        #endregion

        #region Instance Fields

        private short countryCode;
        private short areaCode;
        private short exchange;
        private short number;

        #endregion

        /// <summary>
        ///     Represents an empty phone number. This field is read only.
        /// </summary>
        public static readonly PhoneNumber Empty = new PhoneNumber(0, 0, 0, 0, 0);

        #region Property Accessors

        /// <summary>
        ///     Gets the country code.
        /// </summary>
        /// <value>The country code.</value>
        /// <remarks>
        ///     Use the <see cref="ToString()" /> overloads for more
        ///     formatting options.
        /// </remarks>
        public int CountryCode => countryCode;

        /// <summary>
        ///     Gets the area code.
        /// </summary>
        /// <value>The area code.</value>
        /// <remarks>
        ///     Use the <see cref="ToString()" /> overloads for more
        ///     formatting options.
        /// </remarks>
        public int AreaCode => areaCode;

        /// <summary>
        ///     Gets the first three numbers of the phone number after the area code.
        /// </summary>
        /// <value>The exchange.</value>
        /// <remarks>
        ///     Use the <see cref="ToString()" /> overloads for more
        ///     formatting options.
        /// </remarks>
        public int Exchange => exchange;

        /// <summary>
        ///     Gets the last four digits of the phone number.
        /// </summary>
        /// <value>The number.</value>
        /// <remarks>
        ///     Use the <see cref="ToString()" /> overloads for more
        ///     formatting options.
        /// </remarks>
        public int Number => number;

        /// <summary>
        ///     Gets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public int Extension { get; private set; }

        #endregion

        #region .ctor

        private PhoneNumber(short countryCode, short areaCode, short exchange, short number, int extension)
        {
            this.exchange = exchange;
            this.number = number;
            this.countryCode = countryCode;
            this.areaCode = areaCode;
            Extension = extension;
        }

        #endregion

        #region Parse Methods

        /// <summary>
        ///     Converts the string representation of a phone number to its
        ///     <see cref="PhoneNumber" />
        ///     equivalent object.
        /// </summary>
        /// <param name="phoneNumber">
        ///     A string containing a phone number to
        ///     convert.
        /// </param>
        /// <param name="result">
        ///     A new instance of a <see cref="PhoneNumber" />
        ///     object set to the
        ///     value specified. If the return value is false, the result is set
        ///     to <see cref="PhoneNumber.Empty" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if successful; otherwise
        ///     <see langword="false" />.
        /// </returns>
        public static bool TryParse(string phoneNumber, out PhoneNumber result)
        {
            if (phoneNumber == null)
            {
                result = Empty;
                return false;
            }

            phoneNumber = Normalize(phoneNumber);
            Match match = PhoneParser.Match(phoneNumber);

            if (match.Groups.Count < 5)
            {
                result = Empty;
                return false;
            }

            if (match.Success)
            {
                result = new PhoneNumber();
                if (match.Groups[1].Value.Length > 0)
                    result.countryCode = Convert.ToInt16(match.Groups[1].Value);
                if (match.Groups[2].Value.Length > 0)
                    result.areaCode = Convert.ToInt16(match.Groups[2].Value);
                if (match.Groups[3].Value.Length > 0)
                    result.exchange = Convert.ToInt16(match.Groups[3].Value);
                if (match.Groups[4].Value.Length > 0)
                    result.number = Convert.ToInt16(match.Groups[4].Value);
                if (match.Groups[5].Value.Length > 0)
                    result.Extension = Convert.ToInt32(match.Groups[5].Value);
            }
            else
            {
                result = Empty;
            }

            return match.Success;
        }

        /// <summary>
        ///     Converts the string representation of a phone number to its
        ///     <see cref="PhoneNumber" />
        ///     equivalent object.
        /// </summary>
        /// <param name="phoneNumber">
        ///     A string containing a phone number
        ///     to convert.
        /// </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="phoneNumber" />
        ///     is null.
        /// </exception>
        /// <exception cref="FormatException">
        ///     <paramref name="phoneNumber" /> is not a
        ///     phone number in a valid format.
        /// </exception>
        public static PhoneNumber Parse(string phoneNumber)
        {
            Argument.Assert.IsNotNull(phoneNumber, nameof(phoneNumber));

            phoneNumber = Normalize(phoneNumber);

            Match match = PhoneParser.Match(phoneNumber);
            if (!match.Success)
                throw new FormatException(SR.ExceptionNonParsablePhoneNumber);

            if (match.Groups.Count < 5)
                throw new FormatException(SR.ExceptionNonParsablePhoneNumber);

            PhoneNumber result = new PhoneNumber();
            if (match.Groups[1].Value.Length > 0)
                result.countryCode = Convert.ToInt16(match.Groups[1].Value);
            if (match.Groups[2].Value.Length > 0)
                result.areaCode = Convert.ToInt16(match.Groups[2].Value);
            if (match.Groups[3].Value.Length > 0)
                result.exchange = Convert.ToInt16(match.Groups[3].Value);
            if (match.Groups[4].Value.Length > 0)
                result.number = Convert.ToInt16(match.Groups[4].Value);
            if (match.Groups[5].Value.Length > 0)
                result.Extension = Convert.ToInt32(match.Groups[5].Value);
            return result;
        }

        private static string Normalize(string phoneNUmber)
        {
            if (!AllNumbers.IsMatch(phoneNUmber))
                return phoneNUmber;

            if (phoneNUmber.Length >= 7)
            {
                const string separator = "-";
                return phoneNUmber.Insert(6, separator).Insert(3, separator);
            }

            return phoneNUmber;
        }

        #endregion

        #region ToString and Formatting

        /// <summary>
        ///     Returns a <see cref="String" /> that represents the current
        ///     <see cref="PhoneNumber" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="String" /> that represents the current <see cref="PhoneNumber" />.
        /// </returns>
        public override string ToString()
        {
            if (this == Empty)
                return string.Empty;

            if (countryCode == 0)
                return Extension == 0 ? string.Format(BasicFormat, areaCode, exchange, number) : string.Format(BasicFormatPlusExtension, areaCode, exchange, number, Extension);
            return Extension == 0 ? string.Format(InternationalFormat, countryCode, areaCode, exchange, number) : string.Format(InternationalFormatPlusExtension, countryCode, areaCode, exchange, number, Extension);
        }

        /// <summary>
        ///     Returns a <see cref="String" /> that represents the current
        ///     <see cref="PhoneNumber" />.
        /// </summary>
        /// <param name="format">The string format to use such as {AreaCode}){Exchange}-{Number}.</param>
        /// <returns>
        ///     A <see cref="String" /> that represents the current <see cref="PhoneNumber" />.
        /// </returns>
        public string ToString(string format)
        {
            return format == null ? ToString() : ToString(format, null);
        }

        /// <summary>
        ///     Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>
        ///     A <see cref="String"></see> containing the value of the current instance in the specified format.
        /// </returns>
        /// <param name="format">
        ///     The <see cref="String"></see> specifying the format to use.-or- null to use the
        ///     default format defined for the type of the <see cref="IFormattable"></see>
        ///     implementation.
        /// </param>
        /// <param name="formatProvider">
        ///     The <see cref="IFormatProvider"></see> to use to format the value.-or-
        ///     null to obtain the numeric format information from the current locale setting of the operating
        ///     system.
        /// </param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return FormattableObject.ToString(this, format, formatProvider);
        }

        #endregion

        #region Operator Overloads

        public static bool operator ==(PhoneNumber number1, PhoneNumber number2)
        {
            return number1.Equals(number2);
        }

        public static bool operator !=(PhoneNumber number1, PhoneNumber number2)
        {
            return !number1.Equals(number2);
        }

        public static bool operator <(PhoneNumber x, PhoneNumber y)
        {
            return x.CompareTo(y) < 0;
        }

        public static bool operator >(PhoneNumber x, PhoneNumber y)
        {
            return x.CompareTo(y) > 0;
        }

        public static bool operator <=(PhoneNumber x, PhoneNumber y)
        {
            return x.CompareTo(y) <= 0;
        }

        public static bool operator >=(PhoneNumber x, PhoneNumber y)
        {
            return x.CompareTo(y) >= 0;
        }

        #endregion

        public static explicit operator string(PhoneNumber number)
        {
            return number.ToString();
        }

        #region Int64 Conversions

        public static explicit operator long(PhoneNumber number)
        {
            return number.ToInt64();
        }

        public static implicit operator PhoneNumber(long number)
        {
            return FromInt64(number);
        }

        /// <summary>
        ///     Converts a <see cref="PhoneNumber" /> instance to an <see cref="Int64" /> representation
        ///     without the extension.
        /// </summary>
        /// <remarks>
        ///     If the phone number included an extension it will not be included in the conversion.
        ///     Use <see cref="Extension" /> to retrieve the extension.
        /// </remarks>
        /// <returns>
        ///     An <see cref="Int64" /> representing this <see cref="PhoneNumber" /> instance.
        /// </returns>
        public long ToInt64()
        {
            StringBuilder builder = new StringBuilder(20);
            if (countryCode > 0)
                builder.Append(countryCode);
            if (areaCode > 0)
                builder.Append(areaCode);
            if (exchange > 0)
                builder.Append(exchange);
            if (number > 0)
                builder.Append(number);

            return Convert.ToInt64(builder.ToString());
        }

        /// <summary>
        ///     Converts an <see cref="Int64" /> to a <see cref="PhoneNumber" />
        /// </summary>
        /// <param name="number">
        ///     The number to convert to a <see cref="PhoneNumber" />
        /// </param>
        /// <returns>
        ///     A <see cref="PhoneNumber" />.
        /// </returns>
        public static PhoneNumber FromInt64(long number)
        {
            PhoneNumber phoneNumber;
            if (!TryParse(number.ToString(CultureInfo.InvariantCulture), out phoneNumber))
                throw new InvalidCastException(string.Format("Unable to cast the value {0} to the type PhoneNumber.", number));

            return phoneNumber;
        }

        #endregion

        #region Object Equality Overrides

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        ///     true if obj and this instance are the same type and represent the same
        ///     value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is PhoneNumber && Equals((PhoneNumber) obj);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = countryCode.GetHashCode();
                result = (result * 397) ^ areaCode.GetHashCode();
                result = (result * 397) ^ exchange.GetHashCode();
                result = (result * 397) ^ number.GetHashCode();
                result = (result * 397) ^ Extension;
                return result;
            }
        }

        #endregion

        #region IEquatable<PhoneNumber> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of
        ///     the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the other parameter; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(PhoneNumber other)
        {
            return other.countryCode == countryCode && other.areaCode == areaCode && other.exchange == exchange && other.number == number && other.Extension == Extension;
        }

        #endregion

        #region IComparable<PhoneNumber> Members

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the relative order of the objects
        ///     being compared. The return value has the following meanings: Value
        ///     Meaning Less than zero This object is less than the other parameter.Zero
        ///     This object is equal to other. Greater than zero This object is greater
        ///     than other.
        /// </returns>
        public int CompareTo(PhoneNumber other)
        {
            return string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        #endregion

        #region Implementation of IConvertible

        /// <summary>
        ///     Returns the <see cref="T:System.TypeCode" /> for this instance.
        /// </summary>
        /// <returns>
        ///     The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements
        ///     this interface.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting
        ///     information.
        /// </summary>
        /// <returns>
        ///     A Boolean value equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent Unicode character using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     A Unicode character equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 8-bit signed integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 8-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 16-bit signed integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 16-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 32-bit signed integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 32-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 64-bit signed integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return ToInt64();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific
        ///     formatting information.
        /// </summary>
        /// <returns>
        ///     An 64-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent single-precision floating-point number using the specified
        ///     culture-specific formatting information.
        /// </summary>
        /// <returns>
        ///     A single-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent double-precision floating-point number using the specified
        ///     culture-specific formatting information.
        /// </summary>
        /// <returns>
        ///     A double-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified
        ///     culture-specific formatting information.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified
        ///     culture-specific formatting information.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        ///     Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified
        ///     culture-specific formatting information.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> instance equivalent to the value of this instance.
        /// </returns>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        /// <summary>
        ///     Converts the value of this instance to an <see cref="T:System.Object" /> of the specified
        ///     <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting
        ///     information.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to
        ///     the value of this instance.
        /// </returns>
        /// <param name="conversionType">
        ///     The <see cref="T:System.Type" /> to which the value of this instance is converted.
        /// </param>
        /// <param name="provider">
        ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
        ///     information.
        /// </param>
        /// <filterpriority>2</filterpriority>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}