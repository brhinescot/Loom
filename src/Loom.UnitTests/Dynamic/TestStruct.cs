#region Using Directives

using System;

#endregion

namespace Loom.Dynamic
{
    // ReSharper disable InconsistentNaming    
    public struct TestStruct
    {
        #region Instance Fields

        public int PublicCount;
        public string PublicName;
        public PhoneNumber PublicPhone;
        public object PublicRefType;
        public DateTime PublicDataTime;

        #endregion

        #region Property Accessors

        public string Name { get; private set; }

        public int Count { get; private set; }

        public PhoneNumber Phone { get; private set; }

        public DateTime DateTime { get; private set; }

        public object RefType { get; private set; }

        public static TestStruct Default
        {
            get
            {
                TestStruct testStruct = new TestStruct();
                testStruct.Name = "SomeTestName";
                testStruct.Count = 100;
                testStruct.Phone = 6025551212;
                testStruct.RefType = new object();
                testStruct.NullableInt = 1;

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
                testStruct.Name = null;

                return testStruct;
            }
        }

        public int? NullableInt { get; set; }

        public static TestStruct SetCount(int count)
        {
            TestStruct testStruct = Default;
            testStruct.Count = count;

            return testStruct;
        }

        public static TestStruct SetDateTime(DateTime dateTime)
        {
            TestStruct testStruct = Default;
            testStruct.DateTime = dateTime;

            return testStruct;
        }

        public static TestStruct SetName(string name)
        {
            TestStruct testStruct = Default;
            testStruct.Name = name;

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

        public static int CompareNullableInt(TestStruct x, TestStruct y)
        {
            return Nullable.Compare(x.NullableInt, y.NullableInt);
        }

        #endregion
    }
    // ReSharper restore InconsistentNaming
}