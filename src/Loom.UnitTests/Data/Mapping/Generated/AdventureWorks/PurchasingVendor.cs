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

namespace AdventureWorks.Purchasing
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Purchasing.Vendor table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Purchasing", "Vendor", "BusinessEntityID", ModifiedOnColumn = "ModifiedDate")]
    public class Vendor : DataRecord<Vendor>
    {
        private string _accountNumber;
        private bool _activeFlag;

        private int _businessEntityId;
        private short _creditRating;
        private DateTime _modifiedDate;
        private string _name;
        private bool _preferredVendorStatus;
        private string _purchasingWebServiceURL;

        public Vendor() { }
        protected Vendor(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        ///     Primary key for Vendor records.  Foreign key to BusinessEntity.BusinessEntityID
        /// </summary>
        [ActiveColumn("BusinessEntityID", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("BusinessEntityID", typeof(BusinessEntity), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int BusinessEntityId
        {
            get => _businessEntityId;
            set
            {
                if (value == _businessEntityId && IsPropertyDirty("BusinessEntityID"))
                    return;

                _businessEntityId = value;
                MarkDirty("BusinessEntityID");
            }
        }

        /// <summary>
        ///     Vendor account (identification) number.
        /// </summary>
        [ActiveColumn("AccountNumber", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 15)]
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value == _accountNumber && IsPropertyDirty("AccountNumber"))
                    return;

                _accountNumber = value;
                MarkDirty("AccountNumber");
            }
        }

        /// <summary>
        ///     Company name.
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
        ///     1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average
        /// </summary>
        [ActiveColumn("CreditRating", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short CreditRating
        {
            get => _creditRating;
            set
            {
                if (value == _creditRating && IsPropertyDirty("CreditRating"))
                    return;

                _creditRating = value;
                MarkDirty("CreditRating");
            }
        }

        /// <summary>
        ///     0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.
        /// </summary>
        [ActiveColumn("PreferredVendorStatus", DbType.Boolean, ColumnProperties.None, Ordinal = 5, MaxLength = 0, DefaultValue = "((1))")]
        public bool PreferredVendorStatus
        {
            get => _preferredVendorStatus;
            set
            {
                if (value == _preferredVendorStatus && IsPropertyDirty("PreferredVendorStatus"))
                    return;

                _preferredVendorStatus = value;
                MarkDirty("PreferredVendorStatus");
            }
        }

        /// <summary>
        ///     0 = Vendor no longer used. 1 = Vendor is actively used.
        /// </summary>
        [ActiveColumn("ActiveFlag", DbType.Boolean, ColumnProperties.None, Ordinal = 6, MaxLength = 0, DefaultValue = "((1))")]
        public bool ActiveFlag
        {
            get => _activeFlag;
            set
            {
                if (value == _activeFlag && IsPropertyDirty("ActiveFlag"))
                    return;

                _activeFlag = value;
                MarkDirty("ActiveFlag");
            }
        }

        /// <summary>
        ///     Vendor URL.
        /// </summary>
        [ActiveColumn("PurchasingWebServiceURL", DbType.String, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 1024)]
        public string PurchasingWebServiceURL
        {
            get => _purchasingWebServiceURL;
            set
            {
                if (value == _purchasingWebServiceURL && IsPropertyDirty("PurchasingWebServiceURL"))
                    return;

                _purchasingWebServiceURL = value;
                MarkDirty("PurchasingWebServiceURL");
            }
        }

        /// <summary>
        ///     Date and time the record was last updated.
        /// </summary>
        [ActiveColumn("ModifiedDate", DbType.DateTime, ColumnProperties.None, Ordinal = 8, MaxLength = 0, DefaultValue = "(getdate())")]
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
            public static QueryColumn BusinessEntityId => FetchColumn("BusinessEntityID");

            public static QueryColumn AccountNumber => FetchColumn("AccountNumber");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn CreditRating => FetchColumn("CreditRating");

            public static QueryColumn PreferredVendorStatus => FetchColumn("PreferredVendorStatus");

            public static QueryColumn ActiveFlag => FetchColumn("ActiveFlag");

            public static QueryColumn PurchasingWebServiceURL => FetchColumn("PurchasingWebServiceURL");

            public static QueryColumn ModifiedDate => FetchColumn("ModifiedDate");
        }

        #endregion
    }
}