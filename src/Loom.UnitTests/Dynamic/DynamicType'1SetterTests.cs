#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class DynamicTypeSetterTests
    {
        [TestCase("Name", "Jason")]
        [TestCase("Name", "")]
        [TestCase("Name", null)]
        public void SetStringProperty(string propertyName, string propertyValue)
        {
            PropertySetter<TestClass, string> setNameProperty = DynamicType<TestClass>.CreatePropertySetter<string>(propertyName);

            TestClass testClass = new TestClass();
            setNameProperty(testClass, propertyValue);

            Assert.AreEqual(propertyValue, testClass.Name);
        }

        [TestCase("PublicName", "Jason")]
        [TestCase("PublicName", "")]
        [TestCase("PublicName", null)]
        public void SetStringField(string propertyName, string propertyValue)
        {
            PropertySetter<TestClass, string> setNameProperty = DynamicType<TestClass>.CreatePropertySetter<string>(propertyName);

            TestClass testClass = new TestClass();
            setNameProperty(testClass, propertyValue);

            Assert.AreEqual(propertyValue, testClass.PublicName);
        }

        [TestCase("Count", 1000)]
        [TestCase("Count", 0)]
        [TestCase("Count", -1000)]
        public void SetInt32Property(string propertyName, int propertyValue)
        {
            PropertySetter<TestClass, int> setCountProperty = DynamicType<TestClass>.CreatePropertySetter<int>(propertyName);

            TestClass testClass = new TestClass();
            setCountProperty(testClass, propertyValue);

            Assert.AreEqual(propertyValue, testClass.Count);
        }

        [TestCase("PublicCount", 1000)]
        [TestCase("PublicCount", 0)]
        [TestCase("PublicCount", -1000)]
        public void SetInt32Field(string propertyName, int propertyValue)
        {
            PropertySetter<TestClass, int> setCountProperty = DynamicType<TestClass>.CreatePropertySetter<int>(propertyName);

            TestClass testClass = new TestClass();
            setCountProperty(testClass, propertyValue);

            Assert.AreEqual(propertyValue, testClass.PublicCount);
        }

        [TestCase("Name", "Jason")]
        [TestCase("Name", "")]
        [TestCase("Name", null)]
        [TestCase("PublicName", "Jason")]
        [TestCase("PublicName", "")]
        [TestCase("PublicName", null)]
        [TestCase("Count", 1000)]
        [TestCase("Count", 0)]
        [TestCase("Count", -1000)]
        [TestCase("PublicCount", 1000)]
        [TestCase("PublicCount", 0)]
        [TestCase("PublicCount", -1000)]
        [TestCase("Count", "Invalid", ExpectedException = typeof(InvalidCastException))]
        public void SetPropertiesAsObject(string propertyName, object propertyValue)
        {
            PropertySetter<TestClass, object> propertySetter = DynamicType<TestClass>.CreatePropertySetterAsObject(propertyName);
            PropertyGetter<TestClass, object> propertyGetter = DynamicType<TestClass>.CreatePropertyGetterAsObject(propertyName);

            TestClass testClass = new TestClass();
            propertySetter(testClass, propertyValue);

            Assert.AreEqual(propertyValue, propertyGetter(testClass));
        }

        [Test]
        public void SetPhoneNumberPropertyAsObject()
        {
            PhoneNumber number = 8885551212;
            PropertySetter<TestClass, object> propertySetter = DynamicType<TestClass>.CreatePropertySetterAsObject("Phone");

            TestClass testClass = new TestClass();
            propertySetter(testClass, number);

            Assert.AreEqual(number, testClass.Phone);
        }
    }
}