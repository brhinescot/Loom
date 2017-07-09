#region Using Directives

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Loom.Dynamic;

#endregion

namespace Loom.Data
{
    /// <summary>
    ///     A <see cref="DataTable" /> adapter.
    /// </summary>
    public class DataTableAdapter<T> where T : class
    {
        private readonly IList<ExtraColumn> columns;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTableAdapter{T}" /> class.
        /// </summary>
        public DataTableAdapter()
        {
            columns = new List<ExtraColumn>();
        }

        /// <summary>
        ///     Adds the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="valueGetter">The column value getter.</param>
        public void AddColumn(string name, Type type, Getter<T> valueGetter)
        {
            columns.Add(new ExtraColumn(name, type, valueGetter));
        }

        /// <summary>
        ///     Gets the data table.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public DataTable Create(T item)
        {
            Argument.Assert.IsNotNull(item, nameof(item));

            return Create(new List<T> {item});
        }

        /// <summary>
        ///     Gets the data table.
        /// </summary>
        /// <param name="items">The list.</param>
        /// <returns></returns>
        public DataTable Create(IEnumerable<T> items)
        {
            Argument.Assert.IsNotNull(items, nameof(items));

            DataTable table = new DataTable();

            //Get all the PropertyInfo's once.
            PropertyInfo[] property = typeof(T).GetProperties();
            PropertyGetter<T, object>[] getters = DynamicType<T>.CreateAllPropertyGetters();

            for (int i = 0; i < property.Length; i++)
            {
                Type propertyType = property[i].PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = Nullable.GetUnderlyingType(propertyType);

                table.Columns.Add(new DataColumn(property[i].Name, propertyType));
            }

            //Add all the extra columns
            for (int i = 0; i < columns.Count; i++)
                table.Columns.Add(columns[i].Name, columns[i].Type);

            //Add all the rows to the DataTable.
            foreach (T item in items)
            {
                object[] row = new object[getters.Length + columns.Count];
                //Get all the property values.
                for (int i = 0; i < getters.Length; i++)
                    row[i] = getters[i](item);

                //Get all the extra column values.
                for (int i = 0; i < columns.Count; i++)
                    row[getters.Length + i] = columns[i].ValueGetter(item);

                table.Rows.Add(row);
            }

            return table;
        }

        #region Nested type: ExtraColumn

        private sealed class ExtraColumn
        {
            public readonly string Name;
            public readonly Type Type;
            public readonly Getter<T> ValueGetter;

            public ExtraColumn(string name, Type type, Getter<T> valueGetter)
            {
                ValueGetter = valueGetter;
                Name = name;
                Type = type;
            }
        }

        #endregion
    }
}