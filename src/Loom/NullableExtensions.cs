#region Using Directives

using System;

#endregion

namespace Loom
{
    public static class NullableExtensions
    {
        /// <summary>
        /// </summary>
        /// <remarks>
        ///     If the nullable type has no value, this method will attempt to retrieve a default value from
        ///     the <see cref="NullRepository"></see> class.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static TResult ConvertOrDefault<T, TResult>(this T? nullable, Func<T, TResult> converter) where T : struct
        {
            return nullable.HasValue ? converter(nullable.Value) : NullRepository.RetrieveConversion<T, TResult>();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nullable"></param>
        /// <param name="converter"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult ConvertOrDefault<T, TResult>(this T? nullable, Func<T, TResult> converter, TResult defaultValue) where T : struct
        {
            return nullable.HasValue ? converter(nullable.Value) : defaultValue;
        }
    }
}