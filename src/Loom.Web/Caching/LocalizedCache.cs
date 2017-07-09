#region Using Directives

using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Caching;

#endregion

namespace Loom.Web.Caching
{
    public class LocalizedCache : IEnumerable
    {
        private readonly Cache cache = HttpContext.Current.Cache;

        private LocalizedCache() { }

        public int Count => cache.Count;

        public static LocalizedCache Current { get; } = new LocalizedCache();

        public long EffectivePercentagePhysicalMemoryLimit => cache.EffectivePercentagePhysicalMemoryLimit;

        public long EffectivePrivateBytesLimit => cache.EffectivePrivateBytesLimit;

        public object this[string key]
        {
            get => cache[GetLocalizeKey(key)];
            set => cache[GetLocalizeKey(key)] = value;
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) cache).GetEnumerator();
        }

        #endregion

        public IDictionaryEnumerator GetEnumerator()
        {
            return cache.GetEnumerator();
        }

        public object Get(string key)
        {
            return cache.Get(GetLocalizeKey(key));
        }

        public void Insert(string key, object value)
        {
            cache.Insert(GetLocalizeKey(key), value);
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            cache.Insert(GetLocalizeKey(key), value, dependencies);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            cache.Insert(GetLocalizeKey(key), value, dependencies, absoluteExpiration, slidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            cache.Insert(GetLocalizeKey(key), value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        // Looks like this one is causing a build error because the server needs the latest .Net SP installed.
        //        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        //        {
        //            cache.Insert(GetLocalizeKey(key), value, dependencies, absoluteExpiration, slidingExpiration, onUpdateCallback);
        //        }

        public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            return cache.Add(GetLocalizeKey(key), value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public object Remove(string key)
        {
            return cache.Remove(GetLocalizeKey(key));
        }

        private static string GetLocalizeKey(string key)
        {
            return "__Localized_" + CultureInfo.CurrentUICulture.LCID + "_" + key;
        }
    }
}