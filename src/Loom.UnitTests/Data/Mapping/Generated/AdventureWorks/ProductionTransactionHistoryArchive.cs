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
    ///     This is an DataRecord class which wraps the Production.TransactionHistoryArchive table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Production", "TransactionHistoryArchive", "TransactionID", ModifiedOnColumn = "ModifiedDate")]
    public class TransactionHistoryArchive : DataRecord<TransactionHistoryArchive>
    {
        private decimal _actualCost;
        private DateTime _modifiedDate;
        private int _productId;
        private int _quantity;
        private int _referenceOrderId;
        private int _referenceOrderLineId;
        private DateTime _transactionDate;

        private int _transactionId;
        private string _transactionType;

        public TransactionHistoryArchive() { }
        protected TransactionHistoryArchive(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for TransactionHistoryArchive records.
        /// </summary>
        [ActiveColumn("TransactionID", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int TransactionId
        {
            get => _transactionId;
            set
            {
                if (value == _transactionId && IsPropertyDirty("TransactionID"))
                    return;

                _transactionId = value;
                MarkDirty("TransactionID");
            }
        }

        /// <summary>
        ///     Product identification number. Foreign key to Product.ProductID.
        /// </summary>
        [ActiveColumn("ProductID", DbType.Int32, ColumnProperties.None, Ordinal = 2, MaxLength = 0)]
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
        ///     Purchase order, sales order, or work order identification number.
        /// </summary>
        [ActiveColumn("ReferenceOrderID", DbType.Int32, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public int ReferenceOrderId
        {
            get => _referenceOrderId;
            set
            {
                if (value == _referenceOrderId && IsPropertyDirty("ReferenceOrderID"))
                    return;

                _referenceOrderId = value;
                MarkDirty("ReferenceOrderID");
            }
        }

        /// <summary>
        ///     Line number associated with the purchase order, sales order, or work order.
        /// </summary>
        [ActiveColumn("ReferenceOrderLineID", DbType.Int32, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0))")]
        public int ReferenceOrderLineId
        {
            get => _referenceOrderLineId;
            set
            {
                if (value == _referenceOrderLineId && IsPropertyDirty("ReferenceOrderLineID"))
                    return;

                _referenceOrderLineId = value;
                MarkDirty("ReferenceOrderLineID");
            }
        }

        /// <summary>
        ///     Date and time of the transaction.
        /// </summary>
        [ActiveColumn("TransactionDate", DbType.DateTime, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime TransactionDate
        {
            get => _transactionDate;
            set
            {
                if (value == _transactionDate && IsPropertyDirty("TransactionDate"))
                    return;

                _transactionDate = value;
                MarkDirty("TransactionDate");
            }
        }

        /// <summary>
        ///     W = Work Order, S = Sales Order, P = Purchase Order
        /// </summary>
        [ActiveColumn("TransactionType", DbType.String, ColumnProperties.None, Ordinal = 6, MaxLength = 1)]
        public string TransactionType
        {
            get => _transactionType;
            set
            {
                if (value == _transactionType && IsPropertyDirty("TransactionType"))
                    return;

                _transactionType = value;
                MarkDirty("TransactionType");
            }
        }

        /// <summary>
        ///     Product quantity.
        /// </summary>
        [ActiveColumn("Quantity", DbType.Int32, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
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
        ///     Product cost.
        /// </summary>
        [ActiveColumn("ActualCost", DbType.Currency, ColumnProperties.None, Ordinal = 8, MaxLength = 0)]
        public decimal ActualCost
        {
            get => _actualCost;
            set
            {
                if (value == _actualCost && IsPropertyDirty("ActualCost"))
                    return;

                _actualCost = value;
                MarkDirty("ActualCost");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 9, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn TransactionId => FetchColumn("TransactionID");

            public static QueryColumn ProductId => FetchColumn("ProductID");

            public static QueryColumn ReferenceOrderId => FetchColumn("ReferenceOrderID");

            public static QueryColumn ReferenceOrderLineId => FetchColumn("ReferenceOrderLineID");

            public static QueryColumn TransactionDate => FetchColumn("TransactionDate");

            public static QueryColumn TransactionType => FetchColumn("TransactionType");

            public static QueryColumn Quantity => FetchColumn("Quantity");

            public static QueryColumn ActualCost => FetchColumn("ActualCost");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}