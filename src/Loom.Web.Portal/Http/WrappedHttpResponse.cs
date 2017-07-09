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

namespace Loom.Web.Portal.Http
{
    public sealed class WrappedHttpResponse : IHttpResponse
    {
        private readonly WrappedHttpCachePolicy cache;
        private readonly WrappedHttpCookieCollection cookies;
        private readonly HttpResponse response;

        public WrappedHttpResponse(HttpResponse response)
        {
            Argument.Assert.IsNotNull(response, nameof(response));

            this.response = response;
            cookies = new WrappedHttpCookieCollection(response.Cookies);
            cache = new WrappedHttpCachePolicy(response.Cache);
        }

        #region IHttpResponse Members

        public void DisableKernelCache()
        {
            response.DisableKernelCache();
        }

        public void AddFileDependency(string filename)
        {
            response.AddFileDependency(filename);
        }

        public void AddFileDependencies(ArrayList filenames)
        {
            response.AddFileDependencies(filenames);
        }

        public void AddFileDependencies(string[] filenames)
        {
            response.AddFileDependencies(filenames);
        }

        public void AddCacheItemDependency(string cacheKey)
        {
            response.AddCacheItemDependency(cacheKey);
        }

        public void AddCacheItemDependencies(ArrayList cacheKeys)
        {
            response.AddCacheItemDependencies(cacheKeys);
        }

        public void AddCacheItemDependencies(string[] cacheKeys)
        {
            response.AddCacheItemDependencies(cacheKeys);
        }

        public void AddCacheDependency(params CacheDependency[] dependencies)
        {
            response.AddCacheDependency(dependencies);
        }

        public void Close()
        {
            response.Close();
        }

        public void BinaryWrite(byte[] buffer)
        {
            response.BinaryWrite(buffer);
        }

        public void Pics(string value)
        {
            response.Pics(value);
        }

        public void AppendHeader(string name, string value)
        {
            response.AppendHeader(name, value);
        }

        public void AppendCookie(HttpCookie cookie)
        {
            response.AppendCookie(cookie);
        }

        public void SetCookie(HttpCookie cookie)
        {
            response.SetCookie(cookie);
        }

        public void ClearHeaders()
        {
            response.ClearHeaders();
        }

        public void ClearContent()
        {
            response.ClearContent();
        }

        public void Clear()
        {
            response.Clear();
        }

        public void Flush()
        {
            response.Flush();
        }

        public void AppendToLog(string param)
        {
            response.AppendToLog(param);
        }

        public void Redirect(string url)
        {
            response.Redirect(url);
        }

        public void Redirect(string url, bool endResponse)
        {
            response.Redirect(url, endResponse);
        }

        public void RedirectToRoute(object routeValues)
        {
            response.RedirectToRoute(routeValues);
        }

        public void RedirectToRoute(string routeName)
        {
            response.RedirectToRoute(routeName);
        }

        public void RedirectToRoute(RouteValueDictionary routeValues)
        {
            response.RedirectToRoute(routeValues);
        }

        public void RedirectToRoute(string routeName, object routeValues)
        {
            response.RedirectToRoute(routeName, routeValues);
        }

        public void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
        {
            response.RedirectToRoute(routeName, routeValues);
        }

        public void RedirectToRoutePermanent(object routeValues)
        {
            response.RedirectToRoutePermanent(routeValues);
        }

        public void RedirectToRoutePermanent(string routeName)
        {
            response.RedirectToRoutePermanent(routeName);
        }

        public void RedirectToRoutePermanent(RouteValueDictionary routeValues)
        {
            response.RedirectToRoutePermanent(routeValues);
        }

        public void RedirectToRoutePermanent(string routeName, object routeValues)
        {
            response.RedirectToRoutePermanent(routeName, routeValues);
        }

        public void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
        {
            response.RedirectToRoutePermanent(routeName, routeValues);
        }

        public void RedirectPermanent(string url)
        {
            response.RedirectPermanent(url);
        }

