#region Using Directives

using System;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;

#endregion

namespace Loom.Web.Portal
{
    public interface IHttpContext
    {
        HttpApplication ApplicationInstance { get; set; }
        HttpApplicationState Application { get; }
        IHttpHandler Handler { get; set; }
        IHttpHandler PreviousHandler { get; }
        IHttpHandler CurrentHandler { get; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        ITraceContext Trace { get; }
        IDictionary Items { get; }
        IHttpSessionState Session { get; }
        HttpServerUtility Server { get; }
        Exception Error { get; }
        Exception[] AllErrors { get; }
        IPrincipal User { get; set; }
        ProfileBase Profile { get; }
        bool SkipAuthorization { get; set; }
        bool IsDebuggingEnabled { get; }
        bool IsCustomErrorEnabled { get; }
        DateTime Timestamp { get; }
        Cache Cache { get; }
        RequestNotification CurrentNotification { get; }
        bool IsPostNotification { get; }
        object GetService(Type serviceType);
        void RemapHandler(IHttpHandler handler);
        void AddError(Exception errorInfo);
        void ClearError();
        void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior);
        object GetSection(string sectionName);
        void RewritePath(string path);
        void RewritePath(string path, bool rebaseClientPath);
        void RewritePath(string filePath, string pathInfo, string queryString);
        void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath);
    }
}