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
    ///     This is an DataRecord class which wraps the dbo.titleauthor table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "titleauthor", "title_id")]
    public class Titleauthor : DataRecord<Titleauthor>
    {
        private string _au_Id;
        private short? _au_Ord;
        private int? _royaltyper;
        private string _title_Id;

        public Titleauthor() { }
        protected Titleauthor(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("au_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 11)]
        [ForeignColumn("au_id", typeof(Authors), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 11, DbType = DbType.String)]
        public string Au_id
        {
            get => _au_Id;
            set
            {
                if (value == _au_Id)
                    return;

                _au_Id = value;
                MarkDirty("au_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("title_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 6)]
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
        [ActiveColumn("au_ord", DbType.Int16, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public short? Au_ord
        {
            get => _au_Ord;
            set
            {
                if (value == _au_Ord)
                    return;

                _au_Ord = value;
                MarkDirty("au_ord");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("royaltyper", DbType.Int32, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public int? Royaltyper
        {
            get => _royaltyper;
            set
            {
                if (value == _royaltyper)
                    return;

                _royaltyper = value;
                MarkDirty("royaltyper");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Au_id => FetchColumn("au_id");

            public static QueryColumn Title_id => FetchColumn("title_id");

            public static QueryColumn Au_ord => FetchColumn("au_ord");

            public static QueryColumn Royaltyper => FetchColumn("royaltyper");
        }

        #endregion
    }
}