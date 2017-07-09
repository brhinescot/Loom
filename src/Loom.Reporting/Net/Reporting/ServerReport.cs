#region Using Directives

using System.Collections.ObjectModel;
using System.Xml.Serialization;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for ServerReport.
    /// </summary>
    [XmlRoot("serverActivityReport")]
    public class ServerReport
    {
        /// <summary>
        ///     Gets the file requests.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> FileRequests
        {
            get
            {
                UniqueEntryCollectionBuilder uniqueFileRequests = new UniqueEntryCollectionBuilder();
                foreach (LogEntry record in LogEntryCollection)
                    if (record.ProtocolStatus.Value.Equals("200") || record.ProtocolStatus.Value.Equals("304"))
                        uniqueFileRequests.Append(record.UriStem.Value);
                return uniqueFileRequests.ToCollection();
            }
        }

        /// <summary>
        ///     Gets the missing file requests.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> MissingFileRequests
        {
            get
            {
                UniqueEntryCollectionBuilder uniqueFileNotFoundRequests = new UniqueEntryCollectionBuilder();
                foreach (LogEntry record in LogEntryCollection)
                    if (record.ProtocolStatus.Value.Equals("404"))
                        uniqueFileNotFoundRequests.Append(record.UriStem.Value);
                return uniqueFileNotFoundRequests.ToCollection();
            }
        }

        /// <summary>
        ///     Gets the referers.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> Referers
        {
            get
            {
                string host = "http://" + Domain;
                UniqueEntryCollectionBuilder uniqueReferers = new UniqueEntryCollectionBuilder();
                foreach (LogEntry record in LogEntryCollection)
                    if (!record.Referer.Value.StartsWith(host))
                        uniqueReferers.Append(record.Referer.Value);
                return uniqueReferers.ToCollection();
            }
        }

        /// <summary>
        ///     Gets the user agents.
        /// </summary>
        /// <value></value>
        public Collection<UniqueEntry> UserAgents
        {
            get
            {
                UniqueEntryCollectionBuilder uniqueUserAgents = new UniqueEntryCollectionBuilder();
                foreach (LogEntry record in LogEntryCollection)
                    uniqueUserAgents.Append(record.UserAgent.Value);
                return uniqueUserAgents.ToCollection();
            }
        }

        /// <summary>
        ///     Gets the log entry collection.
        /// </summary>
        /// <value></value>
        internal Collection<LogEntry> LogEntryCollection { get; } = new Collection<LogEntry>();

        /// <summary>
        ///     Gets or sets the domain.
        /// </summary>
        /// <value></value>
        public string Domain { get; set; }
    }
}