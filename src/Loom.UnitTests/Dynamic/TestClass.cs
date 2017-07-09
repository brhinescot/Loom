#region Using Directives

using System;
using System.Net;

#endregion

namespace Loom.Dynamic
{
    public class TestClass
    {
        private AuthenticationSchemes authenticationSchemes;
        public object NullField;
        public AuthenticationSchemes PublicAuthenticationSchemes;

        public int PublicCount = 100;
        public string PublicName = "SomePublicName";
        public PhoneNumber PublicPhone = 6025551212;
        public object PublicRefType = new object();

        public TestClass()
        {
            ReadOnlyName = "SomeTestName";
            Count = 100;
            Phone = 6025551212;
            RefType = new object();
        }

        public TestClass(string name, int count)
        {
            ReadOnlyName = name;
            Count = count;
            Phone = 6025551212;
            RefType = new object();
        }

        /// <summary>
        ///     Default value is "SomeTestName".
        /// </summary>
        public string Name
        {
            get => ReadOnlyName;
            set => ReadOnlyName = value;
        }

        /// <summary>
        ///     Default value is 100.
        /// </summary>
        public int Count { get; set; }

        public PhoneNumber Phone { get; set; }

        public object RefType { get; }

        public object NullProperty => null;

        public int? NullableInt { get; set; }

        public DateTime DateTime { get; set; }

        public AuthenticationSchemes AuthenticationSchemes { get; set; }

        /// <summary>
        ///     Default value is "SomeTestName".
        /// </summary>
        public string ReadOnlyName { get; private set; }

        public AuthenticationSchemes get_AuthenticationSchemes(TestClass testClass)
        {
            return testClass.authenticationSchemes;
        }

        public void set_AuthenticationSchemes(TestClass testClass, AuthenticationSchemes value)
        {
            testClass.authenticationSchemes = value;
        }

        public object get_AuthenticationSchemesObject(TestClass testClass)
        {
            return testClass.AuthenticationSchemes;
        }

        public object get_CountObject(TestClass testClass)
        {
            return testClass.Count;
        }

        public void set_AuthenticationSchemesObject(TestClass testClass, object value)
        {
            testClass.AuthenticationSchemes = (AuthenticationSchemes) (short) value;
        }

        public string get_CountAsString(TestClass testClass, string format)
        {
            return testClass.Count.ToString(format);
        }

        public int get_Count(TestClass testClass)
        {
            return testClass.Count;
        }

        public void set_Count(TestClass testClass, int value)
        {
            testClass.Count = value;
        }

        public void set_CountObject(TestClass testClass, object value)
        {
            testClass.Count = (int) value;
        }

        public void set_NameObject(TestClass testClass, object value)
        {
            testClass.Name = (string) value;
        }

        public void set_PublicCount(TestClass testClass, int value)
        {
            testClass.PublicCount = value;
        }

        public string get_Name(TestClass testClass)
        {
            return testClass.Name;
        }

        public string get_PublicName(TestClass testClass)
        {
            return testClass.PublicName;
        }

        public string get_PublicCountAsString(TestClass testClass)
        {
            return testClass.PublicCount.ToString();
        }

        public object get_PublicCountAsObject(TestClass testClass)
        {
            return testClass.PublicCount;
        }

        public int get_PublicCount(TestClass testClass)
        {
            return testClass.PublicCount;
        }

        public string get_CountryCode(PhoneNumber number)
        {
            return number.CountryCode.ToString();
        }

        public static string get_RefTypeAsString(TestClass testClass)
        {
            if (testClass.PublicRefType == null)
                return null;
            return string.Empty;
        }

        public static object get_RefType(TestClass testClass)
        {
            return testClass.PublicRefType;
        }

        public static TestClass CreateInstance()
        {
            return new TestClass();
        }
    }
}