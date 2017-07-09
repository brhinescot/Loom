#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Net
{
    internal class SR
    {
        public static string Pop3ErrorUserAlreadyAuthenticated => Keys.GetString(Keys.Pop3ErrorUserAlreadyAuthenticated);

        public static string Pop3ErrorUsernameAlreadySpecfied => Keys.GetString(Keys.Pop3ErrorUsernameAlreadySpecfied);

        public static string Pop3ErrorUserNoUsername => Keys.GetString(Keys.Pop3ErrorUserNoUsername);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        public static string Pop3ErrorUserAlreadyLoggedIn(string username)
        {
            return Keys.GetString(Keys.Pop3ErrorUserAlreadyLoggedIn, new object[]
            {
                username
            });
        }

        public static string Pop3OkUsernameOk(string username)
        {
            return Keys.GetString(Keys.Pop3OkUsernameOk, new object[]
            {
                username
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string Pop3ErrorUserAlreadyAuthenticated = "Pop3ErrorUserAlreadyAuthenticated";

            public const string Pop3ErrorUserAlreadyLoggedIn = "Pop3ErrorUserAlreadyLoggedIn";

            public const string Pop3ErrorUsernameAlreadySpecfied = "Pop3ErrorUsernameAlreadySpecfied";

            public const string Pop3ErrorUserNoUsername = "Pop3ErrorUserNoUsername";

            public const string Pop3OkUsernameOk = "Pop3OkUsernameOk";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Net.Resources.SR", typeof(SR).Assembly);

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