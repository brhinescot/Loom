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
    ///     This is an DataRecord class which wraps the dbo.sales table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "sales", "title_id")]
    public class Sales : DataRecord<Sales>
    {
        private DateTime _ord_Date;
        private string _ord_Num;
        private string _payterms;
        private short _qty;

        private string _stor_Id;
        private string _title_Id;

        public Sales() { }
        protected Sales(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("stor_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4)]
        [ForeignColumn("stor_id", typeof(Stores), ColumnProperties = ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4, DbType = DbType.String)]
        public string Stor_id
        {
            get => _stor_Id;
            set
            {
                if (value == _stor_Id)
                    return;

                _stor_Id = value;
                MarkDirty("stor_id");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ord_num", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 2, MaxLength = 20)]
        public string Ord_num
        {
            get => _ord_Num;
            set
            {
                if (value == _ord_Num)
                    return;

                _ord_Num = value;
                MarkDirty("ord_num");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("ord_date", DbType.DateTime, ColumnProperties.None, Ordinal = 3, MaxLength = 0)]
        public DateTime Ord_date
        {
            get => _ord_Date;
            set
            {
                if (value == _ord_Date)
                    return;

                _ord_Date = value;
                MarkDirty("ord_date");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("qty", DbType.Int16, ColumnProperties.None, Ordinal = 4, MaxLength = 0)]
        public short Qty
        {
            get => _qty;
            set
            {
                if (value == _qty)
                    return;

                _qty = value;
                MarkDirty("qty");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("payterms", DbType.String, ColumnProperties.None, Ordinal = 5, MaxLength = 12)]
        public string Payterms
        {
            get => _payterms;
            set
            {
                if (value == _payterms)
                    return;

                _payterms = value;
                MarkDirty("payterms");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("title_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 6, MaxLength = 6)]
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

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Stor_id => FetchColumn("stor_id");

            public static QueryColumn Ord_num => FetchColumn("ord_num");

            public static QueryColumn Ord_date => FetchColumn("ord_date");

            public static QueryColumn Qty => FetchColumn("qty");

            public static QueryColumn Payterms => FetchColumn("payterms");

            public static QueryColumn Title_id => FetchColumn("title_id");
        }

        #endregion
    }
}