        public void RedirectPermanent(string url, bool endResponse)
        {
            response.RedirectPermanent(url, endResponse);
        }

        public void Write(string s)
        {
            response.Write(s);
        }

        public void Write(object obj)
        {
            response.Write(obj);
        }

        public void Write(char ch)
        {
            response.Write(ch);
        }

        public void Write(char[] buffer, int index, int count)
        {
            response.Write(buffer, index, count);
        }

        public void WriteSubstitution(HttpResponseSubstitutionCallback callback)
        {
            response.WriteSubstitution(callback);
        }

        public void WriteFile(string filename)
        {
            response.WriteFile(filename);
        }

        public void WriteFile(string filename, bool readIntoMemory)
        {
            response.WriteFile(filename, readIntoMemory);
        }

        public void TransmitFile(string filename)
        {
            response.TransmitFile(filename);
        }

        public void TransmitFile(string filename, long offset, long length)
        {
            response.TransmitFile(filename, offset, length);
        }

        public void WriteFile(string filename, long offset, long size)
        {
            response.WriteFile(filename, offset, size);
        }

        public void WriteFile(IntPtr fileHandle, long offset, long size)
        {
            response.WriteFile(fileHandle, offset, size);
        }

        public void AddHeader(string name, string value)
        {
            response.AddHeader(name, value);
        }

        public void End()
        {
            response.End();
        }

        public void Complete()
        {
            response.Complete();
        }

        public string ApplyAppPathModifier(string virtualPath)
        {
            return response.ApplyAppPathModifier(virtualPath);
        }

        public IHttpCookieCollection Cookies => cookies;

        public NameValueCollection Headers => response.Headers;

        public int StatusCode
        {
            get => response.StatusCode;
            set => response.StatusCode = value;
        }

        public int SubStatusCode
        {
            get => response.SubStatusCode;
            set => response.SubStatusCode = value;
        }

        public string StatusDescription
        {
            get => response.StatusDescription;
            set => response.StatusDescription = value;
        }

        public bool TrySkipIisCustomErrors
        {
            get => response.TrySkipIisCustomErrors;
            set => response.TrySkipIisCustomErrors = value;
        }

        public bool BufferOutput
        {
            get => response.BufferOutput;
            set => response.BufferOutput = value;
        }

        public string ContentType
        {
            get => response.ContentType;
            set => response.ContentType = value;
        }

        public string Charset
        {
            get => response.Charset;
            set => response.Charset = value;
        }

        public Encoding ContentEncoding
        {
            get => response.ContentEncoding;
            set => response.ContentEncoding = value;
        }

        public Encoding HeaderEncoding
        {
            get => response.HeaderEncoding;
            set => response.HeaderEncoding = value;
        }

        public IHttpCachePolicy Cache => cache;

        public bool IsClientConnected => response.IsClientConnected;

        public bool IsRequestBeingRedirected => response.IsRequestBeingRedirected;

        public string RedirectLocation
        {
            get => response.RedirectLocation;
            set => response.RedirectLocation = value;
        }

        public TextWriter Output
        {
            get => response.Output;
            set => response.Output = value;
        }

        public Stream OutputStream => response.OutputStream;

        public Stream Filter
        {
            get => response.Filter;
            set => response.Filter = value;
        }

        public bool SuppressContent
        {
            get => response.SuppressContent;
            set => response.SuppressContent = value;
        }

        public string Status
        {
            get => response.Status;
            set => response.Status = value;
        }

        public bool Buffer
        {
            get => response.Buffer;
            set => response.Buffer = value;
        }

        public int Expires
        {
            get => response.Expires;
            set => response.Expires = value;
        }

        public DateTime ExpiresAbsolute
        {
            get => response.ExpiresAbsolute;
            set => response.ExpiresAbsolute = value;
        }

        public string CacheControl
        {
            get => response.CacheControl;
            set => response.CacheControl = value;
        }

        #endregion
    }
}