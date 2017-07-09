#region Using Directives

using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AdventureWorks.Person;
using Loom.Data;
using Loom.Data.Mapping;
using Loom.Data.Mapping.Schema;

#endregion

namespace AdventureWorks.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.SalesTaxRate table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "SalesTaxRate", "SalesTaxRateID", ModifiedOnColumn = "ModifiedDate")]
    public class SalesTaxRate : DataRecord<SalesTaxRate>
    {
        private DateTime _modifiedDate;
        private string _name;
        private Guid _rowguid;

        private int _salesTaxRateId;
        private int _stateProvinceId;
        private decimal _taxRate;
        private short _taxType;

        public SalesTaxRate() { }
        protected SalesTaxRate(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for SalesTaxRate records.
        /// </summary>
        [ActiveColumn("SalesTaxRateID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int SalesTaxRateId
        {
            get => _salesTaxRateId;
            set
            {
                if (value == _salesTaxRateId && IsPropertyDirty("SalesTaxRateID"))
                    return;

                _salesTaxRateId = value;
                MarkDirty("SalesTaxRateID");
            }
        }

        /// <summary>
        ///     State, province, or country/region the sales tax applies to.
        /// </summary>
        [ActiveColumn("StateProvinceID", DbType.Int32, ColumnProperties.ForeignKey, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("StateProvinceID", typeof(StateProvince), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int StateProvinceId
        {
            get => _stateProvinceId;
            set
            {
                if (value == _stateProvinceId && IsPropertyDirty("StateProvinceID"))
                    return;

                _stateProvinceId = value;
                MarkDirty("StateProvinceID");
            }
        }

        /// <summary>
        ///     1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales
        ///     (retail and wholesale) transactions.
        /// </summary>
        [ActiveColumn("TaxType", DbType.Int16, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public short TaxType
        {
            get => _taxType;
            set
            {
                if (value == _taxType && IsPropertyDirty("TaxType"))
                    return;

                _taxType = value;
                MarkDirty("TaxType");
            }
        }

        /// <summary>
        ///     Tax rate amount.
        /// </summary>
        [ActiveColumn("TaxRate", DbType.Currency, ColumnProperties.None, Ordinal = 4, MaxLength = 0, DefaultValue = "((0.00))")]
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (value == _taxRate && IsPropertyDirty("TaxRate"))
                    return;

                _taxRate = value;
                MarkDirty("TaxRate");
            }
        }

        /// <summary>
        ///     Tax rate description.
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 50)]
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
            public static QueryColumn SalesTaxRateId => FetchColumn("SalesTaxRateID");

            public static QueryColumn StateProvinceId => FetchColumn("StateProvinceID");

            public static QueryColumn TaxType => FetchColumn("TaxType");

            public static QueryColumn TaxRate => FetchColumn("TaxRate");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Rowguid => FetchColumn("rowguid");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}