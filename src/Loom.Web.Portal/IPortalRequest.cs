#region Using Directives

using System.Collections.Specialized;
using System.IO;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;
using Loom.Web.Portal.Routing;

#endregion

namespace Loom.Web.Portal
{
    public interface IPortalRequest
    {
        /// <summary>
        ///     Gets a value indicating if physical pages are allowed in the portal.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If <c>true</c> and no routes match the current <see cref="Path" />, the portal framework will look for
        ///         a physical file at the path. If one does not exist, a <see cref="FileNotFoundException" /> will be
        ///         thrown.
        ///     </para>
        ///     <para>
        ///         If <c>false</c> and no routes match the current <see cref="Path" />, a <see cref="FileNotFoundException" />
        ///         will be thrown.
        ///     </para>
        /// </remarks>
        bool AllowPhysicalPages { get; }

        /// <summary>
        ///     Gets the name of the <see cref="IController"> used to handle the current request.</see>
        /// </summary>
        string ControllerName { get; }

        string TenantName { get; }
        bool IsXmlHttpRequest { get; }

        /// <summary>
        ///     Gets the current path being served by the portal framework.
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     The title of the route and the page it represents.
        /// </summary>
        /// <remarks>
        ///     Tokens from the route <see cref="Data.Portal.Route.Expression" /> may be used here. The
        ///     value of the token will be substituted at runtime; i.e. "Product Details for {ProductName}"
        /// </remarks>
        string Title { get; set; }

        RouteTokens Tokens { get; }

        /// <summary>
        ///     The <see cref="ActionResult" /> returned when the <see cref="IController" /> represented by the
        ///     <see cref="ControllerName" /> is executed.
        /// </summary>
        ActionResult Result { get; set; }

        string VirtualPath { get; set; }
        bool IsPortalRoute { get; set; }
        string ActionName { get; set; }
        NameValueCollection QueryString { get; }

        /// <summary>
        ///     Gets the value of the first segment of the token represented by the specified <paramref name="name" />.
        /// </summary>
        /// <remarks>
        ///     If the token value is a single segment, it is returned.
        /// </remarks>
        /// <param name="name">A <see cref="string" /> representing the name of the token.</param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value can be
        ///     <c>null</c>; <c>Nothing</c> in Visual Basic.
        /// </returns>
        [CanBeNull]
        string GetFirstTokenSegmentValue([NotNull] string name);

        /// <summary>
        ///     Gets the value of the last segment of the token represented by the specified <paramref name="name" />.
        /// </summary>
        /// If the token value is a single segment, it is returned.
        /// <param name="name">A <see cref="string" /> representing the name of the token.</param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value can be
        ///     <c>null</c>; <c>Nothing</c> in Visual Basic.
        /// </returns>
        [CanBeNull]
        string GetLastTokenSegmentValue([NotNull] string name);

        /// <summary>
        ///     Gets the value of the token represented by the specified <paramref name="name" />.
        /// </summary>
        /// <remarks>
        ///     The return value will include all segments in the route token, i.e. products/mens/shoes/running.
        /// </remarks>
        /// <param name="name">A <see cref="string" /> representing the name of the token.</param>
        /// <returns>
        ///     A <see cref="string" /> representing the value of the token. The value can be
        ///     <c>null</c>; <c>Nothing</c> in Visual Basic.
        /// </returns>
        [CanBeNull]
        string GetTokenValue([NotNull] string name);

        /// <summary>
        ///     Gets a value indicating if the specified token <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">A <see cref="string" /> representing the name of the token.</param>
        /// <returns>A <see cref="bool" /> indicating if the token exists.</returns>
        bool HasToken([NotNull] string name);

        void AddToken([NotNull] string name, string value);
    }
}