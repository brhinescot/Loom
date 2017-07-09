#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom.Security
{
    /// <summary>
    ///     Represents a class for generating cryptographically random alphanumeric strings of varying lengths.
    /// </summary>
    public class CryptoRandom
    {
        private const string CountGroup = "count";
        private const string GroupPrefix = "prefix";
        private const string GroupSuffix = "suffix";

        private static readonly Regex FormatMatch = new Regex(@"(?<prefix>[^\{\}]*)(?:{(?<format>(A|a|Aa|G|g|Gg|N|S)):(?<count>\d+)}|{(?<format>D):(?<count>[A-Z,a-z]+)})(?<suffix>[^\{\}]*)", RegexOptions.Compiled | RegexOptions.Singleline);

        private static readonly char[] Numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        /// <summary>
        ///     The default length of the generated text if no options are specified in a constructor.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate", Justification = "Member is public static readonly. Should not be made public const.")] public static readonly int DefaultLength = 8;

        private readonly string format;
        private readonly List<AlphaNumericProvider> providers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CryptoRandom" /> class.
        /// </summary>
        /// <remarks>
        ///     Generates an 8 character random string using the general format. Equivalent to calling the
        ///     <see cref="CryptoRandom(string)" /> overload with "{G:8}" as the format.
        /// </remarks>
        /// <seealso cref="CryptoRandom(string)" />
        public CryptoRandom() : this(DefaultLength) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CryptoRandom" /> class.
        /// </summary>
        /// <param name="length">The length of the string to generate..</param>
        /// <remarks>
        ///     Generates a random string using the general format and the specified length. Equivalent
        ///     to calling the <see cref="CryptoRandom(string)" /> overload with "{G:#}" as the
        ///     format, Where # is the length of the string represented by <paramref name="length" />.
        /// </remarks>
        /// <seealso cref="CryptoRandom(string)" />
        public CryptoRandom(int length) : this(string.Concat("{G:", length, "}")) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CryptoRandom" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <remarks>
        ///     Generates a random string using the specified format. Characters not included
        ///     in a format definition are added as string literals to the generated string.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Format</term><description>Usage</description>
        ///         </listheader>
        ///         <item>
        ///             <term>{A:#}</term>
        ///             <description>
        ///                 Generates a string containing only alpha characters. The # represents the
        ///                 number of characters to generate.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>{N:#}</term>
        ///             <description>
        ///                 Generates a string containing only numeric characters. The # represents the
        ///                 number of characters to generate.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>{G:#}</term>
        ///             <description>
        ///                 Generates a string containing alpha or numeric characters. The # represents
        ///                 the number of characters to generate.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>{D:#}</term>
        ///             <description>
        ///                 Generates a string containing the current date and/or time. The # represents
        ///                 the <see cref="DateTimeFormatInfo" /> pattern to use.
        ///             </description>
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <example>
        ///     The following example demonstrates different methods of generating the strings.
        ///     <code>
        /// public void Create()
        /// {
        ///     CryptoRandom generator = new CryptoRandom("{A:2}-{G:6}");
        ///     Console.WriteLine(generator.Generate());
        ///     Console.WriteLine(generator.Generate());
        /// 
        ///     CryptoRandom generator = new CryptoRandom("ERR:{G:4}.{D:MMdd}");
        ///     Console.WriteLine(generator.Generate());
        ///     Console.WriteLine(generator.Generate());
        /// 
        /// 
        ///     /* Sample output considering the date is Nov 11th, 2006:
        ///      * 45-RG2H9L
        ///      * 04-Y2J8DE
        ///      * ERR:HU7S.1106
        ///      * ERR:95GY.1106
        ///      */
        /// }
        /// </code>
        /// </example>
        public CryptoRandom(string format)
        {
            Argument.Assert.IsNotNull(format, "format");

            this.format = format;
            Match match = FormatMatch.Match(format);

            if (match.Success)
                providers = new List<AlphaNumericProvider>();

            while (match.Success)
            {
                switch (match.Groups["format"].Value)
                {
                    case "Aa":
                        providers.Add(new AlphaProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = true, UseUppers = true});
                        break;
                    case "A":
                        providers.Add(new AlphaProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = false, UseUppers = true});
                        break;
                    case "a":
                        providers.Add(new AlphaProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = true, UseUppers = false});
                        break;
                    case "Gg":
                        providers.Add(new GeneralProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = true, UseUppers = true});
                        break;
                    case "G":
                        providers.Add(new GeneralProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = false, UseUppers = true});
                        break;
                    case "g":
                        providers.Add(new GeneralProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value) {UseLowers = true, UseUppers = false});
                        break;
                    case "N":
                        providers.Add(new NumberProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value));
                        break;
                    case "D":
                        providers.Add(new DateTimeProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value));
                        break;
                    case "S":
                        providers.Add(new SpecialCharacterProvider(match.Groups[CountGroup].Value, match.Groups[GroupPrefix].Value, match.Groups[GroupSuffix].Value));
                        break;
                }
                match = match.NextMatch();
            }
        }

        public static long NextNumber()
        {
            StringBuilder result = new StringBuilder();

            byte[] data = new byte[19];
            RandomNumberGenerator generator = RandomNumberGenerator.Create();
            generator.GetNonZeroBytes(data);

            foreach (byte t in data)
                result.Append(Numbers[t % (Numbers.Length - 1)]);

            return Convert.ToInt64(result.ToString());
        }

        /// <summary>
        ///     Generates a new cryptographically strong random string.
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            if (providers == null || providers.Count == 0)
                return format;

            StringBuilder result = new StringBuilder();
            foreach (AlphaNumericProvider t in providers)
                t.Generate(result);

            return result.ToString();
        }

        #region Nested type: AlphaNumericProvider

        private abstract class AlphaNumericProvider
        {
            private readonly RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
            private byte[] data;

            protected AlphaNumericProvider(string formatOptions, string prefix, string suffix)
            {
                Prefix = prefix;
                Suffix = suffix;
                FormatOptions = formatOptions;
            }

            protected string FormatOptions { get; }

            protected string Prefix { get; }

            protected string Suffix { get; }

            internal abstract char[] Characters { get; }

            internal virtual void Generate(StringBuilder result)
            {
                if (!Compare.IsNullOrEmpty(Prefix))
                    result.Append(Prefix);

                data = new byte[Convert.ToInt32(FormatOptions)];
                cryptoProvider.GetBytes(data);

                for (int i = 0; i < data.Length; i++)
                {
                    int random = data[i] % Characters.Length;
                    result.Append(Characters[random]);
                }

                if (!Compare.IsNullOrEmpty(Suffix))
                    result.Append(Suffix);
            }
        }

        #endregion

        #region Nested type: AlphaProvider

        private class AlphaProvider : AlphaNumericProvider
        {
            private static readonly char[] LowerChars =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };

            private static readonly char[] MixedChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };

            private static readonly char[] UpperChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            public AlphaProvider(string count, string seperator1, string seperator2) : base(count, seperator1, seperator2) { }

            internal override char[] Characters => UseUppers && UseLowers ? MixedChars : UseUppers ? UpperChars : LowerChars;

            internal bool UseUppers { get; set; }
            internal bool UseLowers { get; set; }
        }

        #endregion

        #region Nested type: DateTimeProvider

        private class DateTimeProvider : AlphaNumericProvider
        {
            public DateTimeProvider(string format, string seperator1, string seperator2) : base(format, seperator1, seperator2) { }

            internal override char[] Characters => null;

            internal override void Generate(StringBuilder result)
            {
                if (!Compare.IsNullOrEmpty(Prefix))
                    result.Append(Prefix);

                result.Append(DateTime.Now.ToString(FormatOptions));

                if (!Compare.IsNullOrEmpty(Suffix))
                    result.Append(Suffix);
            }
        }

        #endregion

        #region Nested type: GeneralProvider

        private class GeneralProvider : AlphaNumericProvider
        {
            private static readonly char[] LowerChars =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };

            private static readonly char[] MixedChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };

            private static readonly char[] UpperChars =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };

            public GeneralProvider(string count, string seperator1, string seperator2) : base(count, seperator1, seperator2) { }

            internal override char[] Characters => UseUppers && UseLowers ? MixedChars : UseUppers ? UpperChars : LowerChars;

            internal bool UseUppers { get; set; }
            internal bool UseLowers { get; set; }
        }

        #endregion

        #region Nested type: NumberProvider

        private class NumberProvider : AlphaNumericProvider
        {
            private static readonly char[] Chars = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

            public NumberProvider(string count, string seperator1, string seperator2) : base(count, seperator1, seperator2) { }

            internal override char[] Characters => Chars;
        }

        #endregion

        #region Nested type: SpecialCharacterProvider

        private class SpecialCharacterProvider : AlphaNumericProvider
        {
            private static readonly char[] Chars =
            {
                '!', '@', '#', '$', '%',
                '^', '&', '*', '_', '+',
                '[', ']', '\\', '/', ':',
                '?', '{', '}', '~'
            };

            public SpecialCharacterProvider(string count, string seperator1, string seperator2) : base(count, seperator1, seperator2) { }

            internal override char[] Characters => Chars;
        }

        #endregion
    }
}