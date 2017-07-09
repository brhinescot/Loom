#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [TestCase("12/01/2010", "December 1st 2010")]
        [TestCase("12/02/2010", "December 2nd 2010")]
        [TestCase("12/11/2010", "December 11th 2010")]
        [TestCase("12/12/2010", "December 12th 2010")]
        [TestCase("12/14/2010", "December 14th 2010")]
        [TestCase("12/22/2010", "December 22nd 2010")]
        public void Test(string date, string expected)
        {
            string dateWords = DateTime.Parse(date).ToDateWords();
            Assert.AreEqual(dateWords, expected);
        }
    }
}