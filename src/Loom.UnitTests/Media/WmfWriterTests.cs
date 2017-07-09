#region Using Directives

using Loom.Media.Meta;
using Loom.Media.Meta.Wmf;
using NUnit.Framework;

#endregion

namespace Loom.Media
{
    [TestFixture]
    public class WmfWriterTests
    {
        [Test]
        public void SetAttribute()
        {
            const string fileName = @"Media\04 Wish You Were Here.wma";

            using (WmfMetaWriter writer = new WmfMetaWriter(fileName))
            {
                writer.SetAttribute("Description", "Pink Floyd Rocks!");
            }

            using (WmfMetaReader reader = new WmfMetaReader(fileName))
            {
                MetaAttributeCollection attributes = reader.GetAllAttributes();
                Assert.AreEqual("Pink Floyd Rocks!", attributes["Description"]);
            }

            using (WmfMetaWriter writer = new WmfMetaWriter(fileName))
            {
                writer.DeleteAttribute("Description");
            }
        }

        [Test]
        public void DeleteAttribute()
        {
            string fileName = @"Media\04 Wish You Were Here.wma";

            using (WmfMetaWriter writer = new WmfMetaWriter(fileName))
            {
                writer.DeleteAttribute("WM/Genre");
            }

            using (WmfMetaReader reader = new WmfMetaReader(fileName))
            {
                MetaAttributeCollection attributes = reader.GetAllAttributes();
                Assert.AreEqual(null, attributes["WM/Genre"]);
            }

            using (WmfMetaWriter writer = new WmfMetaWriter(fileName))
            {
                writer.SetAttribute("WM/Genre", "Floyd");
            }

            using (WmfMetaReader reader = new WmfMetaReader(fileName))
            {
                MetaAttributeCollection attributes = reader.GetAllAttributes();
                Assert.AreEqual("Floyd", attributes["WM/Genre"]);
            }
        }
    }
}