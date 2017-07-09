#region Using Directives

using System;

#endregion

namespace Loom.Data
{
    [Flags]
    public enum FullTextSearchOptions
    {
        None = 0,
        Default = StemAll | TrimPrefixAll,
        StemAll = StemTerms | StemPhrases,
        TrimPrefixAll = TrimPrefixTerms | TrimPrefixPhrases,
        ThrowOnAll = ThrowOnUnbalancedParens | ThrowOnUnbalancedQuotes | ThrowOnInvalidNearUse,

        StemTerms = 1, // Apply FORMSOF(INFLECTIONAL) when not a prefix term or adjoining a NEAR.
        StemPhrases = 2,

        TrimPrefixTerms = 4, // Trim prefix terms to first intra-word asterisk (leaving inner asterisks
        TrimPrefixPhrases = 8, // will always result in no matches).

        ThrowOnUnbalancedParens = 128, // Otherwise silently patches up and prevents under/overflow.
        ThrowOnUnbalancedQuotes = 256, // Otherwise closes at end and assumes inner single instance quotes are intentional.
        ThrowOnInvalidNearUse = 512 // Otherwise silently switches the bad NEARs to ANDs.
    }
}