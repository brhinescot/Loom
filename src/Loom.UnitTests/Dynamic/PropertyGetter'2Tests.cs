#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Dynamic
{
    [TestFixture]
    public class PropertyGetter_2Tests
    {
        [TestCase("PublicName", "SomePublicName")]
        [TestCase("Name", "SomeTestName")]
        public void PropertyGetterStringTest(string propertyName, object expected)
        {
            PropertyGetter<TestClass, string> getString = DynamicType<TestClass>.CreatePropertyGetter<string>(propertyName);

            TestClass obj = new TestClass();
            Assert.AreEqual(expected, getString(obj));
        }

        [TestCase("PublicCount", 100)]
        [TestCase("Count", 100)]
        public void PropertyGetterIntTest(string propertyName, object expected)
        {
            PropertyGetter<TestClass, int> getInt32 = DynamicType<TestClass>.CreatePropertyGetter<int>(propertyName);

            TestClass obj = new TestClass();
            Assert.AreEqual(expected, getInt32(obj));
        }

        [TestCase("PublicRefType")]
        [TestCase("RefType")]
        public void PropertyGetterObjectTest(string propertyName)
        {
            PropertyGetter<TestClass, object> getInt32 = DynamicType<TestClass>.CreatePropertyGetter<object>(propertyName);

            TestClass obj = new TestClass();
            Assert.IsTrue(getInt32(obj).GetType() == typeof(object));
        }

        [TestCase("PublicRefType")]
        [TestCase("RefType")]
        public void ValueTypePropertyGetterObjectTest(string propertyName)
        {
            PropertyGetter<TestStruct, object> getInt32 = DynamicType<TestStruct>.CreatePropertyGetter<object>(propertyName);

            TestStruct obj = TestStruct.Default;
            Assert.IsTrue(getInt32(obj).GetType() == typeof(object));
        }

        [TestCase("PublicCount", 100)]
        [TestCase("Count", 100)]
        public void ValueTypePropertyGetterIntTest(string propertyName, object expected)
        {
            PropertyGetter<TestStruct, int> getInt32 = DynamicType<TestStruct>.CreatePropertyGetter<int>(propertyName);

            TestStruct obj = TestStruct.Default;
            Assert.AreEqual(expected, getInt32(obj));
        }

        [TestCase("PublicName", "SomePublicName")]
        [TestCase("Name", "SomeTestName")]
        public void ValueTypePropertyGetterStringTest(string propertyName, object expected)
        {
            PropertyGetter<TestStruct, string> getInt32 = DynamicType<TestStruct>.CreatePropertyGetter<string>(propertyName);

            TestStruct obj = TestStruct.Default;
            Assert.AreEqual(expected, getInt32(obj));
        }

        [Test]
        public void MultiplePropertyGetterFromSameInstance()
        {
            PropertyGetter<TestClass, int> getPublicCount = DynamicType<TestClass>.CreatePropertyGetter<int>("PublicCount");
            PropertyGetter<TestClass, int> getCount = DynamicType<TestClass>.CreatePropertyGetter<int>("Count");
            PropertyGetter<TestClass, string> getPublicName = DynamicType<TestClass>.CreatePropertyGetter<string>("PublicName");
            PropertyGetter<TestClass, string> getName = DynamicType<TestClass>.CreatePropertyGetter<string>("Name");

            TestClass testClass = new TestClass();
            Assert.AreEqual(100, getPublicCount(testClass));
            Assert.AreEqual(100, getCount(testClass));
            Assert.AreEqual("SomePublicName", getPublicName(testClass));
            Assert.AreEqual("SomeTestName", getName(testClass));
        }

        [TestCase(6025551212, "CountryCode", "0")]
        [TestCase(6025551212, "AreaCode", "602")]
        [TestCase(6025551212, "Exchange", "555")]
        [TestCase(6025551212, "Number", "1212")]
        public void PhoneNumberGetterAsString(long phoneNumber, string propertyName, string expectedValue)
        {
            FormattablePropertyGetter<PhoneNumber> getString = DynamicType<PhoneNumber>.CreateFormattablePropertyGetter(propertyName);

            PhoneNumber number = phoneNumber;
            Assert.AreEqual(expectedValue, getString(number, null));
        }

        [TestCase("Count", "100")]
        [TestCase("PublicCount", "100")]
        [TestCase("Name", "SomeTestName")]
        [TestCase("PublicName", "SomePublicName")]
        [TestCase("Phone", "(602) 555-1212")]
        [TestCase("PublicPhone", "(602) 555-1212")]
        [TestCase("RefType", "System.Object")]
        [TestCase("PublicRefType", "System.Object")]
        public void ReferenceTypePropertyGetterAsString(string propertyName, object expected)
        {
            FormattablePropertyGetter<TestClass> getString = DynamicType<TestClass>.CreateFormattablePropertyGetter(propertyName);

            TestClass obj = new TestClass();
            Assert.AreEqual(expected, getString(obj, null));
        }

        [TestCase("Count", "100")]
        [TestCase("PublicCount", "100")]
        [TestCase("Name", "SomeTestName")]
        [TestCase("PublicName", "SomePublicName")]
        [TestCase("Phone", "(602) 555-1212")]
        [TestCase("PublicPhone", "(602) 555-1212")]
        [TestCase("RefType", "System.Object")]
        [TestCase("PublicRefType", "System.Object")]
        public void ValueTypePropertyGetterAsString(string propertyName, object expected)
        {
            FormattablePropertyGetter<TestStruct> getString = DynamicType<TestStruct>.CreateFormattablePropertyGetter(propertyName);

            TestStruct testStruct = TestStruct.Default;
            Assert.AreEqual(expected, getString(testStruct, null));
        }

        [TestCase("Count", "0000", "0100")]
        [TestCase("PublicCount", "0000", "0100")]
        [TestCase("Name", null, "SomeTestName")]
        [TestCase("PublicName", null, "SomePublicName")]
        [TestCase("Phone", "{{Exchange}}.{{Number}}", "555.1212")]
        [TestCase("PublicPhone", "{{Exchange}}-{{Number}}", "555-1212")]
        public void ReferenceTypePropertyGetterAsFormattedString(string propertyName, string format, object expected)
        {
            FormattablePropertyGetter<TestClass> getString = DynamicType<TestClass>.CreateFormattablePropertyGetter(propertyName);

            TestClass obj = new TestClass();
            Assert.AreEqual(expected, getString(obj, format));
        }

        [TestCase("Count", 100)]
        [TestCase("PublicCount", 100)]
        [TestCase("Name", "SomeTestName")]
        [TestCase("PublicName", "SomePublicName")]
        public void ReferenceTypePropertyGetterAsObject(string propertyName, object expected)
        {
            PropertyGetter<TestClass, object> getObject = DynamicType<TestClass>.CreatePropertyGetterAsObject(propertyName);

            TestClass obj = new TestClass();
            Assert.AreEqual(expected, getObject(obj));
        }

        [TestCase("Count", 100)]
        [TestCase("PublicCount", 100)]
        [TestCase("Name", "SomeTestName")]
        [TestCase("PublicName", "SomePublicName")]
        public void ValueTypePropertyGetterAsObject(string propertyName, object expected)
        {
            PropertyGetter<TestStruct, object> getObject = DynamicType<TestStruct>.CreatePropertyGetterAsObject(propertyName);

            TestStruct testStruct = TestStruct.Default;
            Assert.AreEqual(expected, getObject(testStruct));
        }

        [Test]
        public void ReferenceTypePropertyGetterGetHashCode()
        {
            TestClass obj = new TestClass();

            PropertyGetter<TestClass, int> getHashCode = DynamicType<TestClass>.CreatePropertyGetterGetHashCode("PublicPhone");
            Assert.AreEqual(obj.PublicPhone.GetHashCode(), getHashCode(obj));
        }

        [Test]
        public void ValueTypePropertyGetterGetHashCode()
        {
            PropertyGetter<TestStruct, int> getHashCode = DynamicType<TestStruct>.CreatePropertyGetterGetHashCode("PublicPhone");

            TestStruct testStruct = TestStruct.Default;
            Assert.AreEqual(testStruct.PublicPhone.GetHashCode(), getHashCode(testStruct));
        }

        [Test]
        public void ReferenceTypeCreateAllGetters()
        {
            PropertyGetter<TestClass, object>[] getters = DynamicType<TestClass>.CreateAllPropertyGetters();

            TestClass obj = new TestClass();
            obj.Name = "AllName";
            obj.Count = 100;
            obj.Phone = 5555551212;

            Assert.AreEqual(9, getters.Length);
            for (int i = 0; i < 8; i++)
                TestPropertyGetter(getters[i], obj);
        }

        private void TestPropertyGetter(PropertyGetter<TestClass, object> getter, TestClass instance)
        {
            string name = getter.Method.Name;
            switch (name)
            {
                case "get_Name":
                    Assert.AreEqual(getter(instance), "AllName");
                    break;
                case "get_Count":
                    Assert.AreEqual(getter(instance), 100);
                    break;
                case "get_Phone":
                    Assert.AreEqual(getter(instance), (PhoneNumber) 5555551212);
                    break;
            }
        }

        [Test]
        public void Instantiate()
        {
            Func<object> del = DynamicType<TestClass>.CreateTypeConstructor();
            TestClass testClass = (TestClass) del();

            Assert.IsNotNull(testClass);
        }

        //[Test]
        //public void PropertyGetterStaticTest()
        //{
        //    PropertyGetter<TableData> getTable = DynamicType<Customer>.CreateStaticPropertyGetter<TableData>("Table");

        //    TableData t = getTable();

        //    Assert.AreEqual("Sales", t.Owner);
        //    Assert.AreEqual("Customer", t.Name);
        //}
    }
}