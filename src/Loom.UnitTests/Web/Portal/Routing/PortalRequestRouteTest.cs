#region Using Directives

using MbUnit.Framework;

#endregion

namespace Colossus.Framework.Web.Portal.Routing
{
    [TestFixture]
    public class PortalRequestRouteTest
    {
        [Test]
        public void FirstTokenOfMultiSegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/Products/WallMounts/Cantilever/");
            request.AddToken("category2", "Products/WallMounts/Cantilever/");

            Assert.AreEqual("Products", request.GetFirstTokenSegmentValue("category"));
            Assert.AreEqual("Products", request.GetFirstTokenSegmentValue("category2"));
        }

        [Test]
        public void FirstTokenOfSingleSegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/Products/");
            request.AddToken("category2", "/Products");

            Assert.AreEqual("Products", request.GetFirstTokenSegmentValue("category"));
            Assert.AreEqual("Products", request.GetFirstTokenSegmentValue("category2"));
        }

        [Test]
        public void FirstTokenOfEmptySegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/");

            Assert.AreEqual("", request.GetFirstTokenSegmentValue("category"));
        }

        [Test]
        public void LastTokenOfMultiSegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/Products/WallMounts/Cantilever/");
            request.AddToken("category2", "/Products/WallMounts/Cantilever");

            Assert.AreEqual("Cantilever", request.GetLastTokenSegmentValue("category"));
            Assert.AreEqual("Cantilever", request.GetLastTokenSegmentValue("category2"));
        }

        [Test]
        public void LastTokenOfSingleSegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/Products/");
            request.AddToken("category2", "/Products");

            Assert.AreEqual("Products", request.GetLastTokenSegmentValue("category"));
            Assert.AreEqual("Products", request.GetLastTokenSegmentValue("category2"));
        }

        [Test]
        public void LastTokenOfEmptySegment()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category", "/");

            Assert.AreEqual("", request.GetLastTokenSegmentValue("category"));
        }

        [Test]
        public void GetTokenValue()
        {
            PortalRequest request = new PortalRequest();

            request.AddToken("category0", "/Products/WallMounts/Cantilever/");
            request.AddToken("category1", "Products/WallMounts/Cantilever/");
            request.AddToken("category2", "/Products/");
            request.AddToken("category3", "/Products");
            request.AddToken("category4", "Products");
            request.AddToken("category5", "/");
            request.AddToken("category6", null);

            Assert.AreEqual("Products/WallMounts/Cantilever", request.GetTokenValue("category0"));
            Assert.AreEqual("Products/WallMounts/Cantilever", request.GetTokenValue("category1"));
            Assert.AreEqual("Products", request.GetTokenValue("category2"));
            Assert.AreEqual("Products", request.GetTokenValue("category3"));
            Assert.AreEqual("Products", request.GetTokenValue("category4"));
            Assert.AreEqual(null, request.GetTokenValue("category6"));
        }
    }
}