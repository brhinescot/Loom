#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    /// <summary>
    ///     A class for manipulating the case and spacing of strings.
    /// </summary>
    public static class CodeFormat
    {
        public static string ToPascalCase(string value)
        {
            return ToPascalCase(value, CodeFormatOptions.None);
        }

        public static string ToPascalCase(string value, CodeFormatOptions formatOptions)
        {
            Argument.Assert.IsNotNull(value, nameof(value));
            return PreFormatCode(value, formatOptions).ToPascalCase();
        }

        public static string ToProperCase(string columnName)
        {
            Argument.Assert.IsNotNull(columnName, nameof(columnName));
            return columnName.ToProperCase();
        }

        public static string ToCamelCase(string value)
        {
            return ToCamelCase(value, CodeFormatOptions.None);
        }

        public static string ToCamelCase(string value, CodeFormatOptions formatOptions)
        {
            Argument.Assert.IsNotNull(value, nameof(value));
            return PreFormatCode(value, formatOptions).ToCamelCase();
        }

        private static string PreFormatCode(string value, CodeFormatOptions formatOptions)
        {
            bool removeKeyIndicators = (formatOptions & CodeFormatOptions.RemoveFKPrefix) == CodeFormatOptions.RemoveFKPrefix;
            bool removeTblPrefix = (formatOptions & CodeFormatOptions.RemoveTblPrefix) == CodeFormatOptions.RemoveTblPrefix;

            if (removeKeyIndicators && value.StartsWith("FK_", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(3) + "Id";
            else if (removeKeyIndicators && value.StartsWith("FK", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(2) + "Id";

            if (removeTblPrefix && value.StartsWith("tbl_", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(4);
            else if (removeTblPrefix && value.StartsWith("tbl", StringComparison.OrdinalIgnoreCase))
                value = value.Substring(3);
            return value;
        }
    }
}