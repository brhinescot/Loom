#region Using Directives

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

#endregion

namespace Loom.Security
{
    /// <summary>
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
    [StructLayout(LayoutKind.Auto)]
    internal struct RegExExpressions
    {
        /// <summary>
        /// </summary>
        public static readonly string Money = SR.ExpressionMoney;

        /// <summary>
        /// </summary>
        public static readonly string IntegerDigits = SR.ExpressionAnyDigits;

        /// <summary>
        /// </summary>
        public static readonly string DecimalDigits = @"^(?=.*[1-9].*$)\d*(?:\.\d*)?$";

        /// <summary>
        /// </summary>
        public static readonly string ZipCode = SR.ExpressionZipCode;

        /// <summary>
        /// </summary>
        public static readonly string EmailAddress = SR.ExpressionEmailAddress;

        /// <summary>
        /// </summary>
        public static readonly string PhoneNumber = SR.ExpressionPhoneNumber;

        /// <summary>
        /// </summary>
        public static readonly string CityName = SR.ExpressionCityName;

        /// <summary>
        /// </summary>
        public static readonly string NotMalicious = SR.ExpressionNotMalicious;

        /// <summary>
        /// </summary>
        public static readonly string WordsOnly = SR.ExpressionWordsOnly;

        /// <summary>
        /// </summary>
        public static readonly string IpAddress = SR.ExpressionIpAddress;

        /// <summary>
        /// </summary>
        public static readonly string SocialSecurityNumber = SR.ExpressionSocialSecurityNUmber;

        /// <summary>
        /// </summary>
        public static readonly string Guid = SR.ExpressionGuid;

        /// <summary>
        /// </summary>
        public static readonly string HighStrongPassword = SR.ExpressionHighStrongPassword;

        /// <summary>
        /// </summary>
        public static readonly string MediumStrongPassword = SR.ExpressionMediumStrongPassword;

        /// <summary>
        /// </summary>
        public static readonly string LowStrongPassword = SR.ExpressionLowStrongPassword;
    }
}