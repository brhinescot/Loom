#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using Loom.Annotations;
using Loom.Web.Portal.Controllers;
using Loom.Web.UI;

#endregion

namespace Loom.Web.Portal
{
    public sealed class PortalResponse : IPortalResponse
    {
        private readonly IPortalContext portalContext;

        internal PortalResponse(IPortalContext portalContext)
        {
            this.portalContext = portalContext;
            Tiles = new Collection<TileDefinition>();
        }

        #region IPortalResponse Members

        public bool IsRedirected { get; set; }

        public Uri RedirectLocation { get; set; }

        public Collection<TileDefinition> Tiles { get; internal set; }

        public void AjaxRedirect(string url, bool endResponse = false)
        {
            portalContext.HttpContext.Response.Clear();
            portalContext.HttpContext.Response.AddHeader("X-Portal-Location", url);
            if (endResponse)
                portalContext.HttpContext.Response.Complete();
        }

        public string GetCallbackUrl()
        {
            return Path.HasExtension(portalContext.Request.Path)
                ? portalContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath
                : portalContext.Request.Path;
        }

        public void WriteJson(object o)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            WriteJson(serializer.Serialize(o));
        }

        public void WriteJson(string json)
        {
            IHttpResponse response = portalContext.HttpContext.Response;

            PrepareJsonResponse(response);
            response.Write(string.IsNullOrEmpty(json) ? "[]" : json);
            response.Complete();
        }

        public void WriteJson(StringCreator jsonStringCreator)
        {
            IHttpResponse response = portalContext.HttpContext.Response;

            PrepareJsonResponse(response);
            try
            {
                string s = jsonStringCreator();
                response.Write(string.IsNullOrEmpty(s) ? "[]" : s);
            }
            catch (Exception)
            {
                response.Status = "500 Server Error";
                response.StatusCode = 500;
            }
            response.Complete();
        }

        public void SetLoginCookie(string userName, int userId, string userRole = null)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddDays(1), false, userId + "|" + userRole);
            string cookieString = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieString) {HttpOnly = true};
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #endregion

        public static void WriteChildControls([NotNull] Control control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            AjaxUtility.WriteChildControls(control, true);
        }

        public static void WriteControl([NotNull] Control control)
        {
            Argument.Assert.IsNotNull(control, nameof(control));

            AjaxUtility.WriteControl(control, true);
        }

        private static void PrepareJsonResponse(IHttpResponse response)
        {
            response.Clear();
            response.ClearHeaders();
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}