#region Using Directives

using System;
using System.Globalization;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class MonthTests
    {
        [Test]
        public void NewMonthFromJuneDate()
        {
            Month month = new Month(June.Today);
            Assert.AreEqual(June.FirstDay, month.FirstDay);
            Assert.AreEqual(June.WeekCount, month.ToArray().Length);
            Assert.AreEqual(June.LastDay, month.LastDay);
        }

        [Test]
        public void NewMonthFromJuneParseDate()
        {
            string s = June.Today.ToString(CultureInfo.InvariantCulture);
            Month month = Month.Parse(June.Today.ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual(June.FirstDay, month.FirstDay);
            Assert.AreEqual(June.WeekCount, month.ToArray().Length);
            Assert.AreEqual(June.LastDay, month.LastDay);
        }

        [Test]
        public void NewMonthFromJuneWeek()
        {
            Month month = new Month(June.ThisWeek);
            Assert.AreEqual(June.FirstDay, month.FirstDay);
            Assert.AreEqual(June.WeekCount, month.ToArray().Length);
            Assert.AreEqual(June.LastDay, month.LastDay);
        }

        [Test]
        public void NewMonthFromFebDate()
        {
            Month month = new Month(Feb.Today);
            Assert.AreEqual(Feb.FirstDay, month.FirstDay);
            Assert.AreEqual(Feb.WeekCount, month.ToArray().Length);
            Assert.AreEqual(Feb.LastDay, month.LastDay);
        }

        [Test]
        public void NewMonthFromFebParseDate()
        {
            Month month = Month.Parse(Feb.Today.ToString(CultureInfo.InvariantCulture));
            Assert.AreEqual(Feb.FirstDay, month.FirstDay);
            Assert.AreEqual(Feb.WeekCount, month.ToArray().Length);
            Assert.AreEqual(Feb.LastDay, month.LastDay);
        }

        [Test]
        public void NewMonthFromFebWeek()
        {
            Month month = new Month(Feb.ThisWeek);
            Assert.AreEqual(Feb.FirstDay, month.FirstDay);
            Assert.AreEqual(Feb.WeekCount, month.ToArray().Length);
            Assert.AreEqual(Feb.LastDay, month.LastDay);
        }

        [Test]
        public void EnumberateMonth()
        {
            Month feb = new Month(Feb.ThisWeek);
            int count = 0;

            foreach (Week week in feb)
                count++;

            Assert.AreEqual(Feb.WeekCount, count);
        }

        [Test]
        public void EqualsTest()
        {
            Month month1 = new Month(Feb.Today);
            Month month2 = new Month(Feb.ThisWeek);
            Assert.AreEqual(month1, month2);
        }

        [Test]
        public void NotEqualsTest()
        {
            Month month1 = new Month(Feb.Today);
            Month month2 = new Month(June.ThisWeek);
            Assert.AreNotEqual(month1, month2);
        }

        [Test]
        public void CompareSameTest()
        {
            Month month1 = new Month(Feb.Today);
            Month month2 = new Month(Feb.ThisWeek);
            Assert.AreEqual(0, month1.CompareTo(month2));
            Assert.AreEqual(0, month2.CompareTo(month1));
        }

        [Test]
        public void CompareDifferentTest()
        {
            Month month1 = new Month(Feb.Today);
            Month month2 = new Month(June.ThisWeek);
            Assert.AreEqual(-1, month1.CompareTo(month2));
            Assert.AreEqual(1, month2.CompareTo(month1));
        }

        [Test]
        public void LastMonthOfYear()
        {
            Month month = new Month(DateTime.Parse("12/01/2013"));
            int weeks = 0;
            foreach (Week week in month)
            {
                Console.WriteLine(week.ToString());
                weeks++;
            }

            Assert.AreEqual(6, weeks);
        }

        [Test]
        public void ThisMonth()
        {
            Month month = Month.ThisMonth;
            Assert.IsNotNull(month);
            Assert.AreEqual(month, new Month(DateTime.Today));
        }

        [Test]
        public void ParseMonth()
        {
            Month month = Month.Parse("2/15/2006");
            Assert.AreEqual(Feb.FirstDay, month.FirstDay);
        }

        #region Nested type: Feb

        private struct Feb
        {
            public static readonly DateTime Today = new DateTime(2006, 2, 15);
            public static readonly DateTime FirstDay = new DateTime(2006, 2, 1);
            public static readonly DateTime LastDay = new DateTime(2006, 2, 28);
            public static readonly Week ThisWeek = new Week(new DateTime(2006, 2, 15));
            public static readonly int WeekCount = 5;
        }

        #endregion

        #region Nested type: June

        private struct June
        {
            public static readonly DateTime Today = new DateTime(2006, 6, 15);
            public static readonly DateTime FirstDay = new DateTime(2006, 6, 1);
            public static readonly DateTime LastDay = new DateTime(2006, 6, 30);
            public static readonly Week ThisWeek = new Week(new DateTime(2006, 6, 15));
            public static readonly int WeekCount = 5;
        }

        #endregion
    }
}