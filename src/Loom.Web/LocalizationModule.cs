#region Using Directives

using System;
using System.Web;
using Loom.Web.Localization;

#endregion

namespace Loom.Web
{
    public sealed class LocalizationModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.BeginRequest += HandleBeginRequest;
        }

        public void Dispose() { }

        #endregion

        private static void HandleBeginRequest(object sender, EventArgs e)
        {
            ClientLocalization.SetUserLocale(((HttpApplication) sender).Context, true);
        }
    }
}