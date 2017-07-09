#region Using Directives

#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using Loom.Annotations;

#endregion

// ReSharper disable UnusedParameter.Global

#endregion

namespace Loom
{
    /// <summary>
    ///     <para>Common validation routines for argument validation.</para>
    /// </summary>
    [DebuggerStepThrough]
    public static class Argument
    {
        /// <summary>
        ///     Indicates whether any of the specified <see cref="object" /> arguments are null.
        /// </summary>
        /// <param name="arguments">
        ///     The <see cref="object" /> references.
        /// </param>
        /// <returns>
        ///     true if any of the objects in the arguments parameter is null;
        ///     otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsAnyNull(params object[] arguments)
        {
            foreach (object obj in arguments)
                if (obj == null)
                    return true;
            return false;
        }

        /// <summary>
        ///     Gets the bit count.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        private static int GetBitCount(uint x)
        {
            int bitCount = 0;
            while (x > 0)
            {
                x &= x - 1;
                bitCount++;
            }
            return bitCount;
        }

        #region Nested type: Assert

        /// <summary>
        /// </summary>
        [DebuggerStepThrough]
        public static class Assert
        {
            /// <summary>
            ///     <para>
            ///         Check if the <paramref name="argument" /> is an empty string.
            ///     </para>
            /// </summary>
            /// <param name="argument">
            ///     <para>The value to check.</para>
            /// </param>
            /// <param name="name">
            ///     <para>The name of the argument being checked.</para>
            /// </param>
            /// <exception cref="System.ArgumentNullException">
            ///     <pararef name="argument" /> can not be <see langword="null" /> (Nothing
            ///     in Visual Basic).
            ///     <para>- or -</para>
            ///     <pararef name="name" /> can not be <see langword="null" /> (Nothing
            ///     in Visual Basic).
            /// </exception>
            /// <exception cref="System.ArgumentException">
            ///     <pararef name="argument" /> can not be a zero length <see cref="string" />.
            /// </exception>
            [AssertionMethod]
            public static void IsNotNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, string name)
            {
                IsNotNull(argument, name);
                IsNotNull(name, nameof(name));

                if (Compare.IsNullOrEmpty(argument))
                    throw new ArgumentException(SR.ExceptionEmptyString(name));
            }

