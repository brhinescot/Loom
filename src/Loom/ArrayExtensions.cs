#region Using Directives

using System;
using System.Collections.Generic;
using Loom.Annotations;

#endregion

namespace Loom
{
    /// <summary>
    ///     Summary description for ArrayExtensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Returns a copy of the given array.  The array's size is changed to
        ///     the specified length with it's content preserved.
        /// </summary>
        /// <param name="array">The array to increment.</param>
        /// <param name="newSize">The new length of the Array.</param>
        /// <returns></returns>
        public static T[] Resize<T>([NotNull] this T[] array, int newSize)
        {
            Argument.Assert.IsNotNull(array, nameof(array));
            Argument.Assert.IsNotNegative(newSize, nameof(newSize));

            return ResizePrivate(array, newSize);
        }

        /// <summary>
        ///     Returns a copy of the given array.  The array's size is incremented by
        ///     one with it's content preserved.
        /// </summary>
        /// <param name="array">The array to increment.</param>
        /// <returns></returns>
        public static T[] Increment<T>([NotNull] this T[] array)
        {
            Argument.Assert.IsNotNull(array, nameof(array));

            return ResizePrivate(array, array.Length + 1);
        }

        /// <summary>
        ///     Returns a copy of the given array.  The array's size is incremented by
        ///     one with it's content preserved.
        /// </summary>
        /// <param name="array">The array to increment.</param>
        /// <param name="newValue">The value to place at the new index.</param>
        /// <returns></returns>
        public static T[] Increment<T>([NotNull] this T[] array, [NotNull] T newValue)
        {
            Argument.Assert.IsNotNull(array, nameof(array));
            Argument.Assert.IsNotNull(newValue, nameof(newValue));

            return IncrementPrivate(array, newValue);
        }

        /// <summary>
        ///     Joins the arrays and returns a new array with the values of both arrays.
        /// </summary>
        /// <param name="array1">The first array to join.</param>
        /// <param name="array2">The second array to join.</param>
        /// <returns></returns>
        public static T[] Join<T>([NotNull] this T[] array1, [NotNull] T[] array2)
        {
            Argument.Assert.IsNotNull(array1, nameof(array1));
            Argument.Assert.IsNotNull(array2, nameof(array2));

            return JoinPrivate(array1, array2);
        }

        /// <summary>
        ///     Joins the specified array and collection and returns a new array with the values
        ///     of both.
        /// </summary>
        /// <param name="array">The array to join.</param>
        /// <param name="collection">The collection to join.</param>
        /// <returns></returns>
        public static T[] Join<T>([NotNull] this T[] array, [NotNull] ICollection<T> collection)
        {
            Argument.Assert.IsNotNull(array, nameof(array));
            Argument.Assert.IsNotNull(collection, nameof(collection));

            return JoinPrivate(array, collection);
        }

        /// <summary>
        ///     Segments the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="startIndex">The index.</param>
        /// <returns></returns>
        public static T[] Segment<T>([NotNull] this T[] array, int startIndex)
        {
            Argument.Assert.IsNotNull(array, nameof(array));
            Argument.Assert.IsNotNegative(startIndex, nameof(startIndex));

            return SegmentPrivate(array, startIndex, array.Length - startIndex);
        }

        /// <summary>
        ///     Segments the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="startIndex">The index.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static T[] Segment<T>([NotNull] this T[] array, int startIndex, int length)
        {
            Argument.Assert.IsNotNull(array, nameof(array));

            return SegmentPrivate(array, startIndex, length);
        }

        /// <summary>
        ///     Removes duplicate entries from the <see cref="Array" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] RemoveDuplicates<T>([NotNull] this T[] array)
        {
            Argument.Assert.IsNotNull(array, nameof(array));

            return RemoveDuplicatesPrivate(array);
        }

        private static T[] IncrementPrivate<T>(T[] array, T newValue)
        {
            T[] newArray = Resize(array, array.Length + 1);
            newArray[newArray.Length - 1] = newValue;
            return newArray;
        }

        private static T[] JoinPrivate<T>(T[] array1, T[] array2)
        {
            T[] newArray = Resize(array1, array1.Length + array2.Length);
            array2.CopyTo(newArray, array1.Length);
            return newArray;
        }

        private static T[] JoinPrivate<T>(T[] array, ICollection<T> collection)
        {
            T[] newArray = Resize(array, array.Length + collection.Count);
            collection.CopyTo(newArray, array.Length);
            return newArray;
        }

        private static T[] RemoveDuplicatesPrivate<T>(T[] array)
        {
            T[] result;
            if (array.Length <= 1)
            {
                result = new T[array.Length];
                array.CopyTo(result, 0);
                return result;
            }

            HashSet<T> set = new HashSet<T>(array);
            result = new T[set.Count];
            set.CopyTo(result);
            return result;
        }

        private static T[] ResizePrivate<T>(T[] array, int newSize)
        {
            T[] destination = new T[newSize];
            Array.Copy(array, destination, array.Length > newSize ? newSize : array.Length);
            return destination;
        }

        private static T[] SegmentPrivate<T>(T[] array, int startIndex, int length)
        {
            if (startIndex > array.Length || startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex", "The startIndex can not be less than 0 and can not exceed the length of the array.");

            if (length > array.Length || length < 0)
                throw new ArgumentOutOfRangeException("length", "The length can not be less than 0 and can not exceed the length of the array.");

            if (startIndex + length > array.Length)
                throw new ArgumentException("The startIndex plus the length can not exceed the length of the array.");

            T[] subArray = new T[length];
            Array.Copy(array, startIndex, subArray, 0, length);
            return subArray;
        }
    }
}