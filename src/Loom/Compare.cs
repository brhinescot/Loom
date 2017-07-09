#region Using Directives

#region Using Directives

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Loom.Annotations;

#endregion

// ReSharper disable ReplaceWithStringIsNullOrEmpty

#endregion

namespace Loom
{
    /// <summary>
    ///     A class containing utility methods for comparison operations.
    /// </summary>
    /// <remarks>This class can not be inherited.</remarks>
    [DebuggerStepThrough]
    public static class Compare
    {
        /// <summary>
        ///     Compares two specified <see cref="string" /> objects by evaluating the numeric values of the
        ///     corresponding <see cref="char" /> objects in each string.
        /// </summary>
        /// <param name="strA">
        ///     The first <see cref="string" />.
        /// </param>
        /// <param name="strB">
        ///     The second <see cref="string" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the parameters are equal; otherwise, <see langword="false" />.
        /// </returns>
        public static bool AreSameOrdinal(string strA, string strB)
        {
            return string.CompareOrdinal(strA, strB) == 0;
        }

        /// <summary>
        ///     Compares two specified <see cref="string" /> objects by evaluating the numeric values of the
        ///     corresponding <see cref="char" /> objects in each string.
        /// </summary>
        /// <param name="strA">
        ///     The first <see cref="string" />.
        /// </param>
        /// <param name="strB">
        ///     The second <see cref="string" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the parameters are equal; otherwise, <see langword="false" />.
        /// </returns>
        public static bool AreSameOrdinalIgnoreCase(string strA, string strB)
        {
            return string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        ///     Compares two specified <see cref="string" /> objects.
        /// </summary>
        /// <param name="strA">
        ///     The first <see cref="string" />.
        /// </param>
        /// <param name="strB">
        ///     The second <see cref="string" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the parameters are equal; otherwise, <see langword="false" />.
        /// </returns>
        public static bool AreSameInvariant(string strA, string strB)
        {
            return string.Compare(strA, strB, StringComparison.InvariantCulture) == 0;
        }

        /// <summary>
        ///     Compares two specified <see cref="string" /> objects.
        /// </summary>
        /// <param name="strA">
        ///     The first <see cref="string" />.
        /// </param>
        /// <param name="strB">
        ///     The second <see cref="string" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the parameters are equal; otherwise, <see langword="false" />.
        /// </returns>
        public static bool AreSameInvariantIgnoreCase(string strA, string strB)
        {
            return string.Compare(strA, strB, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        ///     Compares two specified <see cref="string" /> objects.
        /// </summary>
        /// <param name="strA">
        ///     The first <see cref="string" />.
        /// </param>
        /// <param name="strB">
        ///     The second <see cref="string" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the parameters are equal; otherwise, <see langword="false" />.
        /// </returns>
        public static bool AreSameCurrentCulture(string strA, string strB)
        {
            return string.Compare(strA, strB, StringComparison.CurrentCulture) == 0;
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="string" /> object is null or
        ///     an <see cref="string.Empty" /> string.
        /// </summary>
        /// <remarks>
        ///     This method is functionally identical to <see cref="string.IsNullOrEmpty" /> except it
        ///     is not inlined by the compiler. In certain situations where the <see cref="string.IsNullOrEmpty" />
        ///     method is used in a tight loop, the JIT optimization can cause it to throw a <see cref="NullReferenceException" />.
        /// </remarks>
        /// <param name="value">
        ///     A <see cref="string" /> reference.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if the value parameter is null or an empty string ("");
        ///     otherwise, <see langword="false" />.
        /// </returns>
        [AssertionMethod]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value)
        {
            return value == null || value.Length == 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsAnyNullOrEmpty(params string[] values)
        {
            return values == null || values.Any(s => s == null || s.Length == 0);
        }

        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string TrimIfStartsWith(string valueToSearch, string valueToRemove)
        {
            if (IsNullOrEmpty(valueToRemove) || IsNullOrEmpty(valueToSearch))
                return valueToSearch;

            return valueToSearch.StartsWith(valueToRemove, StringComparison.OrdinalIgnoreCase)
                ? valueToSearch.Substring(valueToRemove.Length).TrimStart()
                : valueToSearch;
        }

        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string TrimIfEndsWith(string valueToSearch, string valueToRemove)
        {
            if (IsNullOrEmpty(valueToRemove) || IsNullOrEmpty(valueToSearch))
                return valueToSearch;

            return valueToSearch.EndsWith(valueToRemove, StringComparison.OrdinalIgnoreCase)
                ? valueToSearch.Substring(0, valueToSearch.Length - valueToRemove.Length).TrimEnd()
                : valueToSearch;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsNullOrEmpty(ICollection items)
        {
            return items == null || items.Count == 0;
        }
    }
}