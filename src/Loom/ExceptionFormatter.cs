#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Loom.Cryptography;

#endregion

namespace Loom
{
    /// <summary>
    ///     <para>
    ///         Provides exception formatting when not using the
    ///         Exception Handling block.
    ///     </para>
    /// </summary>
    [DebuggerDisplay("ApplicationName={ApplicationName}, Summary={Summary}")]
    public class ExceptionFormatter : IExceptionFormatter
    {
        private const string InnerExceptionPropertyName = "InnerException";
        public const string LineSeparator = "=========================================================================";
        private const string StackTracePropertyName = "StackTrace";

        private readonly List<string> additionalInfo = new List<string>(0);

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ExceptionFormatter" /> class.
        ///     </para>
        /// </summary>
        public ExceptionFormatter() : this(string.Empty) { }

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ExceptionFormatter" /> class with the additional information and
        ///         the application name.
        ///     </para>
        /// </summary>
        /// <param name="applicationName">
        ///     <para>The application name.</para>
        /// </param>
        public ExceptionFormatter(string applicationName) : this(applicationName, string.Empty, new string[0]) { }

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ExceptionFormatter" /> class with the additional information and
        ///         the application name.
        ///     </para>
        /// </summary>
        /// <param name="applicationName">
        ///     <para>The application name.</para>
        /// </param>
        /// <param name="header">
        ///     <para>The exception summary.</para>
        /// </param>
        public ExceptionFormatter(string applicationName, string header) : this(applicationName, header, new string[0]) { }

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ExceptionFormatter" /> class with the additional information and
        ///         the application name.
        ///     </para>
        /// </summary>
        /// <param name="applicationName">
        ///     <para>The application name.</para>
        /// </param>
        /// <param name="header">
        ///     <para>The exception summary.</para>
        /// </param>
        /// <param name="additionalInfo">
        ///     <para>The additional information to log.</para>
        /// </param>
        public ExceptionFormatter(string applicationName, string header, params string[] additionalInfo) : this(applicationName, header, new DefaultInfoProvider(), additionalInfo) { }

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="ExceptionFormatter" /> class with the additional information and
        ///         the application name.
        ///     </para>
        /// </summary>
        /// <param name="applicationName">
        ///     <para>The application name.</para>
        /// </param>
        /// <param name="header">
        ///     <para>The exception summary.</para>
        /// </param>
        /// <param name="provider">
        ///     <para>The additional information provider from which to collect information to log.</para>
        /// </param>
        public ExceptionFormatter(string applicationName, string header, IAdditionalInfoProvider provider) : this(applicationName, header, provider, new string[0]) { }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ExceptionFormatter" /> class with the additional information and the
        ///     application name.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <param name="summary">The exception summary.</param>
        /// <param name="provider">The additional information provider from which to collect information to log.</param>
        /// <param name="additionalInfo">The additional info.</param>
        public ExceptionFormatter(string applicationName, string summary, IAdditionalInfoProvider provider, params string[] additionalInfo)
        {
            this.additionalInfo.AddRange(additionalInfo);
            this.additionalInfo.AddRange(provider.Generate());
            ApplicationName = applicationName;
            Summary = summary;
        }

