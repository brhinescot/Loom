#region Using Directives

using System;

#endregion

namespace Loom.Data.Mapping.Schema
{
    public interface ITableColumnDataProvider
    {
        /// <summary>
        ///     Gets the <see cref="Type" /> of the class that holds the translated data.
        /// </summary>
        Type DataRecordType { get; }

        /// <summary>
        ///     Gets the name of the column in the table that holds the
        ///     translated data.
        /// </summary>
        string Name { get; }

        ITable GetTable();
    }
}