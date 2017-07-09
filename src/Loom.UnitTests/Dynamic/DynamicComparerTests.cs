#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class DynamicComparerTests
    {
        [Test]
        public void TestStringSameValue()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(new TestClass(), new TestClass());
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestStringXIsLess()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(new TestClass {Name = "Able"}, new TestClass {Name = "Mario"});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestStringXIsGreater()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(new TestClass {Name = "Mario"}, new TestClass {Name = "Able"});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestStringNullX()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(null, new TestClass());
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestStringNullY()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(new TestClass(), null);
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestStringNullXProperty()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            TestClass t = new TestClass {Name = null};
            int value = comparison(t, new TestClass());
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestStringNullYProperty()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            TestClass t = new TestClass {Name = null};
            int value = comparison(new TestClass(), t);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestStringNullXAndYProperties()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Name");

            int value = comparison(new TestClass {Name = null}, new TestClass {Name = null});
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestIntSame()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Count");

            int value = comparison(new TestClass(), new TestClass());
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestIntXIsLess()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Count");

            int value = comparison(new TestClass {Count = 20}, new TestClass {Count = 100});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestIntXIsGreater()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("Count");

            int value = comparison(new TestClass {Count = 100}, new TestClass {Count = 20});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestDateTimeSame()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("DateTime");
            DateTime time = DateTime.Now;

            int value = comparison(new TestClass {DateTime = time}, new TestClass {DateTime = time});
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestNullableIntSame()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = 1}, new TestClass {NullableInt = 1});
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestNullableIntXGreater()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = 10}, new TestClass {NullableInt = 1});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestNullableIntYGreater()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = 1}, new TestClass {NullableInt = 10});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestNullableIntXNull()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = null}, new TestClass {NullableInt = 1});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestNullableIntYNull()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = 1}, new TestClass {NullableInt = null});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestNullableIntBothNull()
        {
            Comparison<TestClass> comparison = DynamicType<TestClass>.CreateComparison("NullableInt");

            int value = comparison(new TestClass {NullableInt = null}, new TestClass {NullableInt = null});
            Assert.AreEqual(0, value);
        }
    }

    public class ResourceComparerString : IComparer<TestClass>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            TestClass xResource = (TestClass) x;
            TestClass yResource = (TestClass) y;

            return string.Compare(xResource.Name, yResource.Name, StringComparison.Ordinal);
        }

        #endregion

        #region IComparer<TestClass> Members

        public int Compare(TestClass x, TestClass y)
        {
            if (x == null || y == null)
                return -1;

            if (x.Name == null && y.Name == null)
                return 0;

            if (x.Name == null)
                return -1;

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }

        #endregion
    }

    public class ResourceComparerInt : IComparer<TestClass>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            TestClass xResource = (TestClass) x;
            TestClass yResource = (TestClass) y;

            return xResource.Count.CompareTo(yResource.Count);
        }

        #endregion

        #region IComparer<TestClass> Members

        public int Compare(TestClass x, TestClass y)
        {
            if (x == null || y == null)
                return -1;

            return x.Count.CompareTo(y.Count);
        }

        #endregion
    }

    public class ResourceComparerDateTime : IComparer<TestClass>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            TestClass xResource = (TestClass) x;
            TestClass yResource = (TestClass) y;

            return xResource.Count.CompareTo(yResource.Count);
        }

        #endregion

        #region IComparer<TestClass> Members

        public int Compare(TestClass x, TestClass y)
        {
            if (x == null || y == null)
                return -1;

            return x.DateTime.CompareTo(y.DateTime);
        }

        #endregion
    }
}