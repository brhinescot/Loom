#region Using Directives

using System;
using System.Collections;
using System.Web;
using NUnit.Framework;

#endregion

namespace Loom.Web.IO
{
    [TestFixture]
    public class WebPathTests
    {
        private const string Path0 = @"/";
        private const string Path1 = @"/webdirectory1";
        private const string Path2 = @"/webdirectory2/";
        private const string Path3 = @"/webdirectory3/webpage1.aspx";
        private const string Path4 = @"/webdirectory4/webdirectory5/webpage1.aspx";
        private const string Path5 = @"/webdirectory6/webdirectory7\webpage1.aspx";
        private const string Path6 = "http://www.thegridmaster.com/admin/webpage1.aspx";
        private const string Path7 = @"/webdirectory8/webdirectory9/";
        private const string Path8 = @"/webdirectory8/webdirectory9";
        private const string Path9 = @"/webdirectory8/webdirectory9/webdirectory10/";
        private const string Path10 = @"webdirectory8/webdirectory9/webdirectory10";
        private const string Path11 = "http://www.thegridmaster.com/admin/users/brian/webpage1.aspx?moduleid=11";

        [Test]
        public void GetFullPath()
        {
            //Assert.AreEqual("/VisualStudio/MyProjects/DevInteropFramework/DevInterop.Tests/bin/Debug/testpath/test.aspx", WebPath.GetFullPath("testpath/test.aspx"));
            Assert.AreEqual("/testpath/test.aspx", WebPath.GetFullPath("/testpath/test.aspx"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFullPathNull()
        {
            WebPath.GetFullPath(null);
        }

        [Test]
        public void Combine()
        {
            string result = WebPath.Combine(Path1, Path2);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory1/webdirectory2/", result);

            result = WebPath.Combine(Path2, Path3);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory2/webdirectory3/webpage1.aspx", result);

            result = WebPath.Combine(Path2, Path5);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory2/webdirectory6/webdirectory7/webpage1.aspx", result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CombineNull1()
        {
            WebPath.Combine(null, Path2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CombineNull2()
        {
            WebPath.Combine(Path2, null);
        }

        [Test]
        public void GetDirectoryName()
        {
            string result = WebPath.GetDirectory(Path4);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory4/webdirectory5", result);

            result = WebPath.GetDirectory(Path3);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory3", result);

            result = WebPath.GetDirectory(Path1);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/webdirectory1", result);

            result = WebPath.GetDirectory(Path0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.AreEqual(@"/", result);
        }

        [Test]
        public void GetLeafNameFile()
        {
            string result = WebPath.GetLeafName(Path6);
            Assert.IsNotNull(result, Path6 + " returns null");
            Assert.IsTrue(result.Length > 0, Path6 + " returns zero length.");
            Assert.AreEqual(@"webpage1.aspx", result);

            result = WebPath.GetLeafName(Path11);
            Assert.IsNotNull(result, Path11 + " returns null");
            Assert.IsTrue(result.Length > 0, Path11 + " returns zero length.");
            Assert.AreEqual(@"webpage1.aspx", result);

            result = WebPath.GetLeafName(Path3);
            Assert.IsNotNull(result, Path3 + " returns null");
            Assert.IsTrue(result.Length > 0, Path3 + " returns zero length.");
            Assert.AreEqual(@"webpage1.aspx", result);
        }

        [Test]
        public void GetLeafNameDirectory()
        {
            string result = WebPath.GetLeafName(Path7);
            Assert.IsNotNull(result, Path7 + " returns null");
            Assert.IsTrue(result.Length > 0, Path7 + " returns zero length.");
            Assert.AreEqual(@"webdirectory9", result);

            result = WebPath.GetLeafName(Path8);
            Assert.IsNotNull(result, Path8 + " returns null");
            Assert.IsTrue(result.Length > 0, Path8 + " returns zero length.");
            Assert.AreEqual(@"webdirectory9", result);
        }

        [Test]
        public void GetLeafNameRoot()
        {
            string result = WebPath.GetLeafName(Path1);
            Assert.IsNotNull(result, Path1 + " returns null");
            Assert.IsTrue(result.Length == 0, Path1 + " should return zero length.");
            Assert.AreEqual(string.Empty, result);

            result = WebPath.GetLeafName(Path2);
            Assert.IsNotNull(result, Path2 + " returns null");
            Assert.IsTrue(result.Length == 0, Path2 + " should return zero length.");
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBranchNameNullPath()
        {
            WebPath.GetBranchName(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetLeafNameNullPath()
        {
            WebPath.GetLeafName(null);
        }

        [Test]
        public void HasExtension()
        {
            Assert.IsTrue(WebPath.HasExtension("default.aspx"));
            Assert.IsTrue(WebPath.HasExtension("usrs/admin/default.aspx"));
            Assert.IsTrue(WebPath.HasExtension("http://www.site.com/users/default.aspx"));

            Assert.IsFalse(WebPath.HasExtension("http://www.site.com/users/"));
            Assert.IsFalse(WebPath.HasExtension("~/users/"));
            Assert.IsFalse(WebPath.HasExtension("admin/blogs/users"));
            Assert.IsFalse(WebPath.HasExtension(null));
        }

        [Test]
        public void GetBranchName()
        {
            string result = WebPath.GetBranchName(Path6);
            Assert.IsNotNull(result, Path6 + " returns null");
            Assert.IsTrue(result.Length > 0, Path6 + " returns zero length.");
            Assert.AreEqual(@"/admin", result);

            result = WebPath.GetBranchName(Path7);
            Assert.IsNotNull(result, Path7 + " returns null");
            Assert.IsTrue(result.Length > 0, Path7 + " returns zero length.");
            Assert.AreEqual(@"/webdirectory8", result);

            result = WebPath.GetBranchName(Path8);
            Assert.IsNotNull(result, Path8 + " returns null");
            Assert.IsTrue(result.Length > 0, Path8 + " returns zero length.");
            Assert.AreEqual(@"/webdirectory8", result);

            result = WebPath.GetBranchName(Path9);
            Assert.IsNotNull(result, Path9 + " returns null");
            Assert.IsTrue(result.Length > 0, Path9 + " returns zero length.");
            Assert.AreEqual(@"/webdirectory8/webdirectory9", result);

            result = WebPath.GetBranchName(Path10);
            Assert.IsNotNull(result, Path10 + " returns null");
            Assert.IsTrue(result.Length > 0, Path10 + " returns zero length.");
            Assert.AreEqual(@"/webdirectory8/webdirectory9", result);

            result = WebPath.GetBranchName(Path11);
            Assert.IsNotNull(result, Path11 + " returns null");
            Assert.IsTrue(result.Length > 0, Path11 + " returns zero length.");
            Assert.AreEqual(@"/admin/users/brian", result);

            result = WebPath.GetBranchName(Path0);
            Assert.IsNotNull(result, Path0 + " returns null");
            Assert.IsTrue(result.Length == 0, Path0 + " should return zero length.");
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void GetFileNameWithoutExtension()
        {
            ArrayList actualList = new ArrayList();
            const string expected = "webpage1";

            actualList.Add("http://www.thegridmaster.com/admin/webpage1.aspx");
            actualList.Add("/webdirectory4/webdirectory5/webpage1.aspx");
            actualList.Add("http://www.thegridmaster.com/admin/users/brian/webpage1.aspx?moduleid=11");
            actualList.Add("/admin/users/brian/webpage1.aspx?moduleid=11");

            string result;
            foreach (string actual in actualList)
            {
                result = WebPath.GetFileNameWithoutExtension(actual);
                Assert.IsNotNull(result, actual + " returns null");
                Assert.IsTrue(result.Length > 0, actual + " returns zero length.");
                Assert.AreEqual(expected, result);
            }
        }

        [Test]
        public void GetExtension()
        {
            string[] mp3Files =
            {
                "files/sample.mp3",
                "/files/sample.mp3",
                "sample.mp3",
                "files/samples/sample.mp3",
                "/files/samples/sample.mp3"
            };

            foreach (string file in mp3Files)
            {
                string result = WebPath.GetExtension(file);
                Assert.IsNotNull(result, "Result should not be null.");
                Assert.AreEqual(4, result.Length, "Extension should be four characters in length.");
                Assert.AreEqual(".mp3", result, "Extension should be .mp3.");
            }
        }

        [Test]
        public void GetExtensionEmptyString()
        {
            string result = WebPath.GetExtension(string.Empty);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Length, "Extension should be zero characters in length.");
            Assert.AreEqual(string.Empty, result, "Extension should be \"\".");
        }

        [Test]
        public void GetExtensionNull()
        {
            string result = WebPath.GetExtension(null);
            Assert.IsNull(result, "Result should be null.");
        }

        [Test]
        public void ChangeExtension()
        {
            Assert.AreEqual("/users/files/image.jpg", WebPath.ChangeExtension("/users/files/image.aspx", "jpg"));
        }

        [Test]
        public void IsAppRelative()
        {
            const string appRelativeUrl = "~/images/image.jpg";
            const string dirRelativeUrl = "images/image.jpg";
            const string absoluteUrl = "/images/image.jpg";
            const string fullUrl = "http://www.google.com/images/image.jpg";

            Assert.IsTrue(VirtualPathUtility.IsAppRelative(appRelativeUrl));
            Assert.IsTrue(WebPath.IsAppRelative(appRelativeUrl));

            Assert.IsFalse(VirtualPathUtility.IsAppRelative(absoluteUrl));
            Assert.IsFalse(WebPath.IsAppRelative(absoluteUrl));

            Assert.IsFalse(VirtualPathUtility.IsAppRelative(dirRelativeUrl));
            Assert.IsFalse(WebPath.IsAppRelative(dirRelativeUrl));

            Assert.IsFalse(WebPath.IsAppRelative(fullUrl));
        }

        [Test]
        public void IsAbsolute()
        {
            const string appRelativeUrl = "~/images/image.jpg";
            const string dirRelativeUrl = "images/image.jpg";
            const string absoluteUrl = "/images/image.jpg";
            const string fullUrl = "http://www.google.com/images/image.jpg";

            Assert.IsTrue(VirtualPathUtility.IsAbsolute(absoluteUrl));
            Assert.IsTrue(WebPath.IsAbsolute(absoluteUrl));

            Assert.IsFalse(VirtualPathUtility.IsAbsolute(appRelativeUrl));
            Assert.IsFalse(WebPath.IsAbsolute(appRelativeUrl));

            Assert.IsFalse(VirtualPathUtility.IsAbsolute(dirRelativeUrl));
            Assert.IsFalse(WebPath.IsAbsolute(dirRelativeUrl));

            Assert.IsFalse(WebPath.IsAbsolute(fullUrl));
        }

        [Test]
        public void IsDirectoryRelative()
        {
            const string appRelativeUrl = "~/images/image.jpg";
            const string dirRelativeUrl = "images/image.jpg";
            const string absoluteUrl = "/images/image.jpg";
            const string fullUrl = "http://www.google.com/images/image.jpg";

            Assert.IsTrue(WebPath.IsDirectoryRelative(dirRelativeUrl));

            Assert.IsFalse(WebPath.IsDirectoryRelative(absoluteUrl));
            Assert.IsFalse(WebPath.IsDirectoryRelative(appRelativeUrl));
            Assert.IsFalse(WebPath.IsDirectoryRelative(fullUrl));
        }
    }
}