#region Using Directives

using System.Text.RegularExpressions;

#endregion

namespace Loom.Security
{
    /// <summary>
    ///     Summary description for ValidationProvider.
    /// </summary>
    public static class ValidationProvider
    {
        private static readonly Regex CityName = new Regex(RegExExpressions.CityName, RegexOptions.Compiled);
        private static readonly Regex DecimalDigits = new Regex(RegExExpressions.DecimalDigits, RegexOptions.Compiled);
        private static readonly Regex EmailAddress = new Regex(RegExExpressions.EmailAddress, RegexOptions.Compiled);
        private static readonly Regex Guid = new Regex(RegExExpressions.Guid, RegexOptions.Compiled);
        private static readonly Regex HighStrongPassword = new Regex(RegExExpressions.HighStrongPassword, RegexOptions.Compiled);
        private static readonly Regex IntegerDigits = new Regex(RegExExpressions.IntegerDigits, RegexOptions.Compiled);
        private static readonly Regex IpAddress = new Regex(RegExExpressions.IpAddress, RegexOptions.Compiled);
        private static readonly Regex LowStrongPassword = new Regex(RegExExpressions.LowStrongPassword, RegexOptions.Compiled);
        private static readonly Regex MediumStrongPassword = new Regex(RegExExpressions.MediumStrongPassword, RegexOptions.Compiled);
        private static readonly Regex Money = new Regex(RegExExpressions.Money, RegexOptions.Compiled);
        private static readonly Regex NotMalicious = new Regex(RegExExpressions.NotMalicious, RegexOptions.Compiled);
        private static readonly Regex PhoneNumber = new Regex(RegExExpressions.PhoneNumber, RegexOptions.Compiled);
        private static readonly Regex SocialSecurityNumber = new Regex(RegExExpressions.SocialSecurityNumber, RegexOptions.Compiled);
        private static readonly Regex Words = new Regex(RegExExpressions.WordsOnly, RegexOptions.Compiled);
        private static readonly Regex ZipCode = new Regex(RegExExpressions.ZipCode, RegexOptions.Compiled);

        /// <summary>
        ///     Determines whether the specified text is in money format.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        ///     <true /> if the text is in money format; otherwise <false />
        /// </returns>
        public static bool IsMoney(string text)
        {
            return Money.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsInteger(string text)
        {
            return IntegerDigits.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsDecimal(string text)
        {
            return DecimalDigits.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsZipCode(string text)
        {
            return ZipCode.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmail(string text)
        {
            return EmailAddress.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string text)
        {
            return PhoneNumber.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsCityName(string text)
        {
            return CityName.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNotMalicious(string text)
        {
            return NotMalicious.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsWords(string text)
        {
            return Words.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsIpAddress(string text)
        {
            return IpAddress.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsSocialSecurityNumber(string text)
        {
            return SocialSecurityNumber.Match(text).Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsGuid(string text)
        {
            return Guid.Match(text).Success;
        }

        /// <summary>
        ///     Must be at least 10 characters, contain at least one one lower case character,
        ///     one upper case character, one digit and one special character.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        /// <returns>
        ///     A <see cref="bool" /> indicating if the <paramref name="text" /> is valid.
        /// </returns>
        public static bool IsHighStrongPassword(string text)
        {
            return HighStrongPassword.Match(text).Success;
        }

        /// <summary>
        ///     Must be at least 8 characters, contain at least one one lower case character,
        ///     one upper case character and one digit or special character.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        /// <returns>
        ///     A <see cref="bool" /> indicating if the <paramref name="text" /> is valid.
        /// </returns>
        public static bool IsMediumStrongPassword(string text)
        {
            return MediumStrongPassword.Match(text).Success;
        }

        /// <summary>
        ///     Must be at least 8 characters, contain at least one lower case character,
        ///     one upper case character and one digit.
        /// </summary>
        /// <param name="text">The text to validate.</param>
        /// <returns>
        ///     A <see cref="bool" /> indicating if the <paramref name="text" /> is valid.
        /// </returns>
        public static bool IsLowStrongPassword(string text)
        {
            return LowStrongPassword.Match(text).Success;
        }
    }
}