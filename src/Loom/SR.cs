#region Using Directives

using System;
using System.Globalization;
using System.Resources;

#endregion

namespace Loom
{
    internal class SR
    {
        public static string ExceptionArgArrayPlusOffTooSmall => Keys.GetString(Keys.ExceptionArgArrayPlusOffTooSmall);

        public static string ExceptionArgInvalidArrayType => Keys.GetString(Keys.ExceptionArgInvalidArrayType);

        public static string ExceptionArgumentMustBeGreaterThanZero => Keys.GetString(Keys.ExceptionArgumentMustBeGreaterThanZero);

        public static string ExceptionArgumentMustBeNonNegative => Keys.GetString(Keys.ExceptionArgumentMustBeNonNegative);

        public static string ExceptionByteArrayValueMustBeGreaterThanZeroBytes => Keys.GetString(Keys.ExceptionByteArrayValueMustBeGreaterThanZeroBytes);

        public static string ExceptionCastingHashAlgorithmInstance => Keys.GetString(Keys.ExceptionCastingHashAlgorithmInstance);

        public static string ExceptionCastingSymmetricAlgorithmInstance => Keys.GetString(Keys.ExceptionCastingSymmetricAlgorithmInstance);

        public static string ExceptionCreatingHashAlgorithmInstance => Keys.GetString(Keys.ExceptionCreatingHashAlgorithmInstance);

        public static string ExceptionCreatingSymmetricAlgorithmInstance => Keys.GetString(Keys.ExceptionCreatingSymmetricAlgorithmInstance);

        public static string ExceptionDecrypting => Keys.GetString(Keys.ExceptionDecrypting);

        public static string ExceptionDetails => Keys.GetString(Keys.ExceptionDetails);

        public static string ExceptionItemCountMustBeGreaterThanZero => Keys.GetString(Keys.ExceptionItemCountMustBeGreaterThanZero);

        public static string ExceptionNonParsablePhoneNumber => Keys.GetString(Keys.ExceptionNonParsablePhoneNumber);

        public static string ExceptionNotSupportedReadOnlyCollection => Keys.GetString(Keys.ExceptionNotSupportedReadOnlyCollection);

        public static string ExceptionObjectDisposedCanNotRenew => Keys.GetString(Keys.ExceptionObjectDisposedCanNotRenew);

        public static string ExceptionPropertyNotNull => Keys.GetString(Keys.ExceptionPropertyNotNull);

        public static string ExceptionServerBindingIsNull => Keys.GetString(Keys.ExceptionServerBindingIsNull);

        public static string ExceptionStackTraceDetails => Keys.GetString(Keys.ExceptionStackTraceDetails);

        public static string ExceptionSummary => Keys.GetString(Keys.ExceptionSummary);

        public static string ExceptionTimeoutWaitingForLock => Keys.GetString(Keys.ExceptionTimeoutWaitingForLock);

        public static string ExceptionType => Keys.GetString(Keys.ExceptionType);

        public static string ExpressionAnyDigits => Keys.GetString(Keys.ExpressionAnyDigits);

        public static string ExpressionCityName => Keys.GetString(Keys.ExpressionCityName);

        public static string ExpressionEmailAddress => Keys.GetString(Keys.ExpressionEmailAddress);

        public static string ExpressionGuid => Keys.GetString(Keys.ExpressionGuid);

        public static string ExpressionHighStrongPassword => Keys.GetString(Keys.ExpressionHighStrongPassword);

        public static string ExpressionIpAddress => Keys.GetString(Keys.ExpressionIpAddress);

        public static string ExpressionLowStrongPassword => Keys.GetString(Keys.ExpressionLowStrongPassword);

        public static string ExpressionMediumStrongPassword => Keys.GetString(Keys.ExpressionMediumStrongPassword);

        public static string ExpressionMoney => Keys.GetString(Keys.ExpressionMoney);

        public static string ExpressionNotMalicious => Keys.GetString(Keys.ExpressionNotMalicious);

        public static string ExpressionPhoneNumber => Keys.GetString(Keys.ExpressionPhoneNumber);

