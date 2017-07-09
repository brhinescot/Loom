#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class AssertTests
    {
        [TestCase("are changing the world", "We are changing the world", "We")]
        [TestCase("We are changing the world", "We are changing the world", "")]
        [TestCase("We are changing the world", "We are changing the world", "You")]
        [TestCase("We are changing the world", "We are changing the world", null)]
        [TestCase(null, null, "token")]
        [TestCase(null, null, null)]
        public void TrimIfStartsWith(string expected, string input, string token)
        {
            string actual = Compare.TrimIfStartsWith(input, token);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("We are changing the", "We are changing the world", "world")]
        [TestCase("We are changing the world", "We are changing the world", "")]
        [TestCase("We are changing the world", "We are changing the world", "worlds")]
        [TestCase("We are changing the world", "We are changing the world", null)]
        [TestCase(null, null, "token")]
        [TestCase(null, null, null)]
        public void TrimIfEndsWith(string expected, string input, string token)
        {
            string actual = Compare.TrimIfEndsWith(input, token);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NullOrEmpty()
        {
            Assert.IsTrue(Compare.IsNullOrEmpty(""));
        }

        [Test]
        public void AnyNullOrEmpty()
        {
            Assert.IsTrue(Compare.IsAnyNullOrEmpty(null));
            Assert.IsTrue(Compare.IsAnyNullOrEmpty(""));
            Assert.IsTrue(Compare.IsAnyNullOrEmpty(null, "", "test"));
            Assert.IsTrue(Compare.IsAnyNullOrEmpty(null, "1", "test"));

            Assert.IsFalse(Compare.IsAnyNullOrEmpty("A", " "));
            Assert.IsFalse(Compare.IsAnyNullOrEmpty("A", "1", "test"));
        }
    }
}