            [AssertionMethod]
            public static void IsNotNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] ICollection argument, string name)
            {
                IsNotNull(argument, name);
                IsNotNull(name, nameof(name));

                if (Compare.IsNullOrEmpty(argument))
                    throw new ArgumentException(SR.ExceptionEmptyString(name));
            }

            /// <summary>
            ///     <para>
            ///         Check if the <paramref name="argument" /> is <see langword="null" />
            ///         (Nothing in Visual Basic).
            ///     </para>
            /// </summary>
            /// <param name="argument">
            ///     <para>The value to check.</para>
            /// </param>
            /// <param name="name">
            ///     <para>The name of the argument being checked.</para>
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///     <pararef name="argument" /> can not <see langword="null" /> (Nothing in
            ///     Visual Basic).
            ///     <para>- or -</para>
            ///     <pararef name="variableName" /> can not <see langword="null" /> (Nothing in
            ///     Visual Basic).
            /// </exception>
            [AssertionMethod]
            public static void IsNotNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object argument, string name)
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                if (argument == null)
                    throw new ArgumentNullException(name);
            }

            /// <summary>
            ///     Checks <paramref name="argument" /> for a negative value and throws an
            ///     <see cref="ArgumentOutOfRangeException" /> exception if the value is negative.
            /// </summary>
            /// <param name="argument">The argument to check.</param>
            /// <param name="name">The name of the argument being checked.</param>
            public static void IsNotNegative(int argument, string name)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeNonNegative);
            }

            /// <summary>
            ///     Checks <paramref name="argument" /> for a negative value and throws an
            ///     <see cref="ArgumentOutOfRangeException" /> exception if the value is negative.
            /// </summary>
            /// <param name="argument">The argument to check.</param>
            /// <param name="name">The name of the argument being checked.</param>
            public static void IsNotNegative(float argument, string name)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeNonNegative);
            }

            /// <summary>
            ///     Checks <paramref name="argument" /> for a negative value and throws an
            ///     <see cref="ArgumentOutOfRangeException" /> exception if the value is negative.
            /// </summary>
            /// <param name="argument">The argument to check.</param>
            /// <param name="name">The name of the argument being checked.</param>
            public static void IsNotNegative(double argument, string name)
            {
                if (argument < 0)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeNonNegative);
            }

            public static void IsGreaterThanZero(int argument, string name)
            {
                if (argument < 1)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeGreaterThanZero);
            }

            public static void IsGreaterThanZero(float argument, string name)
            {
                if (argument < 1)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeGreaterThanZero);
            }

            public static void IsGreaterThanZero(double argument, string name)
            {
                if (argument < 1)
                    throw new ArgumentOutOfRangeException(name, SR.ExceptionArgumentMustBeGreaterThanZero);
            }

            /// <summary>
            ///     <para>
            ///         Checks <paramref name="argument" /> for zero items and throws
            ///         an <see cref="ArgumentException" /> if the count equals zero.
            ///     </para>
            /// </summary>
            /// <param name="argument">
            ///     The <see cref="ICollection" /> to check.
            /// </param>
            /// <param name="name">
            ///     <para>The name of the argument being checked.</para>
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="argument" /> can not <see langword="null" /> (Nothing in
            ///     Visual Basic).
            ///     <para>- or -</para>
            ///     <paramref name="name" /> can not <see langword="null" /> (Nothing
            ///     in Visual Basic).
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <para>
            ///         <paramref name="argument" /> must contain one or more items.
            ///     </para>
            /// </exception>
            [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
            public static void IsNotZeroLength(ICollection argument, string name)
            {
                IsNotNull(argument, nameof(argument));
                IsNotNull(name, nameof(name));

                if (argument.Count == 0)
                    throw new ArgumentException(SR.ExceptionItemCountMustBeGreaterThanZero, name);
            }

            /// <summary>
            ///     <para>
            ///         Check <paramref name="argument" /> to determine if it matches
            ///         the <see cref="Type" /> of <paramref name="type" />.
            ///     </para>
            /// </summary>
            /// <param name="argument">
            ///     <para>The value to check.</para>
            /// </param>
            /// <param name="type">
            ///     <para>
            ///         The <see cref="Type" /> expected type of <paramref name="argument" />.
            ///     </para>
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="argument" /> can not <see langword="null" /> (Nothing
            ///     in Visual Basic).
            ///     <para>- or -</para>
            ///     <paramref name="type" /> can not <see langword="null" /> (Nothing
            ///     in Visual Basic).
            /// </exception>
            /// <exception cref="ArgumentException">
            ///     <paramref name="argument" /> is not the expected <see cref="Type" />.
            /// </exception>
            [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
            public static void IsType(object argument, Type type)
            {
                IsNotNull(argument, nameof(argument));
                IsNotNull(type, nameof(type));

                if (!type.IsInstanceOfType(argument))
                    throw new ArgumentException(SR.ExceptionExpectedType(type.FullName));
            }

            /// <summary>
            ///     Checks the enumeration.
            /// </summary>
            /// <param name="enumType">Type of the enum.</param>
            /// <param name="enumValue">The value.</param>
            /// <param name="name">Name of the argument.</param>
            public static void EnumValueExists(Enum enumType, int enumValue, string name)
            {
                IsNotNull(enumType, nameof(enumType));

                if (!new List<int>((int[]) Enum.GetValues(enumType.GetType())).Contains(enumValue))
                    throw new InvalidEnumArgumentException(name, enumValue, enumType.GetType());
            }

            /// <summary>
            ///     Checks the enumeration.
            /// </summary>
            /// <param name="enumType">Type of the enum.</param>
            /// <param name="enumValue">The value.</param>
            /// <param name="maxNumberOfBitsOn">The max number of bits on.</param>
            /// <param name="name">Name of the argument.</param>
            public static void EnumValueExists(Enum enumType, int enumValue, string name, int maxNumberOfBitsOn)
            {
                IsNotNull(enumType, nameof(enumType));

                bool valid = new List<int>((int[]) Enum.GetValues(enumType.GetType())).Contains(enumValue);
                if (!(valid && GetBitCount((uint) enumValue) <= maxNumberOfBitsOn))
                    throw new InvalidEnumArgumentException(name, enumValue, enumType.GetType());
            }

            /// <summary>
            ///     <para>
            ///         Check <paramref name="path" /> to determine if the file exists.
            ///     </para>
            /// </summary>
            /// <param name="path"></param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="path" /> can not <see langword="null" />
            ///     (Nothing in Visual Basic).
            /// </exception>
            /// <exception cref="FileNotFoundException">
            ///     File
            ///     <paramref name="path" /> can not be found.
            /// </exception>
            public static void FileExists(string path)
            {
                IsNotNull(path, nameof(path));

                if (!File.Exists(path))
                    throw new FileNotFoundException(SR.ExceptionFileNotFound(path), path);
            }

            public static void IsNotSequential(IList<int> items)
            {
                for (int i = 1; i < items.Count; i++)
                    if (items[i] == items[i - 1] + 1)
                        throw new ArgumentException("Sequential item found at index '" + i + "'.");
            }

            /// <summary>
            ///     Asserts that the <paramref name="index" /> is within the range of items in the <paramref name="collection" />.
            /// </summary>
            /// <param name="collection">
            ///     A zero based <see cref="ICollection" />.
            /// </param>
            /// <param name="index">The index to validate.</param>
            /// <param name="name">The name of the argument being checked.</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static void IsInRange(ICollection collection, long index, string name)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));

                IsInRange(0, collection.Count, index, name);
            }

            /// <summary>
            ///     Asserts that the <paramref name="index" /> is within the range of items in the <paramref name="collection" />.
            /// </summary>
            /// <param name="collection">
            ///     A zero based <see cref="ICollection" />.
            /// </param>
            /// <param name="index">The index to validate.</param>
            /// <param name="name">The name of the argument being checked.</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static void IsInRange<T>(ICollection<T> collection, long index, string name)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));

                IsInRange(0, collection.Count, index, name);
            }

            /// <summary>
            ///     Asserts that the <paramref name="index" /> is within the range of items in the <paramref name="array" />.
            /// </summary>
            /// <param name="array">
            ///     A zero based <see cref="ICollection" />.
            /// </param>
            /// <param name="index">The index to validate.</param>
            /// <param name="name">The name of the argument being checked.</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static void IsInRange<T>(T[] array, long index, string name)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                IsInRange(0, array.Length, index, name);
            }

            /// <summary>
            ///     Asserts that the <paramref name="index" /> is within the range of <paramref name="lowerBound" /> and
            ///     <paramref name="upperBound" />.
            /// </summary>
            /// <param name="lowerBound"></param>
            /// <param name="upperBound"></param>
            /// <param name="index"></param>
            /// <param name="name"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public static void IsInRange(long lowerBound, long upperBound, long index, string name)
            {
                if (index < lowerBound || index > upperBound)
                    throw new ArgumentOutOfRangeException(name, index, "The index is invalid.");
            }

            public static void IsInDateRange(DateTime argument, string name, DateTime startDate, DateTime endDate)
            {
                IsNotNullOrEmpty(name, nameof(name));

                if (argument < startDate || argument > endDate)
                    throw new ArgumentOutOfRangeException(name, argument, string.Concat("The date must be between ", startDate, " and ", endDate, "."));
            }

            public static void IsNotInDateRange(DateTime argument, string name, DateTime startDate, DateTime endDate)
            {
                IsNotNullOrEmpty(name, nameof(name));

                if (argument >= startDate || argument <= endDate)
                    throw new ArgumentOutOfRangeException(name, argument, string.Concat("The date must not be between ", startDate, " and ", endDate, "."));
            }

            public static void IsNotCollection(object argument, string name)
            {
                IsNotNull(argument, nameof(argument));
                IsNotNullOrEmpty(name, nameof(name));

                if (argument is ICollection)
                    throw new ArgumentException(string.Format("The parameter '{0}' can not be of type ICollection.", name), name);
            }

            public static void IsNotReadonly<T>(ICollection<T> argument, string name)
            {
                IsNotNull(argument, nameof(argument));
                IsNotNullOrEmpty(name, nameof(name));

                if (argument.IsReadOnly)
                    throw new NotSupportedException("The argument " + name + " is read only and can not be modified.");
            }

            public static void IsNotReadonly(IList argument, string name)
            {
                IsNotNull(argument, nameof(argument));
                IsNotNullOrEmpty(name, nameof(name));

                if (argument.IsReadOnly)
                    throw new NotSupportedException("The argument " + name + " is read only and can not be modified.");
            }
        }

        #endregion
    }
}