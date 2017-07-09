#region Using Directives

using System.Globalization;
using System.Resources;

#endregion

namespace Loom.Data
{
    public class SR
    {
        public static string ExArgCanNotBeCollection => Keys.GetString(Keys.ExArgCanNotBeCollection);

        public static string ExArgCanNotBeEmptyString => Keys.GetString(Keys.ExArgCanNotBeEmptyString);

        public static string ExArgNotLessThanZero => Keys.GetString(Keys.ExArgNotLessThanZero);

        public static string ExDiffBaseLineAndWorkingTypeMismatch => Keys.GetString(Keys.ExDiffBaseLineAndWorkingTypeMismatch);

        public static string ExPrimaryKeyValueNotSet => Keys.GetString(Keys.ExPrimaryKeyValueNotSet);

        public static string ExSelectArgumentTableDoesNotMatchRecord => Keys.GetString(Keys.ExSelectArgumentTableDoesNotMatchRecord);

        public static string GetString(string key)
        {
            return Keys.GetString(key);
        }

        public static string ExFetchByIdPrimaryKeyNotDefinedInTable(string tableName)
        {
            return Keys.GetString(Keys.ExFetchByIdPrimaryKeyNotDefinedInTable, new object[]
            {
                tableName
            });
        }

        public static string ExNoPrimaryKeyDefinedInTable(string tableName)
        {
            return Keys.GetString(Keys.ExNoPrimaryKeyDefinedInTable, new object[]
            {
                tableName
            });
        }

        public static string ExOpenGroupsInWhere(int count)
        {
            return Keys.GetString(Keys.ExOpenGroupsInWhere, new object[]
            {
                count
            });
        }

        #region Nested type: Keys

        private class Keys
        {
            public const string ExArgCanNotBeCollection = "ExArgCanNotBeCollection";

            public const string ExArgCanNotBeEmptyString = "ExArgCanNotBeEmptyString";

            public const string ExArgNotLessThanZero = "ExArgNotLessThanZero";

            public const string ExDiffBaseLineAndWorkingTypeMismatch = "ExDiffBaseLineAndWorkingTypeMismatch";

            public const string ExFetchByIdPrimaryKeyNotDefinedInTable = "ExFetchByIdPrimaryKeyNotDefinedInTable";

            public const string ExNoPrimaryKeyDefinedInTable = "ExNoPrimaryKeyDefinedInTable";

            public const string ExOpenGroupsInWhere = "ExOpenGroupsInWhere";

            public const string ExPrimaryKeyValueNotSet = "ExPrimaryKeyValueNotSet";

            public const string ExSelectArgumentTableDoesNotMatchRecord = "ExSelectArgumentTableDoesNotMatchRecord";

            private static readonly ResourceManager resourceManager = new ResourceManager("Loom.Data.Resources.SR", typeof(SR).Assembly);

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