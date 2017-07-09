#region Using Directives

using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

#endregion

namespace Loom.Web.Portal.Routing
{
    [TestFixture]
    public class RouteParserCaptureTests
    {
        [Test]
        public void CaptureTwosegments()
        {
            const string routeDefinition = "/documents/{category,2}/support";
            const string expected = @"^/documents(?:/(?<category>[\w\d\s\-\+'_%,]+)){2}/support/?$";

            Regex actual = RouteParser.GenerateRegEx(routeDefinition);

            AssertStringsSame(expected, actual.ToString());

            Match matchTwo = actual.Match("/documents/cantilever/fixed/support");
            Assert.IsTrue(matchTwo.Success);
            Assert.AreEqual(2, matchTwo.Groups.Count, "Expected 2 groups.");
            Assert.AreEqual(2, matchTwo.Groups[1].Captures.Count, "Expected 2 captures.");
            Assert.AreEqual("cantilever", matchTwo.Groups["category"].Captures[0].Value, "Expected cantilever for first capture.");
            Assert.AreEqual("fixed", matchTwo.Groups["category"].Captures[1].Value, "Expected fixed for second capture.");
        }

        [Test]
        public void CaptureThreeSegments()
        {
            const string routeDefinition = "/documents/{category,3}/support";
            const string expected = @"^/documents(?:/(?<category>[\w\d\s\-\+'_%,]+)){3}/support/?$";

            Regex actual = RouteParser.GenerateRegEx(routeDefinition);

            AssertStringsSame(expected, actual.ToString());

            Match matchThree = actual.Match("/documents/cantilever/fixed/another/support");
            Assert.IsTrue(matchThree.Success);
            Assert.AreEqual(2, matchThree.Groups.Count, "Expected 2 groups.");
            Assert.AreEqual(3, matchThree.Groups[1].Captures.Count, "Expected 3 captures.");
            Assert.AreEqual("cantilever", matchThree.Groups["category"].Captures[0].Value, "Expected 'cantilever' for first capture.");
            Assert.AreEqual("fixed", matchThree.Groups["category"].Captures[1].Value, "Expected 'fixed' for second capture.");
            Assert.AreEqual("another", matchThree.Groups["category"].Captures[2].Value, "Expected 'another' for third capture.");
        }

        private static void AssertStringsSame(string expected, string actual)
        {
            if (expected != actual)
                Assert.Fail(@"{2}EXPECTED TEXT:{2}  {0}{2}ACTUAL TEXT:{2}  {1}{2}{2}", expected, actual, Environment.NewLine);
        }
    }
}