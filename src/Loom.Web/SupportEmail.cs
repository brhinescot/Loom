#region Using Directives

using System;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using Loom.Web.Configuration;

#endregion

namespace Loom.Web
{
    /// <summary>
    ///     Summary description for SupportEmail.
    /// </summary>
    public class SupportEmail
    {
        private const string ConfigurationKey = "supportEmailSettings";
        private readonly HttpApplication application;
        private string applicationName;
        private SupportEmailSettingsSection config;

        private MailAddressCollection to;

        /// <summary>
        ///     Creates a new <see cref="SupportEmail" /> instance.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="ex">Ex.</param>
        public SupportEmail(Exception ex, HttpApplication context)
        {
            application = context;
            Exception = ex;
            EnsureConfiguration();
        }

        /// <summary>
        ///     Creates a new <see cref="SupportEmail" /> instance.
        /// </summary>
        /// <param name="ex">Exception.</param>
        public SupportEmail(Exception ex) :
            this(ex, HttpContext.Current.ApplicationInstance) { }

        /// <summary>
        ///     Gets or sets the subject.
        /// </summary>
        /// <value></value>
        public string Subject { get; set; }

        /// <summary>
        ///     Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header { get; set; }

        /// <summary>
        ///     Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }

        /// <summary>
        ///     Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets to.
        /// </summary>
        /// <value></value>
        public MailAddressCollection To
        {
            get
            {
                if (to == null)
                    to = new MailAddressCollection();
                return to;
            }
        }

        /// <summary>
        ///     Gets or sets from.
        /// </summary>
        /// <value></value>
        public MailAddress From { get; set; }

        /// <summary>
        ///     Gets or sets the exception.
        /// </summary>
        /// <value></value>
        public Exception Exception { get; set; }

        private void EnsureConfiguration()
        {
            config = (SupportEmailSettingsSection)
                ConfigurationManager.GetSection(ConfigurationKey);

            if (config != null)
            {
                applicationName = config.ApplicationName;
                Server = config.Server;
                Port = config.Port;
                Header = config.Message.Header;
                Subject = config.Message.Subject;
                From = new MailAddress(config.Sender.Address, config.Sender.Name);
                foreach (RecipientsElement reciepient in config.Recipients)
                    to.Add(new MailAddress(reciepient.Address, reciepient.Name));
            }
        }

        /// <summary>
        ///     Sends an email to support personnel.
        /// </summary>
        /// <returns>A string containg the message that was sent.</returns>
        /// <exception cref="InvalidOperationException">
        ///     The object properties are not properly
        ///     initialized.
        /// </exception>
        public string Send()
        {
            try
            {
                Argument.Assert.IsNotNull(Exception, "ex");
                Argument.Assert.IsNotNull(application, "context");
                Argument.Assert.IsNotNull(From, "From");
                Argument.Assert.IsNotNull(to, "To");
                Argument.Assert.IsNotNull(Server, "Server");
                Argument.Assert.IsNotNull(Port, "Port");
            }
            catch (ArgumentNullException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }

            MailMessage message = new MailMessage();
            message.Subject = Subject;
            message.From = From;

            foreach (MailAddress reciepient in to)
                message.To.Add(reciepient);

            IExceptionFormatter formatter = new
                ExceptionFormatter(applicationName, Header, new WebInfoProvider(application.Context));

            message.Body = formatter.Generate(Exception);
            SmtpClient client = new SmtpClient(Server, Port);
            // PERF: Do async call, SmtpClient.SendAsync.
            client.Send(message);

            return message.Body;
        }
    }
}