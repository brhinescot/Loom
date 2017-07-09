namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public class ErrorListItem
    {
        /// <summary>
        ///     Gets or sets the invalid object.
        /// </summary>
        /// <value></value>
        public object InvalidObject { get; set; }

        /// <summary>
        ///     Gets or sets the severity.
        /// </summary>
        /// <value></value>
        public ErrorSeverity Severity { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value></value>
        public string Message { get; set; }
    }
}