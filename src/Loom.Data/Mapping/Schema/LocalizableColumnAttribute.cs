#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LocalizableColumnAttribute : Attribute, ITableColumnDataProvider
    {
        #region Property Accessors

        /// <summary>
        /// Gets the <see cref="Type"/> of the class that holds the translated data.
        /// </summary>
        public Type DataRecordType { get; private set; }

        /// <summary>
        /// Gets the name of the column in the table that holds the 
        /// translated data.
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region .ctor

        /// <summary>
        /// Initializes a new instanse of the <see cref="LocalizableColumnAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the column in the table that holds the 
        /// translated data.</param>
        /// <param name="dataRecordType">The <see cref="Type"/> of the class that holds 
        /// the translated data.</param>
        public LocalizableColumnAttribute(string name, Type dataRecordType)
        {
            Name = name;
            DataRecordType = dataRecordType;
        }

        #endregion

        public TableData GetTable()
        {
            if (DataRecordType.IsEnum)
                return null;
            return TableData.FromInitializedDataRecord(DataRecordType);
        }
    }
}
