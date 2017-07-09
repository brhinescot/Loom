#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom
{
    /// <summary>
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex Tags = new Regex("<[^>]*(>|$)", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex WhiteList = new Regex(@"^</?(a|b(lockquote)?|code|em|h(1|2|3)|i|li|ol|p(re)?|s(ub|up|trong|trike)?|ul)>$|^<(b|h)r\s?/?>$|^<a[^>]+>$|^<img[^>]+/?>$",
            RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
            RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        ///     Extracts a string from between a pair of delimiters. Only the first
        ///     instance is returned.
        /// </summary>
        /// <param name="source">Input String to work on</param>
        /// <param name="startDelimeter">Beginning delimiter</param>
        /// <param name="endDelimeter">ending delimiter</param>
        /// <param name="caseSensitive">Determines whether the search for delimiters is case sensitive</param>
        /// <param name="allowMissingEndDelimiter"></param>
        /// <returns>Extracted string or ""</returns>
        public static string Extract(this string source, string startDelimeter, string endDelimeter, bool caseSensitive = false, bool allowMissingEndDelimiter = false)
        {
            int at1, at2;

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (caseSensitive)
            {
                at1 = source.IndexOf(startDelimeter, StringComparison.Ordinal);
                if (at1 == -1)
                    return "";

                at2 = source.IndexOf(endDelimeter, at1 + startDelimeter.Length, StringComparison.Ordinal);
            }
            else
            {
                string lower = source.ToLower();
                at1 = lower.IndexOf(startDelimeter.ToLower(), StringComparison.Ordinal);
                if (at1 == -1)
                    return string.Empty;

                at2 = lower.IndexOf(endDelimeter.ToLower(), at1 + startDelimeter.Length, StringComparison.Ordinal);
            }

            if (allowMissingEndDelimiter && at2 == -1)
                return source.Substring(at1 + startDelimeter.Length);

            if (at1 > -1 && at2 > 1)
                return source.Substring(at1 + startDelimeter.Length, at2 - at1 - startDelimeter.Length);

            return string.Empty;
        }

        /// <summary>
        ///     Creates a <see cref="Stream" /> from the <see cref="string" /> using <see cref="Encoding.UTF8" /> encoding.
        /// </summary>
        /// <param name="s">
        ///     The <see cref="string" /> to convert.
        /// </param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Stream ToStream(this string s, Encoding encoding = null)
        {
            return encoding == null
                ? new MemoryStream(Encoding.UTF8.GetBytes(s))
                : new MemoryStream(encoding.GetBytes(s));
        }

        /// <summary>
        ///     Capitalizes the first letter of each word and removes any spaces between words to form a
        ///     single, Pascal cased word.
        /// </summary>
        /// <param name="s">The string to transform.</param>
        /// <remarks>
        ///     "Open file for reading" will become "OpenFileForReading".
        /// </remarks>
        /// <returns>A Pascal cased string.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="s" /> is null.
        /// </exception>
        public static string ToPascalCase(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            return ToPascalCasePrivate(s);
        }

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToProperCase(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            return ToProperCasePrivate(s);
        }

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            return ToCamelCasePrivate(s);
        }

        /// <summary>
        ///     Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="s">The string to test whether it is all white space.</param>
        /// <returns>
        ///     true if the string is all white space; otherwise, false.
        /// </returns>
        public static bool IsWhiteSpace(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            if (s.Length == 0)
                return false;

            for (int i = 0; i < s.Length; i++)
                if (!char.IsWhiteSpace(s[i]))
                    return false;

            return true;
        }

        /// <summary>
        ///     Numbers the lines.
        /// </summary>
        /// <param name="s">The string to number.</param>
        /// <returns></returns>
        public static string AddLineNumbers(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            return AddLineNumbersPrivate(s);
        }

        /// <summary>
        ///     Truncates the specified string.
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <param name="suffix">The suffix to place at the end of the truncated string.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int maximumLength, string suffix = "...")
        {
            Argument.Assert.IsNotNull(s, nameof(s));
            Argument.Assert.IsGreaterThanZero(maximumLength, nameof(maximumLength));
            Argument.Assert.IsNotNull(suffix, nameof(suffix));

            return TruncatePrivate(s, maximumLength, suffix);
        }

        /// <summary>
        ///     Sanitize any potentially dangerous tags from the provided raw HTML input using
        ///     a white list based approach, leaving the "safe" HTML tags
        /// </summary>
        public static string Sanitize(this string s)
        {
            Argument.Assert.IsNotNull(s, nameof(s));

            return SanitizePrivate(s);
        }

        /// <summary>
        ///     Determines whether the beginning of the string instance matches any of the supplied string values.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The strings to compare to the substring at the end of this instance.</param>
        /// <returns>
        ///     true if one of the <paramref name="values" /> matches the end of this instance; otherwise, false.
        /// </returns>
        public static bool StartsWithAny(this string s, IEnumerable<string> values)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            return StartsWithAnyPrivate(values, s);
        }

        /// <summary>
        ///     Determines whether the beginning of the string instance matches any of the supplied string values.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The strings to compare to the substring at the end of this instance.</param>
        /// <returns>
        ///     true if one of the <paramref name="values" /> matches the end of this instance; otherwise, false.
        /// </returns>
        public static bool StartsWithAny(this string s, params string[] values)
        {
            return StartsWithAny(s, (IEnumerable<string>) values);
        }

        /// <summary>
        ///     Determines whether the end of the string instance matches any of the supplied string values.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The strings to compare to the substring at the end of this instance.</param>
        /// <returns>
        ///     true if one of the <paramref name="values" /> matches the end of this instance; otherwise, false.
        /// </returns>
        public static bool EndsWithAny(this string s, IEnumerable<string> values)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            foreach (string value in values)
                if (s.EndsWith(value))
                    return true;

            return false;
        }

        /// <summary>
        ///     Determines whether the end of the string instance matches any of the supplied string values.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The strings to compare to the substring at the end of this instance.</param>
        /// <returns>
        ///     true if one of the <paramref name="values" /> matches the end of this instance; otherwise, false.
        /// </returns>
        public static bool EndsWithAny(this string s, params string[] values)
        {
            return EndsWithAny(s, (IEnumerable<string>) values);
        }

        /// <summary>
        ///     Returns a value indicating whether the specified <see cref="String" /> values occurs within the string.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The string values to seek. </param>
        /// <returns>
        ///     true if the <paramref name="values" /> parameter occurs within this string, or if one of
        ///     the <paramref name="values" /> is the empty string (""); otherwise, false.
        /// </returns>
        public static bool ContainsAny(this string s, IEnumerable<string> values)
        {
            Argument.Assert.IsNotNull(values, nameof(values));

            foreach (string value in values)
                if (s.Contains(value))
                    return true;

            return false;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified <see cref="String" /> values occurs within the string.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="values">The string values to seek. </param>
        /// <returns>
        ///     true if the <paramref name="values" /> parameter occurs within this string, or if one of
        ///     the <paramref name="values" /> is the empty string (""); otherwise, false.
        /// </returns>
        public static bool ContainsAny(this string s, params string[] values)
        {
            return ContainsAny(s, (IEnumerable<string>) values);
        }

        /// <summary>
        ///     Returns the specified <paramref name="value" /> if the <see cref="string" /> is null or
        ///     an empty string; otherwise returns the unmodified string.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <param name="value">
        ///     The <see cref="string" /> value to return if the string is null or
        ///     an empty string.
        /// </param>
        /// <returns></returns>
        public static string IfNullOrEmpty(this string s, string value)
        {
            return string.IsNullOrEmpty(s) ? value : s;
        }

        private static void ActionTextReaderLine(TextReader textReader, TextWriter textWriter, ActionLine lineAction)
        {
            string line;
            bool firstLine = true;
            while ((line = textReader.ReadLine()) != null)
            {
                if (!firstLine)
                    textWriter.WriteLine();
                else
                    firstLine = false;

                lineAction(textWriter, line);
            }
        }

        private static string AddLineNumbersPrivate(string s)
        {
            StringWriter writer;
            using (StringReader reader = new StringReader(s))
            {
                writer = new StringWriter();
                int lineNumber = 1;
                ActionTextReaderLine(reader, writer, (textWriter, line) =>
                {
                    textWriter.Write(lineNumber.ToString(CultureInfo.InvariantCulture).PadLeft(4));
                    textWriter.Write(". ");
                    textWriter.Write(line);

                    lineNumber++;
                });
            }

            return writer.ToString();
        }

        /// <summary>
        ///     Utility function to match a regex pattern: case, whitespace, and line insensitive
        /// </summary>
        private static bool IsMatch(string s, string pattern)
        {
            return Regex.IsMatch(s, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase |
                                             RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);
        }

        private static bool IsIdentifierAbbreviation(char c, int position, char[] letters)
        {
            return IsSpecialCase(c, position, letters, 'I', 'D');
        }

        private static bool IsSpecialCase(char c, int position, char[] letters, char previous, char current)
        {
            return c == current && position > 0 && letters[position - 1] == previous;
        }

        private static string SanitizePrivate(string s)
        {
            MatchCollection tagMatches = Tags.Matches(s);

            // iterate through all HTML tags in the input
            for (int i = tagMatches.Count - 1; i > -1; i--)
            {
                Match tag = tagMatches[i];
                string tagname = tag.Value.ToLower();

                if (!WhiteList.IsMatch(tagname))
                {
                    // not on our white list? I SAY GOOD DAY TO YOU, SIR. GOOD DAY!
                    s = s.Remove(tag.Index, tag.Length);
                }
                else if (tagname.StartsWith("<a"))
                {
                    // detailed <a> tag checking
                    if (!IsMatch(tagname, @"<a\shref=""(\#\d+|(https?|ftp)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+)""(\stitle=""[^""]+"")?\s?>"))
                        s = s.Remove(tag.Index, tag.Length);
                }
                else if (tagname.StartsWith("<img"))
                {
                    // detailed <img> tag checking
                    if (!IsMatch(tagname, @"<img\ssrc=""https?://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+""(\swidth=""\d{1,3}"")?(\sheight=""\d{1,3}"")?(\salt=""[^""]*"")?(\stitle=""[^""]*"")?\s?/?>"))
                        s = s.Remove(tag.Index, tag.Length);
                }
            }

            return s;
        }

        private static bool StartsWithAnyPrivate(IEnumerable<string> values, string s)
        {
            foreach (string value in values)
                if (s.StartsWith(value))
                    return true;

            return false;
        }

        private static string ToCamelCasePrivate(string s)
        {
            while (s.Contains("  "))
                s = s.Replace("  ", " ");

            char[] letters = s.ToCharArray();
            StringBuilder output = new StringBuilder(s.Length);

            for (int i = 0; i < letters.Length; i++)
            {
                char c = letters[i];
                char next = char.MinValue;
                if (i < letters.Length - 1)
                    next = letters[i + 1];

                if (c == '\'')
                    continue;

                //Only process whitespace, letters and underscores.
                if (!char.IsLetter(c) && !char.IsNumber(c) && c != '_')
                    c = ' ';

                if (i == 0 && char.IsUpper(c))
                {
                    output.Append(char.ToLower(c));
                }
                else if (i == 0 && char.IsNumber(c))
                {
                    output.Append('n');
                    output.Append(char.ToUpper(c));
                }
                else if (char.IsWhiteSpace(c) && !char.IsLetter(next) && !char.IsNumber(next) && next != '_') { }
                else if (!char.IsLetter(c) && !char.IsNumber(c) && c != '_' && (char.IsLetter(next) || char.IsNumber(next) || next == '_'))
                {
                    output.Append(char.ToUpper(next));
                    i++;
                }
                else if (char.IsWhiteSpace(c))
                {
                    output.Append(char.ToUpper(next));
                    i++;
                }
                else if (c == '_')
                {
                    output.Append(c);
                    if (next > char.MinValue)
                        output.Append(char.ToUpper(next));
                    i++;
                }
                else
                {
                    output.Append(IsIdentifierAbbreviation(c, i, letters) ? char.ToLower(c) : c);
                }
            }

            return output.ToString();
        }

        private static string ToPascalCasePrivate(string s)
        {
            while (s.Contains("  "))
                s = s.Replace("  ", " ");

            char[] letters = s.ToCharArray();
            StringBuilder output = new StringBuilder(s.Length);

            for (int i = 0; i < letters.Length; i++)
            {
                char c = letters[i];
                char next = char.MinValue;
                if (i < letters.Length - 1)
                    next = letters[i + 1];

                if (c == '\'')
                    continue;

                //Only process whitespace and letters.
                if (!char.IsLetter(c) && !char.IsNumber(c) && c != '_')
                    c = ' ';

                if (i == 0 && char.IsLower(c))
                {
                    output.Append(char.ToUpper(c));
                }
                else if (i == 0 && char.IsNumber(c))
                {
                    output.Append('N');
                    output.Append(char.ToUpper(c));
                }
                else if (char.IsWhiteSpace(c) && !char.IsLetter(next) && !char.IsNumber(next) && next != '_') { }
                else if (!char.IsLetter(c) && !char.IsNumber(c) && c != '_' && (char.IsLetter(next) || char.IsNumber(next) || next == '_'))
                {
                    output.Append(char.ToUpper(next));
                    i++;
                }
                else if (char.IsWhiteSpace(c))
                {
                    output.Append(char.ToUpper(next));
                    i++;
                }
                else
                {
                    output.Append(IsIdentifierAbbreviation(c, i, letters) ? char.ToLower(c) : c);
                }
            }

            return output.ToString();
        }

        private static string ToProperCasePrivate(string s)
        {
            char[] letters = s.ToCharArray();
            StringBuilder output = new StringBuilder(s.Length);

            for (int i = 0; i < letters.Length; i++)
            {
                char c = letters[i];
                char prev = char.MinValue;
                if (i != 0)
                    prev = letters[i - 1];

                if (i == 0)
                    output.Append(char.ToUpper(c));
                else if (char.IsUpper(c) && i != 0 && char.IsLower(prev))
                    output.Append(" " + c);
                else if (prev == ' ')
                    output.Append(char.ToUpper(c));
                else if (IsIdentifierAbbreviation(c, i, letters))
                    output.Append(char.ToLower(c));
                else
                    output.Append(c);
            }
            return output.ToString();
        }

        private static string TruncatePrivate(string s, int maximumLength, string suffix)
        {
            int subStringLength = maximumLength - suffix.Length;

            if (subStringLength <= 0)
                throw new ArgumentException("The length of parameter suffix is greater or equal to maximumLength");

            if (s.Length > maximumLength)
            {
                string truncatedString = s.Substring(0, subStringLength);
                truncatedString = truncatedString.TrimEnd();
                truncatedString += suffix;

                return truncatedString;
            }

            return s;
        }

        #region Nested type: ActionLine

        private delegate void ActionLine(TextWriter textWriter, string line);

        #endregion
    }
}