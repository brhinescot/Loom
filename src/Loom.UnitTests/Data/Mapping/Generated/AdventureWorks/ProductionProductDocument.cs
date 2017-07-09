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
    ///     This is an DataRecord class which wraps the Production.ProductDocument table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "ProductDocument", "ProductID", ModifiedOnColumn = "ModifiedDate")]
    public class ProductDocument : DataRecord<ProductDocument>
    {
        private string _documentNode;
        private DateTime _modifiedDate;

        private int _productId;

        public ProductDocument() { }
        protected ProductDocument(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
        ///     Document identification number. Foreign key to Document.DocumentNode.
        /// </summary>
        [ActiveColumn("DocumentNode", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 892)]
        [ForeignColumn("DocumentNode", typeof(Document), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 892, DbType = DbType.String)]
        public string DocumentNode
        {
            get => _documentNode;
            set
            {
                if (value == _documentNode && IsPropertyDirty("DocumentNode"))
                    return;

                _documentNode = value;
                MarkDirty("DocumentNode");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "(getdate())")]
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

            public static QueryColumn DocumentNode => FetchColumn("DocumentNode");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}