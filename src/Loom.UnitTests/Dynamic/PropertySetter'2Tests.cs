#region Using Directives

using System;
using System.Net;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class PropertySetter_2Tests
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
        [TestCase("AuthenticationSchemes", AuthenticationSchemes.Negotiate)]
        [TestCase("AuthenticationSchemes", AuthenticationSchemes.Anonymous)]
        [TestCase("Count", "Invalid", ExpectedException = typeof(InvalidCastException))]
        public void SetPropertiesAsObject(string propertyName, object propertyValue)
        {
            PropertySetter<TestClass, object> propertySetter = DynamicType<TestClass>.CreatePropertySetterAsObject(propertyName);
            PropertyGetter<TestClass, object> propertyGetter = DynamicType<TestClass>.CreatePropertyGetterAsObject(propertyName);

            TestClass testClass = new TestClass();
            propertySetter(testClass, propertyValue);

            Assert.AreEqual(propertyValue, propertyGetter(testClass));
        }

        [TestCase("ScrapReason", 2)]
        public void SetPropertiesAsObject2(string propertyName, object propertyValue)
        {
            PropertySetter<WorkOrder, object> propertySetter = DynamicType<WorkOrder>.CreatePropertySetterAsObject(propertyName);
            PropertyGetter<WorkOrder, object> propertyGetter = DynamicType<WorkOrder>.CreatePropertyGetterAsObject(propertyName);

            WorkOrder testClass = new WorkOrder();
            propertySetter(testClass, propertyValue);

            Assert.AreEqual((ScrapReason) propertyValue, propertyGetter(testClass));
        }

        [TestCase("ScrapReason", (short) 2)]
        public void SetPropertiesAsObject3(string propertyName, object propertyValue)
        {
            DynamicProperty<WorkOrder> property = DynamicType<WorkOrder>.CreateDynamicProperty(propertyName);

            WorkOrder testClass = new WorkOrder();
            property.InvokeSetterOn(testClass, propertyValue);

            Assert.AreEqual((ScrapReason) (short) propertyValue, property.InvokeGetterOn(testClass));
        }

        [Test]
        public void SetPhoneNumberPropertyAsObject()
        {
            PhoneNumber number = 8885551212;
            TestClass testClass = new TestClass();

            PropertySetter<TestClass, object> propertySetter = DynamicType<TestClass>.CreatePropertySetterAsObject("Phone");
            propertySetter(testClass, number);

            Assert.AreEqual(number, testClass.Phone);
        }

        [Test]
        public void SetEnumProperty()
        {
            short num = 2;
            TestClass testClass = new TestClass();
            testClass.set_AuthenticationSchemesObject(testClass, num);

            Assert.AreEqual((AuthenticationSchemes) num, testClass.AuthenticationSchemes);
        }
    }
}