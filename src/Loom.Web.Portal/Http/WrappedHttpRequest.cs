#region Using Directives

using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Routing;

#endregion

namespace Loom.Web.Portal.Http
{
    public sealed class WrappedHttpRequest : IHttpRequest
    {
        private readonly HttpRequest request;

        public WrappedHttpRequest(HttpRequest request)
        {
            Argument.Assert.IsNotNull(request, nameof(request));

            this.request = request;
        }

        #region IHttpRequest Members

        public byte[] BinaryRead(int count)
        {
            return request.BinaryRead(count);
        }

        public void ValidateInput()
        {
            request.ValidateInput();
        }

        public int[] MapImageCoordinates(string imageFieldName)
        {
            return request.MapImageCoordinates(imageFieldName);
        }

        public void SaveAs(string filename, bool includeHeaders)
        {
            request.SaveAs(filename, includeHeaders);
        }

        public string MapPath(string virtualPath)
        {
            return request.MapPath(virtualPath);
        }

        public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
        {
            return request.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
        }

        public void InsertEntityBody(byte[] buffer, int offset, int count)
        {
            request.InsertEntityBody(buffer, offset, count);
        }

        public void InsertEntityBody()
        {
            request.InsertEntityBody();
        }

        public Stream GetBufferlessInputStream()
        {
            return request.GetBufferlessInputStream();
        }

        public RequestContext RequestContext => request.RequestContext;

        public bool IsLocal => request.IsLocal;

        public string HttpMethod => request.HttpMethod;

        public string RequestType
        {
            get => request.RequestType;
            set => request.RequestType = value;
        }

        public string ContentType
        {
            get => request.ContentType;
            set => request.ContentType = value;
        }

        public int ContentLength => request.ContentLength;

        public Encoding ContentEncoding
        {
            get => request.ContentEncoding;
            set => request.ContentEncoding = value;
        }

        public string[] AcceptTypes => request.AcceptTypes;

        public bool IsAuthenticated => request.IsAuthenticated;

        public bool IsSecureConnection => request.IsSecureConnection;

        public string Path => request.Path;

        public string AnonymousID => request.AnonymousID;

        public string FilePath => request.FilePath;

        public string CurrentExecutionFilePath => request.CurrentExecutionFilePath;

        public string CurrentExecutionFilePathExtension => request.CurrentExecutionFilePathExtension;

        public string AppRelativeCurrentExecutionFilePath => request.AppRelativeCurrentExecutionFilePath;

        public string PathInfo => request.PathInfo;

        public string PhysicalPath => request.PhysicalPath;

        public string ApplicationPath => request.ApplicationPath;

        public string PhysicalApplicationPath => request.PhysicalApplicationPath;

        public string UserAgent => request.UserAgent;

        public string[] UserLanguages => request.UserLanguages;

        public HttpBrowserCapabilities Browser
        {
            get => request.Browser;
            set => request.Browser = value;
        }

        public string UserHostName => request.UserHostName;

        public string UserHostAddress => request.UserHostAddress;

        public string RawUrl => request.RawUrl;

        public Uri Url => request.Url;

        public Uri UrlReferrer => request.UrlReferrer;

        public NameValueCollection Params => request.Params;

        public string this[string key] => request[key];

        public NameValueCollection QueryString => request.QueryString;

        public NameValueCollection Form => request.Form;

        public NameValueCollection Headers => request.Headers;

        public NameValueCollection ServerVariables => request.ServerVariables;

        public HttpCookieCollection Cookies => request.Cookies;

        public HttpFileCollection Files => request.Files;

        public Stream InputStream => request.InputStream;

        public int TotalBytes => request.TotalBytes;

        public Stream Filter
        {
            get => request.Filter;
            set => request.Filter = value;
        }

        public HttpClientCertificate ClientCertificate => request.ClientCertificate;

        public WindowsIdentity LogonUserIdentity => request.LogonUserIdentity;

        public ChannelBinding HttpChannelBinding => request.HttpChannelBinding;

        #endregion
    }
}