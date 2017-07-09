#region Using Directives

using System;
using System.Net;
using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Represents a base implementation of IHttpHandler
    /// </summary>
    public abstract class HttpHandlerBase : IHttpHandler
    {
        /// <summary>
        ///     Gets a value indicating whether this handler requires authentication.
        /// </summary>
        /// <value>
        ///     <c>true</c> if requires authentication; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool RequiresAuthentication => false;

        /// <summary>
        ///     Gets the content type.
        /// </summary>
        /// <value>The content type.</value>
        protected virtual string ContentMimeType => HttpContext.Current.Response.ContentType;

        #region IHttpHandler Members

        /// <summary>
        ///     Gets a value indicating whether another request can use the
        ///     <see cref="System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="System.Web.IHttpHandler"></see> instance is reusable;
        ///     otherwise, false.
        /// </returns>
        public bool IsReusable => true;

        /// <summary>
        ///     Enables processing of HTTP Web requests by a custom HttpHandler that implements
        ///     the <see cref="System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="System.Web.HttpContext"></see> object that
        ///     provides references to the intrinsic server objects (for example, Request, Response,
        ///     Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            SetResponseCachePolicy(context.Response.Cache);
            if (!ValidateParameters(context))
            {
                RespondWithInternalError(context);
                return;
            }

            if (RequiresAuthentication && !context.User.Identity.IsAuthenticated)
            {
                RespondWithForbidden(context);
                return;
            }

            context.Response.ContentType = ContentMimeType;
            HandleRequest(context);
        }

        #endregion

        /// <summary>
        ///     Handles the request.
        /// </summary>
        /// <param name="context">The context.</param>
        protected abstract void HandleRequest(HttpContext context);

        /// <summary>
        ///     Validates the parameters.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <value><c>true</c> if [validate parameters]; otherwise, <c>false</c>.</value>
        protected virtual bool ValidateParameters(HttpContext context)
        {
            return true;
        }

        /// <summary>
        ///     Sets the response cache policy.
        /// </summary>
        /// <param name="cache">The cache.</param>
        protected virtual void SetResponseCachePolicy(HttpCachePolicy cache)
        {
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetNoStore();
            cache.SetExpires(DateTime.MinValue);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void RespondWithFileNotFound(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            context.Response.Complete();
        }

        /// <summary>
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void RespondWithInternalError(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.Complete();
        }

        /// <summary>
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void RespondWithForbidden(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
            context.Response.Complete();
        }
    }
}