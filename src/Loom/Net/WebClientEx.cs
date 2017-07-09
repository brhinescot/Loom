#region Using Directives

using System;
using System.Net;
using System.Security;

#endregion

namespace Loom.Net
{
    public class WebClientEx : WebClient
    {
        [SecuritySafeCritical]
        public WebClientEx() { }

        public CookieContainer CookieContainer { get; } = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            if (request != null)
                request.CookieContainer = CookieContainer;
            return request;
        }
    }
}