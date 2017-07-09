#region Using Directives

using System.Collections.Specialized;
using System.IO;
using System.Web;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal
{
    public sealed class PortalRequest : IPortalRequest
    {
        private const char SegmentSeperatorChar = '/';
        private const string SegmentSeperatorString = "/";

        private readonly char[] segmentSeperatorCharArray = {SegmentSeperatorChar};

        #region IPortalRequest Members

        /// <summary>
        ///     Gets the current path being served by the portal framework.
        /// </summary>
        public string Path { get; [NotNull] internal set; }

        /// <summary>
        /// </summary>
        public NameValueCollection QueryString => HttpContext.Current.Request.QueryString;

        /// <summary>
        ///     Gets a value indicating if physical pages are allowed in the portal.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If <c>true</c> and no routes match the current <see cref="Path" />, the portal
        ///         framework will look for a physical file at the path. If one does not exist, a
        ///         <see cref="FileNotFoundException" /> will be thrown.
        ///     </para>
        ///     <para>
        ///         If <c>false</c> and no routes match the current
        ///         <see cref="Path" />, a <see cref="FileNotFoundException" /> will be thrown.
        ///     </para>
        /// </remarks>
        public bool AllowPhysicalPages { get; internal set; }

        /// <summary>
        ///     The title of the route and the page it represents.
        /// </summary>
        /// <remarks>
        ///     Tokens from the route <see cref="Data.Portal.Route.Expression" /> may be used here. The
        ///     value of the token will be substituted at runtime; i.e. "Product Details for {ProductName}"
        /// </remarks>
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        public ActionResult Result { get; set; }

        /// <summary>
        /// </summary>
        public RouteTokens Tokens { get; private set; }

        /// <summary>
        /// </summary>
        public string ControllerName { get; internal set; }

        /// <summary>
        /// </summary>
        public string TenantName { get; internal set; }

        /// <summary>
        /// </summary>
        public bool IsXmlHttpRequest { get; internal set; }

        /// <summary>
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// </summary>
        public bool IsPortalRoute { get; set; }

        /// <summary>
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///     Gets a value indicating if the specified token <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string" /> representing the name of the token.
        /// </param>
        /// <returns>
        ///     A <see cref="bool" /> indicating if the token exists.
        /// </returns>
        public bool HasToken(string name)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));
            return Tokens != null && Tokens.Contains(name);
        }

        /// <summary>
        ///     Gets the value of the token represented by the specified <paramref name="name" />.
        /// </summary>
        /// <remarks>
        ///     The return value will include all segments in the route token, i.e. products/mens/shoes/running.
        /// </remarks>
        /// <param name="name">
        ///     A <see cref="string" /> representing the name of the token.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value can be <c>null</c>;
        ///     <c>Nothing</c> in Visual Basic.
        /// </returns>
        public string GetTokenValue(string name)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));

            if (Tokens == null)
                return null;

            if (!Tokens.Contains(name))
                return null;

            string tempToken = Tokens[name];
            return tempToken == null ? null : (Tokens.Contains(name) ? Tokens[name] : null);
        }

        /// <summary>
        ///     Gets the value of the last segment of the token represented by the
        ///     specified <paramref name="name" />.
        /// </summary>
        /// <remarks>
        ///     If the token value is a single segment, it is returned.
        /// </remarks>
        /// <param name="name">
        ///     A <see cref="string" /> representing the name of the token.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value can
        ///     be <c>null</c>; <c>Nothing</c> in Visual Basic.
        /// </returns>
        public string GetLastTokenSegmentValue(string name)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));

            if (Tokens == null)
                return null;

            if (!Tokens.Contains(name))
                return null;

            string tempToken = Tokens[name];
            return tempToken == null ? null : tempToken.Substring(tempToken.LastIndexOf(SegmentSeperatorChar) + 1);
        }

        /// <summary>
        ///     Gets the value of the first segment of the token represented by the specified
        ///     <paramref name="name" />.
        /// </summary>
        /// <remarks>
        ///     If the token value is a single segment, it is returned.
        /// </remarks>
        /// <param name="name">
        ///     A <see cref="string" /> representing the name of the token.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value
        ///     can be <c>null</c>; <c>Nothing</c> in Visual Basic.
        /// </returns>
        public string GetFirstTokenSegmentValue(string name)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));

            if (Tokens == null)
                return null;

            if (!Tokens.Contains(name))
                return null;

            string tempToken = Tokens[name];
            if (tempToken == null)
                return null;

            return tempToken.Contains(SegmentSeperatorString) ? tempToken.Substring(0, tempToken.IndexOf(SegmentSeperatorChar)) : tempToken;
        }

        void IPortalRequest.AddToken(string name, string value)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));

            if (!Compare.IsNullOrEmpty(value))
                value = value.Trim(segmentSeperatorCharArray);

            if (Tokens == null)
                Tokens = new RouteTokens();
            Tokens.Add(name, value);
        }

        #endregion

        internal void MergeTokens(RouteTokens tokens)
        {
            if (tokens == null)
                return;

            if (Tokens == null)
                Tokens = new RouteTokens();

            Tokens.Merge(tokens);
        }
    }
}