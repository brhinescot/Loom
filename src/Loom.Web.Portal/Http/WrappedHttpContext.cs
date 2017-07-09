#region Using Directives

using System;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;

#endregion

namespace Loom.Web.Portal.Http
{
    public sealed class WrappedHttpContext : IHttpContext
    {
        private readonly HttpContext httpContext;
        private readonly WrappedHttpRequest request;
        private readonly WrappedHttpResponse response;
        private readonly WrappedHttpSessionState sessionState;
        private readonly WrappedTraceContext traceContext;

        public WrappedHttpContext(HttpContext context)
        {
            Argument.Assert.IsNotNull(context, nameof(context));

            httpContext = context;
            request = new WrappedHttpRequest(httpContext.Request);
            response = new WrappedHttpResponse(httpContext.Response);
            traceContext = new WrappedTraceContext(httpContext.Trace);
            sessionState = new WrappedHttpSessionState(httpContext.Session);
        }

        #region IHttpContext Members

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider) httpContext).GetService(serviceType);
        }

        public void RemapHandler(IHttpHandler handler)
        {
            httpContext.RemapHandler(handler);
        }

        public void AddError(Exception errorInfo)
        {
            httpContext.AddError(errorInfo);
        }

        public void ClearError()
        {
            httpContext.ClearError();
        }

        public void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
        {
            httpContext.SetSessionStateBehavior(sessionStateBehavior);
        }

        public object GetSection(string sectionName)
        {
            return httpContext.GetSection(sectionName);
        }

        public void RewritePath(string path)
        {
            httpContext.RewritePath(path);
        }

        public void RewritePath(string path, bool rebaseClientPath)
        {
            httpContext.RewritePath(path, rebaseClientPath);
        }

        public void RewritePath(string filePath, string pathInfo, string queryString)
        {
            httpContext.RewritePath(filePath, pathInfo, queryString);
        }

        public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
        {
            httpContext.RewritePath(filePath, pathInfo, queryString, setClientFilePath);
        }

        public HttpApplication ApplicationInstance
        {
            get => httpContext.ApplicationInstance;
            set => httpContext.ApplicationInstance = value;
        }

        public HttpApplicationState Application => httpContext.Application;

        public IHttpHandler Handler
        {
            get => httpContext.Handler;
            set => httpContext.Handler = value;
        }

        public IHttpHandler PreviousHandler => httpContext.PreviousHandler;

        public IHttpHandler CurrentHandler => httpContext.CurrentHandler;

        public IHttpRequest Request => request;

        public IHttpResponse Response => response;

        public ITraceContext Trace => traceContext;

        public IDictionary Items => httpContext.Items;

        public IHttpSessionState Session => sessionState;

        public HttpServerUtility Server => httpContext.Server;

        public Exception Error => httpContext.Error;

        public Exception[] AllErrors => httpContext.AllErrors;

        public IPrincipal User
        {
            get => httpContext.User;
            set => httpContext.User = value;
        }

        public ProfileBase Profile => httpContext.Profile;

        public bool SkipAuthorization
        {
            get => httpContext.SkipAuthorization;
            set => httpContext.SkipAuthorization = value;
        }

        public bool IsDebuggingEnabled => httpContext.IsDebuggingEnabled;

        public bool IsCustomErrorEnabled => httpContext.IsCustomErrorEnabled;

        public DateTime Timestamp => httpContext.Timestamp;

        public Cache Cache => httpContext.Cache;

        public RequestNotification CurrentNotification => httpContext.CurrentNotification;

        public bool IsPostNotification => httpContext.IsPostNotification;

        #endregion

        public static implicit operator HttpContext(WrappedHttpContext c)
        {
            return c.httpContext;
        }
    }
}