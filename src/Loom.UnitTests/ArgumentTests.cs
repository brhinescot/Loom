#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class ArgumentTests
    {
        [TestCase("")]
        [TestCase("xxxxxxxx")]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void NotNull(object testValue)
        {
            Argument.Assert.IsNotNull(testValue, "testValue");
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void NotNull(float testValue)
        {
            Argument.Assert.IsNotNegative(testValue, "testValue");
        }

        [TestCase("xxxxx")]
        [TestCase("", ExpectedException = typeof(ArgumentException))]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        public void NotNullOrEmpty(string testValue)
        {
            Argument.Assert.IsNotNullOrEmpty(testValue, "testValue");
        }
    }
}