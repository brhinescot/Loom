#region Using Directives

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

#endregion

namespace Loom.Threading
{
    /// <summary>
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    [DebuggerStepThrough]
    public struct SemaphoreLock : IDisposable, IEquatable<SemaphoreLock>
    {
#if DEBUG
        private readonly Sentinel sentinel;
#endif
        private readonly Semaphore semaphore;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemaphoreLock" /> class.
        /// </summary>
        /// <param name="semaphore">The semaphore.</param>
        private SemaphoreLock(Semaphore semaphore)
        {
            this.semaphore = semaphore;
#if DEBUG
            const string className = "SemaphoreLock";
            sentinel = new Sentinel(className);
#endif
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or
        ///     resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (semaphore != null)
            {
                semaphore.Release();
                Thread.EndCriticalRegion();
            }
#if DEBUG
            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect 
            // the error. If Dispose is called, we suppress the
            // finalizer.

            GC.SuppressFinalize(sentinel);
#endif
        }

        /// <summary>
        ///     Locks the specified semaphore.
        /// </summary>
        /// <param name="semaphore">The semaphore.</param>
        /// <returns></returns>
        public static IDisposable Lock(Semaphore semaphore)
        {
            Argument.Assert.IsNotNull(semaphore, nameof(semaphore));

            Thread.BeginCriticalRegion();
            semaphore.WaitOne();
            return new SemaphoreLock(semaphore);
        }

        public static bool operator !=(SemaphoreLock semaphoreLock1, SemaphoreLock semaphoreLock2)
        {
            return !semaphoreLock1.Equals(semaphoreLock2);
        }

        public static bool operator ==(SemaphoreLock semaphoreLock1, SemaphoreLock semaphoreLock2)
        {
            return semaphoreLock1.Equals(semaphoreLock2);
        }

        public bool Equals(SemaphoreLock other)
        {
            return Equals(semaphore, other.semaphore);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SemaphoreLock))
                return false;
            return Equals((SemaphoreLock) obj);
        }

        public override int GetHashCode()
        {
            return 29 * semaphore.GetHashCode();
        }
    }
}