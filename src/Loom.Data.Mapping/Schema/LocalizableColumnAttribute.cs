#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LocalizableColumnAttribute : Attribute, ITableColumnDataProvider
    {
        /// <summary>
        ///     Initializes a new instanse of the <see cref="LocalizableColumnAttribute" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name of the column in the table that holds the
        ///     translated data.
        /// </param>
        /// <param name="dataRecordType">
        ///     The <see cref="Type" /> of the class that holds
        ///     the translated data.
        /// </param>
        public LocalizableColumnAttribute(string name, Type dataRecordType)
        {
            Name = name;
            DataRecordType = dataRecordType;
        }

        #region ITableColumnDataProvider Members

        /// <summary>
        ///     Gets the <see cref="Type" /> of the class that holds the translated data.
        /// </summary>
        public Type DataRecordType { get; }

        /// <summary>
        ///     Gets the name of the column in the table that holds the
        ///     translated data.
        /// </summary>
        public string Name { get; }

        public ITable GetTable()
        {
            return DataRecordType.IsEnum ? null : TableData.FromInitializedDataRecord(DataRecordType);
        }

        #endregion
    }
}