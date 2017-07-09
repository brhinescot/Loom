#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

namespace Loom.Web
{
    public static class TypeCache
    {
        public static IEnumerable<Type> GetFilteredTypesFromAssemblies(string cacheName, Predicate<Type> predicate, IBuildManager buildManager = null, bool includeSystemAssemblies = false)
        {
            Argument.Assert.IsNotNullOrEmpty(cacheName, nameof(cacheName));
            Argument.Assert.IsNotNull(predicate, nameof(predicate));

            TypeCacheSerializer serializer = new TypeCacheSerializer();
            if (buildManager == null)
                buildManager = new BuildManagerWrapper();

            IEnumerable<Type> cachedTypes = ReadTypesFromCache(cacheName, predicate, buildManager, serializer);
            if (cachedTypes != null)
                return cachedTypes;

            List<Type> filteredTypes = FilterTypesInAssemblies(buildManager, predicate, includeSystemAssemblies).ToList();
            SaveTypesToCache(cacheName, filteredTypes, buildManager, serializer);
            return filteredTypes;
        }

        private static IEnumerable<Type> ReadTypesFromCache(string cacheName, Predicate<Type> predicate, IBuildManager buildManager, TypeCacheSerializer serializer)
        {
            try
            {
                Stream stream = buildManager.ReadCachedFile(cacheName);
                if (stream != null)
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        List<Type> list = serializer.DeserializeTypes(streamReader);
                        if (list != null && list.All(type => predicate(type)))
                            return list;
                    }
            }
            catch { }

            return null;
        }

        private static void SaveTypesToCache(string cacheName, IEnumerable<Type> matchingTypes, IBuildManager buildManager, TypeCacheSerializer serializer)
        {
            try
            {
                Stream cachedFile = buildManager.CreateCachedFile(cacheName);
                if (cachedFile == null)
                    return;

                using (StreamWriter streamWriter = new StreamWriter(cachedFile))
                {
                    serializer.SerializeTypes(matchingTypes, streamWriter);
                }
            }
            catch { }
        }

        private static IEnumerable<Type> FilterTypesInAssemblies(IBuildManager buildManager, Predicate<Type> predicate, bool includeSystem = false)
        {
            IEnumerable<Type> enumerable = Type.EmptyTypes;
            foreach (Assembly assembly in buildManager.GetReferencedAssemblies())
            {
                if (!includeSystem && IsSystem(assembly))
                    continue;

                Type[] types;
                try
                {
                    types = assembly.GetExportedTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types;
                }

                enumerable = enumerable.Concat(types);
            }
            return enumerable.Where(type => predicate(type));
        }

        private static bool IsSystem(Assembly assembly)
        {
            return assembly.FullName.StartsWithAny("System.", "Microsoft.", "mscorlib", "Cpp", "AntiXss", "MbUnit", "itextsharp", "Enyim", "log4net", "NHamcrest", "Gallio");
        }
    }
}