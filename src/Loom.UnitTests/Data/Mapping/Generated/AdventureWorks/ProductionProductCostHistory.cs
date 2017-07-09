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
    ///     This is an DataRecord class which wraps the Production.ProductCostHistory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductCostHistory", "StartDate", ModifiedOnColumn = "ModifiedDate")]
    public class ProductCostHistory : DataRecord<ProductCostHistory>
    {
        private DateTime? _endDate;
        private DateTime _modifiedDate;

        private int _productId;
        private decimal _standardCost;
        private DateTime _startDate;

        public ProductCostHistory() { }
        protected ProductCostHistory(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        ///     Product cost start date.
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
        ///     Product cost end date.
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
        ///     Standard cost of the product.
        /// </summary>
        [ActiveColumn("StandardCost", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public decimal StandardCost
        {
            get => _standardCost;
            set
            {
                if (value == _standardCost && IsPropertyDirty("StandardCost"))
                    return;

                _standardCost = value;
                MarkDirty("StandardCost");
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

            public static QueryColumn StandardCost => FetchColumn("StandardCost");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}