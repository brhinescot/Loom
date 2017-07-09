#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Loom
{
    public static class EnumerableExtensions
    {
        public static Type GetElementType<T>(this IEnumerable<T> enumerable)
        {
            Argument.Assert.IsNotNull(enumerable, nameof(enumerable));

            return GetElementTypePrivate(enumerable);
        }

        private static Type GetElementTypePrivate<T>(IEnumerable<T> enumerable)
        {
            Type elementType = enumerable.GetType();

            Type t = FindIEnumerable(elementType);
            return t == null ? elementType : t.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.IsGenericType)
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                        return ienum;
                }

            Type[] interfaces = seqType.GetInterfaces();
            if (interfaces.Length > 0)
                foreach (Type type in interfaces)
                {
                    Type ienum = FindIEnumerable(type);
                    if (ienum != null)
                        return ienum;
                }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
                return FindIEnumerable(seqType.BaseType);

            return null;
        }
    }
}