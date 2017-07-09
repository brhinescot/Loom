#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Web.Portal
{
    internal class SR
    {
        public static string ExceptionInHttpPipeline => Keys.GetString(Keys.ExceptionInHttpPipeline);

        public static string ExceptionSectionNameExists => Keys.GetString(Keys.ExceptionSectionNameExists);

        public static string ExceptionSetupNotConfigured => Keys.GetString(Keys.ExceptionSetupNotConfigured);

        public static string ExceptionUnknownHost => Keys.GetString(Keys.ExceptionUnknownHost);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        public static string ExceptionRoutesMoreThanOnePrimary(object @string)
        {
            return Keys.GetString(Keys.ExceptionRoutesMoreThanOnePrimary, new[]
            {
                @string
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ExceptionInHttpPipeline = "ExceptionInHttpPipeline";

            public const string ExceptionRoutesMoreThanOnePrimary = "ExceptionRoutesMoreThanOnePrimary";

            public const string ExceptionSectionNameExists = "ExceptionSectionNameExists";

            public const string ExceptionSetupNotConfigured = "ExceptionSetupNotConfigured";

            public const string ExceptionUnknownHost = "ExceptionUnknownHost";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Web.Portal.Resources.SR", typeof(SR).Assembly);

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