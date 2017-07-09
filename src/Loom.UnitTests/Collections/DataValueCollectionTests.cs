#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class DataValueCollectionTests
    {
        [Test]
        public void SetAndRetrieve()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Name", "Bob");

            Assert.AreEqual("Bob", collection.RetrieveString("Name"));
        }

        [Test]
        public void SetMultiple()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Name", "Bob");
            collection.Set("Age", 25);

            Assert.AreEqual("Bob", collection.RetrieveString("Name"));
            Assert.AreEqual(25, collection.RetrieveInt32("Age"));
        }

        [Test]
        public void IsSet()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Name", "Bob");
            collection.Set("Age", 25);

            Assert.IsTrue(collection.IsSet("Name"));
            Assert.IsTrue(collection.IsSet("Age"));
        }

        [Test]
        public void RemoveItem()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Name", "Bob");
            collection.Set("Age", 25);

            collection.Remove("Name");

            Assert.IsFalse(collection.IsSet("Name"));
            Assert.IsTrue(collection.IsSet("Age"));
        }

        [Test]
        public void Count()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Name", "Bob");
            collection.Set("Age", 25);

            Assert.AreEqual(2, collection.Count);

            collection.Remove("Name");

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void RetrieveDateAsStringAndDateTime()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Date", "12/12/2014");

            Assert.AreEqual("12/12/2014", collection.RetrieveString("Date"));
            Assert.AreEqual(DateTime.Parse("12/12/2014"), collection.RetrieveDateTime("Date"));
        }

        [Test]
        public void RetrieveNumberAsMultipleTypes()
        {
            DataValueCollection<string> collection = new DataValueCollection<string>();

            collection.Set("Number", 100);

            Assert.AreEqual(100, collection.RetrieveInt16("Number"));
            Assert.AreEqual(100, collection.RetrieveInt32("Number"));
            Assert.AreEqual(100, collection.RetrieveInt64("Number"));
            Assert.AreEqual(100, collection.RetrieveDouble("Number"));
            Assert.AreEqual(100, collection.RetrieveDecimal("Number"));
            Assert.AreEqual("100", collection.RetrieveString("Number"));
        }
    }
}