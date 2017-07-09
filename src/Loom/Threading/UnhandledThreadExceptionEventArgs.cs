#region Using Directives

using System;

#endregion

namespace Loom.Threading
{
    /// <summary>
    ///     Contains event data for an unhandled thread exception.
    /// </summary>
    public sealed class UnhandledThreadExceptionEventArgs<T> : EventArgs
    {
        public UnhandledThreadExceptionEventArgs(Action<T> callback, object state, Exception exception)
        {
            Callback = callback;
            State = state;
            Exception = exception;
        }

        /// <summary>
        /// </summary>
        public Action<T> Callback { get; }

        public Exception Exception { get; }

        public object State { get; }
    }
}