#region Using Directives

#region Using Directives

using System;
using Loom.Collections;
using NUnit.Framework;

#endregion

// ReSharper disable UnusedVariable

#endregion

namespace Loom
{
    [TestFixture]
    public class DelimitedArrayTests
    {
        [Test]
        public void Test1()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 2);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(3, del.Count);
            Assert.AreEqual("2", del[0]);
            Assert.AreEqual("3", del[1]);
            Assert.AreEqual("4", del[2]);
        }

        [Test]
        public void Test1A()
        {
            string[] s = {"0", "1"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(1, del.Count);
            Assert.AreEqual("1", del[0]);
        }

        [Test]
        public void Test1B()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 1);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(1, del.Count);
            Assert.AreEqual("1", del[0]);
        }

        [Test]
        public void Test2()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 2);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(2, del.Count);
            Assert.AreEqual("1", del[0]);
            Assert.AreEqual("2", del[1]);
        }

        [Test]
        public void Test2A()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 3);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(3, del.Count);
            Assert.AreEqual("1", del[0]);
            Assert.AreEqual("2", del[1]);
            Assert.AreEqual("3", del[2]);
        }

        [Test]
        public void Test2B()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 4);

            // Make sure enumerating works and doesn't error.
            foreach (string s1 in del) { }

            Assert.AreEqual(4, del.Count);
            Assert.AreEqual("1", del[0]);
            Assert.AreEqual("2", del[1]);
            Assert.AreEqual("3", del[2]);
            Assert.AreEqual("4", del[3]);
        }

        [Test]
        public void ToArrayTest1()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 3);
            string[] newS = del.ToArray();
            Assert.AreEqual(3, newS.Length);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test3()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            // Should error since offset plus count would go one element beyond the end of the array.
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test3A()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 4);

            // Should error since there is no element at index 4 in a 4 element array.
            string single = del[4];
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test3B()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            DelimitedArray<string> del = new DelimitedArray<string>(s, 1, 3);

            // Should error since there is no element at index 3 in a 3 element array.
            string single = del[3];
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test3C()
        {
            string[] s = {"0", "1", "2", "3", "4"};
            // Should error since count would go one element beyond the end of the array.
            DelimitedArray<string> del = new DelimitedArray<string>(s, 4, 2);
        }
    }
}