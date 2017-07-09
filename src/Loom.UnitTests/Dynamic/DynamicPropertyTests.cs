#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class DynamicPropertyTests
    {
        [Test]
        public void GetAndSet()
        {
            TestClass testClass = new TestClass();

            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("Name");

            Assert.AreEqual("SomeTestName", nameProperty.InvokeGetterOn(testClass));
            nameProperty.InvokeSetterOn(testClass, "Colossus");
            Assert.AreEqual("Colossus", nameProperty.InvokeGetterOn(testClass));
        }

        [Test]
        public void GetAndSetObjectType()
        {
            TestClass testClass = new TestClass();

            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("Name");

            Assert.AreEqual("SomeTestName", nameProperty.InvokeGetterOn(testClass));
            nameProperty.InvokeSetterOn(testClass, "Colossus");
            Assert.AreEqual("Colossus", nameProperty.InvokeGetterOn(testClass));
        }

        [Test]
        public void Instantiation()
        {
            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("Name");

            Assert.AreEqual("Name", nameProperty.AttributeName);
            Assert.AreEqual(typeof(string), nameProperty.Type);
            Assert.IsTrue(nameProperty.HasSetter);
            Assert.IsTrue(nameProperty.HasGetter);
        }

        [Test]
        public void ReadOnlyInstantiation()
        {
            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("ReadOnlyName");

            Assert.AreEqual("ReadOnlyName", nameProperty.AttributeName);
            Assert.AreEqual(typeof(string), nameProperty.Type);
            Assert.IsFalse(nameProperty.HasSetter);
            Assert.IsTrue(nameProperty.HasGetter);
        }

        [Test]
        public void ReadOnlyGet()
        {
            TestClass testClass = new TestClass();
            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("ReadOnlyName");

            Assert.AreEqual("SomeTestName", nameProperty.InvokeGetterOn(testClass));
        }

        [Test]
        public void Set()
        {
            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("Name");

            TestClass testClass = new TestClass();

            Assert.AreEqual("SomeTestName", testClass.Name);
            nameProperty.InvokeSetterOn(testClass, "Colossus");
            Assert.AreEqual("Colossus", testClass.Name);
        }

        [Test]
        [ExpectedException(typeof(DynamicPropertyException))]
        public void RaiseExceptionReadOnlySet()
        {
            TestClass testClass = new TestClass();

            DynamicProperty<TestClass> nameProperty = DynamicType<TestClass>.CreateDynamicProperty("ReadOnlyName");

            Assert.AreEqual("SomeTestName", testClass.Name);
            nameProperty.InvokeSetterOn(testClass, "Colossus");
            Assert.AreEqual("Colossus", testClass.Name);
        }
    }
}