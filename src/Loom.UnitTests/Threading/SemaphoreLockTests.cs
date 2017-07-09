#region Using Directives

using System;
using System.Threading;
using Loom.Data.Mapping;
using NUnit.Framework;

#endregion

namespace Loom.Threading
{
    [TestFixture]
    public class SemaphoreLockTests : ThreadedTestBase
    {
        private static int countIn;
        private readonly Semaphore semaphore = new Semaphore(4, 4);

        [Test]
        public void LockAndSleep()
        {
            ThreadedRepeat(100, (index, threadAsserter) =>
            {
                int id = Thread.CurrentThread.ManagedThreadId;

                using (SemaphoreLock.Lock(semaphore))
                {
                    Thread.Sleep(400);
                    Console.Out.WriteLine("Thread " + id + " IN critical section. " + Interlocked.Increment(ref countIn) + " Threads in critical section.");
                }
                Console.Out.WriteLine("Thread " + id + " OUT critical section. " + Interlocked.Decrement(ref countIn) + " Threads in critical section.");

                threadAsserter.Assert(() => Assert.Less(countIn, 5));
            });
        }
    }
}