#region Using Directives

using System;

#endregion

namespace Loom.Threading
{
    /// <summary>Event arguments representing the completion of a parallel action.</summary>
    public class ParallelEventArgs : EventArgs
    {
        internal ParallelEventArgs(object state, Exception exception)
        {
            State = state;
            Exception = exception;
        }

        /// <summary>The opaque state object that identifies the action (null otherwise).</summary>
        public object State { get; }

        /// <summary>The exception thrown by the parallel action, or null if it completed without exception.</summary>
        public Exception Exception { get; }
    }
}