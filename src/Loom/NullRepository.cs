#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Loom
{
    public static class NullRepository
    {
        /// <summary>
        ///     Registers input value as "null object" for type
        /// </summary>
        /// <typeparam name="T">Input value type</typeparam>
        /// <param name="item">Input value</param>
        public static void RegisterValue<T>(T item) where T : class
        {
            NullObjectRepository<T>.NullObject = item;
        }

        public static void RegisterConversionValue<T>(object defaultValue) where T : struct
        {
            NullObjectRepository<T>.Register(defaultValue);
        }

        /// <summary>
        ///     Gets the current value if the input value is not null, else returns registered null object
        /// </summary>
        /// <typeparam name="T">Input value type</typeparam>
        /// <param name="item">Input value</param>
        /// <returns></returns>
        public static T Retrieve<T>(T item) where T : class
        {
            return item ?? NullObjectRepository<T>.NullObject;
        }

        public static TDefault RetrieveConversion<T, TDefault>()
        {
            return NullObjectRepository<T>.CurrentOrDefaultConversionValue<TDefault>();
        }

        #region Nested type: NullObjectRepository

        private static class NullObjectRepository<T>
        {
            private static readonly Dictionary<Type, object> Repository = new Dictionary<Type, object>();

            public static T NullObject { get; set; }

            public static void Register(object defaultValue)
            {
                Repository.Add(defaultValue.GetType(), defaultValue);
            }

            public static TDefault CurrentOrDefaultConversionValue<TDefault>()
            {
                Type type = typeof(TDefault);
                if (!Repository.ContainsKey(type))
                    return default(TDefault);

                object result = Repository[type];
                if (result == null)
                    return default(TDefault);

                return (TDefault) result;
            }
        }

        #endregion
    }
}