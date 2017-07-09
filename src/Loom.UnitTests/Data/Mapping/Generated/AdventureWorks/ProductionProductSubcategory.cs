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
    ///     This is an DataRecord class which wraps the Production.ProductSubcategory table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductSubcategory", "ProductSubcategoryID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductSubcategory : DataRecord<ProductSubcategory>
    {
        private DateTime _modifiedDate;
        private string _name;
        private int _productCategoryId;

        private int _productSubcategoryId;
        private Guid _rowguid;

        public ProductSubcategory() { }
        protected ProductSubcategory(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ProductSubcategory records.
        /// </summary>
        [ActiveColumn("ProductSubcategoryID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ProductSubcategoryId
        {
            get => _productSubcategoryId;
            set
            {
                if (value == _productSubcategoryId && IsPropertyDirty("ProductSubcategoryID"))
                    return;

                _productSubcategoryId = value;
                MarkDirty("ProductSubcategoryID");
            }
        }

        /// <summary>
        ///     Product category identification number. Foreign key to ProductCategory.ProductCategoryID.
        /// </summary>
        [ActiveColumn("ProductCategoryID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("ProductCategoryID", typeof(ProductCategory), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int ProductCategoryId
        {
            get => _productCategoryId;
            set
            {
                if (value == _productCategoryId && IsPropertyDirty("ProductCategoryID"))
                    return;

                _productCategoryId = value;
                MarkDirty("ProductCategoryID");
            }
        }

        /// <summary>
        ///     Subcategory description.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name && IsPropertyDirty("Name"))
                    return;

                _name = value;
                MarkDirty("Name");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "(newid())")]
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
            public static QueryColumn ProductSubcategoryId => FetchColumn("ProductSubcategoryID");

            public static QueryColumn ProductCategoryId => FetchColumn("ProductCategoryID");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}