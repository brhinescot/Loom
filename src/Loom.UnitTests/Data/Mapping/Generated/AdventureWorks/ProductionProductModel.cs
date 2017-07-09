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
    ///     This is an DataRecord class which wraps the Production.ProductModel table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductModel", "ProductModelID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductModel : DataRecord<ProductModel>
    {
        private string _catalogDescription;
        private string _instructions;
        private DateTime _modifiedDate;
        private string _name;

        private int _productModelId;
        private Guid _rowguid;

        public ProductModel() { }
        protected ProductModel(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ProductModel records.
        /// </summary>
        [ActiveColumn("ProductModelID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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
        ///     Product model description.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
        ///     Detailed product catalog information in xml format.
        /// </summary>
        [ActiveColumn("CatalogDescription", DbType.String, ColumnProperties.Nullable, Ordinal = 3)]
        public string CatalogDescription
        {
            get => _catalogDescription;
            set
            {
                if (value == _catalogDescription && IsPropertyDirty("CatalogDescription"))
                    return;

                _catalogDescription = value;
                MarkDirty("CatalogDescription");
            }
        }

        /// <summary>
        ///     Manufacturing instructions in xml format.
        /// </summary>
        [ActiveColumn("Instructions", DbType.String, ColumnProperties.Nullable, Ordinal = 4)]
        public string Instructions
        {
            get => _instructions;
            set
            {
                if (value == _instructions && IsPropertyDirty("Instructions"))
                    return;

                _instructions = value;
                MarkDirty("Instructions");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn CatalogDescription => FetchColumn("CatalogDescription");

            public static QueryColumn Instructions => FetchColumn("Instructions");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}