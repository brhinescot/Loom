#region Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

#endregion

namespace Loom.Windows.Forms
{
    /// <summary>
    /// </summary>
    public interface IDesignableSurface
    {
        /// <summary>
        ///     Gets or sets the selected control.
        /// </summary>
        /// <value>The selected control.</value>
        [Browsable(false)]
        DesignerControl SelectedControl { get; set; }

        /// <summary>
        ///     Gets the control errors.
        /// </summary>
        /// <value>The control errors.</value>
        [Browsable(false)]
        Collection<DesignerControlError> ControlErrors { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        bool HasErrors { get; }

        /// <summary>
        ///     Occurs when the selected designer control has changed.
        /// </summary>
        event EventHandler<DesignerEventArgs> SelectionChanged;

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="error">The error.</param>
        void AddControlError(DesignerControlError error);

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="message">The message.</param>
        void AddControlError(DesignerControl control, string message);

        /// <summary>
        ///     Adds the control error.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        void AddControlError(DesignerControl control, string message, ErrorSeverity severity);
    }
}