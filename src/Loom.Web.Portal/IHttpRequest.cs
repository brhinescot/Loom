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

namespace Loom.Web.Portal
{
    public interface IHttpRequest
    {
        RequestContext RequestContext { get; }
        bool IsLocal { get; }
        string HttpMethod { get; }
        string RequestType { get; set; }
        string ContentType { get; set; }
        int ContentLength { get; }
        Encoding ContentEncoding { get; set; }
        string[] AcceptTypes { get; }
        bool IsAuthenticated { get; }
        bool IsSecureConnection { get; }
        string Path { get; }
        string AnonymousID { get; }
        string FilePath { get; }
        string CurrentExecutionFilePath { get; }
        string CurrentExecutionFilePathExtension { get; }
        string AppRelativeCurrentExecutionFilePath { get; }
        string PathInfo { get; }
        string PhysicalPath { get; }
        string ApplicationPath { get; }
        string PhysicalApplicationPath { get; }
        string UserAgent { get; }
        string[] UserLanguages { get; }
        HttpBrowserCapabilities Browser { get; set; }
        string UserHostName { get; }
        string UserHostAddress { get; }
        string RawUrl { get; }
        Uri Url { get; }
        Uri UrlReferrer { get; }
        NameValueCollection Params { get; }
        NameValueCollection QueryString { get; }
        NameValueCollection Form { get; }
        NameValueCollection Headers { get; }
        NameValueCollection ServerVariables { get; }
        HttpCookieCollection Cookies { get; }
        HttpFileCollection Files { get; }
        Stream InputStream { get; }
        int TotalBytes { get; }
        Stream Filter { get; set; }
        HttpClientCertificate ClientCertificate { get; }
        WindowsIdentity LogonUserIdentity { get; }
        ChannelBinding HttpChannelBinding { get; }
        string this[string key] { get; }
        byte[] BinaryRead(int count);
        void ValidateInput();
        int[] MapImageCoordinates(string imageFieldName);
        void SaveAs(string filename, bool includeHeaders);
        string MapPath(string virtualPath);
        string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping);
        void InsertEntityBody(byte[] buffer, int offset, int count);
        void InsertEntityBody();
        Stream GetBufferlessInputStream();
    }
}