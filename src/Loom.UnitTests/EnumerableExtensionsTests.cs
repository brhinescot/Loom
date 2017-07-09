#region Using Directives

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void GetElementTypeFromInterfaceDeclaration()
        {
            IEnumerable<ITestClass> test = new List<TestClass> {new TestClass(), new TestClass(), new TestClass()};

            Type elementType = test.GetElementType();
            Assert.AreEqual(typeof(TestClass), elementType);
        }

        [Test]
        public void GetElementTypeFromBaseClassDeclaration()
        {
            IEnumerable<TestClass> test = new List<TestSubClass> {new TestSubClass(), new TestSubClass(), new TestSubClass()};

            Type elementType = test.GetElementType();
            Assert.AreEqual(typeof(TestSubClass), elementType);
        }

        [Test]
        public void GetElementTypeOfSubclassFromInterfaceDeclaration()
        {
            IEnumerable<ITestClass> test = new List<TestSubClass> {new TestSubClass(), new TestSubClass(), new TestSubClass()};

            Type elementType = test.GetElementType();
            Assert.AreEqual(typeof(TestSubClass), elementType);
        }

        [Test]
        public void GetElementTypeOfSubclassFromListOfBaseClassAndInterfaceDeclaration()
        {
            IEnumerable<ITestClass> test = new List<TestClass> {new TestSubClass(), new TestSubClass(), new TestSubClass()};

            Type elementType = test.GetElementType();
            Assert.AreEqual(typeof(TestClass), elementType);
        }

        #region Nested type: ITestClass

        private interface ITestClass { }

        #endregion

        #region Nested type: TestClass

        private class TestClass : ITestClass { }

        #endregion

        #region Nested type: TestSubClass

        private class TestSubClass : TestClass { }

        #endregion
    }
}