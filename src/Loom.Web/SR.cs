#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Web
{
    internal class SR
    {
        public static string ExceptionEmptyPathNoDirectory => Keys.GetString(Keys.ExceptionEmptyPathNoDirectory);

        public static string ExceptionPathMustBeRooted => Keys.GetString(Keys.ExceptionPathMustBeRooted);

        public static string MessageFileNotFound => Keys.GetString(Keys.MessageFileNotFound);

        public static string MessageGeneralError => Keys.GetString(Keys.MessageGeneralError);

        public static string MessageMaliciousInput => Keys.GetString(Keys.MessageMaliciousInput);

        public static string MessageResourceAccessDenied => Keys.GetString(Keys.MessageResourceAccessDenied);

        public static string ScriptRelationalList => Keys.GetString(Keys.ScriptRelationalList);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        public static string ExceptionPostedFileSizeToLarge(string filename)
        {
            return Keys.GetString(Keys.ExceptionPostedFileSizeToLarge, new object[]
            {
                filename
            });
        }

        public static string ExceptionPostedFileSizeZeroLength(string filename)
        {
            return Keys.GetString(Keys.ExceptionPostedFileSizeZeroLength, new object[]
            {
                filename
            });
        }

        public static string ScriptSafeMailLink(int keyLength)
        {
            return Keys.GetString(Keys.ScriptSafeMailLink, new object[]
            {
                keyLength
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ExceptionEmptyPathNoDirectory = "ExceptionEmptyPathNoDirectory";

            public const string ExceptionPathMustBeRooted = "ExceptionPathMustBeRooted";

            public const string ExceptionPostedFileSizeToLarge = "ExceptionPostedFileSizeToLarge";

            public const string ExceptionPostedFileSizeZeroLength = "ExceptionPostedFileSizeZeroLength";

            public const string MessageFileNotFound = "MessageFileNotFound";

            public const string MessageGeneralError = "MessageGeneralError";

            public const string MessageMaliciousInput = "MessageMaliciousInput";

            public const string MessageResourceAccessDenied = "MessageResourceAccessDenied";

            public const string ScriptRelationalList = "ScriptRelationalList";

            public const string ScriptSafeMailLink = "ScriptSafeMailLink";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Web.Resources.SR", typeof(SR).Assembly);

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