#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Production;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.ShoppingCartItem table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "ShoppingCartItem", "ShoppingCartItemID", ModifiedOnColumn = "ModifiedDate")]
    public class ShoppingCartItem : DataRecord<ShoppingCartItem>
    {
        private DateTime _dateCreated;
        private DateTime _modifiedDate;
        private int _productId;
        private int _quantity;
        private string _shoppingCartId;

        private int _shoppingCartItemId;

        public ShoppingCartItem() { }
        protected ShoppingCartItem(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for ShoppingCartItem records.
        /// </summary>
        [ActiveColumn("ShoppingCartItemID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ShoppingCartItemId
        {
            get => _shoppingCartItemId;
            set
            {
                if (value == _shoppingCartItemId && IsPropertyDirty("ShoppingCartItemID"))
                    return;

                _shoppingCartItemId = value;
                MarkDirty("ShoppingCartItemID");
            }
        }

        /// <summary>
        ///     Shopping cart identification number.
        /// </summary>
        [ActiveColumn("ShoppingCartID", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        public string ShoppingCartId
        {
            get => _shoppingCartId;
            set
            {
                if (value == _shoppingCartId && IsPropertyDirty("ShoppingCartID"))
                    return;

                _shoppingCartId = value;
                MarkDirty("ShoppingCartID");
            }
        }

        /// <summary>
        ///     Product quantity ordered.
        /// </summary>
        [ActiveColumn("Quantity", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((1))")]
        public int Quantity
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
        ///     Product ordered. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 4, MaxLength = 0)]
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
        ///     Date the time the record was created.
        /// </summary>
        [ActiveColumn("DateCreated", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                if (value == _dateCreated && IsPropertyDirty("DateCreated"))
                    return;

                _dateCreated = value;
                MarkDirty("DateCreated");
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
            public static QueryColumn ShoppingCartItemId => FetchColumn("ShoppingCartItemID");

            public static QueryColumn ShoppingCartId => FetchColumn("ShoppingCartID");

            public static QueryColumn Quantity => FetchColumn("Quantity");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn DateCreated => FetchColumn("DateCreated");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}