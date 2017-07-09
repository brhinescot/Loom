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
    ///     This is an DataRecord class which wraps the Portal.Setting table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Setting", "TenantId")]
    public class Setting : DataRecord<Setting>
    {
        private int _settingFieldId;

        private int _tenantId;
        private string _value;

        public Setting() { }
        protected Setting(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("TenantId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("SettingFieldId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("SettingFieldId", typeof(SettingField), ColumnProperties = ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
        public int SettingFieldId
        {
            get => _settingFieldId;
            set
            {
                if (value == _settingFieldId && IsPropertyDirty("SettingFieldId"))
                    return;

                _settingFieldId = value;
                MarkDirty("SettingFieldId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Value", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 100)]
        public string Value
        {
            get => _value;
            set
            {
                if (value == _value && IsPropertyDirty("Value"))
                    return;

                _value = value;
                MarkDirty("Value");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.colossusinteractive.com/ns/frameworks/loom/data/mapping", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn TenantId => FetchColumn("TenantId");

            public static QueryColumn SettingFieldId => FetchColumn("SettingFieldId");

            public static QueryColumn Value => FetchColumn("Value");
        }

        #endregion
    }
}