        public static string ExpressionSocialSecurityNUmber => Keys.GetString(Keys.ExpressionSocialSecurityNUmber);

        public static string ExpressionWordsOnly => Keys.GetString(Keys.ExpressionWordsOnly);

        public static string ExpressionZipCode => Keys.GetString(Keys.ExpressionZipCode);

        public static string GenericInvalidData => Keys.GetString(Keys.GenericInvalidData);

        public static string InvalidHexString => Keys.GetString(Keys.InvalidHexString);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        public static string ExceptionArgumentWrongType(string value, Type type)
        {
            return Keys.GetString(Keys.ExceptionArgumentWrongType, new object[]
            {
                value,
                type
            });
        }

        public static string ExceptionDefaultImplementationNotFound(string typeName)
        {
            return Keys.GetString(Keys.ExceptionDefaultImplementationNotFound, new object[]
            {
                typeName
            });
        }

        public static string ExceptionDelimitedArrayIndexOutOfBounds(int index)
        {
            return Keys.GetString(Keys.ExceptionDelimitedArrayIndexOutOfBounds, new object[]
            {
                index
            });
        }

        public static string ExceptionEmptyString(string variableName)
        {
            return Keys.GetString(Keys.ExceptionEmptyString, new object[]
            {
                variableName
            });
        }

        public static string ExceptionEnumerationNotDefined(string variable, string enumName)
        {
            return Keys.GetString(Keys.ExceptionEnumerationNotDefined, new object[]
            {
                variable,
                enumName
            });
        }

        public static string ExceptionExpectedType(string typeName)
        {
            return Keys.GetString(Keys.ExceptionExpectedType, new object[]
            {
                typeName
            });
        }

        public static string ExceptionFileNotFound(string fileName)
        {
            return Keys.GetString(Keys.ExceptionFileNotFound, new object[]
            {
                fileName
            });
        }

        public static string ExceptionInvalidCastFromTo(string from, string to)
        {
            return Keys.GetString(Keys.ExceptionInvalidCastFromTo, new object[]
            {
                from,
                to
            });
        }

        public static string ExceptionInvalidNullNameArgument(string messageName)
        {
            return Keys.GetString(Keys.ExceptionInvalidNullNameArgument, new object[]
            {
                messageName
            });
        }

        public static string ExceptionInvalidType(string typeName)
        {
            return Keys.GetString(Keys.ExceptionInvalidType, new object[]
            {
                typeName
            });
        }

        public static string ExceptionNoConstructorsDefined(string typeName)
        {
            return Keys.GetString(Keys.ExceptionNoConstructorsDefined, new object[]
            {
                typeName
            });
        }

        public static string ExceptionObjectDisposed(string name, string member)
        {
            return Keys.GetString(Keys.ExceptionObjectDisposed, new object[]
            {
                name,
                member
            });
        }

        public static string ExceptionTypeAlreadyRegistered(string typeName)
        {
            return Keys.GetString(Keys.ExceptionTypeAlreadyRegistered, new object[]
            {
                typeName
            });
        }

