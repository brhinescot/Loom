#region Using Directives

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;

#endregion

namespace Loom.Web.Portal
{
    public interface IHttpResponse
    {
        IHttpCookieCollection Cookies { get; }
        NameValueCollection Headers { get; }
        int StatusCode { get; set; }
        int SubStatusCode { get; set; }
        string StatusDescription { get; set; }
        bool TrySkipIisCustomErrors { get; set; }
        bool BufferOutput { get; set; }
        string ContentType { get; set; }
        string Charset { get; set; }
        Encoding ContentEncoding { get; set; }
        Encoding HeaderEncoding { get; set; }
        IHttpCachePolicy Cache { get; }
        bool IsClientConnected { get; }
        bool IsRequestBeingRedirected { get; }
        string RedirectLocation { get; set; }
        TextWriter Output { get; set; }
        Stream OutputStream { get; }
        Stream Filter { get; set; }
        bool SuppressContent { get; set; }
        string Status { get; set; }
        bool Buffer { get; set; }
        int Expires { get; set; }
        DateTime ExpiresAbsolute { get; set; }
        string CacheControl { get; set; }
        void DisableKernelCache();
        void AddFileDependency(string filename);
        void AddFileDependencies(ArrayList filenames);
        void AddFileDependencies(string[] filenames);
        void AddCacheItemDependency(string cacheKey);
        void AddCacheItemDependencies(ArrayList cacheKeys);
        void AddCacheItemDependencies(string[] cacheKeys);
        void AddCacheDependency(params CacheDependency[] dependencies);
        void Close();
        void BinaryWrite(byte[] buffer);
        void Pics(string value);
        void AppendHeader(string name, string value);
        void AppendCookie(HttpCookie cookie);
        void SetCookie(HttpCookie cookie);
        void ClearHeaders();
        void ClearContent();
        void Clear();
        void Flush();
        void AppendToLog(string param);
        void Redirect(string url);
        void Redirect(string url, bool endResponse);
        void RedirectToRoute(object routeValues);
        void RedirectToRoute(string routeName);
        void RedirectToRoute(RouteValueDictionary routeValues);
        void RedirectToRoute(string routeName, object routeValues);
        void RedirectToRoute(string routeName, RouteValueDictionary routeValues);
        void RedirectToRoutePermanent(object routeValues);
        void RedirectToRoutePermanent(string routeName);
        void RedirectToRoutePermanent(RouteValueDictionary routeValues);
        void RedirectToRoutePermanent(string routeName, object routeValues);
        void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues);
        void RedirectPermanent(string url);
        void RedirectPermanent(string url, bool endResponse);
        void Write(string s);
        void Write(object obj);
        void Write(char ch);
        void Write(char[] buffer, int index, int count);
        void WriteSubstitution(HttpResponseSubstitutionCallback callback);
        void WriteFile(string filename);
        void WriteFile(string filename, bool readIntoMemory);
        void TransmitFile(string filename);
        void TransmitFile(string filename, long offset, long length);
        void WriteFile(string filename, long offset, long size);
        void WriteFile(IntPtr fileHandle, long offset, long size);
        void AddHeader(string name, string value);
        void End();
        string ApplyAppPathModifier(string virtualPath);
        void Complete();
    }
}