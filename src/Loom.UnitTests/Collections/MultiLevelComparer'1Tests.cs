#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Loom.Diagnostics;
using Loom.Security;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class MultiLevelComparerTest
    {
        private List<TestClass> dupClasses;
        private List<TestClass> list;

        [SetUp]
        public void Setup()
        {
            list = new List<TestClass>();
            list.Add(new TestClass(1, "BUser", new DateTime(2006, 6, 26)));
            list.Add(new TestClass(2, "ZUser", new DateTime(2004, 5, 11)));
            list.Add(new TestClass(3, "JUser", new DateTime(2005, 2, 2)));

            dupClasses = new List<TestClass>();
            dupClasses.Add(new TestClass(1, "ZUser", new DateTime(2004, 5, 11)));
            dupClasses.Add(new TestClass(1, "JUser", new DateTime(2005, 2, 2)));
            dupClasses.Add(new TestClass(2, "BUser", new DateTime(2006, 6, 26)));
        }

        [TestCase(SortDirection.Ascending, "Name", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, "Name", "ZUser", "JUser", "BUser")]
        [TestCase(SortDirection.Ascending, "Id", "BUser", "ZUser", "JUser")]
        [TestCase(SortDirection.Descending, "Id", "JUser", "ZUser", "BUser")]
        [TestCase(SortDirection.Ascending, "LastOn", "ZUser", "JUser", "BUser")]
        [TestCase(SortDirection.Descending, "LastOn", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, null, "BUser", "JUser", "ZUser", ExpectedException = typeof(ArgumentNullException))]
        public void SortByAddColumn(SortDirection direction, string column, string expected0, string expected1, string expected2)
        {
            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();
            comparer.AddColumn(column, direction);
            list.Sort(comparer);

            Assert.AreEqual(expected0, list[0].Name);
            Assert.AreEqual(expected1, list[1].Name);
            Assert.AreEqual(expected2, list[2].Name);
        }

        [TestCase("Name", "BUser", "JUser", "ZUser")]
        [TestCase("Id", "BUser", "ZUser", "JUser")]
        [TestCase("LastOn", "ZUser", "JUser", "BUser")]
        [TestCase(null, "BUser", "JUser", "ZUser", ExpectedException = typeof(ArgumentNullException))]
        public void SortByAddColumnNoDirection(string column, string expected0, string expected1, string expected2)
        {
            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();
            comparer.AddColumn(column);
            list.Sort(comparer);

            Assert.AreEqual(expected0, list[0].Name);
            Assert.AreEqual(expected1, list[1].Name);
            Assert.AreEqual(expected2, list[2].Name);
        }

        [TestCase(SortDirection.Ascending, "Name", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, "Name", "ZUser", "JUser", "BUser")]
        [TestCase(SortDirection.Ascending, "Id", "BUser", "ZUser", "JUser")]
        [TestCase(SortDirection.Descending, "Id", "JUser", "ZUser", "BUser")]
        [TestCase(SortDirection.Ascending, "LastOn", "ZUser", "JUser", "BUser")]
        [TestCase(SortDirection.Descending, "LastOn", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, null, "BUser", "JUser", "ZUser", ExpectedException = typeof(ArgumentNullException))]
        public void SortByConstructor(SortDirection direction, string column, string expected0, string expected1, string expected2)
        {
            list.Sort(new MultiLevelComparer<TestClass>(column, direction));

            Assert.AreEqual(expected0, list[0].Name);
            Assert.AreEqual(expected1, list[1].Name);
            Assert.AreEqual(expected2, list[2].Name);
        }

        [TestCase("Name", "BUser", "JUser", "ZUser")]
        [TestCase("Id", "BUser", "ZUser", "JUser")]
        [TestCase("LastOn", "ZUser", "JUser", "BUser")]
        [TestCase(null, "BUser", "JUser", "ZUser", ExpectedException = typeof(ArgumentNullException))]
        public void SortByConstructorNoDirection(string column, string expected0, string expected1, string expected2)
        {
            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>(column);
            list.Sort(comparer);

            Assert.AreEqual(expected0, list[0].Name);
            Assert.AreEqual(expected1, list[1].Name);
            Assert.AreEqual(expected2, list[2].Name);
        }

        [Test]
        public void SortNoSortColumn()
        {
            list = new List<TestClass>
            {
                new TestClass(2, "ZUser", new DateTime(2004, 5, 11)),
                new TestClass(1, "BUser", new DateTime(2006, 6, 26)),
                new TestClass(3, "JUser", new DateTime(2005, 2, 2))
            };

            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();

            list.Sort(comparer);
            Assert.AreEqual("ZUser", list[0].Name);
            Assert.AreEqual("BUser", list[1].Name);
            Assert.AreEqual("JUser", list[2].Name);
        }

        [Test]
        public void SortNonComparable()
        {
            List<object> nonCompList = new List<object>();
            nonCompList.Add(new object());
            nonCompList.Add(new object());
            nonCompList.Add(new object());

            MultiLevelComparer<object> comparer = new MultiLevelComparer<object>();

            nonCompList.Sort(comparer);
        }

        [TestCase(SortDirection.Ascending, "Name", "Id", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, "Name", "Id", "ZUser", "JUser", "BUser")]
        [TestCase(SortDirection.Ascending, "Id", "Name", "JUser", "ZUser", "BUser")]
        [TestCase(SortDirection.Descending, "Id", "Name", "BUser", "ZUser", "JUser")]
        public void SortMultipleColumnsWithDuplicateValues(SortDirection direction, string column1, string column2, string expected0, string expected1, string expected2)
        {
            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();
            comparer.AddColumn(column1, direction);
            comparer.AddColumn(column2, direction);
            dupClasses.Sort(comparer);

            Assert.AreEqual(expected0, dupClasses[0].Name);
            Assert.AreEqual(expected1, dupClasses[1].Name);
            Assert.AreEqual(expected2, dupClasses[2].Name);
        }

        [TestCase(SortDirection.Ascending, SortDirection.Descending, "Name", "Id", "BUser", "JUser", "ZUser")]
        [TestCase(SortDirection.Descending, SortDirection.Ascending, "Name", "Id", "ZUser", "JUser", "BUser")]
        public void MultipleColumnsWithDuplicateValuesMixedDirection(SortDirection direction1, SortDirection direction2, string column1, string column2, string expected0, string expected1, string expected2)
        {
            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();
            comparer.AddColumn(column1, direction1);
            comparer.AddColumn(column2, direction2);
            dupClasses.Sort(comparer);

            Assert.AreEqual(expected0, dupClasses[0].Name);
            Assert.AreEqual(expected1, dupClasses[1].Name);
            Assert.AreEqual(expected2, dupClasses[2].Name);
        }

        public void SpeedTest()
        {
            List<TestClass> bigList = new List<TestClass>();
            CryptoRandom random = new CryptoRandom("{A:10}");
            for (int i = 0; i < 100000; i++)
                bigList.Add(new TestClass(i, random.Generate(), DateTime.Now));

            CodeTimer timer = CodeTimer.Start();

            MultiLevelComparer<TestClass> comparer = new MultiLevelComparer<TestClass>();
            comparer.AddColumn("Id", SortDirection.Ascending);
            comparer.AddColumn("Name", SortDirection.Descending);
            bigList.Sort(comparer);

            Console.WriteLine("Sorted in {0} ms", CodeTimer.Stop(timer).TotalMilliseconds);
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