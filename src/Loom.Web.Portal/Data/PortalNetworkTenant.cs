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

namespace Loom.Web.Portal.Data.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.NetworkTenant table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "NetworkTenant", "TenantId")]
    public class NetworkTenant : DataRecord<NetworkTenant>
    {
        private int _networkId;
        private int _tenantId;

        public NetworkTenant() { }
        protected NetworkTenant(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("NetworkId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        [ForeignColumn("NetworkId", typeof(Network), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int NetworkId
        {
            get => _networkId;
            set
            {
                if (value == _networkId && IsPropertyDirty("NetworkId"))
                    return;

                _networkId = value;
                MarkDirty("NetworkId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("TenantId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("TenantId", typeof(Tenant), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int TenantId
        {
            get => _tenantId;
            set
            {
                if (value == _tenantId && IsPropertyDirty("TenantId"))
                    return;

                _tenantId = value;
                MarkDirty("TenantId");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn NetworkId => FetchColumn("NetworkId");

            public static QueryColumn TenantId => FetchColumn("TenantId");
        }

        #endregion
    }
}