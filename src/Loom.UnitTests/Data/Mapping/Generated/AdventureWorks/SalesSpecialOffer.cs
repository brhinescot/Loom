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

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SpecialOffer table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SpecialOffer", "SpecialOfferID", ModifiedOnColumn = "ModifiedDate")]
    public class SpecialOffer : DataRecord<SpecialOffer>
    {
        private string _category;
        private string _description;
        private decimal _discountPct;
        private DateTime _endDate;
        private int? _maxQty;
        private int _minQty;
        private DateTime _modifiedDate;
        private Guid _rowguid;

        private int _specialOfferId;
        private DateTime _startDate;
        private string _type;

        public SpecialOffer() { }
        protected SpecialOffer(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for SpecialOffer records.
        /// </summary>
        [ActiveColumn("SpecialOfferID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int SpecialOfferId
        {
            get => _specialOfferId;
            set
            {
                if (value == _specialOfferId && IsPropertyDirty("SpecialOfferID"))
                    return;

                _specialOfferId = value;
                MarkDirty("SpecialOfferID");
            }
        }

        /// <summary>
        ///     Discount description.
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 255)]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description && IsPropertyDirty("Description"))
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        /// <summary>
        ///     Discount precentage.
        /// </summary>
        [ActiveColumn("DiscountPct", DbType.Currency, ColumnProperties.None, Ordinal = 3, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal DiscountPct
        {
            get => _discountPct;
            set
            {
                if (value == _discountPct && IsPropertyDirty("DiscountPct"))
                    return;

                _discountPct = value;
                MarkDirty("DiscountPct");
            }
        }

        /// <summary>
        ///     Discount type category.
        /// </summary>
        [ActiveColumn("Type", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 50)]
        public string Type
        {
            get => _type;
            set
            {
                if (value == _type && IsPropertyDirty("Type"))
                    return;

                _type = value;
                MarkDirty("Type");
            }
        }

        /// <summary>
        ///     Group the discount applies to such as Reseller or Customer.
        /// </summary>
        [ActiveColumn("Category", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
        public string Category
        {
            get => _category;
            set
            {
                if (value == _category && IsPropertyDirty("Category"))
                    return;

                _category = value;
                MarkDirty("Category");
            }
        }

        /// <summary>
        ///     Discount start date.
        /// </summary>
        [ActiveColumn("StartDate", DbType.DateTime, ColumnProperties.None, Ordinal = 6, MaxLength = 0)]
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
        ///     Discount end date.
        /// </summary>
        [ActiveColumn("EndDate", DbType.DateTime, ColumnProperties.None, Ordinal = 7, MaxLength = 0)]
        public DateTime EndDate
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
        ///     Minimum discount percent allowed.
        /// </summary>
        [ActiveColumn("MinQty", DbType.Int32, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "((0))")]
        public int MinQty
        {
            get => _minQty;
            set
            {
                if (value == _minQty && IsPropertyDirty("MinQty"))
                    return;

                _minQty = value;
                MarkDirty("MinQty");
            }
        }

        /// <summary>
        ///     Maximum discount percent allowed.
        /// </summary>
        [ActiveColumn("MaxQty", DbType.Int32, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 0)]
        public int? MaxQty
        {
            get => _maxQty;
            set
            {
                if (value == _maxQty && IsPropertyDirty("MaxQty"))
                    return;

                _maxQty = value;
                MarkDirty("MaxQty");
            }
        }

        /// <summary>
        ///     ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        [ActiveColumn("rowguid", DbType.Guid, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "(newid())")]
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
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 11, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn SpecialOfferId => FetchColumn("SpecialOfferID");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn DiscountPct => FetchColumn("DiscountPct");

            public static QueryColumn Type => FetchColumn("Type");

            public static QueryColumn Category => FetchColumn("Category");

            public static QueryColumn StartDate => FetchColumn("StartDate");

            public static QueryColumn EndDate => FetchColumn("EndDate");

            public static QueryColumn MinQty => FetchColumn("MinQty");

            public static QueryColumn MaxQty => FetchColumn("MaxQty");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}