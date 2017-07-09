#region Using Directives

using System;

#endregion

namespace Loom.Data.Diff
{
    public class FormattedProperty
    {
        public FormattedProperty(string propertyName, string format) : this(propertyName, format, null) { }

        public FormattedProperty(string propertyName, string format, IFormatProvider formatProvider)
        {
            PropertyName = propertyName;
            Format = format;
            FormatProvider = formatProvider;
        }

        public string PropertyName { get; }

        public string Format { get; }

        public IFormatProvider FormatProvider { get; }
    }
}