#region Using Directives

using NUnit.Framework;

#endregion

namespace Loom.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("the dog jumped over the moon", "The Dog Jumped Over The Moon")]
        [TestCase("The dog jumped over the moon", "The Dog Jumped Over The Moon")]
        [TestCase("TheDogJumpedOverTheMoon", "The Dog Jumped Over The Moon")]
        [TestCase("TheDog JumpedOver TheMoon", "The Dog Jumped Over The Moon")]
        [TestCase("theDog jumpedOver theMoon", "The Dog Jumped Over The Moon")]
        [TestCase("theDog  jumpedOver theMoon", "The Dog  Jumped Over The Moon")]
        [TestCase("HelloWorld", "Hello World")]
        [TestCase("helloWorld", "Hello World")]
        [TestCase("userID", "User Id")] // ID Special case
        public void ToProper(string input, string expected)
        {
            string actual = input.ToProperCase();
            Assert.AreEqual(expected, actual);
        }

        [TestCase("The Dog Jumped Over The Moon", "theDogJumpedOverTheMoon")]
        [TestCase("The dog jumped over the moon", "theDogJumpedOverTheMoon")]
        [TestCase("the dog jumped over the moon", "theDogJumpedOverTheMoon")]
        [TestCase("theDog jumpedOver theMoon", "theDogJumpedOverTheMoon")]
        [TestCase("Hello World", "helloWorld")]
        [TestCase("Hello  World", "helloWorld")]
        [TestCase("User ID", "userId")] // ID Special case
        public void ToCamel(string input, string expected)
        {
            string actual = input.ToCamelCase();
            Assert.AreEqual(expected, actual);
        }

        [TestCase("The Dog Jumped Over The Moon", "TheDogJumpedOverTheMoon")]
        [TestCase("The dog jumped over the moon", "TheDogJumpedOverTheMoon")]
        [TestCase("the dog jumped over the moon", "TheDogJumpedOverTheMoon")]
        [TestCase("Hello World", "HelloWorld")]
        [TestCase("User ID", "UserId")] // ID Special case
        public void ToPascal(string input, string expected)
        {
            string actual = input.ToPascalCase();
            string actual2 = input.ToPascalCase();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual, actual2);
        }

        [TestCase("The Dog Jumped Over The Moon", "The Do...")] // More than max length.
        [TestCase("Hello World", "Hello...")] // More than max length with space after truncate.
        [TestCase("Hello", "Hello")] // Less than max length.
        [TestCase("Hello You", "Hello You")] // Exactly max length.
        public void Truncate(string input, string expected)
        {
            string actual = input.Truncate(9);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(" ", true)]
        [TestCase("  ", true)]
        [TestCase("   ", true)]
        [TestCase("\t", true)]
        [TestCase("\t ", true)]
        [TestCase(" \t ", true)]
        [TestCase("Hello", false)]
        [TestCase("", false)]
        [TestCase("Hello You", false)]
        [TestCase(" Hello", false)]
        public void IsWhitespace(string input, bool expected)
        {
            bool actual = input.IsWhiteSpace();
            Assert.AreEqual(expected, actual);
        }
    }
}