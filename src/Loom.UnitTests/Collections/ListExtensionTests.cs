#region Using Directives

using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class ListExtensionTests
    {
        [Test]
        public void RandomCount()
        {
            List<int> items = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            List<int> randoms0 = items.GetRandom(2);
            List<int> randoms1 = items.GetRandom(4);
            List<int> randoms2 = items.GetRandom(6);
            List<int> randoms3 = items.GetRandom(8);
            List<int> randoms4 = items.GetRandom(4);
            Thread.Sleep(1);
            List<int> randoms5 = items.GetRandom(4);
            List<int> randoms6 = items.GetRandom(11);

            Assert.AreEqual(2, randoms0.Count);
            Assert.AreEqual(4, randoms1.Count);
            Assert.AreEqual(6, randoms2.Count);
            Assert.AreEqual(8, randoms3.Count);
            Assert.AreEqual(4, randoms4.Count);
            Assert.AreEqual(4, randoms5.Count);
            Assert.AreEqual(10, randoms6.Count);

            Assert.AreNotEqual(items, randoms4);
//            Assert.AreNotEqual(randoms4, randoms5);
        }

        [Test]
        public void RandomCountAndPredicate()
        {
            List<int> items = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            List<int> randoms0 = items.GetRandom(2, i => i > 8);
            List<int> randoms1 = items.GetRandom(4, i => i > 5);
            List<int> randoms2 = items.GetRandom(5, i => i > 8);

            Assert.AreEqual(2, randoms0.Count);
            Assert.AreEqual(4, randoms1.Count);
            Assert.AreEqual(2, randoms2.Count);

            randoms0.ForEach(i => Assert.IsTrue(i > 8));
            randoms1.ForEach(i => Assert.IsTrue(i > 5));
        }
    }
}