        public static string PasswordLessThanMinimumLength(int length)
        {
            return Keys.GetString(Keys.PasswordLessThanMinimumLength, new object[]
            {
                length
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ExceptionArgArrayPlusOffTooSmall = "ExceptionArgArrayPlusOffTooSmall";

            public const string ExceptionArgInvalidArrayType = "ExceptionArgInvalidArrayType";

            public const string ExceptionArgumentMustBeGreaterThanZero = "ExceptionArgumentMustBeGreaterThanZero";

            public const string ExceptionArgumentMustBeNonNegative = "ExceptionArgumentMustBeNonNegative";

            public const string ExceptionArgumentWrongType = "ExceptionArgumentWrongType";

            public const string ExceptionByteArrayValueMustBeGreaterThanZeroBytes = "ExceptionByteArrayValueMustBeGreaterThanZeroBytes";

            public const string ExceptionCastingHashAlgorithmInstance = "ExceptionCastingHashAlgorithmInstance";

            public const string ExceptionCastingSymmetricAlgorithmInstance = "ExceptionCastingSymmetricAlgorithmInstance";

            public const string ExceptionCreatingHashAlgorithmInstance = "ExceptionCreatingHashAlgorithmInstance";

            public const string ExceptionCreatingSymmetricAlgorithmInstance = "ExceptionCreatingSymmetricAlgorithmInstance";

            public const string ExceptionDecrypting = "ExceptionDecrypting";

            public const string ExceptionDefaultImplementationNotFound = "ExceptionDefaultImplementationNotFound";

            public const string ExceptionDelimitedArrayIndexOutOfBounds = "ExceptionDelimitedArrayIndexOutOfBounds";

            public const string ExceptionDetails = "ExceptionDetails";

            public const string ExceptionEmptyString = "ExceptionEmptyString";

            public const string ExceptionEnumerationNotDefined = "ExceptionEnumerationNotDefined";

            public const string ExceptionExpectedType = "ExceptionExpectedType";

            public const string ExceptionFileNotFound = "ExceptionFileNotFound";

            public const string ExceptionInvalidCastFromTo = "ExceptionInvalidCastFromTo";

            public const string ExceptionInvalidNullNameArgument = "ExceptionInvalidNullNameArgument";

            public const string ExceptionInvalidType = "ExceptionInvalidType";

            public const string ExceptionItemCountMustBeGreaterThanZero = "ExceptionItemCountMustBeGreaterThanZero";

            public const string ExceptionNoConstructorsDefined = "ExceptionNoConstructorsDefined";

            public const string ExceptionNonParsablePhoneNumber = "ExceptionNonParsablePhoneNumber";

            public const string ExceptionNotSupportedReadOnlyCollection = "ExceptionNotSupportedReadOnlyCollection";

            public const string ExceptionObjectDisposed = "ExceptionObjectDisposed";

            public const string ExceptionObjectDisposedCanNotRenew = "ExceptionObjectDisposedCanNotRenew";

            public const string ExceptionPropertyNotNull = "ExceptionPropertyNotNull";

            public const string ExceptionServerBindingIsNull = "ExceptionServerBindingIsNull";

            public const string ExceptionStackTraceDetails = "ExceptionStackTraceDetails";

            public const string ExceptionSummary = "ExceptionSummary";

            public const string ExceptionTimeoutWaitingForLock = "ExceptionTimeoutWaitingForLock";

            public const string ExceptionType = "ExceptionType";

            public const string ExceptionTypeAlreadyRegistered = "ExceptionTypeAlreadyRegistered";

            public const string ExpressionAnyDigits = "ExpressionAnyDigits";

            public const string ExpressionCityName = "ExpressionCityName";

            public const string ExpressionEmailAddress = "ExpressionEmailAddress";

            public const string ExpressionGuid = "ExpressionGuid";

            public const string ExpressionHighStrongPassword = "ExpressionHighStrongPassword";

            public const string ExpressionIpAddress = "ExpressionIpAddress";

            public const string ExpressionLowStrongPassword = "ExpressionLowStrongPassword";

            public const string ExpressionMediumStrongPassword = "ExpressionMediumStrongPassword";

            public const string ExpressionMoney = "ExpressionMoney";

            public const string ExpressionNotMalicious = "ExpressionNotMalicious";

            public const string ExpressionPhoneNumber = "ExpressionPhoneNumber";

            public const string ExpressionSocialSecurityNUmber = "ExpressionSocialSecurityNUmber";

            public const string ExpressionWordsOnly = "ExpressionWordsOnly";

            public const string ExpressionZipCode = "ExpressionZipCode";

            public const string GenericInvalidData = "GenericInvalidData";

            public const string InvalidHexString = "InvalidHexString";

            public const string PasswordLessThanMinimumLength = "PasswordLessThanMinimumLength";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.SR", typeof(SR).Assembly);

            public static string GetString(string key)
            {
                return resourceManager.GetString(key, CultureInfo.InvariantCulture);
            }

            public static string GetString(string key, object[] args)
            {
                string msg = resourceManager.GetString(key, CultureInfo.InvariantCulture);
                msg = string.Format(msg, args);
                return msg;
            }
        }

        #endregion
    }
}