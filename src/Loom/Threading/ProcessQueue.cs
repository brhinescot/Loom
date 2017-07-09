#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#endregion

namespace Loom.Threading
{
    /// <summary>
    ///     This class is used to process requests or object on another thread.  The user can queue up
    ///     as many object as needed and they will be process on a background thread.
    /// </summary>
    public class ProcessQueue<T> : IDisposable, IEnumerable<T>, ICollection
    {
        private readonly object queueLock = new object();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessQueue{T}" /> class.
        /// </summary>
        /// <param name="processor"></param>
        public ProcessQueue(Action<T> processor)
        {
            Argument.Assert.IsNotNull(processor, nameof(processor));
            Processor = processor;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessQueue{T}" /> class.
        /// </summary>
        public ProcessQueue() { }

        protected AutoResetEvent NewObjEvent { get; } = new AutoResetEvent(false);

        protected Queue<T> ObjectQueue { get; } = new Queue<T>();

        protected Action<T> Processor { get; private set; }

        /// <summary>
        ///     Gets a value indicating if the <see cref="ProcessQueue{T}" /> is actively processing items.
        /// </summary>
        public bool Running { get; protected set; }

        /// <summary>
        ///     Gets or sets the time in milliseconds to wait before processing the next item in the <see cref="ProcessQueue{T}" />
        /// </summary>
        public int ThrottleTime { get; set; }

        #region ICollection Members

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"></see> that is the destination
        ///     of the elements copied from <see cref="ICollection"></see>. The <see cref="Array"></see> must have zero-based
        ///     indexing.
        /// </param>
        /// <param name="index">The zero-based index in array at which copying begins. </param>
        /// <exception cref="ArgumentNullException">array is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException">index is less than zero. </exception>
        /// <exception cref="ArgumentException">
        ///     array is multidimensional.-or- index is equal to or
        ///     greater than the length of array.-or- The number of elements in the source
        ///     <see cref="ICollection"></see> is greater than the available space from index to the
        ///     end of the destination array.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     The type of the source <see cref="ICollection"></see>
        ///     cannot be cast automatically to the type of the destination array.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo((T[]) array, index);
        }

        /// <summary>
        ///     Gets the number of objects in the queue
        /// </summary>
        public int Count => ObjectQueue.Count;

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="ICollection"></see>.
        /// </summary>
        /// <returns>
        ///     An object that can be used to synchronize access to the <see cref="ICollection"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        object ICollection.SyncRoot => ((ICollection) ObjectQueue).SyncRoot;

        /// <summary>
        ///     Gets a value indicating whether access to the <see cref="ICollection"></see> is synchronized (thread safe).
        /// </summary>
        /// <returns>
        ///     true if access to the <see cref="ICollection"></see> is synchronized (thread safe); otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        bool ICollection.IsSynchronized => ((ICollection) ObjectQueue).IsSynchronized;

        #endregion

        #region IDisposable Members

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ObjectQueue.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return ObjectQueue.GetEnumerator();
        }

        #endregion

        /// <summary>
        ///     The system will call the given handler when a object needs to be process from the
        ///     queue.  The call will be on a background thread.
        /// </summary>
        /// <param name="processor">Handler to process the requested object</param>
        public void RegisterProcessor(Action<T> processor)
        {
            Argument.Assert.IsNotNull(processor, nameof(processor));

            if (Running)
                throw new InvalidOperationException("The processor can not be changed while the queue is running");

            Processor = processor;
        }

        /// <summary>
        ///     Start processing objects in the queue
        /// </summary>
        public void Start()
        {
            if (Processor == null)
                throw new InvalidOperationException("No processor method has been registered");

            Running = true;

            Thread thread = new Thread(Process);
            thread.IsBackground = true;
            thread.Name = "ProcessQueue " + GetHashCode();
            thread.Start();
        }

        /// <summary>
        ///     Stop processing objects in the queue.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="ignoreUnprocessed" /> argument indicates
        ///     whether or not we should wait for the queue to be empty before
        ///     we stop processing.
        /// </remarks>
        /// <param name="ignoreUnprocessed"></param>
        public virtual void Stop(bool ignoreUnprocessed = false)
        {
            if (!ignoreUnprocessed)
                while (ObjectQueue.Count > 0)
                    Thread.Sleep(1);

            Running = false;
            NewObjEvent.Set();
        }

        /// <summary>
        ///     Add the given object to the queue
        /// </summary>
        /// <param name="obj">The object to add to the queue</param>
        public void Enqueue(T obj)
        {
            Argument.Assert.IsNotNull(obj, nameof(obj));

            lock (queueLock)
            {
                ObjectQueue.Enqueue(obj);
                NewObjEvent.Set();
            }
        }

        /// <summary>
        ///     The background thread for processing the objects in the queue.
        /// </summary>
        protected virtual void Process()
        {
            while (Running)
            {
                T obj = default(T);

                lock (queueLock)
                {
                    if (ObjectQueue.Count > 0)
                    {
                        obj = ObjectQueue.Dequeue();
                        NewObjEvent.Reset();
                    }
                }

                if (!Equals(obj, default(T)))
                    Processor(obj);
                else
                    NewObjEvent.WaitOne();

                if (ThrottleTime > 0)
                    Thread.Sleep(ThrottleTime);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (NewObjEvent != null)
                    NewObjEvent.Close();
        }

        /// <summary>
        ///     Determines whether the <see cref="ICollection{T}"></see> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if item is found in the <see cref="ICollection{T}"></see>; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="ICollection{T}"></see>.
        /// </param>
        public bool Contains(T item)
        {
            return ObjectQueue.Contains(item);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="ICollection{T}"></see> to an <see cref="Array"></see>,
        ///     starting at a particular <see cref="Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="Array"></see> that is the destination of
        ///     the elements copied from <see cref="ICollection{T}"></see>. The <see cref="Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        /// <exception cref="ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        /// <exception cref="ArgumentNullException">array is null.</exception>
        /// <exception cref="ArgumentException">
        ///     array is multidimensional.-or-arrayIndex is equal to or
        ///     greater than the length of array.-or-The number of elements in the source
        ///     <see cref="ICollection{T}"></see> is greater than the available space from arrayIndex to
        ///     the end of the destination array.-or-Type T cannot be cast automatically to the type of
        ///     the destination array.
        /// </exception>
        public void CopyTo(T[] array, int index)
        {
            Argument.Assert.IsNotNull(array, nameof(array));
            Argument.Assert.IsNotNegative(index, nameof(index));

            ObjectQueue.CopyTo(array, index);
        }
    }
}