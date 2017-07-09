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
    ///     This is an DataRecord class which wraps the dbo.publishers table.
    /// </summary>
    [Serializable]
    [XmlSchemaProvider("CreateSchema")]
    [ActiveTable("dbo", "publishers", "pub_id")]
    public class Publishers : DataRecord<Publishers>
    {
        private string _city;
        private string _country;

        private string _pub_Id;
        private string _pub_Name;
        private string _state;

        public Publishers() { }
        protected Publishers(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// </summary>
        [ActiveColumn("pub_id", DbType.String, ColumnProperties.PrimaryKey | ColumnProperties.Unique, Ordinal = 1, MaxLength = 4)]
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
        [ActiveColumn("pub_name", DbType.String, ColumnProperties.Nullable, Ordinal = 2, MaxLength = 40)]
        public string Pub_name
        {
            get => _pub_Name;
            set
            {
                if (value == _pub_Name)
                    return;

                _pub_Name = value;
                MarkDirty("pub_name");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("city", DbType.String, ColumnProperties.Nullable, Ordinal = 3, MaxLength = 20)]
        public string City
        {
            get => _city;
            set
            {
                if (value == _city)
                    return;

                _city = value;
                MarkDirty("city");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("state", DbType.String, ColumnProperties.Nullable, Ordinal = 4, MaxLength = 2)]
        public string State
        {
            get => _state;
            set
            {
                if (value == _state)
                    return;

                _state = value;
                MarkDirty("state");
            }
        }

        /// <summary>
        /// </summary>
        [ActiveColumn("country", DbType.String, ColumnProperties.Nullable, Ordinal = 5, MaxLength = 30, DefaultValue = "('USA')")]
        public string Country
        {
            get => _country;
            set
            {
                if (value == _country)
                    return;

                _country = value;
                MarkDirty("country");
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

            public static QueryColumn Pub_name => FetchColumn("pub_name");

            public static QueryColumn City => FetchColumn("city");

            public static QueryColumn State => FetchColumn("state");

            public static QueryColumn Country => FetchColumn("country");
        }

        #endregion
    }
}