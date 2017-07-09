#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void TEST()
        {
            DateTime now = DateTime.Now.AddDays(29);
            string actual = now.ToDateTimeWords();
            string actual4 = now.ToDateTimeWords(now.AddDays(-12));
            string actual2 = now.ToDateWords();
            string actual3 = now.ToTimeWords();
        }
    }
}