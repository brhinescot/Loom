#region Using Directives

using System;

#endregion

namespace Loom
{
    /// <summary>
    /// </summary>
    public interface IExceptionFormatter
    {
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
        string Generate(Exception exception);

        /// <summary>
        ///     <para>Get the formatted message to be logged.</para>
        /// </summary>
        /// <param name="exception">
        ///     <para>
        ///         The exception object whose information
        ///         <param name="newLine">The newline character.</param>
        ///         should be written to log file.
        ///     </para>
        /// </param>
        /// <param name="newLine"></param>
        /// <returns>
        ///     <para>The formatted message.</para>
        /// </returns>
        string Generate(Exception exception, string newLine);
    }
}