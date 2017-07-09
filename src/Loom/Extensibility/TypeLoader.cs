#region Using Directives

using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Loom.Configuration;

#endregion

namespace Loom.Extensibility
{
    public static class TypeLoader
    {
        private static TypeLoaderSettingsSection config = (TypeLoaderSettingsSection) ConfigurationManager.GetSection("typeLoaderSettings");

        public static Type GetDerivedType<TBase>(string derivedTypeName)
        {
            Type baseType = typeof(TBase);
            return GetDerivedType(derivedTypeName, baseType);
        }

        public static Type GetDerivedType(string derivedTypeName, Type baseType)
        {
            if (config == null)
                config = new TypeLoaderSettingsSection();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWithAny(from IgnoreTypeElement element in config.IgnoreType select element.Name))
                    continue;

                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    types = exception.Types;
                }

                foreach (Type unknownType in types.Where(unknownType => unknownType.Name == derivedTypeName).Where(unknownType => TypeIsPublicClass(unknownType) && IsDerivedType(baseType, unknownType)))
                    return unknownType;
            }

            return null;
        }

        public static TBase CreateInstance<TBase>(string typeName)
        {
            Argument.Assert.IsNotNullOrEmpty(typeName, nameof(typeName));

            Type baseType = typeof(TBase);
            return (TBase) CreateInstance(typeName, baseType);
        }

        public static object CreateInstance(string typeName, Type baseType)
        {
            return Activator.CreateInstance(GetDerivedType(typeName, baseType));
        }

        /// <exception cref="InvalidOperationException">
        ///     The <paramref name="type" /> specified is not valid.
        /// </exception>
        public static T CreateInstance<T>(Type type) where T : class
        {
            // Instantiate a new T object.
            T t = Activator.CreateInstance(type) as T;
            if (t == null)
                throw new InvalidOperationException(string.Format(SR.ExceptionInvalidType(type.FullName), type));

            return t;
        }

        private static bool IsDerivedType(Type baseType, Type unknownType)
        {
            return baseType.IsAssignableFrom(unknownType) && unknownType.GetConstructor(Type.EmptyTypes) != null;
        }

        private static bool TypeIsPublicClass(Type type)
        {
            return type != null && (type.IsPublic || type.IsNestedPublic) && type.IsClass && !type.IsAbstract;
        }
    }
}