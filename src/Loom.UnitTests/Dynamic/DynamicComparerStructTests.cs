#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class DynamicComparerStructTests
    {
        [Test]
        public void TestStringSameValue()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Name");

            int value = comparison(TestStruct.Default, TestStruct.Default);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestStringXIsLess()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Name");

            int value = comparison(TestStruct.SetName("Abel"), TestStruct.SetName("Mario"));
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestStringXIsGreater()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Name");

            int value = comparison(TestStruct.SetName("Mario"), TestStruct.SetName("Abel"));
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestStringNullXProperty()
        {
            Comparison<TestStruct> dynComparison = DynamicType<TestStruct>.CreateComparison("Name");
            StructComparerString staticComparison = new StructComparerString();

            TestStruct x = TestStruct.NullName;
            TestStruct y = TestStruct.Default;

            int staticResult = staticComparison.Compare(x, y);
            int dynamicResult = dynComparison(x, y);

            Assert.AreEqual(staticResult, dynamicResult);
            Assert.AreEqual(-1, dynamicResult);
        }

        [Test]
        public void TestStringNullYProperty()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Name");

            TestStruct t = TestStruct.NullName;
            int value = comparison(TestStruct.Default, t);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestStringNullXAndYProperties()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Name");

            int value = comparison(TestStruct.NullName, TestStruct.NullName);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestIntSame()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Count");

            int value = comparison(TestStruct.Default, TestStruct.Default);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestIntXIsLess()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Count");

            int value = comparison(TestStruct.SetCount(20), TestStruct.SetCount(100));
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestIntXIsGreater()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("Count");

            int value = comparison(TestStruct.SetCount(100), TestStruct.SetCount(20));
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestDateTimeSame()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("DateTime");
            DateTime time = DateTime.Now;

            int value = comparison(TestStruct.SetDateTime(time), TestStruct.SetDateTime(time));
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestNullableIntSame()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(TestStruct.Default, TestStruct.Default);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void TestNullableIntXGreater()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(new TestStruct {NullableInt = 10}, new TestStruct {NullableInt = 1});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestNullableIntYGreater()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(new TestStruct {NullableInt = 1}, new TestStruct {NullableInt = 10});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestNullableIntXNull()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(new TestStruct {NullableInt = null}, new TestStruct {NullableInt = 1});
            Assert.AreEqual(-1, value);
        }

        [Test]
        public void TestNullableIntYNull()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(new TestStruct {NullableInt = 1}, new TestStruct {NullableInt = null});
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TestNullableIntBothNull()
        {
            Comparison<TestStruct> comparison = DynamicType<TestStruct>.CreateComparison("NullableInt");

            int value = comparison(new TestStruct {NullableInt = null}, new TestStruct {NullableInt = null});
            Assert.AreEqual(0, value);
        }
    }

    public class StructComparerString : IComparer<TestStruct>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return 0;

            TestStruct xResource = (TestStruct) x;
            TestStruct yResource = (TestStruct) y;

            return xResource.Name.CompareTo(yResource.Name);
        }

        #endregion

        #region IComparer<TestStruct> Members

        public int Compare(TestStruct x, TestStruct y)
        {
            if (x.Name == null && y.Name == null)
                return 0;

            if (x.Name == null)
                return -1;

            return x.Name.CompareTo(y.Name);
        }

        #endregion
    }

    public class StructResourceComparerInt : IComparer<TestStruct>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            TestStruct xResource = (TestStruct) x;
            TestStruct yResource = (TestStruct) y;

            return xResource.Count.CompareTo(yResource.Count);
        }

        #endregion

        #region IComparer<TestStruct> Members

        public int Compare(TestStruct x, TestStruct y)
        {
            return x.Count.CompareTo(y.Count);
        }

        #endregion
    }

    public class StruceResourceComparerDateTime : IComparer<TestStruct>, IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
                return -1;

            TestStruct xResource = (TestStruct) x;
            TestStruct yResource = (TestStruct) y;

            return xResource.Count.CompareTo(yResource.Count);
        }

        #endregion

        #region IComparer<TestStruct> Members

        public int Compare(TestStruct x, TestStruct y)
        {
            return x.DateTime.CompareTo(y.DateTime);
        }

        #endregion
    }
}