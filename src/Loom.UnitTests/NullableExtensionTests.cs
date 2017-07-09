#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class NullableExtensionTests
    {
        [Test]
        public void Test()
        {
            DateTime? hasValue = new DateTime(2001, 1, 1);
            DateTime? noValue = null;

            string hasValueActual = hasValue.ConvertOrDefault(x => x.ToString("MM/dd/yyyy"), string.Empty);
            string noValueConvertedToString = noValue.ConvertOrDefault(x => x.ToString("MM/dd/yyyy"), string.Empty);

            Assert.AreEqual("01/01/2001", hasValueActual);
            Assert.AreEqual(string.Empty, noValueConvertedToString);
        }
    }
}