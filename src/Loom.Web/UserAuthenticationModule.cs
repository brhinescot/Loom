#region Using Directives

using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Loom.Web.Security;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Summary description for UserAuthenticationModule.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  	<httpModules>
    /// 			<add name="UserAuthenticationModule" type="Loom.Web.UserAuthenticationModule, Loom.Web" />
    /// 		</httpModules>
    ///  ]]>
    ///  </code>
    /// </example>
    public class UserAuthenticationModule : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        ///     Peaks into the Initialization of the Http request.
        /// </summary>
        /// <param name="context">The current executing context.</param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += HandleAuthenticateRequest;
        }

        /// <summary>
        ///     Implementation of the dispose method.
        /// </summary>
        public void Dispose() { }

        #endregion

        private static void HandleAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication) sender).Context;

            // If something else in the pipeline has already indicated that we should ignore auth altogether, then respect it.
            if (context.SkipAuthorization)
                return;

            // Make sure the url they're trying to access is actually protected under forms auth.
            // The context.User might be null if the user is not logged in, hence the coalesce with a generic principal.
            IPrincipal genericPrincipal = new GenericPrincipal(new GenericIdentity(""), null);
            if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(context.Request.Path, context.User ?? genericPrincipal, context.Request.HttpMethod))
                return;

            HttpCookie cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
                return;

            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (!ticket.Expired)
                {
                    string[] userData = ticket.UserData.Split('|');
                    if (userData.Length == 2)
                    {
                        int userId = Convert.ToInt32(userData[0]);
                        string role = userData[1];
                        context.User = new GenericPrincipal(new FormsUserIdentity(ticket, userId), new[] {role});
                        Thread.CurrentPrincipal = context.User;
                    }
                    else
                    {
                        context.User = new GenericPrincipal(new FormsIdentity(ticket), new[] {ticket.UserData});
                        Thread.CurrentPrincipal = context.User;
                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
            }
            catch (CryptographicException)
            {
                FormsAuthentication.SignOut();
                context.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }
        }
    }
}