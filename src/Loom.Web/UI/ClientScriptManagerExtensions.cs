#region Using Directives

using System;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using Loom.Cryptography;

#endregion

namespace Loom.Web.UI
{
    public static class ClientScriptManagerExtensions
    {
        public static bool IsVirtualClientScriptIncludeRegistered(this ClientScriptManager m, string key)
        {
            return IsVirtualClientScriptIncludeRegistered(m, typeof(Page), key);
        }

        public static bool IsVirtualClientScriptIncludeRegistered(this ClientScriptManager m, Type type, string key)
        {
            return m.IsClientScriptIncludeRegistered(type, "virtual_" + key);
        }

        public static void RegisterVirtualClientScriptInclude(this ClientScriptManager m, string key, string script)
        {
            RegisterVirtualClientScriptInclude(m, typeof(Page), key, script);
        }

        public static void RegisterVirtualClientScriptInclude(this ClientScriptManager m, Type type, string key, string script)
        {
            key = "virtual_" + key;

            if (m.IsClientScriptIncludeRegistered(type, key))
                return;

            string cachKey = HttpServerUtility.UrlTokenEncode(HashProvider.SHA256.GenerateBytes(key)) + ".js";
            string url = "/Virtual_Script/" + cachKey;

            Cache cache = HttpContext.Current.Cache;
            if (cache[cachKey] == null)
                cache.Add(cachKey, script, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60), CacheItemPriority.High, null);

            m.RegisterClientScriptInclude(type, key, url);
        }

        public static void RegisterGlobalClientScriptResource(this ClientScriptManager m, string resourceName)
        {
            m.RegisterClientScriptResource(typeof(JQueryResourcePath), resourceName);
        }
    }
}