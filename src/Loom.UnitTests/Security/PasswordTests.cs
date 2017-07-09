#region Using Directives

using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

#endregion

namespace Loom.Security
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StrongInvalidLength()
        {
            Password.Create(true, 10);
        }

        [Test]
        public void StrongNoLength10000Cycles()
        {
            for (int i = 1; i < 10000; i++)
            {
                string pass = Password.Create(true);
                Assert.IsNotNull(pass, "Password should not be null");
                Assert.IsTrue(IsStrongPassword(pass), "Password " + pass + " is not strong.");
            }
        }

        [Test]
        public void StrongIncreasingLengths()
        {
            string pass;
            for (int i = 14; i <= 128; i++)
            {
                pass = Password.Create(true, i);
                Assert.IsNotNull(pass, "Password should not be null");
                Assert.IsTrue(IsStrongPassword(pass), "Password " + pass + " is not strong.");
                //Console.WriteLine(pass);
            }
        }

        [Test]
        public void WeakNoLengthGiven()
        {
            string pass = Password.Create(false);
            Assert.IsNotNull(pass, "Password should not be null");
        }

        [TestCase(7)]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(40)]
        [TestCase(80)]
        [TestCase(160)]
        [TestCase(255)]
        [TestCase(-20, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(-10, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void WeakPasswordLengthTests(int passLength)
        {
            string pass = Password.Create(false, passLength);
            Assert.IsNotNull(pass, "Password should not be null");
            Assert.AreEqual(passLength, pass.Length, "Password not expected length. Expected " + passLength + ", actual is " + pass.Length);
        }

        [Test]
        public void PinLength4()
        {
            string pass = Password.CreatePin(4);
            Assert.IsNotNull(pass, "Pin is null.");
            Assert.AreEqual(4, pass.Length, "Pin is not 4 characters long.");
            int.Parse(pass);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PinLengthInvalid()
        {
            Password.CreatePin(2);
        }

        [Test]
        public void Properties()
        {
            int strong = Password.StrongLength;
            int weak = Password.WeakLength;
            Assert.AreEqual(strong, 14, "Strong password property is not 14.");
            Assert.AreEqual(weak, 7, "Weak password property is not 7.");
        }

        [Test]
        public void AllCharSet()
        {
            for (int i = 1; i < 10; i++)
                Assert.IsNotNull(RandomCharSet.Next(CharacterSet.All));
        }

        public bool IsStrongPassword(string password)
        {
            // Special Characters (update here then cut & paste to 2 locations below)
            // \-\+\?\*\$\[\]\^\.\(\)\|`~!@#%&_ ={}:;  ',/

            // Defines minimum appearance of characters
            string ex1 =
                @"
                ^        # anchor at the start
                (?=.*\d)    # must contain at least one digit
                (?=.*[a-z])    # must contain at least one lowercase
                (?=.*[A-Z])    # must contain at least one uppercase
                (?=.*[\-\+\?\*\$\[\]\^\.\(\)\|`~!@#%&_ ={}:;<>  ',/])  # must contain at least one special character
                .{14,128}    # min, max length
                $        # anchor at the end";

            // Allow only defined characters
            string ex2 =
                @"
                ^        # anchor at the start
                [\w\-\+\?\*\$\[\]\^\.\(\)\|`~!@#%&_ ={}:;<>  ',/] # alphanumerics and special characters only
                {14,128}      # min, max length
                $        # anchor at the end";

            return IsMatch(password, ex1, RegexOptions.IgnorePatternWhitespace) &&
                   IsMatch(password, ex2, RegexOptions.IgnorePatternWhitespace);
        }

        /// <summary>
        ///     Match a regular expression against a provided string.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="pattern">
        ///     The regular expression pattern used to
        ///     validate the input.
        /// </param>
        /// <param name="options">
        ///     A bitwise OR combination of the
        ///     RegExOption enumeration values
        /// </param>
        /// <returns>
        ///     True if the parameters produce a match, false
        ///     otherwise.
        /// </returns>
        public bool IsMatch(string input, string pattern, RegexOptions options)
        {
            Regex regex = new Regex(pattern, options);
            Match m = regex.Match(input);
            return m.Success;
        }
    }
}