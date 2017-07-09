#region Using Directives

using System;
using System.Text;
using Loom.Threading;

#endregion

namespace Loom.Security
{
    /// <summary>
    ///     A class for working with passwords.
    /// </summary>
    public static class Password
    {
        private const int MaximumLength = 255;
        private const int StrongPasswordLength = 14;
        private const int WeakPasswordLength = 7;

        private static readonly Random Random = new Random(int.Parse(DateTime.Now.ToString("fffffff")));

        /// <summary>
        ///     Gets the minimum length for a strong password.
        /// </summary>
        public static int StrongLength => StrongPasswordLength;

        /// <summary>
        ///     Gets the minimum length for a weak password.
        /// </summary>
        public static int WeakLength => WeakPasswordLength;

        /// <summary>
        ///     Creates a temporary password with the default length.
        /// </summary>
        /// <returns>A temporary password equal to the default length.</returns>
        public static string Create(bool strong)
        {
            return CreatePrivate(strong, strong ? StrongPasswordLength : WeakPasswordLength);
        }

        /// <summary>
        ///     Creates a temporary password of a specified length.
        /// </summary>
        /// <param name="strong">
        ///     Indicates if the password will be strong according to the assembly's
        ///     rules for strong passwords.
        /// </param>
        /// <param name="length">The maximum length of the temporary password to create.</param>
        /// <returns>A temporary password equal to the length specified.</returns>
        public static string Create(bool strong, int length)
        {
            return CreatePrivate(strong, length);
        }

        /// <summary>
        ///     Creates a temporary password with the default length.
        /// </summary>
        /// <returns>A temporary password equal to the default length.</returns>
        public static string CreatePin(int length)
        {
            if ((length < 4) | (length > MaximumLength))
                throw new ArgumentOutOfRangeException("length", length, string.Format("The specified length must be between 4 and {0}.", MaximumLength));

            return CreatePinPrivate(length);
        }

        /// <summary>
        ///     Creates a temporary password with the default length.
        /// </summary>
        /// <returns>A temporary password equal to the default length.</returns>
        public static string CreateCaptcha(int length)
        {
            if ((length < 4) | (length > MaximumLength))
                throw new ArgumentOutOfRangeException("length", length, string.Format("The specified length must be between 4 and {0}.", MaximumLength));

            return CreateCaptchaPrivate(length);
        }

        private static string CreateCaptchaPrivate(int length)
        {
            StringBuilder sb = new StringBuilder(length);

            using (TimedLock.Lock(typeof(Password)))
            {
                for (int i = 0; i < length; i++)
                    sb.Append(RandomCharSet.Next(CharacterSet.Captcha));
            }

            return sb.ToString().Substring(0, length);
        }

        /// <summary>
        ///     Creates a random set of numbers of the specified length.
        /// </summary>
        /// <param name="length">The number of characters in the pin to be created.</param>
        /// <returns>A string representing the pin number.</returns>
        private static string CreatePinPrivate(int length)
        {
            StringBuilder sb = new StringBuilder(length);

            using (TimedLock.Lock(typeof(Password)))
            {
                for (int i = 0; i < length; i++)
                    sb.Append(RandomCharSet.Next(CharacterSet.Number));
            }

            return sb.ToString().Substring(0, length);
        }

        private static string CreatePrivate(bool strong, int length)
        {
            if ((length < 0) | (length > MaximumLength))
                throw new ArgumentOutOfRangeException("length", length, string.Format("The specified length must be a non-negative number and less than {0}", MaximumLength + 1));

            return strong ? CreateRandomStrongPassword(length) : CreateRandomWeakPassword(length);
        }

        /// <summary>
        ///     Creates a random strong password of the specified length.
        /// </summary>
        /// <param name="length">
        ///     The length of the password to create.
        ///     Must be at least as long as <see cref="StrongLength" />.
        /// </param>
        /// <returns></returns>
        private static string CreateRandomStrongPassword(int length)
        {
            if (length < StrongPasswordLength)
                throw new ArgumentOutOfRangeException("length", length, SR.PasswordLessThanMinimumLength(StrongPasswordLength));

            StringBuilder sb = new StringBuilder(length);

            //Randomly insert letters of any case up to 4 from the requested length.
            //
            using (TimedLock.Lock(typeof(Password)))
            {
                for (int i = 0; i < length - 4; i++)
                    sb.Append(RandomCharSet.Next(CharacterSet.Letter));

                //Fill remaining 4 characters with one from each group 
                //(at random indexes) to insure a strong password.
                //
                sb.Insert(Random.Next(0, sb.Length), RandomCharSet.Next(CharacterSet.Lower));
                sb.Insert(Random.Next(0, sb.Length), RandomCharSet.Next(CharacterSet.Upper));
                sb.Insert(Random.Next(0, sb.Length), RandomCharSet.Next(CharacterSet.Number));
                sb.Insert(Random.Next(0, sb.Length), RandomCharSet.Next(CharacterSet.Special));
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Creates a random password of the specified length with only alphanumeric characters.
        /// </summary>
        /// <param name="length">
        ///     The length of the password to be created.
        ///     Must be at least as long as <see cref="WeakLength" />.
        /// </param>
        /// <returns>A string containing the generated password.</returns>
        private static string CreateRandomWeakPassword(int length)
        {
            if (length < WeakPasswordLength)
                throw new ArgumentOutOfRangeException("length", length, SR.PasswordLessThanMinimumLength(WeakPasswordLength));

            StringBuilder sb = new StringBuilder(length);

            using (TimedLock.Lock(typeof(Password)))
            {
                for (int i = 0; i < length; i++)
                    sb.Append(RandomCharSet.Next(CharacterSet.Alphanumeric));
            }

            return sb.ToString().Substring(0, length);
        }
    }
}