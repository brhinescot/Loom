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
    ///     This is an DataRecord class which wraps the Production.ProductInventory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductInventory", "ProductID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductInventory : DataRecord<ProductInventory>
    {
        private short _bin;
        private short _locationId;
        private DateTime _modifiedDate;

        private int _productId;
        private short _quantity;
        private Guid _rowguid;
        private string _shelf;

        public ProductInventory() { }
        protected ProductInventory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID.
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
        ///     Inventory location identification number. Foreign key to Location.LocationID.
        /// </summary>
        [ActiveColumn("LocationID", DbType.Int16, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("LocationID", typeof(Location), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int16)]
        public short LocationId
        {
            get => _locationId;
            set
            {
                if (value == _locationId && IsPropertyDirty("LocationID"))
                    return;

                _locationId = value;
                MarkDirty("LocationID");
            }
        }

        /// <summary>
        ///     Storage compartment within an inventory location.
        /// </summary>
        [ActiveColumn("Shelf", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 10)]
        public string Shelf
        {
            get => _shelf;
            set
            {
                if (value == _shelf && IsPropertyDirty("Shelf"))
                    return;

                _shelf = value;
                MarkDirty("Shelf");
            }
        }

        /// <summary>
        ///     Storage container on a shelf in an inventory location.
        /// </summary>
        [ActiveColumn("Bin", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short Bin
        {
            get => _bin;
            set
            {
                if (value == _bin && IsPropertyDirty("Bin"))
                    return;

                _bin = value;
                MarkDirty("Bin");
            }
        }

        /// <summary>
        ///     Quantity of products in the inventory location.
        /// </summary>
        [ActiveColumn("Quantity", DbType.Int16, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((0))")]
        public short Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity && IsPropertyDirty("Quantity"))
                    return;

                _quantity = value;
                MarkDirty("Quantity");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(newid())")]
        public Guid Rowguid
        {
            get => _rowguid;
            set
            {
                if (value == _rowguid && IsPropertyDirty("rowguid"))
                    return;

                _rowguid = value;
                MarkDirty("rowguid");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn LocationId => FetchColumn("LocationID");

            public static QueryColumn Shelf => FetchColumn("Shelf");

            public static QueryColumn Bin => FetchColumn("Bin");

            public static QueryColumn Quantity => FetchColumn("Quantity");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}