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
    ///     This is an DataRecord class which wraps the dbo.titleview table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "titleview", ReadOnly = true)]
    public class Titleview : DataRecord<Titleview>
    {
        private string _au_Lname;
        private short? _au_Ord;
        private decimal? _price;
        private string _pub_Id;

        private string _title;
        private int? _ytd_Sales;

        public Titleview() { }
        protected Titleview(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("title", DbType.String, ColumnProperties.None, Ordinal = 1, MaxLength = 80)]
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
        [ActiveColumn("au_ord", DbType.Int16, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 0)]
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
        [ActiveColumn("au_lname", DbType.String, ColumnProperties.None, Ordinal = 3, MaxLength = 40)]
        public string Au_lname
        {
            get => _au_Lname;
            set
            {
                if (value == _au_Lname)
                    return;

                _au_Lname = value;
                MarkDirty("au_lname");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("price", DbType.Currency, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
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
        [ActiveColumn("ytd_sales", DbType.Int32, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 0)]
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
        [ActiveColumn("pub_id", DbType.String, ColumnProperties.Nullable, Ordinal = 6, MaxLength = 4)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Title => FetchColumn("title");

            public static QueryColumn Au_ord => FetchColumn("au_ord");

            public static QueryColumn Au_lname => FetchColumn("au_lname");

            public static QueryColumn Price => FetchColumn("price");

            public static QueryColumn Ytd_sales => FetchColumn("ytd_sales");

            public static QueryColumn Pub_id => FetchColumn("pub_id");
        }

        #endregion
    }
}