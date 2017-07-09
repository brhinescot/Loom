#region Using Directives

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class MutableLookupTests
    {
        [Test]
        public void Test()
        {
            MutableLookup<string, int> lookup = new MutableLookup<string, int>();

            lookup.Add("A", 001);
            lookup.Add("A", 002);
            lookup.Add("A", 003);
            lookup.Add("B", 004);
            lookup.Add("B", 005);
            lookup.Add("A", 006);
            lookup.Add("C", 007);
            lookup.Add("C", 008);
            lookup.Add(null, 008);

            List<int> list = lookup["A"].ToList();

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
            Assert.AreEqual(6, list[3]);
        }
    }
}