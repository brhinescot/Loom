#region Using Directives

using System;
using System.Threading;
using Loom.Data.Mapping;
using NUnit.Framework;

#endregion

namespace Loom.Threading
{
    [TestFixture]
    public class TimedLockTest : ThreadedTestBase
    {
        private static readonly object LockTarget = new object();

        [Test]
        public void BasicTimedLockTest()
        {
            ThreadedRepeat(10, index =>
            {
                object lockTarget = new object();
                using (TimedLock.Lock(lockTarget))
                {
                    Thread.Sleep(400);
                }
            });
        }

        [Test]
        public void TimedLockFromMillisecondsTest()
        {
            ThreadedRepeat(10, index =>
            {
                object lockTarget = new object();
                using (TimedLock.LockForMilliseconds(lockTarget, 2000))
                {
                    Thread.Sleep(400);
                }
            });
        }

        [Test]
        public void TimedLockFromSecondsTest()
        {
            ThreadedRepeat(10, index =>
            {
                object lockTarget = new object();
                using (TimedLock.LockForSeconds(lockTarget, 1))
                {
                    Thread.Sleep(200);
                }
            });
        }

        [Test]
        [ExpectedException(typeof(LockTimeoutException))]
        public void TimedLockTimeoutTest()
        {
            ThreadedRepeat(4, index =>
            {
                using (TimedLock.Lock(LockTarget, TimeSpan.FromMilliseconds(500)))
                {
                    Thread.Sleep(1000);
                }
            });
        }
    }
}