#region Using Directives

using System;
using System.Web.Script.Serialization;
using Loom.Diagnostics;
using NUnit.Framework;

#endregion

// ReSharper disable RedundantJumpStatement
// ReSharper disable ValueParameterNotUsed
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedVariable
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local

namespace Loom.Web.UI
{
    [TestFixture]
    public class JsonSerializerTests
    {
        [Test]
        public void SpeedTest()
        {
            TestClass test = new TestClass();
            test.Name = "Jason Zander";
            test.Age = 30;
            test.Employeed = false;
            test.Time = new DateTime(2009, 1, 1);

            const int count = 1000;
            JsonSerializer.Serialize(test);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.Serialize(test);

            CodeTimer timer = CodeTimer.Start();
            for (int i = 0; i < count; i++)
            {
                string s = serializer.Serialize(test);
            }
            CodeTimer.WriteMilliseconds(timer);

            timer = CodeTimer.Start();

            for (int i = 0; i < count; i++)
            {
                string s = JsonSerializer.Serialize(test);
            }
            CodeTimer.WriteMilliseconds(timer);
        }

        [Test]
        public void SerializeClass()
        {
            TestClass test = new TestClass();
            test.Name = "Jason Zander";
            test.Age = 30;
            test.Employeed = false;
            test.Time = new DateTime(2009, 1, 1);

            const string expected = "{'Name':'Jason Zander','Age':30,'Employeed':false,'Time':new Date(1230768000000)}";
            string actual = JsonSerializer.Serialize(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializeClassWithArrayProperty()
        {
            TestClass test = new TestClass();
            test.Name = "Jason Zander";
            test.Age = 30;
            test.Employeed = false;
            test.Time = new DateTime(2009, 1, 1);
            test.Items = new[] {"Item1", "Item2", "Item3", "Item4"};

            const string expected = "{'Name':'Jason Zander','Age':30,'Employeed':false,'Time':new Date(1230768000000),'Items':['Item1','Item2','Item3','Item4']}";
            string actual = JsonSerializer.Serialize(test);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializeAnonymousClass()
        {
            const string expected = "{'Name':'Jason Zander','Age':30,'Employeed':false,'Time':new Date(1230768000000)}";
            string actual = JsonSerializer.Serialize(new {Name = "Jason Zander", Age = 30, Employeed = false, Time = new DateTime(2009, 1, 1)});

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SerializeArrayProperty()
        {
            const string expected = "{'Sizes':['Small','Medium','Large']}";
            string actual = JsonSerializer.Serialize(new {Sizes = new[] {"Small", "Medium", "Large"}});

            Assert.AreEqual(expected, actual);
        }

        #region Nested type: TestClass

        private sealed class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public bool Employeed { get; set; }
            public DateTime Time { get; set; }
            public string[] Items { get; set; }

            public string this[int index]
            {
                get => string.Empty;
                set { return; }
            }
        }

        #endregion
    }
}