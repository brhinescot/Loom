#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web.Portal
{
    public class ContextSettings
    {
        public ContextSettings(HttpApplication httpApplication, DateTime lastModified)
        {
            HttpApplication = httpApplication;
            LastModified = lastModified;
        }

        public HttpApplication HttpApplication { get; set; }
        public DateTime LastModified { get; set; }
    }
}