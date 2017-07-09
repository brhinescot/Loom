namespace Loom.Windows.Forms
{
    /// <summary>
    ///     Summary description for InvalidTargetExcption.
    /// </summary>
    public class DesignerControlError : ErrorListItem
    {
        /// <summary>
        ///     Creates a new <see cref="DesignerControlError" /> instance.
        /// </summary>
        /// <param name="control">Control.</param>
        public DesignerControlError(DesignerControl control) : this(control, ErrorSeverity.Error) { }

        /// <summary>
        ///     Creates a new <see cref="DesignerControlError" /> instance.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="severity">Severity.</param>
        public DesignerControlError(DesignerControl control, ErrorSeverity severity)
            : this(control, severity, "An unknown error was found in a designer on the whiteboard.") { }

        /// <summary>
        ///     Creates a new <see cref="DesignerControlError" /> instance.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="message">Message.</param>
        public DesignerControlError(DesignerControl control, string message) : this(control, ErrorSeverity.Error, message) { }

        /// <summary>
        ///     Creates a new <see cref="DesignerControlError" /> instance.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="severity">Severity.</param>
        /// <param name="message">Message.</param>
        public DesignerControlError(DesignerControl control, ErrorSeverity severity, string message)
        {
            InvalidObject = control;
            Severity = severity;
            Message = message;
        }
    }
}