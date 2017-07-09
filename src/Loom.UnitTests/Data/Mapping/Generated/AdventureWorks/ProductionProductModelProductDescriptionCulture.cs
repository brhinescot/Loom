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
    ///     This is an DataRecord class which wraps the Production.ProductModelProductDescriptionCulture table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductModelProductDescriptionCulture", "ProductModelID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductModelProductDescriptionCulture : DataRecord<ProductModelProductDescriptionCulture>
    {
        private string _cultureId;
        private DateTime _modifiedDate;
        private int _productDescriptionId;

        private int _productModelId;

        public ProductModelProductDescriptionCulture() { }
        protected ProductModelProductDescriptionCulture(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key. Foreign key to ProductModel.ProductModelID.
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("ProductModelID", typeof(ProductModel), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductModelId
        {
            get => _productModelId;
            set
            {
                if (value == _productModelId && IsPropertyDirty("ProductModelID"))
                    return;

                _productModelId = value;
                MarkDirty("ProductModelID");
            }
        }

        /// <summary>
        ///     Primary key. Foreign key to ProductDescription.ProductDescriptionID.
        /// </summary>
        [ActiveColumn("ProductDescriptionID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductDescriptionID", typeof(ProductDescription), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductDescriptionId
        {
            get => _productDescriptionId;
            set
            {
                if (value == _productDescriptionId && IsPropertyDirty("ProductDescriptionID"))
                    return;

                _productDescriptionId = value;
                MarkDirty("ProductDescriptionID");
            }
        }

        /// <summary>
        ///     Culture identification number. Foreign key to Culture.CultureID.
        /// </summary>
        [ActiveColumn("CultureID", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 3, MaxLength = 6)]
        [ForeignColumn("CultureID", typeof(Culture), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 6, DbType = DbType.String)]
        public string CultureId
        {
            get => _cultureId;
            set
            {
                if (value == _cultureId && IsPropertyDirty("CultureID"))
                    return;

                _cultureId = value;
                MarkDirty("CultureID");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn ProductModelId => FetchColumn("ProductModelID");

            public static QueryColumn ProductDescriptionId => FetchColumn("ProductDescriptionID");

            public static QueryColumn CultureId => FetchColumn("CultureID");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}