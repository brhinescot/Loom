#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Loom.Collections
{
    /// <summary>
    ///     Delimits a section of a one-dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array segment.</typeparam>
    /// <remarks>
    ///     <para>
    ///         <see cref="DelimitedArray{T}" /> is a wrapper around an array that delimits a range
    ///         of elements in that array. Multiple <see cref="DelimitedArray{T}" /> instances can
    ///         refer to the same original array and can overlap.
    ///     </para>
    ///     <para>
    ///         The <see cref="ToArray()" /> method returns a new array containing the
    ///         delimited range of elements; therefore, changes made to the array returned
    ///         by <see cref="ToArray()" /> are not are made to the original array.
    ///     </para>
    ///     <para>
    ///         The original array must be one-dimensional and must have zero-based indexing.
    ///     </para>
    ///     <para>
    ///         The array elements not in the delimited range are protected against being
    ///         changed.
    ///     </para>
    /// </remarks>
    public class DelimitedArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// </summary>
        public static readonly DelimitedArray<T> Empty = new DelimitedArray<T>(new T[0]);

        private readonly T[] array;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DelimitedArray{T}" /> class.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="offset">The zero based offset.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="array" /> is a
        ///     null reference (Nothing in Visual
        ///     Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="offset" />
        ///     is less
        ///     than 0 or greater than the length of <paramref name="array" />.
        /// </exception>
        public DelimitedArray(T[] array, int offset = 0) : this(array, offset, array.Length - offset) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DelimitedArray{T}" /> class.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="offset">The zero based offset.</param>
        /// <param name="count">The count.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="array" /> is a null
        ///     reference (Nothing in Visual
        ///     Basic).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="offset" /> or
        ///     <paramref name="count" /> is less
        ///     than 0 or greater than the length of <paramref name="array" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="offset" /> and
        ///     <paramref name="count" /> do not specify a
        ///     valid range in array.
        /// </exception>
        public DelimitedArray(T[] array, int offset, int count)
        {
            Argument.Assert.IsNotNull(array, "array");

            if (offset > array.Length || offset < 0)
                throw new ArgumentOutOfRangeException("offset", "The offset can not be less than 0 and can not exceed the length of the array.");

            if (count > array.Length || count < 0)
                throw new ArgumentOutOfRangeException("count", "The count can not be less than 0 and can not exceed the length of the array.");

            if (offset + count > array.Length)
                throw new ArgumentException("The offset plus the count can not exceed the length of the array.");

            this.array = array;
            Offset = offset;
            Count = count;
        }

        /// <summary>
        ///     Gets the number of elements in the delimited range.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; }

        /// <summary>
        ///     Gets the item at the specified index relative to the start of the
        ///     delimited range.
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get
            {
                int innerIndex = Offset + index;
                if (innerIndex >= Offset + Count || innerIndex < 0)
                    throw new ArgumentOutOfRangeException(SR.ExceptionDelimitedArrayIndexOutOfBounds(index));

                return array[innerIndex];
            }
        }

        /// <summary>
        ///     Gets the position of the first element in the delimited range
        ///     relative to the start of the original array.
        /// </summary>
        /// <value>The offset.</value>
        public int Offset { get; }

        #region IEnumerable<T> Members

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator{T}"></see> object
        ///     that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Offset; i < Offset + Count; i++)
                yield return array[i];
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator"></see> object that can
        ///     be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        ///     Returns a new array containing only the elements in the delimited range.
        /// </summary>
        /// <remarks>
        ///     The <see cref="ToArray()" /> method returns a new array containing
        ///     the delimited range of elements; therefore, changes made to the array
        ///     returned by <see cref="ToArray()" /> are not are made to the original
        ///     array.
        /// </remarks>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] newArray = new T[Count];
            Array.Copy(array, Offset, newArray, 0, Count);
            return newArray;
        }
    }
}