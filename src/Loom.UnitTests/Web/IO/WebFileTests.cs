#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Web.IO
{
    [TestFixture]
    public class WebFileTests
    {
        [TestCase(".pdf", "application/pdf")]
        [TestCase(".jpg", "image/jpeg")]
        [TestCase(".zzz", null)]
        public void Test(string extension, string expected)
        {
            string mimeType = WebFile.GetMimeType(extension);
            Assert.AreEqual(expected, mimeType);
        }
    }
}