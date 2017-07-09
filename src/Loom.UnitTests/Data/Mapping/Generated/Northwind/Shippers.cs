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
    ///     This is an DataRecord class which wraps the dbo.Shippers table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "Shippers", "ShipperID")]
    public class Shippers : DataRecord<Shippers>
    {
        private string _companyName;
        private string _phone;

        private int _shipperId;

        public Shippers() { }
        protected Shippers(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("ShipperID", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int ShipperId
        {
            get => _shipperId;
            set
            {
                if (value == _shipperId)
                    return;

                _shipperId = value;
                MarkDirty("ShipperID");
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
        [ActiveColumn("Phone", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 24)]
        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone)
                    return;

                _phone = value;
                MarkDirty("Phone");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn ShipperId => FetchColumn("ShipperID");

            public static QueryColumn CompanyName => FetchColumn("CompanyName");

            public static QueryColumn Phone => FetchColumn("Phone");
        }

        #endregion
    }
}