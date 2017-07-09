#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web;
using System.Web.Configuration;
using Loom.Web.Configuration;
using Loom.Web.IO;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Summary description for ErrorHandlingModule.
    /// </summary>
    /// <example>
    ///     The following example demonstrates how to configure the HttpModule in the web.config file.
    ///     <code>
    ///  <![CDATA[ 
    ///  	<httpModules>
    /// 			<add name="ErrorHandlingModule" type="Loom.Web.ErrorHandlingModule, Loom.Web" />
    /// 		</httpModules>
    ///  ]]>
    ///  </code>
    /// </example>
    public sealed class ErrorHandlingModule : IHttpModule
    {
        private const string ConfigurationKey = "errorHandlingSettings";

        private readonly ErrorHandlingSettingsSection config;
        private HttpApplication applicationContext;

        /// <summary>
        ///     Creates a new <see cref="ErrorHandlingModule" /> instance.
        /// </summary>
        public ErrorHandlingModule()
        {
            config = (ErrorHandlingSettingsSection)
                WebConfigurationManager.GetWebApplicationSection(ConfigurationKey);
        }

        #region IHttpModule Members

        /// <summary>
        ///     Peaks into the Initialization of the Http request.
        /// </summary>
        /// <param name="context">The current executing context.</param>
        /// <exception cref="ArgumentNullException">The HttpApplication parameter is null.</exception>
        public void Init(HttpApplication context)
        {
            applicationContext = context;
            applicationContext.Error += HandleError;
            AppDomain.CurrentDomain.UnhandledException += HandleCurrentDomainUnhandledException;
        }

        /// <summary>
        ///     Implementation of the dispose method
        /// </summary>
        public void Dispose()
        {
            if (applicationContext != null)
                applicationContext.Error -= HandleError;
            AppDomain.CurrentDomain.UnhandledException -= HandleCurrentDomainUnhandledException;
        }

        #endregion

        private void ClearServerError()
        {
            applicationContext.Server.ClearError();
        }

        private bool ExecutingErrorPage()
        {
            return applicationContext.Request.CurrentExecutionFilePath.IndexOf(WebPath.GetFileName(config.DefaultRedirect), StringComparison.Ordinal) > 0;
        }

        private void HandleCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // TODO: Add error handling code.
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void HandleError(object sender, EventArgs e)
        {
            Exception ex = applicationContext.Server.GetLastError();
            string message = null;

            //Clear error to prevent ASP.NET's error page
            ClearServerError();

            if (ex is HttpUnhandledException)
                ex = ex.InnerException;

            if (ex is HttpRequestValidationException)
                message = config.MaliciousInput.Message;
            else if (ex is FileNotFoundException)
                message = config.FileNotFound.Message;

            try
            {
                SupportEmail supportEmail = new SupportEmail(ex, applicationContext);
                supportEmail.Send();
            }
            catch (Exception)
            {
                // Clear error to prevent ASP.NET's error page
                // TODO: Log inability to send email notification. 
                ClearServerError();
            }

            if (ExecutingErrorPage())
                WriteErrorToBrowser();
            else
                RedirectToErrorPage(message);
        }

        private void RedirectToErrorPage(string message)
        {
            if (!Compare.IsNullOrEmpty(message))
                applicationContext.Server.Transfer(string.Format("{0}?message={1}", config.DefaultRedirect, message));
            else
                applicationContext.Server.Transfer(config.DefaultRedirect);
        }

        private void WriteErrorToBrowser()
        {
            applicationContext.Response.Write(config.DefaultMessage);
        }
    }
}