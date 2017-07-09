#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom
{
    /// <summary>
    ///     Summary description for PhoneNumberTests.
    /// </summary>
    [TestFixture]
    public class PhoneNumberTests
    {
        [Test]
        public void BasicCompare()
        {
            PhoneNumber number1 = PhoneNumber.Parse("222-222-2222");
            PhoneNumber number2 = PhoneNumber.Parse("222-222-4444");

            Assert.AreNotEqual(number1, number2);
            Assert.AreNotEqual(number1.ToString(), number2.ToString());
        }

        [TestCase("773-659-9878")]
        [TestCase("(773)659-9878")]
        [TestCase("(773) 659-9878")]
        [TestCase("(773)659.9878")]
        [TestCase("(773) 659.9878")]
        [TestCase("7736599878")]
        [TestCase("773.659.9878")]
        [TestCase("773 659-9878")]
        [TestCase("773 659.9878")]
        [TestCase("773 659 9878")]
        public void ParseMultipleStandardNumberFormats(string value)
        {
            PhoneNumber phone = PhoneNumber.Parse(value);
            Assert.AreEqual(773, phone.AreaCode);
            Assert.AreEqual(659, phone.Exchange);
            Assert.AreEqual(9878, phone.Number);
            Assert.AreEqual(0, phone.Extension);
            Assert.AreEqual("659-9878", phone.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual("(773)659-9878", phone.ToString("({{AreaCode}}){{Exchange}}-{{Number}}"));
        }

        [Test]
        public void ParseStandardNumberWithTemplateText()
        {
            PhoneNumber number = PhoneNumber.Parse("773-659-9878");
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("My phone number is: (773)659-9878", number.ToString("My phone number is: ({{AreaCode}}){{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void ParseStandardNumberWithExtension()
        {
            PhoneNumber number = PhoneNumber.Parse("773-659-9878 x345");
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(345, number.Extension);
        }

        [Test]
        public void ParseStandardNumberWithParenthesisAndExtension()
        {
            PhoneNumber number = PhoneNumber.Parse("(773)659-9878 x3456");
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(3456, number.Extension);
        }

        [Test]
        public void ParseStandardNumberWithLeadingZeros()
        {
            PhoneNumber number = PhoneNumber.Parse("773-009-0078");
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("009-0078", number.ToString("{{Exchange:000}}-{{Number:0000}}"));
        }

        [Test]
        public void ImplicitFromLongConversion()
        {
            PhoneNumber number = 7736599878;
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void ExpilcitToLongConversion()
        {
            PhoneNumber number = PhoneNumber.Parse("7736599878");
            long digits = (long) number;
            Assert.AreEqual(7736599878, digits);
        }

        [Test]
        public void FromInt64()
        {
            PhoneNumber number = PhoneNumber.FromInt64(7736599878);
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void ToInt64()
        {
            PhoneNumber number = PhoneNumber.Parse("7736599878");
            long digits = number.ToInt64();
            Assert.AreEqual(7736599878, digits);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void ParseInvalidPhoneNumber()
        {
            PhoneNumber.Parse("34563453457");
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void FromInt64InvalidNumber()
        {
            PhoneNumber.FromInt64(773);
        }

        [Test]
        public void TryParseStandardNumber()
        {
            PhoneNumber number;
            bool success = PhoneNumber.TryParse("773-659-9878", out number);
            Assert.IsTrue(success);
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void TryParseInternationalNumber()
        {
            PhoneNumber number;
            bool success = PhoneNumber.TryParse("+44 773-659-9878", out number);
            Assert.IsTrue(success);
            Assert.AreEqual(44, number.CountryCode);
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual(659, number.Exchange);
            Assert.AreEqual(9878, number.Number);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void TryParseInternationalNumberNoPlus()
        {
            PhoneNumber number;
            bool success = PhoneNumber.TryParse("44 773-659-9878", out number);
            Assert.AreEqual(44, number.CountryCode);
            Assert.IsTrue(success);
            Assert.AreEqual(773, number.AreaCode);
            Assert.AreEqual("659-9878", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }

        [Test]
        public void ToStringWithExtension()
        {
            PhoneNumber number = PhoneNumber.Parse("234-234-8989 x3545");
            Assert.IsNotNull(number, "PhoneNumber should not be null");
            Assert.AreEqual("(234) 234-8989 x3545", number.ToString());
        }

        [Test]
        public void ToStringWithoutExtension()
        {
            PhoneNumber number = PhoneNumber.Parse("234-234-8989");
            Assert.IsNotNull(number, "PhoneNumber should not be null");
            Assert.AreEqual("(234) 234-8989", number.ToString());
        }

        [Test]
        public void ToStringWithCountryAndExtension()
        {
            PhoneNumber number = PhoneNumber.Parse("+44 234-234-8989 x3545");
            Assert.IsNotNull(number, "PhoneNumber should not be null");
            Assert.AreEqual("+44 (234) 234-8989 x3545", number.ToString());
        }

        [Test]
        public void ToStringWithCountry()
        {
            PhoneNumber number = PhoneNumber.Parse("+44 234-234-8989");
            Assert.IsNotNull(number, "PhoneNumber should not be null");
            Assert.AreEqual("+44 (234) 234-8989", number.ToString());
        }

        [Test]
        public void OperatorEqual()
        {
            PhoneNumber number1 = PhoneNumber.Parse("234-234-8989");
            PhoneNumber number2 = PhoneNumber.Parse("234-234-8989");
            Assert.IsNotNull(number1, "First PhoneNumber should not be null");
            Assert.IsNotNull(number2, "Second PhoneNumber should not be null");
            Assert.IsTrue(number1 == number2);
            Assert.IsTrue(number1.Equals(number2));
            Assert.IsTrue(Equals(number1, number2));
            Assert.AreEqual(number2.GetHashCode(), number1.GetHashCode());
        }

        [Test]
        public void OperatorNotEqual()
        {
            PhoneNumber number1 = PhoneNumber.Parse("234-234-8989");
            PhoneNumber number2 = PhoneNumber.Parse("432-234-8989");
            Assert.IsNotNull(number1, "First PhoneNumber should not be null");
            Assert.IsNotNull(number2, "Second PhoneNumber should not be null");
            Assert.IsFalse(number1 == number2);
            Assert.IsFalse(number1.Equals(number2));
            Assert.IsFalse(Equals(number1, number2));
            Assert.AreNotEqual(number2.GetHashCode(), number1.GetHashCode());
        }

        [Test]
        public void ComparisonOperators()
        {
            PhoneNumber number1 = PhoneNumber.Parse("234-234-8989");
            PhoneNumber number2 = PhoneNumber.Parse("(432)234-8989");
            PhoneNumber number3 = PhoneNumber.Parse("502-234-8989");
            Assert.IsNotNull(number1, "First PhoneNumber should not be null");
            Assert.IsNotNull(number2, "Second PhoneNumber should not be null");
            Assert.IsNotNull(number1, "Third PhoneNumber should not be null");
            Assert.IsTrue(number1 < number2);
            Assert.IsTrue(number1 <= number2);
            Assert.IsTrue(number3 > number2);
            Assert.IsTrue(number3 >= number2);
        }

        [Test]
        public void TryParseFail()
        {
            PhoneNumber number;
            bool success = PhoneNumber.TryParse("97897987", out number);
            Assert.AreEqual(number, PhoneNumber.Empty);
            Assert.IsFalse(success);
            Assert.AreEqual(0, number.AreaCode);
            Assert.AreEqual("0-0", number.ToString("{{Exchange}}-{{Number}}"));
            Assert.AreEqual(0, number.Extension);
        }
    }
}