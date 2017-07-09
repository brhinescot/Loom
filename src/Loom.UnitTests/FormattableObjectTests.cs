#region Using Directives

using System;
using System.Globalization;
using NUnit.Framework;

#endregion

namespace Loom
{
    [TestFixture]
    public class FormattableObjectTests
    {
        [Test]
        public void MakePersonComplexFormattedStringWithFormatAndCulture()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("My birthday is {{BirthDate:MM/dd/yy HH:mm:ss}} and my name is {{FirstName}} {{LastName}} and I'm cool.", CultureInfo.InvariantCulture);
            Assert.AreEqual("My birthday is 01/22/74 00:00:00 and my name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedString()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{BirthDate}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("1/22/1974 00:00:00 My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithBadData()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{bogus}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("{{bogus}} My name is Sea Sharpe and I'm cool.", foo);
        }

        /// <summary>
        ///     This will likely pass only in the en-us culture
        /// </summary>
        [Test]
        public void MakePersonFormattedStringWithFormat()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{BirthDate:D}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("Tuesday, January 22, 1974 My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithFormatAndChineseCulture()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{BirthDate:D}} My name is {{FirstName}} {{LastName}} and I'm cool.", new CultureInfo("fr-FR"));
            Assert.AreEqual("mardi 22 janvier 1974 My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithFormatAndCulture()
        {
            TestPerson p = new TestPerson();
            string foo =
                p.ToString("{{BirthDate:D}} My name is {{FirstName}} {{LastName}} and I'm cool.", CultureInfo.InvariantCulture);
            Assert.AreEqual("Tuesday, 22 January 1974 My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithQuestionableData()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{BirthDate:}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("1/22/1974 00:00:00 My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithSlightlyQuestionableData()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{}} My name is {{FirstName}} {{LastName}} and I'm cool {}.");
            Assert.AreEqual("{{}} My name is Sea Sharpe and I'm cool {}.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithVeryBadData()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{bogus:}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("{{bogus:}} My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakePersonFormattedStringWithVeryBadFormattingData()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{BirthDate:poo}} My name is {{FirstName}} {{LastName}} and I'm cool.");
            Assert.AreEqual("poo My name is Sea Sharpe and I'm cool.", foo);
        }

        [Test]
        public void MakeSimplePersonFormattedString()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{LastName}}, {{FirstName}}");

            Assert.AreEqual("Sharpe, Sea", foo);
        }

        [Test]
        public void MakeSimplePersonFormattedStringWithDate()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{LastName}}, {{FirstName}} {{BirthDate}}");
            Assert.AreEqual("Sharpe, Sea 1/22/1974 00:00:00", foo);
        }

        [Test]
        public void MakeSimplePersonFormattedStringWithDateAndOneBadPropertyName()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{LastName}}, {{Invalid}} {{BirthDate}}");
            Assert.AreEqual("Sharpe, {{Invalid}} 1/22/1974 00:00:00", foo);
        }

        [Test]
        public void MakeSimplePersonFormattedStringWithDouble()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{Money}} {{LastName}}, {{Invalid}} {{BirthDate}}");
            Assert.AreEqual("3.43 Sharpe, {{Invalid}} 1/22/1974 00:00:00", foo);
        }

        /// <summary>
        ///     This will likely pass only in the en-us culture
        /// </summary>
        [Test]
        public void MakeSimplePersonFormattedStringWithDoubleFormatted()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{Money:C}} {{LastName}}, {{Invalid}} {{BirthDate}}");
            Assert.AreEqual("$3.43 Sharpe, {{Invalid}} 1/22/1974 00:00:00", foo);
        }

        [Test]
        public void MakeSimplePersonFormattedStringWithDoubleFormattedInHongKong()
        {
            TestPerson p = new TestPerson();
            string foo = p.ToString("{{Money:C}} {{LastName}}, {{Invalid}} {{BirthDate}}", new CultureInfo("zh-hk"));
            Assert.AreEqual("HK$3.43 Sharpe, {{Invalid}} 1/22/1974 00:00:00", foo);
        }
    }

    public class TestPerson : FormattableObject
    {
        public DateTime BirthDate { get; set; } = new DateTime(1974, 1, 22);

        public string FirstName { get; set; } = "Sea";

        public string LastName { get; set; } = "Sharpe";

        public string MiddleName { get; set; } = "M";

        public double Money { get; set; } = 3.43;
    }
}