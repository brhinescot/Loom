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
    ///     This is an DataRecord class which wraps the dbo.Quarterly Orders table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Quarterly Orders", ReadOnly = true)]
    public class QuarterlyOrders : DataRecord<QuarterlyOrders>
    {
        private string _city;
        private string _companyName;
        private string _country;

        private string _customerId;

        public QuarterlyOrders() { }
        protected QuarterlyOrders(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerID", DbType.String, ColumnProperties.Nullable, Ordinal = 1, MaxLength = 5)]
        public string CustomerId
        {
            get => _customerId;
            set
            {
                if (value == _customerId)
                    return;

                _customerId = value;
                MarkDirty("CustomerID");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("CompanyName", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 40)]
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
        [ActiveColumn("City", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 15)]
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
        [ActiveColumn("Country", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 15)]
        public string Country
        {
            get => _country;
            set
            {
                if (value == _country)
                    return;

                _country = value;
                MarkDirty("Country");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CustomerId => FetchColumn("CustomerID");

            public static QueryColumn CompanyName => FetchColumn("CompanyName");

            public static QueryColumn City => FetchColumn("City");

            public static QueryColumn Country => FetchColumn("Country");
        }

        #endregion
    }
}