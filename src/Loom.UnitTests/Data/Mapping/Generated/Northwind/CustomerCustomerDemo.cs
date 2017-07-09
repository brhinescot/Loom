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
    ///     This is an DataRecord class which wraps the dbo.CustomerCustomerDemo table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "CustomerCustomerDemo", "CustomerTypeID")]
    public class CustomerCustomerDemo : DataRecord<CustomerCustomerDemo>
    {
        private string _customerId;
        private string _customerTypeId;

        public CustomerCustomerDemo() { }
        protected CustomerCustomerDemo(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerID", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 5)]
        [ForeignColumn("CustomerID", typeof(Customers), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 5, DbType = DbType.String)]
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
        [ActiveColumn("CustomerTypeID", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 10)]
        [ForeignColumn("CustomerTypeID", typeof(CustomerDemographics), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 10, DbType = DbType.String)]
        public string CustomerTypeId
        {
            get => _customerTypeId;
            set
            {
                if (value == _customerTypeId)
                    return;

                _customerTypeId = value;
                MarkDirty("CustomerTypeID");
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

            public static QueryColumn CustomerTypeId => FetchColumn("CustomerTypeID");
        }

        #endregion
    }
}