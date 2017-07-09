#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Loom.Collections;

#endregion

namespace Loom
{
    // TODO: Need a good home for all the extensions. These may interfere visually with the ActiveData classes.
    public static class ObjectExtensions
    {
        public static bool IsAnyOf<T>(this T item, params T[] items)
        {
            return items.Contains(item);
        }

//
//        public static TResult Convert<TSource, TResult>(this TSource source, Func<TSource, TResult> converter)
//        {
//            return converter.Invoke(source);
//        }
//
//        public static Func<TSource, TResult> CreateConverter<TSource, TResult>(this TSource source, Func<TSource, TResult> converter)
//        {
//            return converter;
//        }
//

        public static dynamic ToDynamic(this object obj, string dtoName = null)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            Dictionary<string, object> dict = obj as Dictionary<string, object>;
            if (dict == null)
                return new DictionaryDynamic();

            return Compare.IsNullOrEmpty(dtoName)
                ? new DictionaryDynamic(dict)
                : new DictionaryDynamic((Dictionary<string, object>) dict[dtoName]);
        }

        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            return obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(property => property.Name, property => property.GetValue(obj, null));
        }
    }
}