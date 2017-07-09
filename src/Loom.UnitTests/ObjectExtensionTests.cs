#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void IsAnyOf()
        {
            const string dog = "dog";

            Assert.IsTrue(dog.IsAnyOf("cat", "dog", "bird"));
            Assert.IsFalse(dog.IsAnyOf("cat", "mouse", "bird"));
        }
    }
}