#region Using Directives

using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web
{
    public static class HttpExtensions
    {
        public static HttpCookie GetCookie(this HttpRequest request, string name)
        {
            HttpCookieCollection cookies = request.Cookies;
            return FindCookie(cookies, name);
        }

        public static string GetSafeParameter(this HttpRequest request, string parameterName)
        {
            Argument.Assert.IsNotNull(request, "request");

            string value = request.Form[parameterName];
            return value != null ? AntiXss.HtmlEncode(value) : null;
        }

        public static void Complete(this HttpResponse response)
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static HttpCookie GetCookie(this HttpResponse response, string name)
        {
            HttpCookieCollection cookies = response.Cookies;
            return FindCookie(cookies, name);
        }

        public static void DeleteCookie(this HttpResponse response, string name)
        {
            if (HttpContext.Current.Request.Cookies[name] == null)
                return;

            response.Cookies.Add(new HttpCookie(name) {Expires = DateTime.Now.AddDays(-1d)});
        }

        public static void WriteJson(this HttpResponse response, StringCreator jsonStringCreator)
        {
            PrepareJsonResponse(response);
            try
            {
                string s = jsonStringCreator();
                response.Write(string.IsNullOrEmpty(s) ? "[]" : s);
            }
            catch (Exception)
            {
                response.Status = "500 Server Error";
                response.StatusCode = 500;
            }

            response.Complete();
        }

        public static void WriteJson(this HttpResponse response, string json)
        {
            PrepareJsonResponse(response);
            response.Write(string.IsNullOrEmpty(json) ? "[]" : json);
            response.Complete();
        }

        public static void RegisterVirtualPathProvider(VirtualPathProvider provider)
        {
            // We get the current instance of HostingEnvironment class. We can't create a new one
            // because it is not allowed to do so. An AppDomain can only have one HostingEnvironment
            // instance.
            HostingEnvironment hostingEnvironmentInstance = (HostingEnvironment) typeof(HostingEnvironment).InvokeMember("_theHostingEnvironment", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
            if (hostingEnvironmentInstance == null)
                return;

            // We get the MethodInfo for RegisterVirtualPathProviderInternal method which is internal
            // and also static.
            MethodInfo mi = typeof(HostingEnvironment).GetMethod("RegisterVirtualPathProviderInternal", BindingFlags.NonPublic | BindingFlags.Static);

            // Finally we invoke RegisterVirtualPathProviderInternal method with one argument which
            // is the instance of our own VirtualPathProvider.
            mi?.Invoke(hostingEnvironmentInstance, new object[] {provider});
        }

        private static void PrepareJsonResponse(HttpResponse response)
        {
            response.Clear();
            response.ClearHeaders();
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        private static HttpCookie FindCookie(HttpCookieCollection cookies, string name)
        {
            int count = cookies.Count;

            for (int i = 0; i < count; i++)
            {
                HttpCookie httpCookie = cookies[i];
                if (httpCookie != null && string.Compare(httpCookie.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
                    return cookies[i];
            }

            return null;
        }
    }
}