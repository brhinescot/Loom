#region Using Directives

#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Loom.Collections;
using NUnit.Framework;

#endregion

// ReSharper disable UnusedAutoPropertyAccessor.Local

#endregion

namespace Loom
{
    [TestFixture]
    public class ListExtensionTests
    {
        private readonly List<string> paginateList = new List<string> {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

        [Test]
        public void SortByDateProperty()
        {
            List<Sortable> list = GetList();

            list.SortBy("Date");

            Assert.IsTrue(list[0].Name == "Zander");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Bill");
        }

        [Test]
        public void SortByDatePropertyDesc()
        {
            List<Sortable> list = GetList();

            list.SortBy("Date", SortDirection.Descending);

            Assert.IsTrue(list[0].Name == "Bill");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Zander");
        }

        [Test]
        public void SortByIntProperty()
        {
            List<Sortable> list = GetList();

            list.SortBy("Age");

            Assert.IsTrue(list[0].Name == "Ted");
            Assert.IsTrue(list[1].Name == "Bill");
            Assert.IsTrue(list[2].Name == "Zander");
        }

        [Test]
        public void SortByIntPropertyDesc()
        {
            List<Sortable> list = GetList();

            list.SortBy("Age", SortDirection.Descending);

            Assert.IsTrue(list[0].Name == "Zander");
            Assert.IsTrue(list[1].Name == "Bill");
            Assert.IsTrue(list[2].Name == "Ted");
        }

        [Test]
        public void SortByStringProperty()
        {
            List<Sortable> list = GetList();

            list.SortBy("Name");

            Assert.IsTrue(list[0].Name == "Bill");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Zander");
        }

        [Test]
        public void SortByStringPropertyDesc()
        {
            List<Sortable> list = GetList();

            list.SortBy("Name", SortDirection.Descending);

            Assert.IsTrue(list[0].Name == "Zander");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Bill");
        }

        [Test]
        public void SortByStringAndIntProperty()
        {
            List<Sortable> list = GetList();

            list.SortBy("Name", "Age");

            Assert.IsTrue(list[0].Name == "Bill");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Zander");
        }

        [Test]
        public void SortByStringIntAndDateProperty()
        {
            List<Sortable> list = GetList();

            list.SortBy("Name", "Age", "Date");

            Assert.IsTrue(list[0].Name == "Bill");
            Assert.IsTrue(list[1].Name == "Ted");
            Assert.IsTrue(list[2].Name == "Zander");
        }

        [Test]
        public void BackwardPage()
        {
            List<string> result = paginateList.CirclePaginate(3, -11).ToList();

            Assert.AreEqual(11, result.Count);
            Assert.AreEqual("4", result[0]);
            Assert.AreEqual("3", result[1]);
            Assert.AreEqual("2", result[2]);
            Assert.AreEqual("1", result[3]);
            Assert.AreEqual("10", result[4]);
            Assert.AreEqual("9", result[5]);
            Assert.AreEqual("8", result[6]);
            Assert.AreEqual("7", result[7]);
            Assert.AreEqual("6", result[8]);
            Assert.AreEqual("5", result[9]);
            Assert.AreEqual("4", result[10]);
        }

        [Test]
        public void ForwardPage()
        {
            List<string> result = paginateList.CirclePaginate(3, 11).ToList();

            Assert.AreEqual(11, result.Count);
            Assert.AreEqual("4", result[0]);
            Assert.AreEqual("5", result[1]);
            Assert.AreEqual("6", result[2]);
            Assert.AreEqual("7", result[3]);
            Assert.AreEqual("8", result[4]);
            Assert.AreEqual("9", result[5]);
            Assert.AreEqual("10", result[6]);
            Assert.AreEqual("1", result[7]);
            Assert.AreEqual("2", result[8]);
            Assert.AreEqual("3", result[9]);
            Assert.AreEqual("4", result[10]);
        }

        [Test]
        public void StartNegativeBackwardPage()
        {
            List<string> result = paginateList.CirclePaginate(-3, -11).ToList();

            Assert.AreEqual(11, result.Count);
            Assert.AreEqual("8", result[0]);
            Assert.AreEqual("7", result[1]);
            Assert.AreEqual("6", result[2]);
            Assert.AreEqual("5", result[3]);
            Assert.AreEqual("4", result[4]);
            Assert.AreEqual("3", result[5]);
            Assert.AreEqual("2", result[6]);
            Assert.AreEqual("1", result[7]);
            Assert.AreEqual("10", result[8]);
            Assert.AreEqual("9", result[9]);
            Assert.AreEqual("8", result[10]);
        }

        [Test]
        public void StartNegativeForwardPage()
        {
            List<string> result = paginateList.CirclePaginate(-3, 11).ToList();

            Assert.AreEqual(11, result.Count);
            Assert.AreEqual("8", result[0]);
            Assert.AreEqual("9", result[1]);
            Assert.AreEqual("10", result[2]);
            Assert.AreEqual("1", result[3]);
            Assert.AreEqual("2", result[4]);
            Assert.AreEqual("3", result[5]);
            Assert.AreEqual("4", result[6]);
            Assert.AreEqual("5", result[7]);
            Assert.AreEqual("6", result[8]);
            Assert.AreEqual("7", result[9]);
            Assert.AreEqual("8", result[10]);
        }

        private static List<Sortable> GetList()
        {
            return new List<Sortable>
            {
                new Sortable {Age = 20, Date = DateTime.Parse("12/12/1995"), Name = "Ted"},
                new Sortable {Age = 40, Date = DateTime.Parse("12/12/1990"), Name = "Zander"},
                new Sortable {Age = 30, Date = DateTime.Parse("12/12/2000"), Name = "Bill"}
            };
        }

        #region Nested type: Sortable

        public class Sortable
        {
            public int Age { get; set; }
            public DateTime Date { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }
}