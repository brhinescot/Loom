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

namespace Pubs
{
    /// <summary>
    ///     This is an DataRecord class which wraps the dbo.roysched table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "roysched")]
    public class Roysched : DataRecord<Roysched>
    {
        private int? _hirange;
        private int? _lorange;
        private int? _royalty;

        private string _title_Id;

        public Roysched() { }
        protected Roysched(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("title_id", DbType.String, ColumnProperties.ForeignKey, Ordinal = 1, MaxLength = 6)]
        [ForeignColumn("title_id", typeof(Titles), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 6, DbType = DbType.String)]
        public string Title_id
        {
            get => _title_Id;
            set
            {
                if (value == _title_Id)
                    return;

                _title_Id = value;
                MarkDirty("title_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("lorange", DbType.Int32, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
        public int? Lorange
        {
            get => _lorange;
            set
            {
                if (value == _lorange)
                    return;

                _lorange = value;
                MarkDirty("lorange");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("hirange", DbType.Int32, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public int? Hirange
        {
            get => _hirange;
            set
            {
                if (value == _hirange)
                    return;

                _hirange = value;
                MarkDirty("hirange");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("royalty", DbType.Int32, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public int? Royalty
        {
            get => _royalty;
            set
            {
                if (value == _royalty)
                    return;

                _royalty = value;
                MarkDirty("royalty");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Title_id => FetchColumn("title_id");

            public static QueryColumn Lorange => FetchColumn("lorange");

            public static QueryColumn Hirange => FetchColumn("hirange");

            public static QueryColumn Royalty => FetchColumn("royalty");
        }

        #endregion
    }
}