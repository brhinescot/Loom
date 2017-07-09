#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Loom.Data
{
    public sealed class FullTextSearch
    {
        private readonly List<string> searchTerms;

        public FullTextSearch(string condition) : this(condition, FullTextSearchOptions.Default) { }

        public FullTextSearch(string condition, FullTextSearchOptions options)
        {
            Condition = condition;
            Options = options;

            ConditionParser parser = new ConditionParser(condition, options);

            NormalForm = parser.RootExpression.ToString();

            searchTerms = new List<string>();

            foreach (ConditionExpression exp in parser.RootExpression)
            {
                if (exp.IsSubexpression)
                    continue;
                if (exp.Term.Length == 0)
                    continue;
                searchTerms.Add(exp.Term);
            }
        }

        public string Condition { get; }

        public string NormalForm { get; }

        public FullTextSearchOptions Options { get; }

        public string[] SearchTerms => searchTerms.ToArray();

        public override string ToString()
        {
            return NormalForm;
        }

        #region Nested type: ConditionExpression

        private sealed class ConditionExpression : IEnumerable<ConditionExpression>
        {
            private readonly int index;
            private readonly FullTextSearchOptions options;
            private readonly List<ConditionExpression> subexpressions;
            private readonly ConditionOperator conditionOperator;

            private ConditionExpression()
            {
                Term = string.Empty;
                subexpressions = new List<ConditionExpression>();
            }

            public ConditionExpression(FullTextSearchOptions options) : this()
            {
                this.options = options;
            }

            private ConditionExpression(ConditionExpression parent, ConditionOperator op) : this(parent.options)
            {
                index = parent.subexpressions.Count;
                Parent = parent;
                conditionOperator = op;
            }

            private ConditionExpression(ConditionExpression parent, ConditionOperator op, string term) : this(parent, op)
            {
                Term = term;

                IsTerm = true;

                TermIsPhrase = term.IndexOf(' ') != -1;
                int prefixIndex = term.IndexOf('*');
                TermIsPrefix = prefixIndex != -1;

                if (!TermIsPrefix)
                    return;

                if (!TermIsPhrase)
                {
                    if ((options & FullTextSearchOptions.TrimPrefixTerms) == 0)
                        return;
                    if (prefixIndex == term.Length - 1)
                        return;
                    Term = prefixIndex == 0 ? "" : term.Remove(prefixIndex + 1);
                    return;
                }

                if ((options & FullTextSearchOptions.TrimPrefixPhrases) == 0)
                    return;
                term = Regex.Replace(term, @"(\*[^ ]+)|(\*)", "");
                term = Regex.Replace(term.Trim(), @"[ ]{2,}", " ");
                Term = term + "*";
            }

            private bool HasSubexpressions => subexpressions.Count > 0;

            private bool IsLastSubexpression => IsRoot || !IsRoot && index == Parent.subexpressions.Count - 1;

            public bool IsRoot => Parent == null;

            public bool IsSubexpression => !IsTerm;

            private bool IsTerm { get; }

            private ConditionExpression LastSubexpression => HasSubexpressions ? subexpressions[subexpressions.Count - 1] : null;

            private ConditionExpression NextSubexpression => !IsLastSubexpression ? Parent.subexpressions[index + 1] : null;

            public ConditionExpression Parent { get; }

            public string Term { get; }

            private bool TermIsPhrase { get; }

            private bool TermIsPrefix { get; }

            #region IEnumerable<ConditionExpression> Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<ConditionExpression> GetEnumerator()
            {
                foreach (ConditionExpression exp in subexpressions)
                {
                    yield return exp;
                    if (exp.HasSubexpressions)
                        foreach (ConditionExpression exp2 in exp)
                            yield return exp2;
                }
            }

            #endregion

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                if (IsTerm)
                {
                    bool doStem = DoStem();

                    if (doStem)
                        sb.Append("formsof(inflectional, ");

                    sb.Append("\"");
                    sb.Append(Term.Replace("\"", "\"\""));
                    sb.Append("\"");

                    if (doStem)
                        sb.Append(")");
                }
                else
                {
                    if (!IsRoot)
                        sb.Append("(");

                    if (!HasSubexpressions)
                        sb.Append("\"\""); // Want to avoid 'Null or empty full-text predicate' exception.
                    else
                        for (int i = 0; i < subexpressions.Count; i++)
                        {
                            ConditionExpression exp = subexpressions[i];
                            if (i > 0)
                            {
                                sb.Append(" ");
                                sb.Append(exp.conditionOperator);
                                sb.Append(" ");
                            }
                            sb.Append(exp);
                        }

                    if (!IsRoot)
                        sb.Append(")");
                }

                return sb.ToString();
            }

            public ConditionExpression AddSubexpression(ConditionOperator op)
            {
                ConditionOperator newOp = op;
                if (op == ConditionOperator.Near)
                {
                    if ((options & FullTextSearchOptions.ThrowOnInvalidNearUse) != 0)
                        throw new InvalidOperationException("Invalid near operator before subexpression.");
                    newOp = ConditionOperator.And;
                }

                ConditionExpression exp = new ConditionExpression(this, newOp);

                subexpressions.Add(exp);

                return exp;
            }

            public void AddTerm(ConditionOperator op, string term)
            {
                if (!HasSubexpressions)
                {
                    op = ConditionOperator.And;
                }
                else
                {
                    if (op == ConditionOperator.Near)
                        if (LastSubexpression.HasSubexpressions)
                        {
                            if ((options & FullTextSearchOptions.ThrowOnInvalidNearUse) != 0)
                                throw new InvalidOperationException("Invalid near operator after subexpression.");
                            op = ConditionOperator.And;
                        }
                }

                ConditionExpression exp = new ConditionExpression(this, op, term);

                subexpressions.Add(exp);
            }

            private bool DoStem()
            {
                if (IsSubexpression)
                    return false;
                if (Term.Length < 2)
                    return false;
                if (TermIsPrefix)
                    return false;
                if (!TermIsPhrase && (options & FullTextSearchOptions.StemTerms) == 0 || TermIsPhrase && (options & FullTextSearchOptions.StemPhrases) == 0)
                    return false;
                if (conditionOperator == ConditionOperator.Near)
                    return false;
                if (!IsLastSubexpression && NextSubexpression.conditionOperator == ConditionOperator.Near)
                    return false;

                return true;
            }
        }

        #endregion

        #region Nested type: ConditionOperator

        private struct ConditionOperator
        {
            #region Constants

            private const char And1Symbol = '&';
            private const char And2Symbol = '+';
            private const char And3Symbol = ',';
            private const char And4Symbol = ';';
            private const char AndNot1Symbol = '-';
            private const char AndNot2Symbol = '!';
            private const char NearSymbol = '~';

            private const int OpAnd = 0;
            private const int OpAndNot = 1;
            private const int OpNear = 3;
            private const int OpOr = 2;
            private const char OrSymbol = '|';

            #endregion

            #region Member Fields

            private static readonly ConditionOperator AndNot = new ConditionOperator(OpAndNot);
            public static ConditionOperator And = new ConditionOperator(OpAnd);
            public static ConditionOperator Near = new ConditionOperator(OpNear);
            private static ConditionOperator Or = new ConditionOperator(OpOr);

            private readonly int value;

            #endregion

            #region .ctors

            private ConditionOperator(int value)
            {
                this.value = value;
            }

            #endregion

            public static bool IsSymbol(char symbol)
            {
                switch (symbol)
                {
                    case And1Symbol:
                        return true;
                    case And2Symbol:
                        return true;
                    case And3Symbol:
                        return true;
                    case And4Symbol:
                        return true;
                    case AndNot1Symbol:
                        return true;
                    case AndNot2Symbol:
                        return true;
                    case OrSymbol:
                        return true;
                    case NearSymbol:
                        return true;
                }
                return false;
            }

            public static bool TryParse(string s, ref ConditionOperator op)
            {
                if (s.Length == 1)
                {
                    switch (s[0])
                    {
                        case And1Symbol:
                            goto case And4Symbol;
                        case And2Symbol:
                            goto case And4Symbol;
                        case And3Symbol:
                            goto case And4Symbol;
                        case And4Symbol:
                            op = And;
                            return true;
                        case AndNot1Symbol:
                            op = AndNot;
                            return true;
                        case AndNot2Symbol:
                            if (op != And)
                                return false;
                            op = AndNot;
                            return true;
                        case OrSymbol:
                            op = Or;
                            return true;
                        case NearSymbol:
                            op = Near;
                            return true;
                    }
                    return false;
                }

                if (s.Equals(And.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    op = And;
                    return true;
                }
                if (s.Equals("not", StringComparison.OrdinalIgnoreCase) && op == And)
                {
                    op = AndNot;
                    return true;
                }
                if (s.Equals(Or.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    op = Or;
                    return true;
                }
                if (s.Equals(Near.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    op = Near;
                    return true;
                }

                return false;
            }

            public static bool operator ==(ConditionOperator obj1, ConditionOperator obj2)
            {
                return obj1.Equals(obj2);
            }

            public static bool operator !=(ConditionOperator obj1, ConditionOperator obj2)
            {
                return !obj1.Equals(obj2);
            }

            public override string ToString()
            {
                switch (value)
                {
                    case OpAndNot:
                        return "and not";
                    case OpOr:
                        return "or";
                    case OpNear:
                        return "near";
                    default:
                        return "and";
                }
            }

            public override bool Equals(object obj)
            {
                return obj is ConditionOperator && Equals((ConditionOperator) obj);
            }

            public override int GetHashCode()
            {
                return value.GetHashCode();
            }

            #region Private Methods

            private bool Equals(ConditionOperator obj)
            {
                return value == obj.value;
            }

            #endregion
        }

        #endregion

        #region Nested type: ConditionParser

        private sealed class ConditionParser
        {
            private readonly bool inQuotes;
            private readonly FullTextSearchOptions options;

            private ConditionExpression currentExpression;
            private ConditionOperator lastOp;
            private StringBuilder token;

            public ConditionParser(string condition, FullTextSearchOptions options)
            {
                ConditionStream stream = new ConditionStream(condition, options);

                this.options = options;

                RootExpression = new ConditionExpression(options);
                currentExpression = RootExpression;

                Reset();

                while (stream.Read())
                {
                    if (ConditionOperator.IsSymbol(stream.Current))
                    {
                        PutToken();
                        SetToken(stream.Current);
                        PutToken();
                        continue;
                    }
                    switch (stream.Current)
                    {
                        case ' ':
                            PutToken();
                            continue;
                        case '(':
                            PushExpression();
                            continue;
                        case ')':
                            PopExpression();
                            continue;
                        case '"':
                            PutToken();
                            inQuotes = true;
                            SetToken(stream.ReadQuote());
                            PutToken();
                            inQuotes = false;
                            continue;
                    }
                    AddToken(stream.Current);
                }
                PutToken();

                if (!ReferenceEquals(RootExpression, currentExpression))
                    if ((options & FullTextSearchOptions.ThrowOnUnbalancedParens) != 0)
                        throw new InvalidOperationException("Unbalanced parentheses.");
            }

            public ConditionExpression RootExpression { get; }

            private void AddToken(char c)
            {
                token.Append(c);
            }

            private void PopExpression()
            {
                PutToken();
                if (currentExpression.IsRoot)
                {
                    if ((options & FullTextSearchOptions.ThrowOnUnbalancedParens) != 0)
                        throw new InvalidOperationException("Unbalanced parentheses.");
                }
                else
                {
                    currentExpression = currentExpression.Parent;
                }
                Reset();
            }

            private void PushExpression()
            {
                PutToken();
                currentExpression = currentExpression.AddSubexpression(lastOp);
            }

            private void PutToken()
            {
                // Check to see if the token is an operator.

                if (!inQuotes && ConditionOperator.TryParse(token.ToString(), ref lastOp))
                {
                    ResetToken();
                    return;
                }

                // Not an operator, so it's a term.

                string term = token.ToString();
                if (inQuotes)
                    term = Regex.Replace(term.Trim(), @"[ ]{2,}", " ");
                if (term.Length == 0 && !inQuotes)
                    return;

                currentExpression.AddTerm(lastOp, term);

                Reset();
            }

            private void Reset()
            {
                ResetToken();
                lastOp = ConditionOperator.And;
            }

            private void ResetToken()
            {
                token = new StringBuilder();
            }

            private void SetToken(char c)
            {
                SetToken(c.ToString());
            }

            private void SetToken(string s)
            {
                token = new StringBuilder(s);
            }
        }

        #endregion

        #region Nested type: ConditionStream

        private sealed class ConditionStream
        {
            private readonly string condition;
            private readonly FullTextSearchOptions options;
            private int index;

            public ConditionStream(string condition, FullTextSearchOptions options)
            {
                this.options = options;
                this.condition = Regex.Replace(condition ?? string.Empty, @"\x09|\x0D|\x0A|[\x01-\x08]|\x10|[\x0B-\x0C]|[\x0E-\x1F]", " ");
                index = -1;
            }

            public char Current => Eoq() || Boq() ? (char) 0 : condition[index];

            public bool Read()
            {
                index++;
                if (Eoq())
                    return false;
                return true;
            }

            public string ReadQuote()
            {
                StringBuilder sb = new StringBuilder();
                while (Read())
                {
                    if (Current.Equals('"'))
                    {
                        if (index + 1 == condition.Length)
                        {
                            index = condition.Length;
                            return sb.ToString();
                        }
                        char peek = condition[index + 1];
                        if (peek == ' ' || peek == ')' || peek == '(' || ConditionOperator.IsSymbol(peek))
                            return sb.ToString();
                        if (peek == '"')
                        {
                            index += 1;
                        }
                        else
                        {
                            if ((options & FullTextSearchOptions.ThrowOnUnbalancedQuotes) != 0)
                                return sb.ToString();
                        }
                    }
                    sb.Append(Current);
                }
                if ((options & FullTextSearchOptions.ThrowOnUnbalancedQuotes) != 0)
                    throw new InvalidOperationException("Unbalanced quotes.");
                return sb.ToString();
            }

            private bool Boq()
            {
                return index < 0;
            }

            private bool Eoq()
            {
                return index >= condition.Length;
            }
        }

        #endregion
    }
}