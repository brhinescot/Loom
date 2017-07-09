#region Using Directives

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Loom
{
    [TestFixture]
    public class ArrayExtensionsTest
    {
        [Test]
        public void StringIncrement()
        {
            string[] origArray = {"test1.jpg", "test2.jpg"};
            const string newValue = "test3.jpg";

            string[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "test1.jpg");
            Assert.IsTrue(newArray[1] == "test2.jpg");
            Assert.IsTrue(newArray[2] == "test3.jpg");
        }

        [Test]
        public void IntIncrement()
        {
            int[] origArray = {1, 2};
            const int newValue = 3;

            int[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
        }

        [Test]
        public void FloatIncrement()
        {
            float[] origArray = {1, 2};
            const float newValue = 3;

            float[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
        }

        [Test]
        public void DoubleIncrement()
        {
            double[] origArray = {1, 2};
            const double newValue = 3;

            double[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
        }

        [Test]
        public void LongIncrement()
        {
            long[] origArray = {1, 2};
            const long newValue = 3;

            long[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
        }

        [Test]
        public void ObjectIncrement()
        {
            object[] origArray = {"1", "2"};
            object newValue = "3";

            object[] newArray = origArray.Increment();
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue((string) newArray[0] == "1");
            Assert.IsTrue((string) newArray[1] == "2");
            Assert.IsTrue((string) newArray[2] == "3");
        }

        [Test]
        public void StringResizeByOne()
        {
            string[] origArray = {"test1.jpg", "test2.jpg"};
            const string newValue = "test3.jpg";

            string[] newArray = origArray.Resize(3);
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "test1.jpg");
            Assert.IsTrue(newArray[1] == "test2.jpg");
            Assert.IsTrue(newArray[2] == "test3.jpg");
        }

        [Test]
        public void StringResizeByTwo()
        {
            string[] origArray = {"test1.jpg", "test2.jpg"};
            const string newValue = "test3.jpg";

            string[] newArray = origArray.Resize(4);
            newArray[newArray.Length - 2] = newValue;
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "test1.jpg");
            Assert.IsTrue(newArray[1] == "test2.jpg");
            Assert.IsTrue(newArray[2] == "test3.jpg");
            Assert.IsTrue(newArray[3] == "test3.jpg");
        }

        [Test]
        public void IntegerResizeByOne()
        {
            int[] origArray = {1, 2};
            const int newValue = 3;

            int[] newArray = origArray.Resize(3);
            newArray[newArray.Length - 1] = newValue;

            Assert.IsTrue(newArray.Length == 3, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
        }

        [Test]
        public void IntegerResizeByNegativeOne()
        {
            int[] origArray = {1, 2, 3};
            int[] newArray = origArray.Resize(2);

            Assert.IsTrue(newArray.Length == 2, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
        }

        [Test]
        public void IntegerIncrementWithNewValue()
        {
            int[] origArray = {1, 2, 3};
            int[] newArray = origArray.Increment(5);

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1);
            Assert.IsTrue(newArray[1] == 2);
            Assert.IsTrue(newArray[2] == 3);
            Assert.IsTrue(newArray[3] == 5);
        }

        [Test]
        public void StringIncrementWithNewValue()
        {
            string[] origArray = {"1", "2", "3"};
            string[] newArray = origArray.Increment("5");

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "1");
            Assert.IsTrue(newArray[1] == "2");
            Assert.IsTrue(newArray[2] == "3");
            Assert.IsTrue(newArray[3] == "5");
        }

        [Test]
        public void FloatIncrementWithNewValue()
        {
            float[] origArray = {1f, 2f, 3f};
            float[] newArray = origArray.Increment(5f);

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == 1f);
            Assert.IsTrue(newArray[1] == 2f);
            Assert.IsTrue(newArray[2] == 3f);
            Assert.IsTrue(newArray[3] == 5f);
        }

        [Test]
        public void ObjectIncrementWithNewValue()
        {
            object[] origArray = {1f, "2f", 3f};
            object[] newArray = origArray.Increment("5f");

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue((float) newArray[0] == 1f);
            Assert.IsTrue((string) newArray[1] == "2f");
            Assert.IsTrue((float) newArray[2] == 3f);
            Assert.IsTrue((string) newArray[3] == "5f");
        }

        [Test]
        public void DateTimeIncrementWithNewValue()
        {
            DateTime[] origArray = {DateTime.MinValue, DateTime.MinValue, DateTime.MinValue};
            DateTime[] newArray = origArray.Increment(DateTime.MaxValue);

            Assert.IsTrue(newArray.Length == 4, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == DateTime.MinValue);
            Assert.IsTrue(newArray[1] == DateTime.MinValue);
            Assert.IsTrue(newArray[2] == DateTime.MinValue);
            Assert.IsTrue(newArray[3] == DateTime.MaxValue);
        }

        [Test]
        public void ArrayJoin()
        {
            string[] origArray1 = {"1", "2", "3"};
            string[] origArray2 = {"4", "5", "6"};
            string[] newArray = origArray1.Join(origArray2);

            Assert.IsTrue(newArray.Length == 6, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "1");
            Assert.IsTrue(newArray[1] == "2");
            Assert.IsTrue(newArray[2] == "3");
            Assert.IsTrue(newArray[3] == "4");
            Assert.IsTrue(newArray[4] == "5");
            Assert.IsTrue(newArray[5] == "6");
        }

        [Test]
        public void ArrayAndListJoin()
        {
            string[] origArray1 = {"1", "2", "3"};
            List<string> origArray2 = new List<string> {"4", "5", "6"};
            string[] newArray = origArray1.Join(origArray2);

            Assert.IsTrue(newArray.Length == 6, "Array is not the expected length");
            Assert.IsTrue(newArray[0] == "1");
            Assert.IsTrue(newArray[1] == "2");
            Assert.IsTrue(newArray[2] == "3");
            Assert.IsTrue(newArray[3] == "4");
            Assert.IsTrue(newArray[4] == "5");
            Assert.IsTrue(newArray[5] == "6");
        }

        [Test]
        public void ArraySegmentNoLength()
        {
            string[] original = {"0", "1", "2", "3", "4", "5"};
            string[] segment = original.Segment(1);

            Assert.AreEqual(5, segment.Length);
            Assert.AreEqual("1", segment[0]);
            Assert.AreEqual("2", segment[1]);
            Assert.AreEqual("3", segment[2]);
            Assert.AreEqual("4", segment[3]);
            Assert.AreEqual("5", segment[4]);
        }

        [Test]
        public void ArraySegmentWithLength()
        {
            string[] original = {"0", "1", "2", "3", "4", "5"};
            string[] segment = original.Segment(1, 3);

            Assert.AreEqual(3, segment.Length);
            Assert.AreEqual("1", segment[0]);
            Assert.AreEqual("2", segment[1]);
            Assert.AreEqual("3", segment[2]);
        }

        [Test]
        public void RemoveDuplicates()
        {
            string[] original = {"0", "1", "1", "2", "3", "3", "4", "5"};
            string[] expected = {"0", "1", "2", "3", "4", "5"};
            string[] actual = original.RemoveDuplicates();

            Assert.IsNotNull(actual);
            Assert.That(expected, Is.EquivalentTo(actual));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringResizeWithNull()
        {
            string[] origArray = null;
            origArray.Resize(2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StringResizeWithNegativeNewSize()
        {
            string[] origArray = {"1", "2", "3"};
            origArray.Resize(-2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringIncrementWithNull()
        {
            string[] origArray = null;
            origArray.Increment();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringJoinWithNull()
        {
            string[] badArray = null;
            string[] goodArray = {"1", "2", "3"};
            badArray.Join(goodArray);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringJoinWithNull2()
        {
            string[] badArray = null;
            string[] goodArray = {"1", "2", "3"};
            goodArray.Join(badArray);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringJoinWithNull3()
        {
            string[] badArray = null;
            string[] goodArray = null;
            badArray.Join(goodArray);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DateTimeIncrementWithNull()
        {
            DateTime[] origArray = null;
            origArray.Increment();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DateTimeRemoveDuplicatesWithNull()
        {
            DateTime[] origArray = null;
            origArray.RemoveDuplicates();
        }
    }
}