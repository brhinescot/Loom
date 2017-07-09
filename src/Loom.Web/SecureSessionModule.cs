#region Using Directives

using System;
using System.Text;
using System.Web;
using System.Web.Security;
using Loom.Annotations;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     A class for securing a users session against hijacking.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///      <system.web>
    ///          <httpModules>
    /// 	            <add name="SecureSessionModule" type="Loom.Web.SecureSessionModule, Loom.Web" />
    /// 	        </httpModules>
    ///      ...
    ///  ]]>
    ///  </code>
    /// </example>
    [UsedImplicitly]
    public class SecureSessionModule : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        ///     Peaks into the Initialization of the Http request.
        /// </summary>
        /// <param name="context">The current executing context.</param>
        public void Init(HttpApplication context)
        {
            PrivateInit(context);
        }

        /// <summary>
        ///     Implementation of the dispose method.
        /// </summary>
        public void Dispose() { }

        #endregion

        private static void PrivateInit(HttpApplication context)
        {
            // Register handlers for BeginRequest and EndRequest events
            context.BeginRequest += HandleBeginRequest;
            context.EndRequest += HandleEndRequest;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidSessionException">InvalidSessionException</exception>
        private static void HandleBeginRequest(object sender, EventArgs e)
        {
            // Look for an incoming cookie named "ASP.NET_SessionID"
            HttpRequest request = ((HttpApplication) sender).Request;
            HttpCookie cookie = request.GetCookie("ASP.NET_SessionId");

            if (cookie == null)
                return;

            // Throw an exception if the cookie lacks a MAC
            if (cookie.Value.Length <= 24)
                throw new InvalidSessionException(request.UserHostAddress);

            // Separate the session ID and the MAC
            string id = cookie.Value.Substring(0, 24);
            string mac1 = cookie.Value.Substring(24);

            // Generate a new MAC from the session ID and requestor info
            string mac2 = GetSessionIdMac(id, request.UserHostAddress, request.UserAgent);

            // Throw an exception if the MACs don't match
            if (string.CompareOrdinal(mac1, mac2) != 0)
                throw new InvalidSessionException(request.UserHostAddress);

            // Strip the MAC from the cookie before ASP.NET sees it
            cookie.Value = id;
        }

        private static void HandleEndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication) sender;
            HttpRequest request = context.Request;
            HttpCookie cookie = context.Response.GetCookie("ASP.NET_SessionId");

            // Add a MAC to the "ASP.NET_SessionID" cookie
            if (cookie != null)
                cookie.Value += GetSessionIdMac(cookie.Value, request.UserHostAddress, request.UserAgent);
        }

        private static string GetSessionIdMac(string id, string ip, string agent)
        {
            StringBuilder builder = new StringBuilder(id, 512);
            builder.Append(ip.Substring(0, ip.IndexOf('.', ip.IndexOf('.') + 1)));
            builder.Append(agent);

            return Encoding.UTF8.GetString(MachineKey.Protect(Encoding.UTF8.GetBytes(builder.ToString()), "SecureSession"));
        }
    }
}