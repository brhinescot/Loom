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
    ///     This is an DataRecord class which wraps the Test.MountTranslations table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("Test", "MountTranslations", "TranslationId")]
    public class MountTranslations : DataRecord<MountTranslations>
    {
        private string _description;

        private string _locale;
        private int _mountId;
        private string _name;
        private int _translationId;

        public MountTranslations() { }
        protected MountTranslations(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("Locale", DbType.String, ColumnProperties.Unique, Ordinal = 3, MaxLength = 5)]
        public string Locale
        {
            get => _locale;
            set
            {
                if (value == _locale)
                    return;

                _locale = value;
                MarkDirty("Locale");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Name", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 50)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("TranslationId", DbType.Int32, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0)]
        public int TranslationId
        {
            get => _translationId;
            set
            {
                if (value == _translationId)
                    return;

                _translationId = value;
                MarkDirty("TranslationId");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("Description", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 50)]
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
        [ActiveColumn("MountId", DbType.Int32, ColumnProperties.ForeignKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 0)]
        [ForeignColumn("MountId", typeof(Mount), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 0, DbType = DbType.Int32)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Locale => FetchColumn("Locale");

            public static QueryColumn Name => FetchColumn("Name");

            public static QueryColumn TranslationId => FetchColumn("TranslationId");

            public static QueryColumn Description => FetchColumn("Description");

            public static QueryColumn MountId => FetchColumn("MountId");
        }

        #endregion
    }
}