#region Using Directives

using System;
using System.Globalization;
using NUnit.Framework;

#endregion

namespace Loom
{
    /// <summary>
    ///     Summary description for WeekTests.
    /// </summary>
    [TestFixture]
    public class WeekTests
    {
        private readonly DateTime[] expectedValues =
        {
            new DateTime(2005, 2, 28),
            new DateTime(2005, 3, 1),
            new DateTime(2005, 3, 2),
            new DateTime(2005, 3, 3),
            new DateTime(2005, 3, 4),
            new DateTime(2005, 3, 5),
            new DateTime(2005, 3, 6)
        };

        // March 1st 2005, Tuesday
        private readonly DateTime testDay = new DateTime(2005, 3, 1);

        [Test]
        public void AddWeeksTest()
        {
            DateTime today = new DateTime(2005, 2, 25);
            //Add 1 week to get back to the expected test week
            Week week = Week.NewWeek(today).AddWeeks(1);

            Assert.AreEqual(expectedValues[0], week.Monday);
            Assert.AreEqual(expectedValues[1], week.Tuesday);
            Assert.AreEqual(expectedValues[2], week.Wednesday);
            Assert.AreEqual(expectedValues[3], week.Thursday);
            Assert.AreEqual(expectedValues[4], week.Friday);
            Assert.AreEqual(expectedValues[5], week.Saturday);
            Assert.AreEqual(expectedValues[6], week.Sunday);
        }

        [Test]
        public void Default()
        {
            Week week = new Week(testDay);
            Week week2 = new Week(testDay);
            Week week23 = new Week(testDay);

            Assert.AreEqual(expectedValues[0], week.Monday);
            Assert.AreEqual(expectedValues[1], week.Tuesday);
            Assert.AreEqual(expectedValues[2], week.Wednesday);
            Assert.AreEqual(expectedValues[3], week.Thursday);
            Assert.AreEqual(expectedValues[4], week.Friday);
            Assert.AreEqual(expectedValues[5], week.Saturday);
            Assert.AreEqual(expectedValues[6], week.Sunday);
        }

        [Test]
        public void MaxWeekNoArgumentOutOfRangeException()
        {
            Week week = Week.MaxValue;

            try
            {
                DateTime test;
                test = week.Monday;
                test = week.Tuesday;
                test = week.Wednesday;
                test = week.Thursday;
                test = week.Friday;
                test = week.Saturday;
                test = week.Sunday;
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MinWeekNoArgumentOutOfRangeException()
        {
            Week week = Week.MinValue;

            try
            {
                DateTime test;
                test = week.Monday;
                test = week.Tuesday;
                test = week.Wednesday;
                test = week.Thursday;
                test = week.Friday;
                test = week.Saturday;
                test = week.Sunday;
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ToArrayTest()
        {
            Week week = new Week(testDay);
            DateTime[] dateArray = week.ToArray();

            Assert.AreEqual(7, dateArray.Length);
            Assert.AreEqual(expectedValues[0], dateArray[0]);
            Assert.AreEqual(expectedValues[1], dateArray[1]);
            Assert.AreEqual(expectedValues[2], dateArray[2]);
            Assert.AreEqual(expectedValues[3], dateArray[3]);
            Assert.AreEqual(expectedValues[4], dateArray[4]);
            Assert.AreEqual(expectedValues[5], dateArray[5]);
            Assert.AreEqual(expectedValues[6], dateArray[6]);
        }

        [Test]
        public void ToStringNullValuesWithoutExceptionTest()
        {
            Week week = new Week(testDay);

            week.ToString();
            week.ToString((string) null);
            week.ToString(null, CultureInfo.InvariantCulture);
            week.ToString("G", (IFormatProvider) null);
            week.ToString("G", (string) null);
            week.ToString(null, null, null);
        }

        [Test]
        public void ToStringWithFormatAndSeperatorTest()
        {
            Week week = new Week(testDay);

            string weekString = week.ToString("MM/dd/yyyy", "to");
            Assert.AreEqual("02/28/2005 to 03/06/2005", weekString);

            weekString = week.ToString("MM/dd", "thru");
            Assert.AreEqual("02/28 thru 03/06", weekString);

            weekString = week.ToString("MMMM d", ":");
            Assert.AreEqual("February 28 : March 6", weekString);
        }

        [Test]
        public void ToStringWithFormatTest()
        {
            Week week = new Week(testDay);

            string weekString = week.ToString("MM/dd/yyyy");
            Assert.AreEqual("02/28/2005 - 03/06/2005", weekString);

            weekString = week.ToString("MM/dd");
            Assert.AreEqual("02/28 - 03/06", weekString);

            weekString = week.ToString("MMMM d");
            Assert.AreEqual("February 28 - March 6", weekString);
        }
    }
}