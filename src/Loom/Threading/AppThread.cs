#region Using Directives

using System;
using System.Collections.Specialized;
using System.Threading;

#endregion

// ReSharper disable StaticFieldInGenericType

namespace Loom.Threading
{
    /// <summary>
    ///     Summary description for AppThread.
    /// </summary>
    internal sealed class AppThread<T> : IDisposable
    {
        #region Delegates

        /// <summary>
        /// </summary>
        public delegate void ThreadCompletedHandler(AppThread<T> thread);

        /// <summary>
        /// </summary>
        public delegate void UnhandledExceptionHandler(AppThread<T> thread, Exception e);

        #endregion

        private static readonly int IsRunningMask = BitVector32.CreateMask();
        private static readonly int IsCompleteMask = BitVector32.CreateMask(IsRunningMask);

        private readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        private BitVector32 flags = new BitVector32(0);
        private Thread workerThread;

        /// <summary>
        /// </summary>
        public Action<T> Callback { get; set; }

        /// <summary>
        ///     Get weather the thread is running or idle
        /// </summary>
        public bool IsRunning => flags[IsRunningMask];

        /// <summary>
        /// </summary>
        public T StateObject { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            resetEvent?.Dispose();
        }

        #endregion

        /// <summary>
        /// </summary>
        public event ThreadCompletedHandler ThreadCompleted;

        /// <summary>
        /// </summary>
        public event UnhandledExceptionHandler UnhandledException;

        /// <summary>
        ///     Execute the callback function for this thread
        /// </summary>
        public void Run()
        {
            StartThread();
            resetEvent.Set();
        }

        /// <summary>
        ///     Stop processing and shut down the thread
        /// </summary>
        public void Stop()
        {
            flags[IsCompleteMask] = true;
            resetEvent.Set();
        }

        /// <summary>
        ///     Blocks the calling thread until a thread terminates.
        /// </summary>
        public void Join()
        {
            workerThread.Join();
        }

        /// <summary>
        ///     Raises a ThreadAbortException on the thread, to begin the process of
        ///     terminating the thread. Calling this method usually terminates the thread.
        /// </summary>
        public void Abort()
        {
            workerThread.Abort();
        }

        private void StartThread()
        {
            if (workerThread != null)
                return;

            workerThread = new Thread(ThreadProc)
            {
                IsBackground = true,
                Name = "AppThreadPool " + GetHashCode()
            };
            workerThread.Start();
        }

        /// <summary>
        /// </summary>
        private void ThreadProc()
        {
            while (!flags[IsCompleteMask])
            {
                resetEvent.WaitOne();
                flags[IsRunningMask] = true;

                try
                {
                    if (Callback != null)
                        try
                        {
                            Callback(StateObject);
                        }
                        catch (Exception e)
                        {
                            UnhandledExceptionHandler unhandledExceptionHandler = UnhandledException;
                            unhandledExceptionHandler?.Invoke(this, e);

//							TraceCode.Exception( e, "AppThreadPool Unhandled Exception" );
                        }

                    Callback = null;
                    StateObject = default(T);

                    ThreadCompletedHandler threadCompletedHandler = ThreadCompleted;
                    if (threadCompletedHandler != null && !flags[IsCompleteMask])
                        threadCompletedHandler.Invoke(this);
                }
                finally
                {
                    flags[IsRunningMask] = false;
                }
            }
        }
    }
}