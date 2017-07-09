#region Using Directives

using System;
using System.Collections.Generic;
using System.Net.Mail;
using NUnit.Framework;

#endregion

namespace Loom.Net.Mail
{
    [TestFixture]
    public class TemplatedMailMessageTests
    {
        [Test]
        public void ConstructorsTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl", "brian.scott@colossusinteractive.com", "brian.scott2@colossusinteractive.com");

            Assert.AreEqual(@"Net\Mail\NoComments.tpl", email.Content);
            Assert.AreEqual("brian.scott@colossusinteractive.com", email.From.Address);
            Assert.AreEqual(1, email.To.Count);
            Assert.AreEqual("brian.scott2@colossusinteractive.com", email.To[0].Address);
        }

        [Test]
        public void MemberSetTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl");
            TestClass mergeObject = new TestClass();

            email.From = new MailAddress("brian.scott@colossusinteractive.com");
            email.To.Add("brian.scott2@colossusinteractive.com");

            email.Merge(mergeObject);

            Assert.AreEqual(@"Net\Mail\NoComments.tpl", email.Content);
            Assert.AreSame(mergeObject, email.MergeObject);
            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContent, email.Body);
        }

        [Test]
        public void NoCommentsTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl");
            email.Merge(new TestClass());

            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContent, email.Body);
        }

        [Test]
        public void SingleLineCommentsTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\SingleLineComments.tpl");
            email.Merge(new TestClass());

            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContent, email.Body);
        }

        [Test]
        public void MultiLineCommentsTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\MultiLineComments.tpl");
            email.Merge(new TestClass());

            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContent, email.Body);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetValueObjectToNull()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl");
            email.Merge(new TestClass());

            email.Merge(null);
        }

        [Test]
        public void GetMergeValuesTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl");
            email.Subject = "Test";
            email.Merge(new TestClass());

            Assert.IsNotNull(email.GetMergeValue("FullName"));
            Assert.AreEqual("Ben Jammin", email.GetMergeValue("FullName"));
            Assert.IsNotNull(email.GetMergeValue("Link"));
            Assert.AreEqual("www.thesite.com/activate?uid=s87df87at827gf4f7g78fg34", email.GetMergeValue("Link"));
        }

        [Test]
        public void AnonymousClassValues()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\NoComments.tpl");
            email.Subject = "Test";
            email.Merge(new
            {
                FullName = "Ben Jammin",
                Link = "www.thesite.com/activate?uid=s87df87at827gf4f7g78fg34"
            });

            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContent, email.Body);
        }

        [Test]
        public void LayoutTemplateTest()
        {
            TemplatedMailMessage email = new TemplatedMailMessage(@"Net\Mail\TemplateContent.tpl");
            email.Layout = @"Net\Mail\TemplateLayout.tpl";
            email.AddTemplate("Header", "Hello {{FullName}}");
            email.AddTemplate("Footer", "Thank You,<br/><br/>The Team<br/><br/>*This is a disclaimer.");
            email.Merge(new TestClass());

            Assert.IsNotNull(email.Body);
            Assert.IsNotEmpty(email.Body);
            Assert.AreEqual(TestClass.ExpectedContentWithLayout, email.Body);
        }

        #region Nested type: TestClass

        public class TestClass
        {
            internal const string ExpectedContent = "Dear Ben Jammin,\r\n\r\nWelcome to the new site. Please visit <a href=\"www.thesite.com/activate?uid=s87df87at827gf4f7g78fg34\">this link</a> to activate your account.\r\n\r\nThank you,\r\n\r\nThe Team\r\n";
            internal const string ExpectedContentWithLayout = "<html>\r\n\t<head>\r\n\t\t<style type=\"text/css\">\r\n\t\t\tbody{\r\n\t\t\t\tbackground-color:#acacac;\r\n\t\t\t}\r\n\t\t</style>\r\n\t</head>\r\n\t<body>\r\n\r\n\t\r\nHello Ben Jammin\r\n\r\nWelcome to the new site. Please visit <a href=\"www.thesite.com/activate?uid=s87df87at827gf4f7g78fg34\">this link</a> to activate your account.\r\n\r\nThank You,<br/><br/>The Team<br/><br/>*This is a disclaimer.\r\n\r\n\r\n\t</body>\r\n</html>\r\n\r\n";

            public TestClass()
            {
                FullName = "Ben Jammin";
                Link = "www.thesite.com/activate?uid=s87df87at827gf4f7g78fg34";
                Users = new List<TestPerson>
                {
                    new TestPerson {FirstName = "Ted"},
                    new TestPerson {FirstName = "Fred"},
                    new TestPerson {FirstName = "Red"}
                };
            }

            public string FullName { get; }
            public string Link { get; }
            public List<TestPerson> Users { get; set; }
        }

        #endregion
    }
}