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

namespace OmniMount.Sales
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Sales.CompanyAddress table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Sales", "CompanyAddress")]
    public class CompanyAddress : DataRecord<CompanyAddress>
    {
        private int _addressId;

        private int _companyId;

        public CompanyAddress() { }
        protected CompanyAddress(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("CompanyId", typeof(Company), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int CompanyId
        {
            get => _companyId;
            set
            {
                if (value == _companyId && IsPropertyDirty("CompanyId"))
                    return;

                _companyId = value;
                MarkDirty("CompanyId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("AddressId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("AddressId", typeof(Address), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int AddressId
        {
            get => _addressId;
            set
            {
                if (value == _addressId && IsPropertyDirty("AddressId"))
                    return;

                _addressId = value;
                MarkDirty("AddressId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CompanyId => FetchColumn("CompanyId");

            public static QueryColumn AddressId => FetchColumn("AddressId");
        }

        #endregion
    }
}