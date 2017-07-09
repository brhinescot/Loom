#region Using Directives

using System;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class EntityPoolTests
    {
        [SetUp]
        public void Clear()
        {
            SingleMockPool.Instance.Clear();
        }

        [Test]
        public void Borrow()
        {
            MockEntityPool pool = new MockEntityPool();
            pool.Borrow();
            pool.Borrow();
            pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);
        }

        [Test]
        public void BorrowAndReturn()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            MockEntity obj2 = pool.Borrow();
            MockEntity obj3 = pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Return(obj1);
            pool.Return(obj2);
            pool.Return(obj3);

            Assert.AreEqual(0, pool.WorkingCount);
            Assert.AreEqual(3, pool.AvailableCount);
        }

        [Test]
        public void BorrowReturnAllAndReuse()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            MockEntity obj2 = pool.Borrow();
            MockEntity obj3 = pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Return(obj1);
            pool.Return(obj2);
            pool.Return(obj3);

            Assert.AreEqual(0, pool.WorkingCount);
            Assert.AreEqual(3, pool.AvailableCount);

            pool.Borrow();
            pool.Borrow();

            Assert.AreEqual(2, pool.WorkingCount);
            Assert.AreEqual(1, pool.AvailableCount);
        }

        [Test]
        public void BorrowAndReuseOneById()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            obj1.Id = 1;
            MockEntity obj2 = pool.Borrow();
            obj2.Id = 2;
            MockEntity obj3 = pool.Borrow();
            obj3.Id = 3;

            pool.Return(obj2);
            MockEntity obj4 = pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);
            Assert.AreEqual(2, obj4.Id, "Id should be 2 because the item should be from the pool." +
                                        "It should be the one that was returned.");
        }

        [Test]
        public void BorrowAndReturnOneInvalid()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            obj1.Id = 1;
            MockEntity obj2 = pool.Borrow();
            obj2.Id = 2;
            MockEntity obj3 = pool.Borrow();
            obj3.Id = 3;

            // Mark it as invalid so it will not be reused;
            obj2.Valid = false;
            pool.Return(obj2);
            MockEntity obj4 = pool.Borrow();
            Assert.AreEqual(0, obj4.Id, "Id should be 0 because the item should be new, not from the pool.");
        }

        [Test]
        public void BorrowFromTwoInstances()
        {
            MockEntityPool pool = new MockEntityPool();
            pool.Borrow();
            pool.Borrow();
            pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            MockEntityPool pool2 = new MockEntityPool();
            pool2.Borrow();
            pool2.Borrow();
            pool2.Borrow();

            Assert.AreEqual(3, pool2.WorkingCount, "Expected 3 working pool items. Two instances of a pool should not share data.");
            Assert.AreEqual(0, pool2.AvailableCount, "Expected 0 available pool items. Two instances of a pool should not share data.");
        }

        [Test]
        public void BorrowAndClear()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            MockEntity obj2 = pool.Borrow();
            pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Clear();

            // No objects returned, should be nothing to clear.
            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Return(obj1);
            pool.Clear();

            // One object returned and cleared, should be no available objects.
            Assert.AreEqual(2, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Return(obj2);
            pool.Clear();

            // One more object returned and cleared, should be no available objects.
            Assert.AreEqual(1, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ReturnObjectNotCreatedByPool()
        {
            MockEntityPool pool = new MockEntityPool();
            pool.Borrow();
            pool.Borrow();

            pool.Return(new MockEntity());
        }

        [Test]
        public void BorrowAndReturnSameObjectMultipleTime()
        {
            MockEntityPool pool = new MockEntityPool();
            MockEntity obj1 = pool.Borrow();
            pool.Borrow();
            pool.Borrow();

            Assert.AreEqual(3, pool.WorkingCount);
            Assert.AreEqual(0, pool.AvailableCount);

            pool.Return(obj1);

            Assert.AreEqual(2, pool.WorkingCount);
            Assert.AreEqual(1, pool.AvailableCount);

            pool.Return(obj1);

            Assert.AreEqual(2, pool.WorkingCount);
            Assert.AreEqual(1, pool.AvailableCount);
        }

        [Test]
        public void SingletonBorrowAndReturn()
        {
            MockEntity obj1 = SingleMockPool.Instance.Borrow();
            MockEntity obj2 = SingleMockPool.Instance.Borrow();
            MockEntity obj3 = SingleMockPool.Instance.Borrow();

            Assert.AreEqual(3, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(0, SingleMockPool.Instance.AvailableCount);

            SingleMockPool.Instance.Return(obj1);
            SingleMockPool.Instance.Return(obj2);
            SingleMockPool.Instance.Return(obj3);

            Assert.AreEqual(0, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(3, SingleMockPool.Instance.AvailableCount);
        }

        [Test]
        public void SingletonBorrowReturnAllAndReuse()
        {
            MockEntity obj1 = SingleMockPool.Instance.Borrow();
            MockEntity obj2 = SingleMockPool.Instance.Borrow();
            MockEntity obj3 = SingleMockPool.Instance.Borrow();

            Assert.AreEqual(3, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(0, SingleMockPool.Instance.AvailableCount);

            SingleMockPool.Instance.Return(obj1);
            SingleMockPool.Instance.Return(obj2);
            SingleMockPool.Instance.Return(obj3);

            Assert.AreEqual(0, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(3, SingleMockPool.Instance.AvailableCount);

            MockEntity obj4 = SingleMockPool.Instance.Borrow();
            MockEntity obj5 = SingleMockPool.Instance.Borrow();

            Assert.AreEqual(2, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(1, SingleMockPool.Instance.AvailableCount);

            SingleMockPool.Instance.Return(obj4);
            SingleMockPool.Instance.Return(obj5);

            Assert.AreEqual(0, SingleMockPool.Instance.WorkingCount);
            Assert.AreEqual(3, SingleMockPool.Instance.AvailableCount);
        }
    }

    internal class MockEntity
    {
        public MockEntity()
        {
            Thread.Sleep(100);
        }

        public int Id { get; set; }

        public bool Valid { get; set; } = true;
    }

    internal class MockEntityPool : EntityPool<MockEntity>
    {
        protected override MockEntity Create()
        {
            return new MockEntity();
        }

        protected override bool Validate(MockEntity obj)
        {
            return obj.Valid;
        }

        protected override void Expire(MockEntity obj)
        {
            obj.Valid = false;
        }

        public MockEntity Borrow()
        {
            return RetrieveObject();
        }

        public void Return(MockEntity obj)
        {
            ReturnObject(obj);
        }
    }

    internal class SingleMockPool : EntityPool<MockEntity>
    {
        private static SingleMockPool instance;
        private static readonly object instanceLock = new object();

        private SingleMockPool() { }

        public static SingleMockPool Instance
        {
            get
            {
                if (instance == null)
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new SingleMockPool();
                    }
                return instance;
            }
        }

        protected override MockEntity Create()
        {
            return new MockEntity();
        }

        protected override bool Validate(MockEntity obj)
        {
            return obj.Valid;
        }

        protected override void Expire(MockEntity obj)
        {
            obj.Valid = false;
        }

        public MockEntity Borrow()
        {
            return RetrieveObject();
        }

        public void Return(MockEntity obj)
        {
            ReturnObject(obj);
        }
    }
}