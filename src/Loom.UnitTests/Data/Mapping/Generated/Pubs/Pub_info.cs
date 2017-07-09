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
    ///     This is an DataRecord class which wraps the dbo.pub_info table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "pub_info", "pub_id")]
    public class Pub_info : DataRecord<Pub_info>
    {
        private byte[] _logo;
        private string _pr_Info;

        private string _pub_Id;

        public Pub_info() { }
        protected Pub_info(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("pub_id", DbType.String, ColumnProperties.ForeignKey | ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4)]
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
        [ActiveColumn("logo", DbType.Binary, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 2147483647)]
        public byte[] Logo
        {
            get => _logo;
            set
            {
                if (value == _logo)
                    return;

                _logo = value;
                MarkDirty("logo");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("pr_info", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 2147483647)]
        public string Pr_info
        {
            get => _pr_Info;
            set
            {
                if (value == _pr_Info)
                    return;

                _pr_Info = value;
                MarkDirty("pr_info");
            }
        }

        public static XmlQualifiedName CreateSchema(XmlSchemaSet schemas)
        {
            return CreateXmlSchema(schemas, "http://www.devinterop.com/framework/data/active", true);
        }

        #region Nested type: Columns

        public struct Columns
        {
            public static QueryColumn Pub_id => FetchColumn("pub_id");

            public static QueryColumn Logo => FetchColumn("logo");

            public static QueryColumn Pr_info => FetchColumn("pr_info");
        }

        #endregion
    }
}