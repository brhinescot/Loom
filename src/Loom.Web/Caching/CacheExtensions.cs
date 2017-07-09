#region Using Directives

using System.Collections;
using System.Web.Caching;
using Loom.Web.Localization;

#endregion

namespace Loom.Web.Caching
{
    public static class CacheExtensions
    {
        internal const string CachePrefix = "__CachedControlResource_";

        public static void ClearLocalizationCache(this Cache cache)
        {
            DbResourceConfiguration.ClearResourceCache();

            foreach (DictionaryEntry item in cache)
            {
                string key = item.Key as string;
                if (key == null)
                    continue;

                if (key.StartsWith(CachePrefix))
                    cache.Remove(key);
            }
        }
    }
}