#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom
{
    /// <summary>
    ///     Provides functionality to format the value of an object into a string
    ///     representation.
    /// </summary>
    /// <remarks>
    ///     If you are unable to derive from FormattableObject,
    ///     you can just implement IFormattable and include these two methods.
    ///     <code>
    ///     <![CDATA[
    ///  public string ToString(string format, System.IFormatProvider formatProvider)
    ///  {
    ///      return Loom.FormattableObject.ToString(this, format, formatProvider);
    ///  }
    /// 
    ///  public string ToString(string format)
    ///  {
    ///      return ToString(format, null);
    ///  }
    ///  ]]>
    ///   </code>
    /// </remarks>
    public abstract class FormattableObject : IFormattable
    {
        private static readonly Regex Reg = new Regex(@"(\{{)([^}]+)(}})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #region IFormattable Members

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </summary>
        /// <param name="format">A format specification.</param>
        /// <param name="formatProvider">
        ///     An <see cref="System.IFormatProvider" /> that
        ///     supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToStringPrivate(this, format, formatProvider, null);
        }

        #endregion

        /// <summary>
        ///     Converts the value of the supplied <paramref name="obj" /> to its equivalent string
        ///     representation using the specified <paramref name="format" /> and culture-specific
        ///     format information.
        /// </summary>
        /// <param name="obj">The object to format.</param>
        /// <param name="format">A format specification.</param>
        /// <param name="formatProvider">
        ///     An <see cref="IFormatProvider" /> that supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <param name="values"></param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </returns>
        public static string ToString(object obj, string format, IFormatProvider formatProvider, IDictionary<string, string> values = null)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));
            Argument.Assert.IsNotNull(format, nameof(format));

            return ToStringPrivate(obj, format, formatProvider, values);
        }

        /// <summary>
        ///     Converts the values in the supplied <paramref name="dictionary" /> to their equivalent string
        ///     representation using the specified <paramref name="format" /> and culture-specific
        ///     format information.
        /// </summary>
        /// <param name="dictionary">The object to format.</param>
        /// <param name="format">A format specification.</param>
        /// <param name="formatProvider">
        ///     An <see cref="IFormatProvider" /> that supplies
        ///     culture-specific formatting information.
        /// </param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </returns>
        public static string ToString(IDictionary dictionary, string format, IFormatProvider formatProvider)
        {
            Argument.Assert.IsNotNull(dictionary, nameof(dictionary));
            Argument.Assert.IsNotNull(format, nameof(format));

            return ToStringPrivate(dictionary, format, formatProvider);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </summary>
        /// <param name="format">A format specification.</param>
        /// <returns>
        ///     A <see cref="System.String" /> that represents the
        ///     current <see cref="FormattableObject" />.
        /// </returns>
        public string ToString(string format)
        {
            return ToStringPrivate(this, format, null, null);
        }

        private static void AddToValueDictionary(string toGet, string result, IDictionary<string, string> values)
        {
            if (values.ContainsKey(toGet))
                values[toGet] = result;
            else
                values.Add(toGet, result);
        }

        private static string ToStringPrivate(object obj, string format, IFormatProvider formatProvider, IDictionary<string, string> values)
        {
            IDictionary dict = obj as IDictionary;
            if (dict != null)
                return ToString(dict, format, formatProvider);

            MatchCollection matches = Reg.Matches(format);
            Type type = obj.GetType();

            StringBuilder sb = new StringBuilder();
            int startIndex = 0;
            foreach (Match match in matches)
            {
                Group group = match.Groups[2]; //it's second in the match between {{ and }
                int length = group.Index - startIndex - 2;
                if (length < 0)
                    continue;

                sb.Append(format.Substring(startIndex, length));
                startIndex = group.Index + group.Length + 2;

                string toGet;
                string toFormat = string.Empty;
                int formatIndex = group.Value.IndexOf(":", StringComparison.Ordinal); //formatting would be to the right of a :
                if (formatIndex == -1) //no formatting, no worries
                {
                    toGet = group.Value;
                }
                else //pickup the formatting
                {
                    toGet = group.Value.Substring(0, formatIndex);
                    toFormat = group.Value.Substring(formatIndex + 1);
                }

                //first try properties
                PropertyInfo retrievedProperty = type.GetProperty(toGet);
                Type retrievedType = null;
                object retrievedObject = null;
                if (retrievedProperty != null)
                {
                    retrievedType = retrievedProperty.PropertyType;
                    retrievedObject = retrievedProperty.GetValue(obj, null);
                }
                else //try fields
                {
                    FieldInfo retrievedField = type.GetField(toGet);
                    if (retrievedField != null)
                    {
                        retrievedType = retrievedField.FieldType;
                        retrievedObject = retrievedField.GetValue(obj);
                    }
                }

                if (retrievedType != null)
                {
                    string result;
                    if (toFormat.Length == 0) //no format info
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, null) as string;
                    else //format info
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, new object[] {toFormat, formatProvider}) as string;

                    if (values != null)
                        AddToValueDictionary(toGet, result, values);

                    sb.Append(result);
                }
                else if (values != null && values.ContainsKey(toGet))
                {
                    sb.Append(values[toGet]);
                }
                else //didn't find a property with that name, so be gracious and put it back
                {
                    sb.Append("{{");
                    sb.Append(group.Value);
                    sb.Append("}}");
                }
            }

            if (startIndex < format.Length) //include the rest (end) of the string
                sb.Append(format.Substring(startIndex));

            return sb.ToString();
        }

        private static string ToStringPrivate(IDictionary dictionary, string format, IFormatProvider formatProvider)
        {
            MatchCollection matchs = Reg.Matches(format);

            StringBuilder sb = new StringBuilder();
            int startIndex = 0;
            foreach (Match match in matchs)
            {
                Group group = match.Groups[2]; //it's second in the match between { and }
                int length = group.Index - startIndex - 2;
                sb.Append(format.Substring(startIndex, length));
                startIndex = group.Index + group.Length + 2;

                string toGet;
                string toFormat = string.Empty;
                int formatIndex = group.Value.IndexOf(":", StringComparison.Ordinal); //formatting would be to the right of a :
                if (formatIndex == -1) //no formatting, no worries
                {
                    toGet = group.Value;
                }
                else //pickup the formatting
                {
                    toGet = group.Value.Substring(0, formatIndex);
                    toFormat = group.Value.Substring(formatIndex + 1);
                }

                if (!dictionary.Contains(toGet))
                {
                    //didn't find a property with that name, so be gracious and put it back
                    sb.Append("{{");
                    sb.Append(group.Value);
                    sb.Append("}}");

                    continue;
                }

                object retrievedObject = dictionary[toGet];
                if (retrievedObject == null)
                {
                    sb.Append("{{");
                    sb.Append(group.Value);
                    sb.Append("}}");
                    continue;
                }

                Type retrievedType = retrievedObject.GetType();
                string result;
                if (toFormat.Length == 0) //no format info
                    result = retrievedType.InvokeMember("ToString",
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                        , null, retrievedObject, null) as string;
                else //format info
                    result = retrievedType.InvokeMember("ToString",
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                        , null, retrievedObject, new object[] {toFormat, formatProvider}) as string;
                sb.Append(result);
            }

            if (startIndex < format.Length) //include the rest (end) of the string
                sb.Append(format.Substring(startIndex));

            return sb.ToString();
        }
    }
}