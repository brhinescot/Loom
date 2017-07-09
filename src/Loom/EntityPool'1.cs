#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

#endregion

namespace Loom
{
    // TODO: Write an example usage of the EntityPool'1 class.
    /// <summary>
    ///     Represents a base class for constructing a pool of expensive objects.
    /// </summary>
    /// <remarks>
    ///     Carefully consider the type that is to be pooled. The overhead of
    ///     pooling the objects could be more expensive than creating the objects as needed.
    ///     Objects should only be pooled if they are expensive to create in terms of CPU
    ///     utilization, memory, or time.
    /// </remarks>
    /// <example>
    ///     <code>
    /// 
    ///   </code>
    /// </example>
    [DebuggerDisplay("Working=[{WorkingCount}], Available=[{AvailableCount}], ReclaimInterval={ReclaimInterval}")]
    public abstract class EntityPool<T> : IDisposable where T : class
    {
        private readonly Dictionary<T, long> available = new Dictionary<T, long>();
        private readonly List<T> expired = new List<T>();
        private readonly object listLock = new object();
        private readonly Dictionary<T, long> working = new Dictionary<T, long>();

        private Timer cleanupTimer;
        private long reclaimInterval = 90 * 1000; //90 Seconds

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityPool{T}" /> class.
        /// </summary>
        protected EntityPool()
        {
            InitializeTimer();
        }

        /// <summary>
        ///     Gets the number of objects in the pool that are available for work.
        /// </summary>
        public int AvailableCount => available.Count;

        /// <summary>
        ///     Gets or sets a value indicating whether to collect invalid or
        ///     expired objects as they are found.
        /// </summary>
        /// <value>
        ///     <c>true</c> if collect as found; otherwise, <c>false</c>.
        /// </value>
        protected bool CollectFast { get; set; }

        /// <summary>
        ///     Gets or sets the object reclaim interval.
        /// </summary>
        /// <value></value>
        protected long ReclaimInterval
        {
            get => reclaimInterval;
            set
            {
                reclaimInterval = value;
                InitializeTimer();
            }
        }

        /// <summary>
        ///     Gets the number of objects in the pool that are currently performing work.
        /// </summary>
        public int WorkingCount => working.Count;

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        /// <summary>
        ///     Removes all pooled objects that are not in a working state.
        /// </summary>
        public virtual void Clear()
        {
            ClearPrivate();
        }

        /// <summary>
        ///     Retrieves the next available object from the pool.
        /// </summary>
        /// <returns></returns>
        protected T RetrieveObject()
        {
            return RetrieveObjectPrivate();
        }

        /// <summary>
        ///     Returns the object to the pool.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        /// <exception cref="ArgumentException">The specified object was not found in the pool.</exception>
        protected void ReturnObject(T obj)
        {
            lock (listLock)
            {
                if (working.Count == 0 || obj == null || available.ContainsKey(obj))
                    return;
            }

            if (!working.ContainsKey(obj))
                throw new ArgumentException("The specified object was not found in the pool.");

            ReturnObjectPrivate(obj);
        }

        /// <summary>
        ///     Disposes the current instance.
        /// </summary>
        /// <param name="disposing">
        ///     if set to <c>true</c> [disposing].
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            cleanupTimer?.Dispose();
        }

        private void ClearPrivate()
        {
            lock (listLock)
            {
                foreach (T obj in available.Keys)
                    Expire(obj);
                available.Clear();
            }
        }

        private T RetrieveObjectPrivate()
        {
            T newObj = default(T);
            lock (listLock)
            {
                long resetTime = DateTime.Now.Ticks;
                foreach (T obj in available.Keys)
                {
                    if (Validate(obj))
                    {
                        available.Remove(obj);
                        working.Add(obj, resetTime);
                        newObj = obj;
                        break;
                    }

                    if (CollectFast && ReclaimInterval > 0)
                        expired.Add(obj);
                }
                if (newObj == null)
                {
                    newObj = Create();
                    working.Add(newObj, resetTime);
                }
                if (CollectFast && ReclaimInterval > 0 && expired.Count > 0)
                    RemoveExpired();
            }

            return newObj;
        }

        private void ReturnObjectPrivate(T obj)
        {
            lock (listLock)
            {
                if (working.Remove(obj))
                    available.Add(obj, DateTime.Now.Ticks);
            }
        }

        private void HandleCleanupTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (listLock)
            {
                long now = DateTime.Now.Ticks;

                foreach (KeyValuePair<T, long> entry in available)
                {
                    T obj = entry.Key;
                    if (now - available[obj] > ReclaimInterval || !Validate(obj))
                        expired.Add(obj);
                }
                RemoveExpired();
            }
        }

        private void InitializeTimer()
        {
            if (cleanupTimer == null)
            {
                //Create a timer to track the expired objects for cleanup
                cleanupTimer = new Timer();
                cleanupTimer.Elapsed += HandleCleanupTimerElapsed;
            }

            if (ReclaimInterval > 0)
            {
                cleanupTimer.Enabled = true;
                cleanupTimer.Interval = ReclaimInterval;
            }
            else
            {
                cleanupTimer.Enabled = false;
            }
        }

        private void RemoveExpired()
        {
            // No lock as this is called from within a locked segment of code. 
            for (int i = 0; i < expired.Count; i++)
            {
                available.Remove(expired[i]);
                Expire(expired[i]);
                expired[i] = default(T);
            }
            expired.Clear();
        }

        /// <summary>
        ///     When implemented in a derived class, creates a new pooled object.
        /// </summary>
        /// <returns></returns>
        protected abstract T Create();

        /// <summary>
        ///     When implemented in a derived class, expires the specified object.
        /// </summary>
        /// <param name="obj">The object to expire.</param>
        protected abstract void Expire(T obj);

        /// <summary>
        ///     When implemented in a derived class, validates the specified object.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <returns></returns>
        protected abstract bool Validate(T obj);
    }
}