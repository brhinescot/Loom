#region Using Directives

using Loom.Media.Meta;
using Loom.Media.Meta.Wmf;
using NUnit.Framework;

#endregion

namespace Loom.Media
{
    [TestFixture]
    public class WmfMetaReaderTests
    {
        [Test]
        public void GetAllAttributes()
        {
            using (WmfMetaReader reader = new WmfMetaReader(@"Media\04 Wish You Were Here.wma"))
            {
                Assert.IsNotNull(reader);

                MetaAttributeCollection attributes = reader.GetAllAttributes();
                Assert.AreEqual(40, attributes.Count);
                Assert.AreEqual("Wish You Were Here", attributes["wm/albumtitle"]);
                Assert.AreEqual("Pink Floyd", attributes["author"]);
                Assert.AreEqual("Wish You Were Here", attributes["title"]);
                Assert.AreEqual("04", attributes["wm/tracknumber"]);
            }
        }

        [Test]
        public void Enumerate()
        {
            using (WmfMetaReader reader = new WmfMetaReader(@"Media\04 Wish You Were Here.wma"))
            {
                Assert.IsNotNull(reader);

                foreach (MetaAttribute attr in reader)
                    if (attr.Name == "wm/albumtitle")
                    {
                        Assert.AreEqual("Wish You Were Here", attr.Value);
                        break;
                    }
            }
        }
    }
}