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
    ///     This is an DataRecord class which wraps the dbo.discounts table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "discounts")]
    public class Discounts : DataRecord<Discounts>
    {
        private decimal _discount;

        private string _discounttype;
        private short? _highqty;
        private short? _lowqty;
        private string _stor_Id;

        public Discounts() { }
        protected Discounts(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("discounttype", DbType.String, ColumnProperties.None, Ordinal = 1, MaxLength = 40)]
        public string Discounttype
        {
            get => _discounttype;
            set
            {
                if (value == _discounttype)
                    return;

                _discounttype = value;
                MarkDirty("discounttype");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("stor_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.Nullable, Ordinal = 2, MaxLength = 4)]
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
        [ActiveColumn("lowqty", DbType.Int16, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 0)]
        public short? Lowqty
        {
            get => _lowqty;
            set
            {
                if (value == _lowqty)
                    return;

                _lowqty = value;
                MarkDirty("lowqty");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("highqty", DbType.Int16, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 0)]
        public short? Highqty
        {
            get => _highqty;
            set
            {
                if (value == _highqty)
                    return;

                _highqty = value;
                MarkDirty("highqty");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("discount", DbType.Decimal, ColumnProperties.None, Ordinal = 5, MaxLength = 0)]
        public decimal Discount
        {
            get => _discount;
            set
            {
                if (value == _discount)
                    return;

                _discount = value;
                MarkDirty("discount");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Discounttype => FetchColumn("discounttype");

            public static QueryColumn Stor_id => FetchColumn("stor_id");

            public static QueryColumn Lowqty => FetchColumn("lowqty");

            public static QueryColumn Highqty => FetchColumn("highqty");

            public static QueryColumn Discount => FetchColumn("discount");
        }

        #endregion
    }
}