#region Using Directives

using System;
using System.Web;

#endregion

namespace Loom.Web.Portal.Http
{
    public class WrappedHttpCachePolicy : IHttpCachePolicy
    {
        private readonly HttpCachePolicy policy;

        public WrappedHttpCachePolicy(HttpCachePolicy policy)
        {
            this.policy = policy;
        }

        #region IHttpCachePolicy Members

        /// <summary>
        ///     Stops all origin-server caching for the current response.
        /// </summary>
        public void SetNoServerCaching()
        {
            policy.SetNoServerCaching();
        }

        /// <summary>
        ///     Specifies a custom text string to vary cached output responses by.
        /// </summary>
        /// <param name="custom">The text string to vary cached output by. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="custom" /> is null. </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The
        ///     <see cref="M:System.Web.HttpCachePolicy.SetVaryByCustom(System.String)" /> method has already been called.
        /// </exception>
        public void SetVaryByCustom(string custom)
        {
            policy.SetVaryByCustom(custom);
        }

        /// <summary>
        ///     Appends the specified text to the Cache-Control HTTP header.
        /// </summary>
        /// <param name="extension">The text to append to the Cache-Control header. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="extension" /> is null. </exception>
        public void AppendCacheExtension(string extension)
        {
            policy.AppendCacheExtension(extension);
        }

        /// <summary>
        ///     Sets the Cache-Control: no-transform HTTP header.
        /// </summary>
        public void SetNoTransforms()
        {
            policy.SetNoTransforms();
        }

        /// <summary>
        ///     Sets the Cache-Control header to one of the values of <see cref="T:System.Web.HttpCacheability" />.
        /// </summary>
        /// <param name="cacheability">An <see cref="T:System.Web.HttpCacheability" /> enumeration value. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="cacheability" /> is not one of the enumeration
        ///     values.
        /// </exception>
        public void SetCacheability(HttpCacheability cacheability)
        {
            policy.SetCacheability(cacheability);
        }

        /// <summary>
        ///     Sets the Cache-Control header to one of the values of <see cref="T:System.Web.HttpCacheability" /> and appends an
        ///     extension to the directive.
        /// </summary>
        /// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to. </param>
        /// <param name="field">The cache control extension to add to the header. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="field" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="cacheability" /> is not
        ///     <see cref="F:System.Web.HttpCacheability.Private" /> or <see cref="F:System.Web.HttpCacheability.NoCache" />.
        /// </exception>
        public void SetCacheability(HttpCacheability cacheability, string field)
        {
            policy.SetCacheability(cacheability, field);
        }

        /// <summary>
        ///     Sets the Cache-Control: no-store HTTP header.
        /// </summary>
        public void SetNoStore()
        {
            policy.SetNoStore();
        }

        /// <summary>
        ///     Sets the Expires HTTP header to an absolute date and time.
        /// </summary>
        /// <param name="date">The absolute <see cref="T:System.DateTime" /> value to set the Expires header to. </param>
        public void SetExpires(DateTime date)
        {
            policy.SetExpires(date);
        }

        /// <summary>
        ///     Sets the Cache-Control: max-age HTTP header based on the specified time span.
        /// </summary>
        /// <param name="delta">The time span used to set the Cache - Control: max-age header. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="delta" /> is less than 0 or greater than one
        ///     year.
        /// </exception>
        public void SetMaxAge(TimeSpan delta)
        {
            policy.SetMaxAge(delta);
        }

        /// <summary>
        ///     Sets the Cache-Control: s-maxage HTTP header based on the specified time span.
        /// </summary>
        /// <param name="delta">The time span used to set the Cache-Control: s-maxage header. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="delta" /> is less than 0. </exception>
        public void SetProxyMaxAge(TimeSpan delta)
        {
            policy.SetProxyMaxAge(delta);
        }

        /// <summary>
        ///     Sets cache expiration to from absolute to sliding.
        /// </summary>
        /// <param name="slide">true or false. </param>
        public void SetSlidingExpiration(bool slide)
        {
            policy.SetSlidingExpiration(slide);
        }

        /// <summary>
        ///     Specifies whether the ASP.NET cache should ignore HTTP Cache-Control headers sent by the client that invalidate the
        ///     cache.
        /// </summary>
        /// <param name="validUntilExpires">true if the cache ignores Cache-Control invalidation headers; otherwise, false. </param>
        public void SetValidUntilExpires(bool validUntilExpires)
        {
            policy.SetValidUntilExpires(validUntilExpires);
        }

