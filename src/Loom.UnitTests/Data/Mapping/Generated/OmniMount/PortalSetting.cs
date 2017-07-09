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

namespace OmniMount.Portal
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Portal.Setting table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Portal", "Setting", "SettingId")]
    public class Setting : DataRecord<Setting>
    {
        private string _name;

        private int _settingId;
        private string _value;

        public Setting() { }
        protected Setting(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("SettingId", DbType.Int32, ColumnProperties.Identity | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int SettingId
        {
            get => _settingId;
            set
            {
                if (value == _settingId && IsPropertyDirty("SettingId"))
                    return;

                _settingId = value;
                MarkDirty("SettingId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
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
        /// </summary>
        [ActiveColumn("Value", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 5000)]
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
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn SettingId => FetchColumn("SettingId");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn Value => FetchColumn("Value");
        }

        #endregion
    }
}