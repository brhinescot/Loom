#region Using Directives

using System;
using System.Diagnostics;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class AutoKeyDictionaryTests
    {
        [Test]
        public void TEST()
        {
            AutoKeyDictionary<TestClass> dictionary = new AutoKeyDictionary<TestClass>("Id", "Name");
        }

        #region Nested type: TestClass

        [DebuggerDisplay("Name={Name}, Id={Id}, LastOn={LastOn}")]
        public class TestClass
        {
            public TestClass() { }

            public TestClass(int id, string name, DateTime lastOn)
            {
                Id = id;
                Name = name;
                LastOn = lastOn;
            }

            public int Id { get; }

            public string Name { get; }

            public DateTime LastOn { get; }
        }

        #endregion
    }
}