        /// <summary>
        ///     Makes the response is available in the client browser History cache, regardless of the
        ///     <see cref="T:System.Web.HttpCacheability" /> setting made on the server, when the <paramref name="allow" />
        ///     parameter is true.
        /// </summary>
        /// <param name="allow">
        ///     true to direct the client browser to store responses in the History folder; otherwise false. The
        ///     default is false.
        /// </param>
        public void SetAllowResponseInBrowserHistory(bool allow)
        {
            policy.SetAllowResponseInBrowserHistory(allow);
        }

        /// <summary>
        ///     Sets the Cache-Control HTTP header to either the must-revalidate or the proxy-revalidate directives based on the
        ///     supplied enumeration value.
        /// </summary>
        /// <param name="revalidation">
        ///     The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the
        ///     Cache-Control header to.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="revalidation" /> is not one of the enumeration
        ///     values.
        /// </exception>
        public void SetRevalidation(HttpCacheRevalidation revalidation)
        {
            policy.SetRevalidation(revalidation);
        }

        /// <summary>
        ///     Sets the ETag HTTP header to the specified string.
        /// </summary>
        /// <param name="etag">The text to use for the ETag header. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="etag" /> is null. </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The ETag header has already been set. - or -The
        ///     <see cref="M:System.Web.HttpCachePolicy.SetETagFromFileDependencies" /> has already been called.
        /// </exception>
        public void SetETag(string etag)
        {
            policy.SetETag(etag);
        }

        /// <summary>
        ///     Sets the Last-Modified HTTP header to the <see cref="T:System.DateTime" /> value supplied.
        /// </summary>
        /// <param name="date">The new <see cref="T:System.DateTime" /> value for the Last-Modified header. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="date" /> is later than the current DateTime. </exception>
        public void SetLastModified(DateTime date)
        {
            policy.SetLastModified(date);
        }

        /// <summary>
        ///     Sets the Last-Modified HTTP header based on the time stamps of the handler's file dependencies.
        /// </summary>
        public void SetLastModifiedFromFileDependencies()
        {
            policy.SetLastModifiedFromFileDependencies();
        }

        /// <summary>
        ///     Sets the ETag HTTP header based on the time stamps of the handler's file dependencies.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The ETag header has already been set. </exception>
        public void SetETagFromFileDependencies()
        {
            policy.SetETagFromFileDependencies();
        }

        /// <summary>
        ///     Specifies whether the response should contain the vary:* header when varying by parameters.
        /// </summary>
        /// <param name="omit">
        ///     true to direct the <see cref="T:System.Web.HttpCachePolicy" /> to not use the * value for its
        ///     <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, false.
        /// </param>
        public void SetOmitVaryStar(bool omit)
        {
            policy.SetOmitVaryStar(omit);
        }

        /// <summary>
        ///     Registers a validation callback for the current response.
        /// </summary>
        /// <param name="handler">The <see cref="T:System.Web.HttpCacheValidateHandler" /> value. </param>
        /// <param name="data">
        ///     The arbitrary user-supplied data that is passed back to the
        ///     <see cref="M:System.Web.HttpCachePolicy.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" />
        ///     delegate.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">The specified <paramref name="handler" /> is null. </exception>
        public void AddValidationCallback(HttpCacheValidateHandler handler, object data)
        {
            policy.AddValidationCallback(handler, data);
        }

        /// <summary>
        ///     Gets the list of Content-Encoding headers that will be used to vary the output cache.
        /// </summary>
        /// <returns>
        ///     An object that specifies which Content-Encoding headers are used to select the cached response.
        /// </returns>
        public HttpCacheVaryByContentEncodings VaryByContentEncodings => policy.VaryByContentEncodings;

        /// <summary>
        ///     Gets the list of all HTTP headers that will be used to vary cache output.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Web.HttpCacheVaryByHeaders" /> that specifies which HTTP headers are used to select the
        ///     cached response.
        /// </returns>
        public HttpCacheVaryByHeaders VaryByHeaders => policy.VaryByHeaders;

        /// <summary>
        ///     Gets the list of parameters received by an HTTP GET or HTTP POST that affect caching.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Web.HttpCacheVaryByParams" /> that specifies which cache-control headers are used to select
        ///     the cached response.
        /// </returns>
        public HttpCacheVaryByParams VaryByParams => policy.VaryByParams;

        #endregion
    }
}