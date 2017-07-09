#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Web.Services
{
    internal class SR
    {
        #region Nested type: Keys

        private class Keys
        {
            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Web.Services.SR", typeof(SR).Assembly);

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