        /// <summary>
        ///     Gets or sets the application name that this <see cref="ExceptionFormatter" />
        ///     is generating reports for.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        ///     Gets or sets the summary for the generated exception report.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if the currently loaded assemblies are generated with the exception
        ///     report.
        /// </summary>
        public bool WriteExceptionHash { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating if the currently loaded assemblies are generated with the exception
        ///     report.
        /// </summary>
        public bool WriteLoadedAssemblies { get; set; }

        #region IExceptionFormatter Members

        /// <summary>
        ///     <para>Generates a formatted exception detail report.</para>
        /// </summary>
        /// <param name="exception">
        ///     <para>The exception object from which to generate the report.</para>
        /// </param>
        /// <param name="newLine">
        ///     The newline character to use in the report. The default is
        ///     <see cref="Environment.NewLine" />
        /// </param>
        /// <returns>
        ///     <para>The formatted message.</para>
        /// </returns>
        public string Generate(Exception exception, string newLine)
        {
            if (newLine == null)
                newLine = Environment.NewLine;

            StringBuilder sb = new StringBuilder(5000);

            if (!Compare.IsNullOrEmpty(Summary))
            {
                sb.AppendFormat("{0}{2}{1}{2}", "Summary:", LineSeparator, newLine);
                sb.AppendFormat("{0}{1}{1}", Summary, newLine);
            }

            if (WriteExceptionHash)
            {
                sb.AppendFormat("{0}{2}{1}{2}", "Exception Hash:", LineSeparator, newLine);
                sb.AppendFormat("{0}{1}{1}", GetHash(exception), newLine);
            }
            sb.AppendFormat("{0}: {1}{3}{2}", "Environment", ApplicationName, LineSeparator, newLine);

            // Record the contents of the AdditionalInfo collection.
            foreach (string info in additionalInfo)
                sb.AppendFormat("{1}{0}", info, newLine);

            for (Exception currException = exception; currException != null; currException = currException.InnerException)
            {
                sb.AppendFormat("{2}{2}{0}{2}{1}", SR.ExceptionDetails, LineSeparator, newLine);
                sb.AppendFormat("{2}--> {0}: {1}", SR.ExceptionType, currException.GetType().FullName, newLine);

                ReflectException(currException, sb, newLine);

                // Record the StackTrace with a separate label.
                if (currException.StackTrace == null)
                    continue;

                sb.AppendFormat("{2}{2}{0} {2}{1}", SR.ExceptionStackTraceDetails, LineSeparator, newLine);
                sb.AppendFormat("{1}{0}{1}", currException.StackTrace, newLine);
            }

            if (WriteLoadedAssemblies)
                ProcessLoadedAssemblies(sb, newLine);

            return sb.ToString();
        }

        /// <summary>
        ///     <para>Get the formatted message to be logged.</para>
        /// </summary>
        /// <param name="exception">
        ///     <para>
        ///         The exception object whose information
        ///         should be written to log file.
        ///     </para>
        /// </param>
        /// <returns>
        ///     <para>The formatted message.</para>
        /// </returns>
        public string Generate(Exception exception)
        {
            return Generate(exception, null);
        }

        #endregion

        public static string GetHash(Exception exception)
        {
            using (HashProvider hashProvider = HashProvider.SHA256)
            {
                return hashProvider.GenerateString(exception.ToString());
            }
        }

        private static void ProcessAdditionalInfo(PropertyInfo propinfo, Exception currException, StringBuilder sb, string newLine)
        {
            // Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
            if (propinfo.Name == "Data")
            {
                if (propinfo.GetValue(currException, null) != null)
                {
                    // Cast the collection into a local variable.
                    IDictionary currAdditionalInfo = (IDictionary) propinfo.GetValue(currException, null);

                    // Check if the collection contains values.
                    if (currAdditionalInfo.Count > 0)
                        foreach (DictionaryEntry entry in currAdditionalInfo)
                            sb.AppendFormat("{2}--> {0}: {1}", entry.Key, entry.Value, newLine);
                }
            }
            else
            {
                // Otherwise just write the ToString() value of the property.
                sb.AppendFormat("{2}--> {0}: {1}", propinfo.Name, propinfo.GetValue(currException, null).ToString().Replace(Environment.NewLine, newLine + "--> "), newLine);
            }
        }

        private static void ProcessLoadedAssemblies(StringBuilder sb, string newLine)
        {
            sb.AppendFormat("{1}{1}Loaded Assemblies\n{0}", LineSeparator, newLine);
            foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
                sb.AppendFormat("{1}--> {0}", loadedAssembly.FullName, newLine);
        }

        private static void ReflectException(Exception currException, StringBuilder sb, string newLine)
        {
            PropertyInfo[] publicProperties = currException.GetType().GetProperties();
            foreach (PropertyInfo property in publicProperties)
            {
                // Do not log information for the InnerException or StackTrace. This information is 
                // captured later in the process.
                if (property.Name == InnerExceptionPropertyName || property.Name == StackTracePropertyName)
                    continue;

                if (property.GetValue(currException, null) == null)
                    sb.AppendFormat("{1}--> {0}: NULL", property.Name, newLine);
                else
                    ProcessAdditionalInfo(property, currException, sb, newLine);
            }
        }
    }
}