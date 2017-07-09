#region Using Directives

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Loom.Annotations;

#endregion

namespace Loom.Web.Portal.Routing
{
    public static class RouteParser
    {
        private const string Parser = @"{(?<token>[\w%\s-_]*)(?:,\s?(?<exactly>\d)|,\s?(?<min>\d),\s?(?<max>\d?)|,\s?(?<wildcard>[\*\+\?]{1})|,\s?\[(?:(?<constraint>[\w\d\s\-\.\+\*\\'_%,]+),?\s?)+\])?}";

        private const string SingelTokenSegment = @"(?:/(?<{0}>[\w\d\s\-\+'_%,]+)){1}";
        private const string FirstMultiTokenSegment = @"(?:/(?<{0}>[\w\d\s\-\+'_%,]{1}))";
        private const string RemainingMultiTokenOptionSegments = @"(?:(?<{0}>{1}))";
        private const string RemainingMultiTokenSegments = @"(?:(?<{0}>[\w\d\s\-\+'_%,]{1}))";

        private const string SingleOrFirstMultiTokenOptionSegment = @"(?:/(?<{0}>{1}))";

        private const string Tail = "/?";
        private const string PatternStart = "^";
        private const string PatternEnd = "$";
        private const string ArgumentRouteDefinition = "routeDefinition";
        private const char SegmentSplit = '/';

        private static readonly Regex ParserRegex = new Regex(Parser, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex GenerateRegEx([NotNull] string routeDefinition)
        {
            Argument.Assert.IsNotNullOrEmpty(routeDefinition, ArgumentRouteDefinition);

            string[] segments = routeDefinition.Split(new[] {SegmentSplit}, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder builder = new StringBuilder();
            builder.Append(PatternStart);

            foreach (string segment in segments)
            {
                if (AttemptLiteralSegment(builder, segment))
                    continue;

                if (AttemptTokenSegment(builder, segment))
                    continue;

                throw new ArgumentException("Unable to parse the route '" + routeDefinition + "'.");
            }

            if (!Path.HasExtension(routeDefinition))
                builder.Append(Tail);
            builder.Append(PatternEnd);

            return new Regex(builder.ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private static bool AttemptLiteralSegment(StringBuilder builder, string segment)
        {
            bool isLiteral = segment.IndexOf("{", StringComparison.Ordinal) == -1 && segment.IndexOf("}", StringComparison.Ordinal) == -1;
            if (isLiteral)
                builder.Append("/" + segment);

            return isLiteral;
        }

        private static bool AttemptTokenSegment(StringBuilder builder, string segment)
        {
            MatchCollection matches = ParserRegex.Matches(segment);

            if (matches.Count == 0)
                return false;

            bool isMultiTokenSegmant = segment.LastIndexOf("{", StringComparison.Ordinal) > segment.IndexOf("}", StringComparison.Ordinal);
            bool hasLiterealPrefix = segment.IndexOf("{", StringComparison.Ordinal) > 0;

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];

                string token = match.Groups["token"].Value;

                string wildcard = match.Groups["wildcard"].Value;
                string exactly = match.Groups["exactly"].Value;
                string min = match.Groups["min"].Value;
                string max = match.Groups["max"].Value;
                CaptureCollection constraints = match.Groups["constraint"].Captures;

                string count = GetSegmentCount(wildcard, exactly, min, max, constraints);
                string replacement = GetReplacementText(i, constraints, isMultiTokenSegmant, hasLiterealPrefix, token, count);

                string replacementpattern = match.Value
                    .Replace(@"\", @"\\")
                    .Replace("+", @"\+")
                    .Replace("*", @"\*")
                    .Replace("[", @"\[")
                    .Replace("]", @"\]");

                segment = segment == replacementpattern ? replacement : Regex.Replace(segment, replacementpattern, replacement);
            }

            builder.Append(hasLiterealPrefix ? "/" + segment : segment);
            return true;
        }

        private static string GetReplacementText(int i, ICollection constraints, bool isMultiTokenSegmant, bool hasLiterealPrefix, string token, string count)
        {
            string replacement = constraints.Count > 0
                ? string.Format(isMultiTokenSegmant
                    ? (i == 0
                        ? SingleOrFirstMultiTokenOptionSegment
                        : RemainingMultiTokenOptionSegments)
                    : hasLiterealPrefix
                        ? RemainingMultiTokenOptionSegments
                        : SingleOrFirstMultiTokenOptionSegment, token, count, CultureInfo.CurrentCulture)
                : string.Format(isMultiTokenSegmant
                    ? (i == 0
                        ? FirstMultiTokenSegment
                        : RemainingMultiTokenSegments)
                    : hasLiterealPrefix
                        ? RemainingMultiTokenSegments
                        : SingelTokenSegment, token, count, CultureInfo.CurrentCulture);

            return replacement;
        }

        private static string GetSegmentCount(string wildcard, string exactly, string min, string max, CaptureCollection constraints)
        {
            string count;
            if (!Compare.IsNullOrEmpty(wildcard))
            {
                count = wildcard;
            }
            else if (!Compare.IsNullOrEmpty(exactly))
            {
                count = "{" + exactly + "}";
            }
            else if (!Compare.IsNullOrEmpty(min) && !Compare.IsNullOrEmpty(max))
            {
                count = "{" + min + "," + max + "}";
            }
            else if (constraints.Count > 0)
            {
                Capture option = constraints[0];
                count = option.Value.Replace(",", "|");
            }
            else
            {
                count = "{1}";
            }
            return count;
        }
    }
}