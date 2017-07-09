namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for LogRecord.
    /// </summary>
    internal class LogEntry
    {
        internal LogEntry()
        {
            Date = LogField.Date;
            Time = LogField.Time;
            Method = LogField.Method;
            UriStem = LogField.UriStem;
            UriQuery = LogField.UriQuery;
            Username = LogField.Username;
            ClientIP = LogField.ClientIP;
            ProtocolVersion = LogField.ProtocolVersion;
            UserAgent = LogField.UserAgent;
            Referer = LogField.Referer;
            ProtocolStatus = LogField.ProtocolStatus;
            SentBytes = LogField.SentBytes;
            ReceivedBytes = LogField.ReceivedBytes;
        }

        [LogField("date", Required = true)]
        internal LogField Date { get; set; }

        [LogField("time", Required = true)]
        internal LogField Time { get; set; }

        [LogField("cs-method", Required = true)]
        internal LogField Method { get; set; }

        [LogField("cs-uri-stem", Required = true)]
        internal LogField UriStem { get; set; }

        [LogField("cs-uri-query", Required = true)]
        internal LogField UriQuery { get; set; }

        [LogField("cs-username")]
        internal LogField Username { get; set; }

        [LogField("c-ip")]
        internal LogField ClientIP { get; set; }

        [LogField("cs-version")]
        internal LogField ProtocolVersion { get; set; }

        [LogField("cs(User-Agent)")]
        internal LogField UserAgent { get; set; }

        [LogField("cs(Referer)")]
        internal LogField Referer { get; set; }

        [LogField("sc-status")]
        internal LogField ProtocolStatus { get; set; }

        [LogField("sc-bytes")]
        internal LogField SentBytes { get; set; }

        [LogField("cs-bytes")]
        internal LogField ReceivedBytes { get; set; }
    }
}