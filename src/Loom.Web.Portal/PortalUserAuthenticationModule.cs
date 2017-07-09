#region Using Directives

using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Loom.Web.Security;

#endregion

namespace Loom.Web.Portal
{
    public class PortalUserAuthenticationModule : IHttpModule
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
            HttpCookie cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
                return;

            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (!ticket.Expired)
                {
                    string[] userData = ticket.UserData.Split('|');
                    if (userData.Length != 2)
                        throw new PortalFatalException("User data in the authentication cookie is invalid.");

                    int userId = Convert.ToInt32(userData[0]);
                    string role = userData[1];
                    context.User = new GenericPrincipal(new FormsUserIdentity(ticket, userId), new[] {role});
                    Thread.CurrentPrincipal = context.User;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    context.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
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