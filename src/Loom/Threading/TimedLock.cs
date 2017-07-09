#region Using Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

#endregion

namespace Loom.Threading
{
    /// <summary>
    ///     Represents a class for acquiring a timed lock on an object with a usage pattern
    ///     similar to the <see langword="lock" /> statement
    /// </summary>
    /// <example>
    ///     The following example demonstrates a common usage of the <see cref="TimedLock" /> class.
    ///     <code>
    /// private static readonly object lockTarget = new object();
    /// 
    /// public void Start()
    /// {
    ///     using(TimedLock.Lock(lockTarget, Timespan.FromSeconds(20)))
    ///     {
    ///         DoWork();
    ///     }
    /// }
    /// </code>
    /// </example>
    [StructLayout(LayoutKind.Auto)]
    [DebuggerStepThrough]
    public struct TimedLock : IDisposable, IEquatable<TimedLock>
    {
        #region Instance Fields

#if DEBUG
        private readonly Sentinel sentinel;
#endif
        private readonly object lockTarget;
        private readonly bool locked;

        #endregion

        #region .ctor

        private TimedLock(object target, TimeSpan timeout)
        {
#if DEBUG
            sentinel = new Sentinel("TimedLock");
#endif
            bool lockTaken = false;
            Monitor.TryEnter(lockTarget = target, timeout, ref lockTaken);
            if (!lockTaken)
            {
#if DEBUG
                // It's a bad error if someone forgets to call Dispose,
                // so in Debug builds, we put a finalizer in to detect
                // the error. If a LockTimeoutException is thrown, we suppress the
                // finalizer.
                GC.SuppressFinalize(sentinel);
#endif
                throw new LockTimeoutException();
            }
            locked = true;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (locked)
                Monitor.Exit(lockTarget);
#if DEBUG
            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
            GC.SuppressFinalize(sentinel);
#endif
        }

        #endregion

        /// <summary>
        ///     Attempts, for the specified amount of time, to acquire an exclusive lock on the
        ///     specified <paramref name="lockTarget" />.
        /// </summary>
        /// <param name="lockTarget">The object on which to acquire the lock.</param>
        /// <exception cref="ArgumentNullException">The obj parameter is null. </exception>
        /// <exception cref="ArgumentException">The obj parameter is a value type. </exception>
        /// <exception cref="LockTimeoutException">
        ///     The timeout period elapses waiting for an
        ///     exclusive lock.
        /// </exception>
        /// <returns>
        ///     An <see cref="IDisposable" /> object that causes the lock to exit when
        ///     <see cref="IDisposable.Dispose" /> is called.
        /// </returns>
        public static IDisposable Lock(object lockTarget)
        {
            return Lock(lockTarget, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        ///     Attempts, for the specified amount of time, to acquire an exclusive lock on the
        ///     specified <paramref name="lockTarget" />.
        /// </summary>
        /// <param name="lockTarget">The object on which to acquire the lock.</param>
        /// <param name="milliseconds">
        ///     An <see cref="int" /> representing the amount of time to wait for the lock.
        ///     A value of –1 millisecond specifies an infinite wait.
        /// </param>
        /// <exception cref="ArgumentNullException">The obj parameter is null. </exception>
        /// <exception cref="ArgumentException">The obj parameter is a value type. </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of timeout in milliseconds is negative
        ///     and is not equal to <see cref="Timeout.Infinite"></see> (–1 millisecond), or is greater
        ///     than <see cref="Int32.MaxValue"></see>.
        /// </exception>
        /// <exception cref="LockTimeoutException">
        ///     The timeout period elapses waiting for an
        ///     exclusive lock.
        /// </exception>
        /// <returns>
        ///     An <see cref="IDisposable" /> object that causes the lock to exit when
        ///     <see cref="IDisposable.Dispose" /> is called.
        /// </returns>
        public static IDisposable LockForMilliseconds(object lockTarget, int milliseconds)
        {
            return Lock(lockTarget, TimeSpan.FromMilliseconds(milliseconds));
        }

        /// <summary>
        ///     Attempts, for the specified amount of time, to acquire an exclusive lock on the
        ///     specified <paramref name="lockTarget" />.
        /// </summary>
        /// <param name="lockTarget">The object on which to acquire the lock.</param>
        /// <param name="seconds">
        ///     An <see cref="int" /> representing the amount of time to wait for the lock.
        /// </param>
        /// <exception cref="ArgumentNullException">The obj parameter is null. </exception>
        /// <exception cref="ArgumentException">The obj parameter is a value type. </exception>
        /// <exception cref="LockTimeoutException">
        ///     The timeout period elapses waiting for an
        ///     exclusive lock.
        /// </exception>
        /// <returns>
        ///     An <see cref="IDisposable" /> object that causes the lock to exit when
        ///     <see cref="IDisposable.Dispose" /> is called.
        /// </returns>
        public static IDisposable LockForSeconds(object lockTarget, int seconds)
        {
            return Lock(lockTarget, TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        ///     Attempts, for the specified amount of time, to acquire an exclusive lock on the
        ///     specified <paramref name="lockTarget" />.
        /// </summary>
        /// <param name="lockTarget">The object on which to acquire the lock. </param>
        /// <param name="timeout">
        ///     A <see cref="TimeSpan"></see> representing the amount of time to
        ///     wait for the lock. A value of –1 millisecond specifies an infinite wait.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="lockTarget" /> parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="lockTarget" /> parameter is a value type.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The value of timeout in milliseconds is
        ///     negative and is not equal to <see cref="Timeout.Infinite"></see> (–1 millisecond), or
        ///     is greater than <see cref="Int32.MaxValue"></see>.
        /// </exception>
        /// <exception cref="LockTimeoutException">
        ///     The timeout period elapses waiting for an
        ///     exclusive lock.
        /// </exception>
        /// <returns>
        ///     An <see cref="IDisposable" /> object that causes the lock to exit when
        ///     <see cref="IDisposable.Dispose" /> is called.
        /// </returns>
        public static IDisposable Lock(object lockTarget, TimeSpan timeout)
        {
            Argument.Assert.IsNotNull(lockTarget, nameof(lockTarget));

            return new TimedLock(lockTarget, timeout);
        }

        #region Equality Members

        public static bool operator !=(TimedLock timedLock1, TimedLock timedLock2)
        {
            return !timedLock1.Equals(timedLock2);
        }

        public static bool operator ==(TimedLock timedLock1, TimedLock timedLock2)
        {
            return timedLock1.Equals(timedLock2);
        }

        public bool Equals(TimedLock other)
        {
            return Equals(lockTarget, other.lockTarget);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TimedLock))
                return false;
            return Equals((TimedLock) obj);
        }

        public override int GetHashCode()
        {
            return 29 * lockTarget.GetHashCode();
        }

        #endregion
    }
}