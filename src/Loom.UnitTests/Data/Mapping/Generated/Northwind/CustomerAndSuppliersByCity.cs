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

namespace Northwind
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.Customer and Suppliers by City table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Customer and Suppliers by City", ReadOnly = true)]
    public class CustomerAndSuppliersByCity : DataRecord<CustomerAndSuppliersByCity>
    {
        private string _city;
        private string _companyName;
        private string _contactName;
        private string _relationship;

        public CustomerAndSuppliersByCity() { }
        protected CustomerAndSuppliersByCity(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 15)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city)
                    return;

                _city = value;
                MarkDirty("City");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyName", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 40)]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (value == _companyName)
                    return;

                _companyName = value;
                MarkDirty("CompanyName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ContactName", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 30)]
        public string ContactName
        {
            get => _contactName;
            set
            {
                if (value == _contactName)
                    return;

                _contactName = value;
                MarkDirty("ContactName");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Relationship", DbType.String, ColumnProperties.None, Ordinal = 4, MaxLength = 9)]
        public string Relationship
        {
            get => _relationship;
            set
            {
                if (value == _relationship)
                    return;

                _relationship = value;
                MarkDirty("Relationship");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn CompanyName => FetchColumn("CompanyName");

            public static QueryColumn ContactName => FetchColumn("ContactName");

            public static QueryColumn Relationship => FetchColumn("Relationship");
        }

        #endregion
    }
}