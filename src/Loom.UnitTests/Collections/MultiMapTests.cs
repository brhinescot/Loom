#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Collections
{
    [TestFixture]
    public class MultiMapTests
    {
        [Test]
        public void TEST()
        {
            MultiMap<string, int> map = new MultiMap<string, int>();
            map.Add("Test", 2);
            map.Add("Test", 4);
            map.Add("Test", 5);
            map.Add("Test", 6);

            Assert.AreEqual(1, map.Count);

            Assert.AreEqual(4, map["Test"].Count);
            Assert.AreEqual(2, map["Test"][0]);
            Assert.AreEqual(4, map["Test"][1]);
            Assert.AreEqual(5, map["Test"][2]);
            Assert.AreEqual(6, map["Test"][3]);
        }
    }
}