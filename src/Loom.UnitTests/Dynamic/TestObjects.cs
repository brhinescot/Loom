using System;                                

namespace Colossus.Framework.Dynamic
{
    public struct TestStruct
    {
        #region Instance Fields

        private int count;
        private int? nullableInt;
        private string name;
        private PhoneNumber phone;
        private DateTime dateTime;
        public int PublicCount;
        public string PublicName;
        public PhoneNumber PublicPhone;
        public object PublicRefType;
        public DateTime PublicDataTime;

        private object refType;

        #endregion

        #region Property Accessors

        public string Name
        {
            get { return name; }
        }

        public int Count
        {
            get { return count; }
        }

        public PhoneNumber Phone
        {
            get { return phone; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
        }

        public object RefType
        {
            get { return refType; }
        }

        public static TestStruct Default
        {
            get
            {
                TestStruct testStruct = new TestStruct();
                testStruct.name = "SomeTestName";
                testStruct.count = 100;
                testStruct.phone = 6025551212;
                testStruct.refType = new object();
                testStruct.nullableInt = 1;

                testStruct.PublicName = "SomePublicName";
                testStruct.PublicCount = 100;
                testStruct.PublicPhone = 6025551212;
                testStruct.PublicRefType = new object();

                return testStruct;
            }
        }

        public static TestStruct NullName
        {
            get
            {
                TestStruct testStruct = Default;
                testStruct.name = null;

                return testStruct;
            }
        }

        public int? NullableInt
        {
            get { return nullableInt; }
        }

        public static TestStruct SetCount(int count)
        {
            TestStruct testStruct = Default;
            testStruct.count = count;

            return testStruct;
        }

        public static TestStruct SetDateTime(DateTime dateTime)
        {
            TestStruct testStruct = Default;
            testStruct.dateTime = dateTime;

            return testStruct;
        }

        public static TestStruct SetName(string name)
        {
            TestStruct testStruct = Default;
            testStruct.name = name;

            return testStruct;
        }

        #endregion

        #region IL Helpers

        public static string get_CountAsString(TestStruct testStruct)
        {
            return testStruct.Count.ToString();
        }

        public static int get_Count(TestStruct testStruct)
        {
            return testStruct.Count;
        }         

        public static int? get_NullableInt(TestStruct testStruct)
        {
            return testStruct.NullableInt;
        }

        public static string get_Name(TestStruct testStruct)
        {
            return testStruct.Name;
        }

        public static string get_CountryCode(PhoneNumber number)
        {
            return number.CountryCode.ToString();
        }

        public static string get_RefTypeAsString(TestStruct testStruct)
        {
            return testStruct.RefType.ToString();
        }

        public static object get_RefType(TestStruct testStruct)
        {
            return testStruct.RefType;
        }

        #endregion
    }

    public class TestClass
    {
        #region Instance Fields

        private readonly object refType;
        private System.Net.AuthenticationSchemes authenticationSchemes;
                                                                                                             

        #endregion

        public int PublicCount = 100;
        public string PublicName = "SomePublicName";
        public PhoneNumber PublicPhone = 6025551212;
        public object PublicRefType = new object();
        public object NullField;
        public System.Net.AuthenticationSchemes PublicAuthenticationSchemes;

        #region Property Accessors
              
        /// <summary>
        /// Default value is "SomeTestName".
        /// </summary>
        public string Name
        {
            get { return ReadOnlyName; }
            set { ReadOnlyName = value; }
        }

        /// <summary>
        /// Default value is 100.
        /// </summary>
        public int Count { get; set; }

        public PhoneNumber Phone { get; set; }

        public object RefType
        {
            get { return refType; }
        }

        public object NullProperty
        {
            get { return null; }
        }

        public int? NullableInt { get; set; }

        public DateTime DateTime { get; set; }

        public System.Net.AuthenticationSchemes AuthenticationSchemes
        {
            get { return authenticationSchemes; }
            set { authenticationSchemes = value; }
        }

        /// <summary>
        /// Default value is "SomeTestName".
        /// </summary>
        public string ReadOnlyName { get; private set; }

        #endregion

        #region .ctor

        public TestClass()
        {
            ReadOnlyName = "SomeTestName";
            Count = 100;
            Phone = 6025551212;
            refType = new object();
        }

        public TestClass(string name, int count)
        {
            ReadOnlyName = name;
            Count = count;
            Phone = 6025551212;
            refType = new object();
        }

        #endregion

        #region IL Helpers

        public System.Net.AuthenticationSchemes get_AuthenticationSchemes(TestClass testClass)
        {
            return testClass.authenticationSchemes;
        }

        public void set_AuthenticationSchemes(TestClass testClass, System.Net.AuthenticationSchemes value)
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
            testClass.AuthenticationSchemes = (System.Net.AuthenticationSchemes)(short)value;
//                (System.Net.AuthenticationSchemes)Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(System.Net.AuthenticationSchemes)));
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
            testClass.Name = (string)value;
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

        #endregion
    }
}
