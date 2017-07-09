#region Using Directives

using System.Web;

#endregion

namespace Loom.Web
{
    /// <summary>
    /// </summary>
    public class WebInfoProvider : DefaultInfoProvider
    {
        private readonly HttpContext context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebInfoProvider" /> class.
        /// </summary>
        public WebInfoProvider() : this(HttpContext.Current) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebInfoProvider" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public WebInfoProvider(HttpContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// </summary>
        /// <param name="additionalInfo"></param>
        protected override void OnGenerate(AdditionalInfo additionalInfo)
        {
            additionalInfo.AddHeader("Web Info");
            additionalInfo.AddInfo("Request IP Address", context.Request.UserHostAddress);
            additionalInfo.AddInfo("Request Host Name", context.Request.UserHostName);
            additionalInfo.AddInfo("File Path", context.Request.CurrentExecutionFilePath);
            additionalInfo.AddInfo("Request Type", context.Request.RequestType);
            additionalInfo.AddInfo("QueryString", context.Request.QueryString);
            additionalInfo.AddInfo("Form Fields", context.Request.Form);

            additionalInfo.AddInfo("URL Referrer", context.Request.UrlReferrer);
            additionalInfo.AddInfo("Is Authenticated", context.Request.IsAuthenticated);
            additionalInfo.AddInfo(context.User);

            if (context.Session != null)
                additionalInfo.AddInfo("SessionID", context.Session.SessionID);

            additionalInfo.AddInfo("Platform", context.Request.Browser.Platform);
            additionalInfo.AddInfo("Browser", context.Request.Browser.Browser);
            additionalInfo.AddInfo("Is Mobile Device", context.Request.Browser.IsMobileDevice);
            additionalInfo.AddInfo("User Agent", context.Request.UserAgent);

            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
                additionalInfo.AddInfo("Posted Files", string.Empty);
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                additionalInfo.AddInfo("     File " + i + 1, file.FileName);
                additionalInfo.AddInfo("     ContentLength", file.ContentLength);
                additionalInfo.AddInfo("     ContentType", file.ContentType);
            }
            additionalInfo.AddInfo("Total Bytes", context.Request.TotalBytes);
        }
    }
}