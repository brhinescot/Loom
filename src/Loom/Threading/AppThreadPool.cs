#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#endregion

// ReSharper disable StaticFieldInGenericType

namespace Loom.Threading
{
    /// <summary>
    ///     Summary description for AppThreadPool.
    /// </summary>
    internal sealed class AppThreadPool<T>
    {
        private static readonly object InstanceLock = new object();
        private static AppThreadPool<T> sysPool;

        private readonly Queue<AppThread<T>> availableThreads = new Queue<AppThread<T>>();
        private readonly Queue<ItemInfo<T>> pendingQueue = new Queue<ItemInfo<T>>();
        private readonly ArrayList workingThreads = new ArrayList();
        private int maxThreads = -1;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AppThreadPool{T}" /> class.
        /// </summary>
        public AppThreadPool()
        {
            AppDomain.CurrentDomain.DomainUnload += HandleCurrentDomainDomainUnload;
        }

        /// <summary>
        ///     Get number of available (idle) threads
        /// </summary>
        public int AvailableThreadCount
        {
            get
            {
                lock (InstanceLock)
                {
                    return availableThreads.Count;
                }
            }
        }

        /// <summary>
        ///     Get/Set the maximum number of thread in the thread pool.
        /// </summary>
        /// <remarks>If a -1 is returned then the pool will grow as needed</remarks>
        public int MaximumThreads
        {
            get => maxThreads;

            set
            {
                if (value < -1 || value == 0)
                    throw new ArgumentException("MaximumThreads can only be set to -1 or a positive value");

                maxThreads = value;
            }
        }

        /// <summary>
        ///     Gets the number of pending work items in the queue.
        /// </summary>
        /// <value>The number work items in the queue.</value>
        public int PendingWorkItems => pendingQueue.Count;

        /// <summary>
        ///     Gets or sets a value indicating whether work items should be queued
        ///     up if the maximum number of threads has been reached.
        /// </summary>
        /// <value>
        ///     <c>true</c> if work items will be queue; otherwise, <c>false</c>.
        /// </value>
        public bool QueueWorkItems { get; set; } = true;

        /// <summary>
        ///     Get the current system thread pool
        /// </summary>
        public static AppThreadPool<T> SystemPool
        {
            get
            {
                if (sysPool == null)
                    lock (InstanceLock)
                    {
                        if (sysPool == null)
                            sysPool = new AppThreadPool<T>();
                    }

                return sysPool;
            }
        }

        /// <summary>
        ///     Get total number of thread in the pool
        /// </summary>
        public int TotalThreadCount
        {
            get
            {
                lock (InstanceLock)
                {
                    return workingThreads.Count + availableThreads.Count;
                }
            }
        }

        /// <summary>
        ///     Get number of working threads
        /// </summary>
        public int WorkingThreadCount
        {
            get
            {
                lock (InstanceLock)
                {
                    return workingThreads.Count;
                }
            }
        }

        /// <summary>
        ///     Occurs when the workflow service catches an exception that no one else processed.
        /// </summary>
        public event EventHandler<UnhandledThreadExceptionEventArgs<T>> UnhandledThreadException;

        /// <summary>
        ///     Clears the pending queue.
        /// </summary>
        public void ClearPendingQueue()
        {
            pendingQueue.Clear();
        }

        /// <summary>
        ///     Wait for all the current worker threads to exit.
        /// </summary>
        /// <returns>
        ///     If true then all the threads exited, if false then
        ///     the system timed out before all worker threads exited.
        /// </returns>
        public void JoinWorkers()
        {
            JoinWorkers(int.MaxValue);
        }

        /// <summary>
        ///     Wait for all the current worker threads to exit.
        /// </summary>
        /// <param name="wait">The time to wait</param>
        /// <returns>
        ///     If true then all the threads exited, if false then
        ///     the system timed out before all worker threads exited.
        /// </returns>
        public bool JoinWorkers(TimeSpan wait)
        {
            return JoinWorkers((int) wait.TotalMilliseconds);
        }

        /// <summary>
        ///     Wait for all the current worker threads to exit.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to wait</param>
        /// <returns>
        ///     If true then all the threads exited, if false then
        ///     the system timed out before all worker threads exited.
        /// </returns>
        public bool JoinWorkers(int milliseconds)
        {
            AppThread<T>[] appThreads;
            DateTime stop = DateTime.Now.AddMilliseconds(milliseconds);

            lock (InstanceLock)
            {
                appThreads = (AppThread<T>[]) workingThreads.ToArray(typeof(AppThread<T>));
                if (appThreads.Length == 0)
                    return true;
            }

            bool allDone = false;
            while (!allDone)
            {
                allDone = true;

                // Yield off the thread
                Thread.Sleep(1);

                // Check the threads and see if they done or not
                int count = appThreads.Length;
                for (int x = 0; x < count; x++)
                {
                    AppThread<T> thread = appThreads[x];
                    if (thread == null)
                        continue;

                    if (thread.IsRunning)
                    {
                        allDone = false;
                        break;
                    }

                    // This thread is done so remove it from the array.
                    appThreads[x] = null;
                }

                if (stop <= DateTime.Now)
                    break;
            }

            return allDone;
        }

        /// <summary>
        ///     Blocks the calling thread until all the available threads have been terminated.
        /// </summary>
        public void ReleaseAvailableThreads()
        {
            List<AppThread<T>> list = new List<AppThread<T>>();

            lock (InstanceLock)
            {
                if (availableThreads.Count > 0)
                {
                    AppThread<T> thread = availableThreads.Dequeue();
                    while (thread != null)
                    {
                        thread.Stop();
                        list.Add(thread);

                        try
                        {
                            thread = availableThreads.Dequeue();
                        }
                        catch (Exception)
                        {
                            thread = null;
                        }
                    }
                }
            }

            // Release the lock and now wait for all the threads to finish up
            foreach (AppThread<T> thread in list)
                thread.Join();
        }

