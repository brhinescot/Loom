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
    ///     This is an DataRecord class which wraps the dbo.CustomerDemographics table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "CustomerDemographics", "CustomerTypeID")]
    public class CustomerDemographics : DataRecord<CustomerDemographics>
    {
        private string _customerDesc;

        private string _customerTypeId;

        public CustomerDemographics() { }
        protected CustomerDemographics(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerTypeID", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 10)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("CustomerDesc", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 1073741823)]
        public string CustomerDesc
        {
            get => _customerDesc;
            set
            {
                if (value == _customerDesc)
                    return;

                _customerDesc = value;
                MarkDirty("CustomerDesc");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn CustomerTypeId => FetchColumn("CustomerTypeID");

            public static QueryColumn CustomerDesc => FetchColumn("CustomerDesc");
        }

        #endregion
    }
}