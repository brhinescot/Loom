#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    /// </summary>
    public class ServerActivity
    {
        private readonly ServerReport serverReport = new ServerReport();
        private string[] activeLogFields;

        private ServerActivity() { }

        /// <summary>
        ///     Gets the file requests.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> FileRequests => serverReport.FileRequests;

        /// <summary>
        ///     Gets the missing file requests.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> MissingFileRequests => serverReport.MissingFileRequests;

        /// <summary>
        ///     Gets the referers.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> Referers => serverReport.Referers;

        /// <summary>
        ///     Gets the user agents.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> UserAgents => serverReport.UserAgents;

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ServerActivity" /> is available.
        /// </summary>
        /// <value>
        ///     <c>true</c> if available; otherwise, <c>false</c>.
        /// </value>
        public bool Available { get; private set; }

        /// <summary>
        ///     Gets or sets the domain.
        /// </summary>
        /// <value></value>
        public string Domain
        {
            get => serverReport.Domain;
            set => serverReport.Domain = value;
        }

        /// <summary>
        ///     Loads the specified domain.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <returns></returns>
        public static ServerActivity Load(string domain)
        {
            return Load(domain, DateTime.Now);
        }

        /// <summary>
        ///     Loads the specified domain.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="date">Date.</param>
        /// <returns></returns>
        public static ServerActivity Load(string domain, DateTime date)
        {
            return Load(domain, date, date);
        }

        /// <summary>
        ///     Loads the specified domain.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <returns></returns>
        public static ServerActivity Load(string domain, DateTime startDate, DateTime endDate)
        {
            string logDirectory;
            logDirectory = WebsiteFactory.Open(domain).LogDirectory;
            TimeSpan span = endDate - startDate;

            ServerActivity activity = new ServerActivity();
            activity.Domain = domain;

            for (int index = 0; index <= span.Days; index++)
            {
                string logFile = string.Format("ex{0:yyMMdd}.log", startDate.AddDays(index));
                string fullPath = Path.Combine(logDirectory, logFile);
                if (File.Exists(fullPath))
                    activity.Available = activity.ParseEntries(fullPath);
            }
            return activity;
        }

        /// <summary>
        ///     Gets the files by directory.
        /// </summary>
        /// <param name="directory">Directory.</param>
        /// <returns></returns>
        public Collection<UniqueEntry> GetFilesByDirectory(string directory)
        {
            UniqueEntryCollectionBuilder fileRequestsByDirectry = new UniqueEntryCollectionBuilder();
            foreach (LogEntry record in serverReport.LogEntryCollection)
                if (record.UriStem.Value.StartsWith(directory))
                    fileRequestsByDirectry.Append(record.UriStem.Value);
            return fileRequestsByDirectry.ToCollection();
        }

        private bool ParseEntries(string fullPath)
        {
            string[] logFields = { };
            try
            {
                string[] entries = GetLogEntries(fullPath);
                foreach (string entry in entries)
                    if (entry.StartsWith("#Fields: "))
                        logFields = ParseFields(entry);
                    else if (logFields.Length > 0)
                        ParseEntry(entry, logFields);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        private void ParseEntry(string entry, string[] logFields)
        {
            string[] items = entry.Split(' ');
            LogEntry logEntry = new LogEntry();

            logEntry.Date = LogField.Parse(logEntry.Date, logFields, items);
            logEntry.Time = LogField.Parse(logEntry.Time, logFields, items);
            logEntry.Method = LogField.Parse(logEntry.Method, logFields, items);
            logEntry.UriStem = LogField.Parse(logEntry.UriStem, logFields, items);
            logEntry.UriQuery = LogField.Parse(logEntry.UriQuery, logFields, items);
            logEntry.Username = LogField.Parse(logEntry.Username, logFields, items);
            logEntry.ClientIP = LogField.Parse(logEntry.ClientIP, logFields, items);
            logEntry.ProtocolVersion = LogField.Parse(logEntry.ProtocolVersion, logFields, items);
            logEntry.UserAgent = LogField.Parse(logEntry.UserAgent, logFields, items);
            logEntry.Referer = LogField.Parse(logEntry.Referer, logFields, items);
            logEntry.ProtocolStatus = LogField.Parse(logEntry.ProtocolStatus, logFields, items);
            logEntry.SentBytes = LogField.Parse(logEntry.SentBytes, logFields, items);

            serverReport.LogEntryCollection.Add(logEntry);
        }

        private string[] ParseFields(string entry)
        {
            string[] logFields;
            string line = entry.Replace("#Fields: ", string.Empty);
            logFields = line.Split(' ');
            if (activeLogFields == null)
            {
                activeLogFields = new string[logFields.Length];
                Array.Copy(logFields, activeLogFields, logFields.Length);
            }
            return logFields;
        }

        private static string[] GetLogEntries(string logPath)
        {
            List<string> logEntries = new List<string>();
            FileStream fs = new FileStream(logPath, FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length != 0 && (line.StartsWith("#Fields: ") || !line.StartsWith("#")))
                        logEntries.Add(line);
                }
            }
            string[] returnValue = new string[logEntries.Count];
            logEntries.CopyTo(returnValue);
            logEntries.Clear();
            return returnValue;
        }

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        public void Save()
        {
            TextWriter writer = null;
            XmlSerializer serializer = new XmlSerializer(typeof(ServerReport));
            try
            {
                writer = new StreamWriter("test.xml");
                serializer.Serialize(writer, serverReport);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}