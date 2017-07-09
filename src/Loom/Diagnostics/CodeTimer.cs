#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Diagnostics
{
    /// <summary>
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct CodeTimer : IEquatable<CodeTimer>
    {
        private readonly DateTime start;

        private CodeTimer(DateTime startTime)
        {
            start = startTime;
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        /// <returns></returns>
        public static CodeTimer Start()
        {
            return new CodeTimer(DateTime.Now);
        }

        /// <summary>
        ///     Stops the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns></returns>
        public static TimeSpan Stop(CodeTimer timer)
        {
            return DateTime.Now - timer.start;
        }

        /// <summary>
        ///     Stops the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns></returns>
        public static TimeSpan WriteMilliseconds(CodeTimer timer)
        {
            TimeSpan span = DateTime.Now - timer.start;
            Console.Out.WriteLine("TotalMilliseconds = {0}", span.TotalMilliseconds);
            return span;
        }

        /// <summary>
        ///     Stops the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns></returns>
        public static TimeSpan WriteTime(CodeTimer timer)
        {
            DateTime now = DateTime.Now;
            Console.Out.WriteLine(timer.start.ToElapsedTime(now));
            return now - timer.start;
        }

        public static bool operator !=(CodeTimer codeTimer1, CodeTimer codeTimer2)
        {
            return !codeTimer1.Equals(codeTimer2);
        }

        public static bool operator ==(CodeTimer codeTimer1, CodeTimer codeTimer2)
        {
            return codeTimer1.Equals(codeTimer2);
        }

        public bool Equals(CodeTimer other)
        {
            return Equals(start, other.start);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CodeTimer))
                return false;
            return Equals((CodeTimer) obj);
        }

        public override int GetHashCode()
        {
            return start.GetHashCode();
        }
    }
}