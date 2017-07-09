#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class IfTests
    {
        [Test]
        public void Test()
        {
            const string s = "NotNull";
            string result = null;

            If.NotNull(s, () => result = "success");

            Assert.AreEqual("success", result);
        }
    }
}