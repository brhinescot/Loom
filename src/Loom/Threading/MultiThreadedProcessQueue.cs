#region Using Directives

using System;
using System.Threading;

#endregion

// ReSharper disable StaticFieldInGenericType

namespace Loom.Threading
{
    /// <summary>
    ///     The MultiThreadedProcessQueue class extends the functionality of the ProcessQueue class.
    ///     Like the ProcessQueue class, this class is a FIFO queue for which the user may register a
    ///     callback to process requests or objects in the background as they are dequeued.
    ///     Unlike the ProcessQueue, instead of being limited to just one dedicated thread to process
    ///     the requests or objects, the MultiThreadedProcessQueue employs an entire thread pool.
    ///     Once again, the user can queue up to as many objects as needed and they will be
    ///     processed on a background thread.  However, because the thread pool is
    ///     initialized to a maximum number of worker threads (to avoid performance problems), if
    ///     there are more objects or requests than there are available worker threads, reading
    ///     from the queue will block until a worker thread becomes available.
    /// </summary>
    public class MultiThreadedProcessQueue<T> : ProcessQueue<T>
    {
        private static readonly object QueueLock = new object();

        private readonly AppThreadPool<T> appThreadPool;

        /// <summary>
        ///     Basic constructor expects an integer that sets the maximum number of worker threads
        ///     in the AppThreadPool used to process items from the queue.
        /// </summary>
        /// <param name="maxWorkerThreads">An integer greater than or equal to 1.</param>
        public MultiThreadedProcessQueue(int maxWorkerThreads)
        {
            if (maxWorkerThreads < 1)
                throw new ArgumentOutOfRangeException("maxWorkerThreads", maxWorkerThreads, "The number of worker threads you specify must be greater than or equal to 1.");

            appThreadPool = new AppThreadPool<T> {MaximumThreads = maxWorkerThreads};
        }

        /// <summary>
        ///     Stop processing objects in the queue.  The "wait" argument indicates
        ///     whether or not we should wait for the queue to be empty before
        ///     we stop processing.  Even if we choose not to wait for the queue
        ///     to empty, this will block until all the currently working threads
        ///     in the thread pool have finished processing their objects.
        /// </summary>
        /// <param name="ignoreUnprocessed"></param>
        public override void Stop(bool ignoreUnprocessed = false)
        {
            base.Stop(ignoreUnprocessed);
            appThreadPool.AbortAllThreads();
        }

        /// <summary>
        ///     The background thread for processing the objects in the queue.
        /// </summary>
        protected override void Process()
        {
            while (Running)
            {
                T obj = default(T);

                // Lock the queue while we get the next object
                lock (QueueLock)
                {
                    // Get the object from the queue.  If there are no object in the queue
                    // and we call the Dequeue() method it will throw.
                    if (ObjectQueue.Count > 0)
                    {
                        obj = ObjectQueue.Dequeue();
                        NewObjEvent.Reset();
                    }
                }

                if (!Equals(obj, default(T)))
                    appThreadPool.QueueUserWorkItem(Processor, obj);
                else
                    NewObjEvent.WaitOne();

                if (ThrottleTime > 0)
                    Thread.Sleep(ThrottleTime);
            }
        }
    }
}