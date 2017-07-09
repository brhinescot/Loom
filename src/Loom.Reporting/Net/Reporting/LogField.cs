#region Using Directives

using System;

#endregion

namespace Loom.Web.Reporting
{
    /// <summary>
    ///     Summary description for LogFields.
    /// </summary>
    [Serializable]
    internal class LogField : IEquatable<LogField>
    {
        private string value;

        internal LogField(string name) : this(name, false) { }

        internal LogField(string name, bool required) : this(name, required, null) { }

        internal LogField(string name, bool required, string value)
        {
            Name = name;
            Required = required;
            this.value = value;
        }

        internal string Value
        {
            get => value;
            set => this.value = value;
        }

        internal string Name { get; }

        internal bool Required { get; }

        internal static LogField Date => new LogField("date", true);

        internal static LogField Time => new LogField("time", true);

        internal static LogField Method => new LogField("cs-method", true);

        internal static LogField UriStem => new LogField("cs-uri-stem", true);

        internal static LogField UriQuery => new LogField("cs-uri-query");

        internal static LogField Username => new LogField("cs-username");

        internal static LogField ClientIP => new LogField("c-ip");

        internal static LogField ProtocolVersion => new LogField("cs-version");

        internal static LogField UserAgent => new LogField("cs(User-Agent)");

        internal static LogField Referer => new LogField("cs(Referer)");

        internal static LogField ProtocolStatus => new LogField("sc-status");

        internal static LogField SentBytes => new LogField("sc-bytes");

        internal static LogField ReceivedBytes => new LogField("cs-bytes");

        #region IEquatable<LogField> Members

        public bool Equals(LogField other)
        {
            return !(other.Name.Equals(Name) &&
                     other.Value.Equals(Value) &&
                     other.Required.Equals(Required));
        }

        #endregion

        internal static LogField Parse(LogField logField, string[] logFields, string[] logRecord)
        {
            Argument.Assert.IsNotNull(logField, "logField");

            int index = Array.IndexOf(logFields, logField.Name);
            if (index < 0)
            {
                if (logField.Required)
                    throw new ArgumentException(SR.ExceptionLogFieldMissing(logField.Name));

                return new LogField(logField.Name, logField.Required, string.Empty);
            }
            string value = logRecord[index];
            return new LogField(logField.Name, logField.Required, value);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LogField))
                return false;

            return Equals((LogField) obj);
        }

        public static bool operator ==(LogField left, LogField right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LogField left, LogField right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return value;
        }
    }
}