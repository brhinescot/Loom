#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Drawing
{
    internal class SR
    {
        public static string ThumbnailHeightOutOfRange => Keys.GetString(Keys.ThumbnailHeightOutOfRange);

        public static string ThumbnailSizeOutOfRange => Keys.GetString(Keys.ThumbnailSizeOutOfRange);

        public static string ThumbnailWidthOutOfRange => Keys.GetString(Keys.ThumbnailWidthOutOfRange);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ThumbnailHeightOutOfRange = "ThumbnailHeightOutOfRange";

            public const string ThumbnailSizeOutOfRange = "ThumbnailSizeOutOfRange";

            public const string ThumbnailWidthOutOfRange = "ThumbnailWidthOutOfRange";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Drawing.Resources.SR", typeof(SR).Assembly);

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