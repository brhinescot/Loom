#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.Category Sales for 1997 table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Category Sales for 1997", ReadOnly = true)]
    public class CategorySalesFor1997 : DataRecord<CategorySalesFor1997>
    {
        private string _categoryName;
        private decimal? _categorySales;

        public CategorySalesFor1997() { }
        protected CategorySalesFor1997(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategoryName", DbType.String, ColumnProperties.None, Ordinal = 1, MaxLength = 15)]
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                if (value == _categoryName)
                    return;

                _categoryName = value;
                MarkDirty("CategoryName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CategorySales", DbType.Currency, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public decimal? CategorySales
        {
            get => _categorySales;
            set
            {
                if (value == _categorySales)
                    return;

                _categorySales = value;
                MarkDirty("CategorySales");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CategoryName => FetchColumn("CategoryName");

            public static QueryColumn CategorySales => FetchColumn("CategorySales");
        }

        #endregion
    }
}