        /// <summary>
        ///     Blocks the calling thread until all the working threads have been terminated.
        ///     It will call the Abort() on all working threads threads.
        /// </summary>
        /// <remarks>
        ///     Use this method with caution
        /// </remarks>
        public void AbortWorkingThreads()
        {
            ArrayList list = new ArrayList();

            lock (InstanceLock)
            {
                foreach (AppThread<T> thread in workingThreads)
                {
                    thread.Abort();
                    list.Add(thread);
                }

                // Release the lock and now wait for all the threads to finish up
                foreach (AppThread<T> thread in list)
                    workingThreads.Remove(thread);
            }
        }

        /// <summary>
        ///     Clears the pending queue aborts working threads and releases available threads
        /// </summary>
        public void AbortAllThreads()
        {
            ClearPendingQueue();
            AbortWorkingThreads();
            ReleaseAvailableThreads();
        }

        /// <summary>
        ///     This will queue up an item to be run on the thread pool.
        /// </summary>
        /// <param name="callback">The handler for the Thread proc to run.</param>
        /// <param name="state">A state object passed to the thread proc</param>
        /// <returns>
        ///     Returns 1 if the work item was dispatched to a thread.  Returns 2
        ///     if the work items was queued because the maximum number of working
        ///     threads has been was reached.  Returns 0 if the work items was
        ///     not queued up because the maximum number of working threads
        ///     has been reached and QueueWorkItems is set to false.
        /// </returns>
        public int QueueUserWorkItem(Action<T> callback, T state = default(T))
        {
            lock (InstanceLock)
            {
                // Max threads reached so queue the work item
                AppThread<T> workingThread = GetWorkingThread();
                if (workingThread == null)
                {
                    if (QueueWorkItems)
                    {
                        pendingQueue.Enqueue(new ItemInfo<T>(callback, state));
                        return 2;
                    }

                    return 0;
                }

                // Launch the working thread
                workingThreads.Add(workingThread);

                workingThread.Callback = callback;
                workingThread.StateObject = state;
                workingThread.Run();

                return 1;
            }
        }

        /// <summary>
        ///     Creates a new worker thread
        /// </summary>
        /// <returns>Reference to the new worker thread</returns>
        private AppThread<T> CreateNewThread()
        {
            AppThread<T> workingThread = null;

            lock (InstanceLock)
            {
                if (maxThreads == -1 || TotalThreadCount < maxThreads)
                {
                    workingThread = new AppThread<T>();
                    workingThread.ThreadCompleted += HandleWorkingThreadThreadCompleted;
                    workingThread.UnhandledException += HandleWorkingThreadUnhandledException;
                }
            }

            return workingThread;
        }

        /// <summary>
        ///     Get the next available worker thread
        /// </summary>
        /// <returns>
        ///     Returns a worker thread.  If null then no worker threads are available
        ///     and the max threads in the pool has been reached
        /// </returns>
        private AppThread<T> GetWorkingThread()
        {
            AppThread<T> workingThread = null;

            lock (InstanceLock)
            {
                if (availableThreads.Count != 0)
                    workingThread = availableThreads.Dequeue();

                // No thread available so create a new one
                if (workingThread == null)
                    workingThread = CreateNewThread();
            }

            return workingThread;
        }

        /// <summary>
        ///     When the current domain exits then release all our threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleCurrentDomainDomainUnload(object sender, EventArgs args)
        {
            AbortAllThreads();
            sysPool = null;
        }

        /// <summary>
        ///     Called when the worker thread completes it's task
        /// </summary>
        private void HandleWorkingThreadThreadCompleted(AppThread<T> workingThread)
        {
            lock (InstanceLock)
            {
                workingThreads.Remove(workingThread);
                availableThreads.Enqueue(workingThread);

                if (pendingQueue.Count > 0)
                {
                    ItemInfo<T> info = pendingQueue.Dequeue();
                    if (info != null)
                        QueueUserWorkItem(info.Callback, info.State);
                }
            }
        }

        /// <summary>
        ///     Called if there is an unhandled exception on a worker thread.
        /// </summary>
        /// <param name="thread">The thread that caused the exception</param>
        /// <param name="e">The exception that was thrown</param>
        private void HandleWorkingThreadUnhandledException(AppThread<T> thread, Exception e)
        {
            lock (InstanceLock)
            {
                // Remove the thread from the working list. We are just going to let the system
                // drop the thread and clean up the object.  We have no idea what state things 
                // are in.  The AppThread object will rethrow the exception.
                workingThreads.Remove(thread);

                // Fire the exception event
                OnUnhandledThreadException(new UnhandledThreadExceptionEventArgs<T>(thread.Callback, thread.StateObject, e));
            }
        }

        private void OnUnhandledThreadException(UnhandledThreadExceptionEventArgs<T> e)
        {
            EventHandler<UnhandledThreadExceptionEventArgs<T>> handler = UnhandledThreadException;
            handler?.Invoke(this, e);
        }

        #region Nested type: ItemInfo

        /// <summary>
        /// </summary>
        private class ItemInfo<TDelegate>
        {
            /// <summary>
            /// </summary>
            /// <param name="callback"></param>
            /// <param name="state"></param>
            public ItemInfo(Action<TDelegate> callback, TDelegate state)
            {
                Callback = callback;
                State = state;
            }

            public Action<TDelegate> Callback { get; }

            public TDelegate State { get; }
        }

        #endregion

        // Local Instnace Values
    }
}