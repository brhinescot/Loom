#region Using Directives

using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Caching;
using Loom.Web.Configuration;

#endregion

namespace Loom.Web
{
    public sealed class PageViewScriptBlockerModule : IHttpModule
    {
        private const string ConfigSection = "pageViewScriptBlockerSettings";
        private static readonly PageViewScriptBlockerSettingsSection Config = (PageViewScriptBlockerSettingsSection) ConfigurationManager.GetSection(ConfigSection);

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        public void Dispose() { }

        #endregion

        private static void ContextBeginRequest(object sender, EventArgs e)
        {
            if (IsValid())
                return;

            HttpResponse response = HttpContext.Current.Response;

            response.SuppressContent = true;
            response.Complete();
        }

        public static bool IsValid()
        {
            HttpContext context = HttpContext.Current;

            if (Path.GetExtension(context.Request.Url.AbsolutePath) != Config.Extension)
                return true;
            if (context.Request.Browser.Crawler)
                return false;

            string key = context.Request.Url.AbsolutePath + context.Request.UserHostAddress;

            Client client = (Client) (context.Cache[key] ?? new Client());
            if (client.Banned)
                return false;

            if (client.HitCount > Config.MaxRequestsPerCycle)
            {
                client.Banned = true;
                context.Cache.Insert(key, client, null, DateTime.Now.AddMinutes(Config.BanMinutes), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                return false;
            }

            client.HitCount++;

            if (client.HitCount == 1)
                context.Cache.Add(key, client, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(Config.RequestCycleSeconds), CacheItemPriority.High, null);

            return true;
        }

        #region Nested type: Client

        private class Client
        {
            public int HitCount { get; set; }
            public bool Banned { get; set; }
        }

        #endregion
    }
}