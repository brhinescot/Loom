#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Web
{
    internal class SR
    {
        public static string ExceptionLogFieldMissing(string name)
        {
            return Keys.GetString(Keys.ExceptionLogFieldMissing, new object[]
            {
                name
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ExceptionLogFieldMissing = "ExceptionLogFieldMissing";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Reporting.Resources.SR", typeof(SR).Assembly);

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