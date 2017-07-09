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

namespace AdventureWorks.Test
{
    /// <summary>
    ///     This is an DataRecord class which wraps the Test.Mount table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Test", "Mount", "MountId")]
    public class Mount : DataRecord<Mount>
    {
        private string _description;
        private int _mountId;
        private string _name;

        public Mount() { }
        protected Mount(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 50)]
        [LocalizableColumn("Description", typeof(MountTranslations))]
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description)
                    return;

                _description = value;
                MarkDirty("Description");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("MountId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int MountId
        {
            get => _mountId;
            set
            {
                if (value == _mountId)
                    return;

                _mountId = value;
                MarkDirty("MountId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 50)]
        [LocalizableColumn("Name", typeof(MountTranslations))]
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;

                _name = value;
                MarkDirty("Name");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn MountId => FetchColumn("MountId");

            public static QueryColumn Name => FetchColumn("Name");
        }

        #endregion
    }
}