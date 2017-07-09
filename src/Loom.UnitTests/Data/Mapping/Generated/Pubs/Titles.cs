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
    ///     This is an DataRecord class which wraps the dbo.titles table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "titles", "title_id")]
    public class Titles : DataRecord<Titles>
    {
        private decimal? _advance;
        private string _notes;
        private decimal? _price;
        private string _pub_Id;
        private DateTime _pubdate;
        private int? _royalty;
        private string _title;

        private string _title_Id;
        private string _type;
        private int? _ytd_Sales;

        public Titles() { }
        protected Titles(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("title_id", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 6)]
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
        [ActiveColumn("title", DbType.String, ColumnProperties.None, Ordinal = 2, MaxLength = 80)]
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                    return;

                _title = value;
                MarkDirty("title");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("type", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 12, DefaultValue = "('UNDECIDED')")]
        public string Type
        {
            get => _type;
            set
            {
                if (value == _type)
                    return;

                _type = value;
                MarkDirty("type");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("pub_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 4, MaxLength = 4)]
        [ForeignColumn("pub_id", typeof(Publishers), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4, DbType = DbType.String)]
        public string Pub_id
        {
            get => _pub_Id;
            set
            {
                if (value == _pub_Id)
                    return;

                _pub_Id = value;
                MarkDirty("pub_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("price", DbType.Currency, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
        public decimal? Price
        {
            get => _price;
            set
            {
                if (value == _price)
                    return;

                _price = value;
                MarkDirty("price");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("advance", DbType.Currency, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 0)]
        public decimal? Advance
        {
            get => _advance;
            set
            {
                if (value == _advance)
                    return;

                _advance = value;
                MarkDirty("advance");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("royalty", DbType.Int32, ColumnProperties.Nullable, Ordinal = 7, MaxLength = 0)]
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

        /// <summary>
        /// </summary>
        [ActiveColumn("ytd_sales", DbType.Int32, ColumnProperties.Nullable, Ordinal = 8, MaxLength = 0)]
        public int? Ytd_sales
        {
            get => _ytd_Sales;
            set
            {
                if (value == _ytd_Sales)
                    return;

                _ytd_Sales = value;
                MarkDirty("ytd_sales");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("notes", DbType.String, ColumnProperties.Nullable, Ordinal = 9, MaxLength = 200)]
        public string Notes
        {
            get => _notes;
            set
            {
                if (value == _notes)
                    return;

                _notes = value;
                MarkDirty("notes");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("pubdate", DbType.DateTime, ColumnProperties.None, Ordinal = 10, MaxLength = 0, DefaultValue = "(getdate())")]
        public DateTime Pubdate
        {
            get => _pubdate;
            set
            {
                if (value == _pubdate)
                    return;

                _pubdate = value;
                MarkDirty("pubdate");
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

            public static QueryColumn Title => FetchColumn("title");

            public static QueryColumn Type => FetchColumn("type");

            public static QueryColumn Pub_id => FetchColumn("pub_id");

            public static QueryColumn Price => FetchColumn("price");

            public static QueryColumn Advance => FetchColumn("advance");

            public static QueryColumn Royalty => FetchColumn("royalty");

            public static QueryColumn Ytd_sales => FetchColumn("ytd_sales");

            public static QueryColumn Notes => FetchColumn("notes");

            public static QueryColumn Pubdate => FetchColumn("pubdate");
        }

        #endregion
    }
}