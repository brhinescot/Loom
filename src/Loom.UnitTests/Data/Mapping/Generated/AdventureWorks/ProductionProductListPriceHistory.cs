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

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Production.ProductListPriceHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductListPriceHistory", "StartDate", ModifiedOnColumn = "ModifiedDate")]
    public class ProductListPriceHistory : DataRecord<ProductListPriceHistory>
    {
        private DateTime? _endDate;
        private decimal _listPrice;
        private DateTime _modifiedDate;

        private int _productId;
        private DateTime _startDate;

        public ProductListPriceHistory() { }
        protected ProductListPriceHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductID", typeof(Product), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId && IsPropertyDirty("ProductID"))
                    return;

                _productId = value;
                MarkDirty("ProductID");
            }
        }

        /// <summary>
        ///     List price start date.
        /// </summary>
        [ActiveColumn("StartDate", DbType.DateTime, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value == _startDate && IsPropertyDirty("StartDate"))
                    return;

                _startDate = value;
                MarkDirty("StartDate");
            }
        }

        /// <summary>
        ///     List price end date
        /// </summary>
        [ActiveColumn("EndDate", DbType.DateTime, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (value == _endDate && IsPropertyDirty("EndDate"))
                    return;

                _endDate = value;
                MarkDirty("EndDate");
            }
        }

        /// <summary>
        ///     Product list price.
        /// </summary>
        [ActiveColumn("ListPrice", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public decimal ListPrice
        {
            get => _listPrice;
            set
            {
                if (value == _listPrice && IsPropertyDirty("ListPrice"))
                    return;

                _listPrice = value;
                MarkDirty("ListPrice");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime ModifiedDate
        {
            get => _modifiedDate;
            set
            {
                if (value == _modifiedDate && IsPropertyDirty("ModifiedDate"))
                    return;

                _modifiedDate = value;
                MarkDirty("ModifiedDate");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn ListPrice => FetchColumn("ListPrice");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}